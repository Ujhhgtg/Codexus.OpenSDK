using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Codexus.Cipher.Connection.Protocols;
using Codexus.Cipher.Entities.WPFLauncher.NetGame;
using Codexus.Cipher.Entities.WPFLauncher.NetGame.Mods;
using Codexus.Cipher.Entities.WPFLauncher.NetGame.Texture;
using Codexus.Cipher.Protocol;
using Codexus.Cipher.Skip32;
using Codexus.Cipher.Utils;
using Codexus.Cipher.Utils.Cipher;
using Codexus.Development.SDK.Utils;
using Codexus.Game.Launcher.Entities;
using Codexus.Game.Launcher.Services.Java.RPC;
using Codexus.Game.Launcher.Utils;
using Codexus.Game.Launcher.Utils.Progress;
using Serilog;

namespace Codexus.Game.Launcher.Services.Java;

public sealed class LauncherService : IDisposable
{
    public EntityLaunchGame Entity { get; }

    public Guid Identifier { get; }

    private Process? GameProcess { get; set; }

    public EntityProgressUpdate LastProgress { get; private set; }

    public event Action<Guid>? Exited;

    private LauncherService(EntityLaunchGame? entityLaunchGame, string? userToken, WPFLauncher? wpfLauncher,
        string? protocolVersion, IProgress<EntityProgressUpdate>? progress)
    {
        ArgumentNullException.ThrowIfNull(entityLaunchGame);
        Entity = entityLaunchGame;
        ArgumentNullException.ThrowIfNull(userToken);
        _userToken = userToken;
        ArgumentNullException.ThrowIfNull(wpfLauncher);
        _wpf = wpfLauncher;
        ArgumentNullException.ThrowIfNull(protocolVersion);
        _protocolVersion = protocolVersion;
        ArgumentNullException.ThrowIfNull(progress);
        _progress = progress;
        _skip32 = new Skip32Cipher((from c in "SaintSteve".ToCharArray()
            select (byte)c).ToArray());
        _socketPort = NetworkUtil.GetAvailablePort(9876);
        Identifier = Guid.NewGuid();
        LastProgress = new EntityProgressUpdate
        {
            Id = Identifier,
            Percent = 0,
            Message = "Initialized"
        };
    }

    public static LauncherService CreateLauncher(EntityLaunchGame entityLaunchGame, string userToken,
        WPFLauncher wpfLauncher, string protocolVersion, IProgress<EntityProgressUpdate> progress)
    {
        var launcherService = new LauncherService(entityLaunchGame, userToken, wpfLauncher, protocolVersion, progress);
        Task.Run(launcherService.LaunchGameAsync);
        return launcherService;
    }

    public Process? GetProcess()
    {
        return GameProcess;
    }

    public async Task ShutdownAsync()
    {
        try
        {
            var gameRpcService = _gameRpcService;
            gameRpcService?.CloseControlConnection();
            if (GameProcess is { HasExited: false })
            {
                GameProcess.Kill();
                await GameProcess.WaitForExitAsync();
            }
        }
        catch (Exception exception)
        {
            Log.Warning(exception, "Error occurred during shutdown");
        }
    }

    private async Task LaunchGameAsync()
    {
        var progressHandler = CreateProgressHandler();
        try
        {
            await ExecuteLaunchStepsAsync(progressHandler);
        }
        catch (Exception exception)
        {
            Log.Error(exception, "Failed to launch game");
            ReportProgress(progressHandler, 100, "Game launch failed");
            throw;
        }
    }

