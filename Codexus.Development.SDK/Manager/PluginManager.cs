using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading;
using Codexus.Development.SDK.Plugin;
using Serilog;

namespace Codexus.Development.SDK.Manager;

public class PluginManager
{
	public class PluginState(string id, string name, string description, string version, string author, string[]? dependencies, string path, Assembly? assembly, IPlugin? plugin)
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

	private static PluginManager? _instance;

	private readonly HashSet<string> _loadedFiles = [];

	private readonly Lock _writeFileLock = new();

	public readonly Dictionary<string, PluginState> Plugins = new();

	public static string[] PluginExtensions { get; set; } = [".ug", ".dll", ".UG"];

	public static PluginManager Instance => _instance ??= new PluginManager();

	public void EnsureUninstall()
	{
		using (_writeFileLock.EnterScope())
		{
			if (!File.Exists(".ug_cache"))
			{
				File.WriteAllText(".ug_cache", JsonSerializer.Serialize(new HashSet<string>()));
				return;
			}
			var hashSet = JsonSerializer.Deserialize<HashSet<string>>(File.ReadAllText(".ug_cache"));
			if (hashSet == null)
			{
				return;
			}
			foreach (var item in hashSet)
			{
				File.Delete(item);
			}
			File.Delete(".ug_cache");
			File.WriteAllText(".ug_cache", JsonSerializer.Serialize(new HashSet<string>()));
		}
	}

	public void LoadPlugins(string directory)
	{
		if (!Directory.Exists(directory))
		{
			Directory.CreateDirectory(directory);
			return;
		}
		var array = (from f in Directory.EnumerateFiles(directory)
			where PluginExtensions.Contains<string>(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase)
			select f).ToArray();
		foreach (var text in array)
		{
			if (_loadedFiles.Contains(text))
			{
				continue;
			}
			try
			{
				var assembly = Assembly.LoadFrom(text);
				foreach (var item in from type in assembly.GetTypes()
					where typeof(IPlugin).IsAssignableFrom(type) && type is { IsAbstract: false, IsInterface: false }
					select type)
				{
					Attributes.Plugin? customAttribute;
					try
					{
						customAttribute = item.GetCustomAttribute<Attributes.Plugin>(inherit: false);
					}
					catch (MissingMemberException)
					{
						Log.Warning("插件 {TypeFullName} 没有插件属性", [item.FullName]);
						continue;
					}
					if (customAttribute == null)
					{
						Log.Warning("插件 {TypeFullName} 没有插件属性", [item.FullName]);
					}
					else if (!Plugins.ContainsKey(customAttribute.Id))
					{
						if (!(Activator.CreateInstance(item) is IPlugin plugin))
						{
							Log.Warning("插件 {TypeFullName} 没有继承 IPlugin", [item.FullName]);
						}
						else
						{
							Plugins.Add(customAttribute.Id.ToUpper(), new PluginState(customAttribute.Id.ToUpper(), customAttribute.Name, customAttribute.Description, customAttribute.Version, customAttribute.Author, customAttribute.Dependencies, text, assembly, plugin));
							_loadedFiles.Add(text);
						}
					}
				}
			}
			catch (Exception exception)
			{
				Log.Error(exception, "Failed to load plugin from {File}", [text]);
			}
		}
		CheckDependencies();
		Log.Information("识别了 {Count} 个插件", [Plugins.Count]);
		InitializePlugins();
	}

	public bool HasPlugin(string id)
	{
		return Plugins.ContainsKey(id.ToUpper());
	}

	public PluginState GetPlugin(string id)
	{
		if (!Plugins.ContainsKey(id.ToUpper()))
		{
			throw new InvalidOperationException("Plugin " + id + " is not loaded");
		}
		return Plugins[id.ToUpper()];
	}

	public List<string> GetPluginAndDependencyPaths(string pluginId, Func<string, bool>? excludeRule = null)
	{
		pluginId = pluginId.ToUpper();
		if (!HasPlugin(pluginId))
		{
			throw new InvalidOperationException("Plugin " + pluginId + " is not loaded");
		}
		var hashSet = new HashSet<string>();
		CollectDependencyPaths(pluginId, hashSet, [], excludeRule);
		return hashSet.ToList();
	}

	private void CollectDependencyPaths(string pluginId, HashSet<string> pathSet, HashSet<string> visitedPlugins, Func<string, bool>? excludeRule = null)
	{
		if (!visitedPlugins.Add(pluginId) || (excludeRule != null && excludeRule(pluginId)))
		{
			return;
		}
		if (!HasPlugin(pluginId))
		{
			Log.Warning("Plugin {PluginId} is not loaded", [pluginId]);
			return;
		}
		var plugin = GetPlugin(pluginId);
		pathSet.Add(plugin.Path);
		if (plugin.Dependencies != null)
		{
			var dependencies = plugin.Dependencies;
			foreach (var dependency in dependencies)
			{
				CollectDependencyPaths(dependency.ToUpper(), pathSet, visitedPlugins, excludeRule);
			}
		}
	}

