using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using Codexus.Development.SDK.Plugin;
using Serilog;

namespace Codexus.Development.SDK.Manager;

public class PluginManager
{
    public static string[] PluginExtensions { get; set; } = [".ug", ".dll", ".UG"];

    public static PluginManager Instance
    {
        get
        {
            PluginManager? pluginManager;
            if ((pluginManager = _instance) == null) pluginManager = _instance = new PluginManager();
            return pluginManager;
        }
    }

    public void EnsureUninstall()
    {
        using (_writeFileLock.EnterScope())
        {
            var flag = !File.Exists(".ug_cache");
            if (flag)
            {
                File.WriteAllText(".ug_cache", JsonSerializer.Serialize<HashSet<string>>([]));
            }
            else
            {
                var hashSet = JsonSerializer.Deserialize<HashSet<string>>(File.ReadAllText(".ug_cache"));
                if (hashSet != null)
                {
                    foreach (var text in hashSet) File.Delete(text);
                    File.Delete(".ug_cache");
                    File.WriteAllText(".ug_cache", JsonSerializer.Serialize<HashSet<string>>([]));
                }
            }
        }
    }

    // public void LoadPlugins(string directory)
    // {
    // 	var flag = !Directory.Exists(directory);
    // 	if (flag)
    // 	{
    // 		Directory.CreateDirectory(directory);
    // 	}
    // 	else
    // 	{
    // 		var array = (from f in Directory.EnumerateFiles(directory)
    // 			where PluginExtensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase)
    // 			select f).ToArray<string>();
    // 		foreach (var text in array)
    // 		{
    // 			var flag2 = _loadedFiles.Contains(text);
    // 			if (!flag2)
    // 			{
    // 				try
    // 				{
    // 					var assembly = Assembly.LoadFrom(text);
    // 					foreach (var type2 in from type in assembly.GetTypes()
    // 					         where typeof(IPlugin).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface
    // 					         select type)
    // 					{
    // 						Attributes.Plugin? customAttribute;
    // 						try
    // 						{
    // 							customAttribute = type2.GetCustomAttribute(false);
    // 						}
    // 						catch (MissingMemberException)
    // 						{
    // 							Log.Warning("插件 {TypeFullName} 没有插件属性", [type2.FullName]);
    // 							continue;
    // 						}
    // 						var flag3 = customAttribute == null;
    // 						if (flag3)
    // 						{
    // 							Log.Warning("插件 {TypeFullName} 没有插件属性", [type2.FullName]);
    // 						}
    // 						else
    // 						{
    // 							var flag4 = !Plugins.ContainsKey(customAttribute.Id);
    // 							if (flag4)
    // 							{
    // 								var plugin = Activator.CreateInstance(type2) as IPlugin;
    // 								var flag5 = plugin == null;
    // 								if (flag5)
    // 								{
    // 									Log.Warning("插件 {TypeFullName} 没有继承 IPlugin", [type2.FullName]);
    // 								}
    // 								else
    // 								{
    // 									Plugins.Add(customAttribute.Id.ToUpper(), new PluginState(customAttribute.Id.ToUpper(), customAttribute.Name, customAttribute.Description, customAttribute.Version, customAttribute.Author, customAttribute.Dependencies, text, assembly, plugin));
    // 									_loadedFiles.Add(text);
    // 								}
    // 							}
    // 						}
    // 					}
    // 				}
    // 				catch (Exception ex)
    // 				{
    // 					Log.Error(ex, "Failed to load plugin from {File}", [text]);
    // 				}
    // 			}
    // 		}
    // 		CheckDependencies();
    // 		Log.Information("识别了 {Count} 个插件", [Plugins.Count]);
    // 		InitializePlugins();
    // 	}
    // }
    public void LoadPlugins(string directory)
    {
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            return; // Early return to avoid nesting the rest of the logic
        }