    private async Task ExecuteLaunchStepsAsync(IProgress<EntityProgressUpdate> progressHandler)
    {
        ReportProgress(progressHandler, 5, "Installing game mods");
        var enumVersion = GameVersionConverter.Convert(Entity.GameVersionId);
        var entityModsList = await InstallGameModsAsync(enumVersion);
        _modList = entityModsList;
        entityModsList = null;
        ReportProgress(progressHandler, 15, "Preparing Java runtime");
        await PrepareJavaRuntimeAsync(enumVersion);
        ReportProgress(progressHandler, 30, "Preparing Minecraft client");
        await PrepareMinecraftClientAsync(enumVersion);
        ReportProgress(progressHandler, 45, "Setting up runtime");
        var workingDirectory = SetupGameRuntime();
        ReportProgress(progressHandler, 60, "Applying core mods");
        ApplyCoreMods(workingDirectory);
        ReportProgress(progressHandler, 75, "Initializing launcher");
        var valueTuple = InitializeLauncher(enumVersion, workingDirectory);
        var commandService = valueTuple.Item1;
        var rpcPort = valueTuple.Item2;
        ReportProgress(progressHandler, 80, "Starting RPC service");
        LaunchRpcService(enumVersion, rpcPort);
        ReportProgress(progressHandler, 90, "Starting authentication socket service");
        StartAuthenticationService();
        ReportProgress(progressHandler, 95, "Launching game process");
        await StartGameProcessAsync(commandService, progressHandler);
    }

    private IProgress<EntityProgressUpdate> CreateProgressHandler()
    {
        var progress = new SyncProgressBarUtil.ProgressBar(100);
        var uiProgress = new SyncCallback<SyncProgressBarUtil.ProgressReport>(
            delegate(SyncProgressBarUtil.ProgressReport update) { progress.Update(update.Percent, update.Message); });
        return new SyncCallback<EntityProgressUpdate>(delegate(EntityProgressUpdate update)
        {
            uiProgress.Report(new SyncProgressBarUtil.ProgressReport
            {
                Percent = update.Percent,
                Message = update.Message
            });
            _progress.Report(update);
            LastProgress = update;
        });
    }

    private void ReportProgress(IProgress<EntityProgressUpdate> handler, int percent, string message)
    {
        handler.Report(new EntityProgressUpdate
        {
            Id = Identifier,
            Percent = percent,
            Message = message
        });
    }

    private async Task<EntityModsList?> InstallGameModsAsync(EnumGameVersion enumVersion)
    {
        return await InstallerService.InstallGameMods(Entity.UserId, _userToken, enumVersion, _wpf, Entity.GameId,
            Entity.GameType == EnumGType.ServerGame);
    }

    private static async Task PrepareJavaRuntimeAsync(EnumGameVersion enumVersion)
    {
        var path = enumVersion > EnumGameVersion.V_1_16 ? "jdk17" : "jre8";
        var flag = !File.Exists(Path.Combine(Path.Combine(PathUtil.JavaPath, path), "bin", "javaw.exe"));
        if (flag) await JreService.PrepareJavaRuntime();
    }

    private async Task PrepareMinecraftClientAsync(EnumGameVersion enumVersion)
    {
        await InstallerService.PrepareMinecraftClient(Entity.UserId, _userToken, _wpf, enumVersion);
    }

    private string SetupGameRuntime()
    {
        var text = InstallerService.PrepareGameRuntime(Entity.UserId, Entity.GameId, Entity.RoleName, Entity.GameType);
        InstallerService.InstallNativeDll(GameVersionConverter.Convert(Entity.GameVersionId));
        return Path.Combine(text, ".minecraft");
    }

    private void ApplyCoreMods(string workingDirectory)
    {
        var text = Path.Combine(workingDirectory, "mods");
        var loadCoreMods = Entity.LoadCoreMods;
        if (loadCoreMods)
            InstallerService.InstallCoreMods(Entity.GameId, text);
        else
            RemoveCoreModFiles(text);
    }

    private static void RemoveCoreModFiles(string modsPath)
    {
        var array = FileUtil.EnumerateFiles(modsPath, "jar");
        foreach (var text in array)
        {
            var flag = text.Contains("@3");
            if (flag) FileUtil.DeleteFileSafe(text);
        }
    }