	private void CheckDependencies()
	{
		foreach (var value in Plugins.Values)
		{
			if (value.Dependencies == null)
			{
				continue;
			}
			var dependencies = value.Dependencies;
			foreach (var text in dependencies)
			{
				if (!Plugins.ContainsKey(text))
				{
					throw new InvalidOperationException($"Plugin {value.Name}({value.Id}) depends on {text}, but it is not loaded");
				}
			}
		}
	}

	private static Version ParseVersion(string version)
	{
		try
		{
			var array = version.Split('.');
			return new Version(array.Length != 0 && int.TryParse(array[0], out var result) ? result : 0, array.Length > 1 && int.TryParse(array[1], out var result2) ? result2 : 0, array.Length > 2 && int.TryParse(array[2], out var result3) ? result3 : 0);
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "Failed to parse plugin version: {Version}. Using default 0.0.0", [version]);
			return new Version(0, 0, 0);
		}
	}

	private void InitializePlugins()
	{
		var dictionary = new Dictionary<string, PluginState>();
		foreach (var item in from p in Plugins.Values
			group p by p.Id.ToUpper())
		{
			var pluginState = item.OrderByDescending(p => ParseVersion(p.Version)).First();
			dictionary[pluginState.Id] = pluginState;
			if (item.Count() > 1)
			{
				Log.Information("Multiple versions of plugin {PluginId} found. Using version {Version} from {Path}",
					[pluginState.Id, pluginState.Version, pluginState.Path]);
			}
		}
		Plugins.Clear();
		foreach (var item2 in dictionary)
		{
			Plugins.Add(item2.Key, item2.Value);
		}
		var dictionary2 = new Dictionary<string, List<string>>();
		var inDegree = new Dictionary<string, int>();
		foreach (var value in Plugins.Values)
		{
			dictionary2[value.Id] = [];
			inDegree[value.Id] = 0;
		}
		foreach (var value2 in Plugins.Values)
		{
			if (value2.Dependencies == null)
			{
				continue;
			}
			var dependencies = value2.Dependencies;
			foreach (var key in dependencies)
			{
				if (Plugins.ContainsKey(key))
				{
					dictionary2[key].Add(value2.Id);
					inDegree[value2.Id]++;
				}
			}
		}
		var queue = new Queue<string>();
		foreach (var item3 in Plugins.Values.Where(plugin => inDegree[plugin.Id] == 0))
		{
			queue.Enqueue(item3.Id);
		}
		var list = new List<string>();
		while (queue.Count > 0)
		{
			var text = queue.Dequeue();
			list.Add(text);
			foreach (var item4 in dictionary2[text])
			{
				inDegree[item4]--;
				if (inDegree[item4] == 0)
				{
					queue.Enqueue(item4);
				}
			}
		}
		if (list.Count != Plugins.Count)
		{
			var text2 = string.Join(", ", Plugins.Keys.Except(list).ToList());
			Log.Error("Circular dependency detected among plugins: {CircularDependencies}", [text2]);
			throw new InvalidOperationException("Circular dependency detected among plugins: " + text2);
		}
		foreach (var item5 in list.Select(pluginId => Plugins[pluginId]))
		{
			if (!item5.IsInitialized)
			{
				Log.Information("{Name}", item5.Name);
				PacketManager.Instance.RegisterPacketFromAssembly(item5.Assembly!);
				item5.Plugin!.OnInitialize();
				item5.IsInitialized = true;
			}
		}
	}

	public void UninstallPlugin(string pluginId)
	{
		if (Plugins.TryGetValue(pluginId, out var value))
		{
			value.Status = "Waiting Restart";
			UninstallPluginWithPaths([value.Path]);
		}
	}

	public void UninstallPluginWithPaths(List<string> paths)
	{
		using (_writeFileLock.EnterScope())
		{
			var hashSet = JsonSerializer.Deserialize<HashSet<string>>(File.ReadAllText(".ug_cache"));
			if (hashSet == null)
			{
				Log.Error("Failed to read uninstall file", []);
				return;
			}
			foreach (var path in paths)
			{
				hashSet.Add(path);
			}
			File.WriteAllText(".ug_cache", JsonSerializer.Serialize(hashSet));
		}
	}

	public static void RestartGateway()
	{
		try
		{
			var text = Environment.ProcessPath;
			if (string.IsNullOrEmpty(text))
			{
				using var process = Process.GetCurrentProcess();
				text = process.MainModule?.FileName;
			}
			if (string.IsNullOrEmpty(text))
			{
				Log.Error("Failed to determine executable path.", []);
				return;
			}
			var text2 = string.Join(" ", Environment.GetCommandLineArgs().Skip(1));
			var startInfo = new ProcessStartInfo
			{
				FileName = text,
				Arguments = text2,
				UseShellExecute = true
			};
			Log.Information("Preparing to restart gateway, Path: {ExecutablePath}, Arguments: {Arguments}", [text, text2
			]);
			Process.Start(startInfo);
			Log.Information("New process started.", []);
			Environment.Exit(0);
		}
		catch (Exception exception)
		{
			Log.Error(exception, "Failed to restart gateway.", []);
		}
	}
}
