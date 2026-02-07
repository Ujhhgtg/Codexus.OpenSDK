using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using Codexus.Cipher.Entities.WPFLauncher.Minecraft;
using Codexus.Cipher.Entities.WPFLauncher.NetGame;
using Codexus.Cipher.Entities.WPFLauncher.NetGame.Texture;
using Codexus.Cipher.Utils;
using Codexus.Game.Launcher.Utils;

namespace Codexus.Game.Launcher.Services.Java;
public class CommandService
{
	public bool Init(EnumGameVersion gameVersion, int maxMemory, string roleName, string serverIp, int serverPort, string userId, string dToken, string gameId, string workPath, string uuid, int socketPort, string protocolVersion = "", bool isFilter = true, int rpcPort = 11413)
	{
		_roleName = roleName;
		_version = GameVersionUtil.GetGameVersionFromEnum(gameVersion);
		_serverIp = serverIp;
		_serverPort = serverPort;
		_gameVersion = gameVersion;
		_rpcPort = rpcPort;
		_userId = userId;
		_uuid = uuid;
		_authToken = dToken;
		_gameId = gameId;
		_isFilter = isFilter;
		_workPath = workPath;
		_relLibPath = "libraries\\";
		_relVerPath = "versions\\" + _version + "\\";
		_protocolVersion = protocolVersion;
		var text = Path.Combine(new[]
		{
			PathUtil.GameBasePath,
			".minecraft",
			"versions",
			_version,
			_version + ".json"
		});
		if (!File.Exists(text))
		{
			throw new Exception("Game version JSON not found, please go to Setting to fix the game file and try again.");
		}
		var dictionary = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(File.ReadAllText(text), JsonSerializerOptions);
		BuildJarLists(dictionary, _version);
		if (_newJavaVersionList.Contains(gameVersion))
		{
			BuildCommandEx(dictionary, _version, maxMemory, socketPort);
		}
		else
		{
			BuildCommand(dictionary, _version, maxMemory, socketPort, _jarList);
		}
		return true;
	}
	
	private static readonly JsonSerializerOptions JsonSerializerOptions = new()
	{
		AllowTrailingCommas = true
	};

	public Process? StartGame()
	{
		return Process.Start(new ProcessStartInfo(Path.Combine(_gameVersion >= EnumGameVersion.V_1_16 ? PathUtil.Jre17Path : PathUtil.Jre8Path, "bin", "javaw.exe"), _cmd)
		{
			UseShellExecute = false,
			CreateNoWindow = true,
			WorkingDirectory = _workPath
		});
	}

	private void BuildJarLists(Dictionary<string, JsonElement> cfg, string version)
	{
		_jarList.Clear();
		var flag = cfg.TryGetValue("libraries", out var jsonElement) && jsonElement.ValueKind == JsonValueKind.Array;
		if (flag)
		{
			foreach (var jsonElement2 in jsonElement.EnumerateArray())
			{
				var flag2 = jsonElement2.TryGetProperty("name", out var jsonElement3);
				if (flag2)
				{
					var @string = jsonElement3.GetString();
					var array = @string?.Split(':');
					var flag3 = array is { Length: >= 3 } && !array[1].Contains("platform");
					if (flag3)
					{
						var text = array[0].Replace('.', '\\');
						var text2 = array[1] + "-" + array[2] + ".jar";
						_jarList.Add(_relLibPath + Path.Combine(text, array[1], array[2], text2));
					}
				}
			}
		}
		_jarList.Add(_relVerPath + version + ".jar");
	}

	private void BuildCommand(
		Dictionary<string, JsonElement> cfg,
		string version,
		int mem,
		int socketPort,
		List<string> jars)
	{
		var sb = new StringBuilder();

		AppendJvmSystemProps(sb, socketPort);
		AddNativePath(sb);

		sb.Append(" -XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump")
			.Append(" -Xmx").Append(mem).Append("M")
			.Append(" -Xmn128M -XX:PermSize=64M -XX:MaxPermSize=128M")
			.Append(" -XX:+UseConcMarkSweepGC -XX:+CMSIncrementalMode -XX:-UseAdaptiveSizePolicy");

		sb.Append(" -cp \"");
		foreach (var jar in jars)
		{
			sb.Append(Path.Combine(PathUtil.GameBasePath, ".minecraft", jar)).Append(';');
		}
		sb.Append("\" ");

		sb.Append(cfg["mainClass"].GetString()).Append(' ');

		sb.Append(BuildMinecraftArgs(cfg, version));

		sb.Append(" --server ").Append(_serverIp)
			.Append(" --port ").Append(_serverPort)
			.Append(" --userPropertiesEx ")
			.Append(GetUserPropertiesEx());

		_cmd = sb.ToString();
	}
	
	private string BuildMinecraftArgs(
		Dictionary<string, JsonElement> cfg,
		string version)
	{
		var args = cfg.GetValueOrDefault("minecraftArguments").GetString() ?? string.Empty;

		return args
			.Replace("${version_name}", version)
			.Replace(
				"${assets_root}",
				Path.Combine(PathUtil.GameBasePath, ".minecraft", "assets")
			)
			.Replace("${assets_index_name}", version)
			.Replace("${auth_uuid}", _uuid)
			.Replace(
				"${auth_access_token}",
				RandomUtil.GetRandomString(32, "ABCDEF1234567890")
			)
			.Replace("${auth_player_name}", _roleName)
			.Replace("${user_properties}", GetUserProperties(version))
			.Replace("--userType ${user_type}", string.Empty)
			.Replace("--gameDir ${game_directory}", $"--gameDir {_workPath}")
			.Replace("--versionType ${version_type}", string.Empty);
	}