    private (CommandService commandService, int rpcPort) InitializeLauncher(EnumGameVersion enumVersion,
        string workingDirectory)
    {
        var commandService = new CommandService();
        var availablePort = NetworkUtil.GetAvailablePort(11413);
        var text = TokenUtil.GenerateEncryptToken(_userToken);
        var text2 = _skip32.GenerateRoleUuid(Entity.RoleName, Convert.ToUInt32(Entity.UserId));
        commandService.Init(enumVersion, Entity.MaxGameMemory, Entity.RoleName, Entity.ServerIp, Entity.ServerPort,
            Entity.UserId, text, Entity.GameId, workingDirectory, text2, _socketPort, _protocolVersion, true,
            availablePort);
        return (commandService, availablePort);
    }

    private void LaunchRpcService(EnumGameVersion gameVersion, int rpcPort)
    {
        var text = Path.Combine(PathUtil.CachePath, "Skins");
        var flag = !Directory.Exists(text);
        if (flag) Directory.CreateDirectory(text);
        _gameRpcService = new GameRpcService(rpcPort, Entity.ServerIp, Entity.ServerPort.ToString(), Entity.RoleName,
            Entity.UserId, _userToken, gameVersion);
        _gameRpcService.Connect(text, _wpf.GetSkinListInGame, _wpf.GetNetGameComponentDownloadList);
    }

    private void StartAuthenticationService()
    {
        _authLibProtocol = new AuthLibProtocol(IPAddress.Parse("127.0.0.1"), _socketPort,
            JsonSerializer.Serialize<EntityModsList>(_modList), Entity.GameVersion, Entity.AccessToken);
        _authLibProtocol.Start();
    }

    private async Task StartGameProcessAsync(CommandService commandService,
        IProgress<EntityProgressUpdate> progressHandler)
    {
        var process = commandService.StartGame();
        if (process != null)
            await HandleSuccessfulLaunch(process, progressHandler);
        else
            HandleFailedLaunch(progressHandler);
    }

    private Task HandleSuccessfulLaunch(Process process, IProgress<EntityProgressUpdate> progressHandler)
    {
        GameProcess = process;
        GameProcess.EnableRaisingEvents = true;
        GameProcess.Exited += OnGameProcessExited;
        ReportProgress(progressHandler, 100, "Running");
        SyncProgressBarUtil.ProgressBar.ClearCurrent();
        Console.WriteLine();
        Log.Information(
            "Game launched successfully. Game Version: {GameVersion}, Process ID: {ProcessId}, Role: {Role}",
            Entity.GameVersion, process.Id, Entity.RoleName);
        MemoryOptimizer.GetInstance();
        return Task.CompletedTask;
    }

    private void HandleFailedLaunch(IProgress<EntityProgressUpdate> progressHandler)
    {
        ReportProgress(progressHandler, 100, "Game launch failed");
        SyncProgressBarUtil.ProgressBar.ClearCurrent();
        Log.Error("Game launch failed. Game Version: {GameVersion}, Role: {Role}", Entity.GameVersion, Entity.RoleName);
    }

    private void OnGameProcessExited(object? sender, EventArgs e)
    {
        Exited?.Invoke(Identifier);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
                try
                {
                    _authLibProtocol?.Dispose();
                    _gameRpcService?.CloseControlConnection();
                    if (GameProcess is { HasExited: false })
                    {
                        GameProcess.Kill();
                        GameProcess.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Log.Warning(ex, "Error occurred during disposal");
                }

            _disposed = true;
        }
    }

    ~LauncherService()
    {
        Dispose(false);
    }

    private const string Skip32Key = "SaintSteve";
    private const int DefaultSocketPort = 9876;
    private const int DefaultRpcPort = 11413;
    private const string JavaExeName = "javaw.exe";
    private const string MinecraftDirectory = ".minecraft";
    private const string ModsDirectory = "mods";
    private const string SkinsDirectory = "Skins";
    private const string CoreModPattern = "@3";
    private const string JarExtension = "jar";
    private readonly IProgress<EntityProgressUpdate> _progress;
    private readonly string _protocolVersion;
    private readonly Skip32Cipher _skip32;
    private readonly int _socketPort;
    private readonly string _userToken;
    private readonly WPFLauncher _wpf;

    private AuthLibProtocol? _authLibProtocol;

    private GameRpcService? _gameRpcService;

    private EntityModsList? _modList;
    private bool _disposed;
}