        // 1. Get all valid plugin files that haven't been loaded yet
        var files = Directory.EnumerateFiles(directory)
            .Where(f => PluginExtensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
            .Where(f => !_loadedFiles.Contains(f));

        foreach (var file in files)
            try
            {
                var assembly = Assembly.LoadFrom(file);

                // 2. Find concrete classes implementing IPlugin
                var pluginTypes = assembly.GetTypes().Where(t =>
                    typeof(IPlugin).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);

                foreach (var type in pluginTypes)
                {
                    // 3. Extract Plugin Attribute
                    var attr = type.GetCustomAttribute<Attributes.Plugin>(false);
                    if (attr == null)
                    {
                        Log.Warning("插件 {TypeFullName} 没有插件属性", type.FullName);
                        continue;
                    }

                    // 4. Instantiate and Add (checking for duplicates)
                    var pluginId = attr.Id.ToUpper();
                    if (!Plugins.ContainsKey(pluginId))
                    {
                        if (Activator.CreateInstance(type) is IPlugin plugin)
                        {
                            Plugins.Add(pluginId, new PluginState(
                                pluginId, attr.Name, attr.Description, attr.Version,
                                attr.Author, attr.Dependencies, file, assembly, plugin));

                            _loadedFiles.Add(file);
                        }
                        else
                        {
                            Log.Warning("插件 {TypeFullName} 实例化失败或未继承 IPlugin", type.FullName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to load plugin from {File}", file);
            }

        CheckDependencies();
        Log.Information("识别了 {Count} 个插件", Plugins.Count);
        InitializePlugins();
    }

    public bool HasPlugin(string id)
    {
        return Plugins.ContainsKey(id.ToUpper());
    }

    public PluginState GetPlugin(string id)
    {
        var flag = !Plugins.ContainsKey(id.ToUpper());
        if (flag) throw new InvalidOperationException("Plugin " + id + " is not loaded");
        return Plugins[id.ToUpper()];
    }

    public List<string> GetPluginAndDependencyPaths(string pluginId, Func<string, bool> excludeRule = null)
    {
        pluginId = pluginId.ToUpper();
        var flag = !HasPlugin(pluginId);
        if (flag) throw new InvalidOperationException("Plugin " + pluginId + " is not loaded");
        var hashSet = new HashSet<string>();
        var hashSet2 = new HashSet<string>();
        CollectDependencyPaths(pluginId, hashSet, hashSet2, excludeRule);
        return hashSet.ToList();
    }

    private void CollectDependencyPaths(string pluginId, HashSet<string> pathSet, HashSet<string> visitedPlugins,
        Func<string, bool>? excludeRule = null)
    {
        if (!(!visitedPlugins.Add(pluginId) || (excludeRule != null && excludeRule(pluginId))))
        {
            if (!HasPlugin(pluginId))
            {
                Log.Warning("Plugin {PluginId} is not loaded", [pluginId]);
            }
            else
            {
                var plugin = GetPlugin(pluginId);
                pathSet.Add(plugin.Path);
                if (plugin.Dependencies != null)
                {
                    var dependencies = plugin.Dependencies;
                    foreach (var text in dependencies)
                        CollectDependencyPaths(text.ToUpper(), pathSet, visitedPlugins, excludeRule);
                }
            }
        }
    }

    private void CheckDependencies()
    {
        foreach (var pluginState in Plugins.Values)
        {
            var flag = pluginState.Dependencies == null;
            if (!flag)
            {
                var dependencies = pluginState.Dependencies;
                foreach (var text in dependencies)
                {
                    var flag2 = !Plugins.ContainsKey(text);
                    if (flag2)
                    {
                        var defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 3);
                        defaultInterpolatedStringHandler.AppendLiteral("Plugin ");
                        defaultInterpolatedStringHandler.AppendFormatted(pluginState.Name);
                        defaultInterpolatedStringHandler.AppendLiteral("(");
                        defaultInterpolatedStringHandler.AppendFormatted(pluginState.Id);
                        defaultInterpolatedStringHandler.AppendLiteral(") depends on ");
                        defaultInterpolatedStringHandler.AppendFormatted(text);
                        defaultInterpolatedStringHandler.AppendLiteral(", but it is not loaded");
                        throw new InvalidOperationException(defaultInterpolatedStringHandler.ToStringAndClear());
                    }
                }
            }
        }
    }

    private static Version ParseVersion(string version)
    {
        Version version2;
        try
        {
            var array = version.Split('.');
            var num = array.Length != 0 && int.TryParse(array[0], out var num2) ? num2 : 0;
            var num3 = array.Length > 1 && int.TryParse(array[1], out var num4) ? num4 : 0;
            var num5 = array.Length > 2 && int.TryParse(array[2], out var num6) ? num6 : 0;
            version2 = new Version(num, num3, num5);
        }
        catch (Exception ex)
        {
            Log.Warning(ex, "Failed to parse plugin version: {Version}. Using default 0.0.0", [version]);
            version2 = new Version(0, 0, 0);
        }

        return version2;
    }

    // private void InitializePlugins()
    // {
    // 	var dictionary = new Dictionary<string, PluginState>();
    // 	foreach (var grouping in from p in Plugins.Values
    // 	         group p by p.Id.ToUpper())
    // 	{
    // 		var pluginState = grouping.OrderByDescending(p => ParseVersion(p.Version)).First<PluginState>();
    // 		dictionary[pluginState.Id] = pluginState;
    // 		var flag = grouping.Count<PluginState>() > 1;
    // 		if (flag)
    // 		{
    // 			Log.Information("Multiple versions of plugin {PluginId} found. Using version {Version} from {Path}",
    // 				[pluginState.Id, pluginState.Version, pluginState.Path]);
    // 		}
    // 	}
    // 	Plugins.Clear();
    // 	foreach (var keyValuePair in dictionary)
    // 	{
    // 		Plugins.Add(keyValuePair.Key, keyValuePair.Value);
    // 	}
    // 	var dictionary2 = new Dictionary<string, List<string>>();
    // 	var inDegree = new Dictionary<string, int>();
    // 	foreach (var pluginState2 in Plugins.Values)
    // 	{
    // 		dictionary2[pluginState2.Id] = [];
    // 		inDegree[pluginState2.Id] = 0;
    // 	}
    // 	foreach (var pluginState3 in Plugins.Values)
    // 	{
    // 		var flag2 = pluginState3.Dependencies == null;
    // 		if (!flag2)
    // 		{
    // 			var dependencies = pluginState3.Dependencies;
    // 			foreach (var text in dependencies)
    // 			{
    // 				var flag3 = Plugins.ContainsKey(text);
    // 				if (flag3)
    // 				{
    // 					dictionary2[text].Add(pluginState3.Id);
    // 					var inDegree3 = inDegree;
    // 					var text2 = pluginState3.Id;
    // 					var num = inDegree3[text2];
    // 					inDegree3[text2] = num + 1;
    // 				}
    // 			}
    // 		}
    // 	}
    // 	var queue = new Queue<string>();
    // 	IEnumerable<PluginState> values = Plugins.Values;
    // 	Func<PluginState, bool> func4;
    // 	Func<PluginState, bool> func;
    // 	if ((func = func4) == null)
    // 	{
    // 		func = (func4 = (PluginState plugin) => inDegree[plugin.Id] == 0);
    // 	}
    // 	foreach (var pluginState4 in values.Where(func))
    // 	{
    // 		queue.Enqueue(pluginState4.Id);
    // 	}
    // 	var list = new List<string>();
    // 	while (queue.Count > 0)
    // 	{
    // 		var text3 = queue.Dequeue();
    // 		list.Add(text3);
    // 		foreach (var text4 in dictionary2[text3])
    // 		{
    // 			var inDegree2 = inDegree;
    // 			var text2 = text4;
    // 			var num = inDegree2[text2];
    // 			inDegree2[text2] = num - 1;
    // 			var flag4 = inDegree[text4] == 0;
    // 			if (flag4)
    // 			{
    // 				queue.Enqueue(text4);
    // 			}
    // 		}
    // 	}
    // 	var flag5 = list.Count != Plugins.Count;
    // 	if (flag5)
    // 	{
    // 		var list2 = Plugins.Keys.Except(list).ToList();
    // 		var text5 = string.Join(", ", list2);
    // 		Log.Error("Circular dependency detected among plugins: {CircularDependencies}", [text5]);
    // 		throw new InvalidOperationException("Circular dependency detected among plugins: " + text5);
    // 	}
    // 	IEnumerable<string> enumerable = list;
    // 	Func<string, PluginState> func3;
    // 	Func<string, PluginState> func2;
    // 	if ((func2 = func3) == null)
    // 	{
    // 		func2 = (func3 = (string pluginId) => Plugins[pluginId]);
    // 	}
    // 	foreach (var pluginState5 in enumerable.Select(func2))
    // 	{
    // 		var flag6 = !pluginState5.IsInitialized;
    // 		if (flag6)
    // 		{
    // 			Log.Information(pluginState5.Name, []);
    // 			PacketManager.Instance.RegisterPacketFromAssembly(pluginState5.Assembly);
    // 			pluginState5.Plugin.OnInitialize();
    // 			pluginState5.IsInitialized = true;
    // 		}
    // 	}
    // }
    private void InitializePlugins()
    {
        // 1. Deduplicate: Keep only the latest version of each plugin
        var latestPlugins = Plugins.Values
            .GroupBy(p => p.Id.ToUpper())
            .Select(group =>
            {
                var latest = group.OrderByDescending(p => ParseVersion(p.Version)).First();
                if (group.Count() > 1)
                    Log.Information("Multiple versions of plugin {PluginId} found. Using version {Version} from {Path}",
                        latest.Id, latest.Version, latest.Path);
                return latest;
            })
            .ToDictionary(p => p.Id);

        Plugins.Clear();
        foreach (var p in latestPlugins) Plugins.Add(p.Key, p.Value);

        // 2. Build Dependency Graph (Kahn's Algorithm)
        var adjacencyList = Plugins.Keys.ToDictionary(id => id, _ => new List<string>());
        var inDegree = Plugins.Keys.ToDictionary(id => id, _ => 0);

        foreach (var plugin in Plugins.Values)
        {
            if (plugin.Dependencies == null) continue;

            foreach (var depId in plugin.Dependencies)
                if (Plugins.ContainsKey(depId))
                {
                    adjacencyList[depId].Add(plugin.Id); // depId is a prerequisite for plugin.Id
                    inDegree[plugin.Id]++;
                }
        }

        // 3. Perform Topological Sort
        var queue = new Queue<string>(inDegree.Where(kvp => kvp.Value == 0).Select(kvp => kvp.Key));
        var sortedIds = new List<string>();

        while (queue.TryDequeue(out var currentId))
        {
            sortedIds.Add(currentId);
            foreach (var dependentId in adjacencyList[currentId])
            {
                inDegree[dependentId]--;
                if (inDegree[dependentId] == 0)
                    queue.Enqueue(dependentId);
            }
        }

        // 4. Check for Circular Dependencies
        if (sortedIds.Count != Plugins.Count)
        {
            var cyclicIds = Plugins.Keys.Except(sortedIds).ToList();
            var errorMsg = $"Circular dependency detected: {string.Join(", ", cyclicIds)}";
            Log.Error(errorMsg);
            throw new InvalidOperationException(errorMsg);
        }

        // 5. Initialize Plugins in Order
        foreach (var state in sortedIds.Select(id => Plugins[id]).Where(state => !state.IsInitialized))
        {
            Log.Information("Initializing plugin: {Name}", state.Name);
            PacketManager.Instance.RegisterPacketFromAssembly(state.Assembly);
            state.Plugin.OnInitialize();
            state.IsInitialized = true;
        }
    }

    // public unsafe void UninstallPlugin(string pluginId)
    // {
    // 	var flag = Plugins.TryGetValue(pluginId, out var pluginState);
    // 	if (flag)
    // 	{
    // 		pluginState.Status = "Waiting Restart";
    // 		var num = 1;
    // 		var list = new List<string>(num);
    // 		CollectionsMarshal.SetCount<string>(list, num);
    // 		var span = CollectionsMarshal.AsSpan(list);
    // 		var num2 = 0;
    // 		*span[num2] = pluginState.Path;
    // 		UninstallPluginWithPaths(list);
    // 	}
    // }
    public void UninstallPlugin(string pluginId)
    {
        if (Plugins.TryGetValue(pluginId, out var pluginState))
        {
            pluginState.Status = "Waiting Restart";
            var paths = new List<string> { pluginState.Path };
            UninstallPluginWithPaths(paths);
        }
    }

    public void UninstallPluginWithPaths(List<string> paths)
    {
        using (_writeFileLock.EnterScope())
        {
            var hashSet = JsonSerializer.Deserialize<HashSet<string>>(File.ReadAllText(".ug_cache"));
            var flag = hashSet == null;
            if (flag)
            {
                Log.Error("Failed to read uninstall file", []);
            }
            else
            {
                foreach (var text in paths) hashSet.Add(text);
                File.WriteAllText(".ug_cache", JsonSerializer.Serialize<HashSet<string>>(hashSet));
            }
        }
    }

    public static void RestartGateway()
    {
        try
        {
            var text = Environment.ProcessPath;
            var flag = string.IsNullOrEmpty(text);
            if (flag)
            {
                using var currentProcess = Process.GetCurrentProcess();
                var mainModule = currentProcess.MainModule;
                text = mainModule != null ? mainModule.FileName : null;
            }

            var flag2 = string.IsNullOrEmpty(text);
            if (flag2)
            {
                Log.Error("Failed to determine executable path.", []);
            }
            else
            {
                var text2 = string.Join(" ", Environment.GetCommandLineArgs().Skip(1));
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = text,
                    Arguments = text2,
                    UseShellExecute = true
                };
                Log.Information("Preparing to restart gateway, Path: {ExecutablePath}, Arguments: {Arguments}", [
                    text, text2
                ]);
                Process.Start(processStartInfo);
                Log.Information("New process started.", []);
                Environment.Exit(0);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to restart gateway.", []);
        }
    }

    private const string UninstallPluginFile = ".ug_cache";
    private static PluginManager? _instance;
    private readonly HashSet<string> _loadedFiles = [];
    private readonly Lock _writeFileLock = new();
    public readonly Dictionary<string, PluginState> Plugins = new();

    public class PluginState(
        string id,
        string name,
        string description,
        string version,
        string author,
        string[]? dependencies,
        string path,
        Assembly? assembly,
        IPlugin? plugin)
    {
        public string Id { get; } = id;
        public string Name { get; } = name;
        public string Description { get; } = description;
        public string Version { get; } = version;
        public string Author { get; } = author;

        public string[]? Dependencies { get; } = dependencies;
        public string Path { get; } = path;
        public Assembly? Assembly { get; } = assembly;
        public IPlugin? Plugin { get; } = plugin;
        public string Status { get; set; } = "Online";
        public bool IsInitialized { get; set; }
    }
}