	private void BuildCommandEx(
		Dictionary<string, JsonElement> cfg,
		string version,
		int mem,
		int socketPort)
	{
		var args = cfg.GetValueOrDefault("parameter_arguments").GetString();
		var jvm = cfg.GetValueOrDefault("jvm_arguments").GetString();

		if (args == null || jvm == null)
		{
			return;
		}

		jvm = jvm
			.Replace("-Xmx2G", string.Empty)
			.Replace(
				"-DlibraryDirectory=libraries",
				$"-DlibraryDirectory={Path.Combine(PathUtil.GameBasePath, ".minecraft", "libraries")}"
			);

		jvm = RewriteClasspathArgument(jvm, "-cp");
		jvm = RewriteClasspathArgument(jvm, "-p");

		args = args
			.Replace("--assetsDir assets", $"--assetsDir {Path.Combine(PathUtil.GameBasePath, ".minecraft", "assets")}")
			.Replace("--gameDir .", $"--gameDir {_workPath}")
			.Replace("${auth_player_name}", _roleName)
			.Replace("${auth_uuid}", _uuid)
			.Replace(
				"${auth_access_token}",
				_gameVersion >= EnumGameVersion.V_1_18
					? "0"
					: RandomUtil.GetRandomString(32, "ABCDEF0123456789")
			);

		var sb = new StringBuilder();

		AppendJvmSystemProps(sb, socketPort);
		AddNativePath(sb);

		sb.Append(" -Xmx").Append(mem).Append("M -Xmn128M ")
			.Append(jvm)
			.Append(' ')
			.Append(args)
			.Append(" --userProperties ").Append(GetUserProperties(version))
			.Append(" --userPropertiesEx ").Append(GetUserPropertiesEx())
			.Append(" --server ").Append(_serverIp)
			.Append(" --port ").Append(_serverPort);

		_cmd = sb.ToString();
	}

	private void AddNativePath(StringBuilder sb)
	{
		var natives = Path.Combine(
			PathUtil.GameBasePath,
			".minecraft",
			"versions",
			_version,
			"natives");

		var runtime = Path.Combine(natives, "runtime");

		sb.Append(" -Djava.library.path=\"")
			.Append(natives.Replace("\\", @"\\"))
			.Append("\" -Druntime_path=\"")
			.Append(runtime.Replace("\\", @"\\"))
			.Append("\" ");
	}
	
	private void AppendJvmSystemProps(StringBuilder sb, int socketPort)
	{
		sb.Append(" -DlauncherControlPort=").Append(socketPort)
			.Append(" -DlauncherGameId=").Append(_gameId)
			.Append(" -DuserId=").Append(_userId)
			.Append(" -DToken=").Append(_authToken)
			.Append(" -DServer=RELEASE ");
	}

	private string GetUserPropertiesEx(EnumGType t = EnumGType.NetGame)
	{
		return JsonSerializer.Serialize(new EntityUserPropertiesEx
		{
			GameType = (int)t,
			Channel = "netease",
			TimeDelta = 0,
			IsFilter = _isFilter,
			LauncherVersion = _protocolVersion
		});
	}

	private string GetUserProperties(string version)
	{
		var text = version == "1.7.10" ? "\"uid\":[{0}],\"gameid\":[{1}],\"launcherport\":[{2}],\\\"filterkey\\\":[\\\"{3}\\\",\\\"0\\\"],\\\"filterpath\\\":[\\\"\\\",\\\"0\\\"],\\\"timedelta\\\":[0,0],\\\"launchversion\\\":[\\\"{3}\\\",\\\"0\\\"]" : "\\\"uid\\\":[{0},0],\\\"gameid\\\":[{1},0],\\\"launcherport\\\":[{2},0],\\\"filterkey\\\":[\\\"{3}\\\",\\\"0\\\"],\\\"filterpath\\\":[\\\"\\\",\\\"0\\\"],\\\"timedelta\\\":[0,0],\\\"launchversion\\\":[\\\"{4}\\\",\\\"0\\\"]";
		var text2 = string.Format(text, new object[]
		{
			_userId,
			0,
			_rpcPort,
			RandomUtil.GetRandomString(32, "abcdefghijklmnopqrstuvwxyz"),
			_protocolVersion
		});
		return "\"{" + text2 + "}\"";
	}

	private static string RewriteClasspathArgument(string input, string opt)
	{
		var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		for (var i = 0; i < parts.Length - 1; i++)
		{
			if (parts[i] != opt)
			{
				continue;
			}

			var entries = parts[i + 1].Split(';', StringSplitOptions.RemoveEmptyEntries);

			for (var j = 0; j < entries.Length; j++)
			{
				entries[j] = Path.Combine(
					PathUtil.GameBasePath,
					".minecraft",
					entries[j]);
			}

			parts[i + 1] = string.Join(';', entries);
			break;
		}

		return string.Join(' ', parts);
	}
	private readonly List<string> _jarList = [];
	private readonly List<EnumGameVersion> _newJavaVersionList =
	[
		EnumGameVersion.V_1_13_2,
		EnumGameVersion.V_1_14_3,
		EnumGameVersion.V_1_16,
		EnumGameVersion.V_1_18,
		EnumGameVersion.V_1_20,
		EnumGameVersion.V_1_21
	];
	private string _authToken = string.Empty;
	private string _cmd = string.Empty;
	private string _gameId = string.Empty;
	private EnumGameVersion _gameVersion;
	private bool _isFilter = true;
	private string _protocolVersion = string.Empty;
	private string _relLibPath = string.Empty;
	private string _relVerPath = string.Empty;
	private string _roleName = string.Empty;
	private int _rpcPort = 11413;
	private string _serverIp = string.Empty;
	private int _serverPort;
	private string _userId = string.Empty;
	private string _uuid = string.Empty;
	private string _version = string.Empty;
	private string _workPath = string.Empty;
}