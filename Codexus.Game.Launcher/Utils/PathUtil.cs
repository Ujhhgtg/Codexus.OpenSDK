using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Serilog;

namespace Codexus.Game.Launcher.Utils;
public static class PathUtil
{

	public static void OpenDirectory(string path)
	{
		try
		{
			var flag = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
			if (flag)
			{
				Process.Start("explorer.exe", path);
			}
			else
			{
				var flag2 = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
				if (flag2)
				{
					Process.Start("open", path);
				}
				else
				{
					Process.Start("xdg-open", path);
				}
			}
		}
		catch (Exception ex)
		{
			Log.Error<string>(ex, "Failed to open directory: {Path}", path);
		}
	}

	public static bool ContainsChinese(string path)
	{
		return Regex.IsMatch(path, @"[\u4e00-\u9fff]");
	}
	public static readonly string CachePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".game_cache");
	public static readonly string ResourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources");
	public static readonly string UpdaterPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "updater");
	public static readonly string ScriptPath = Path.Combine(UpdaterPath, RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "update.bat" : "update.sh");
	public static readonly string CustomModsPath = Path.Combine(ResourcePath, "mods");
	public static readonly string WebSitePath = Path.Combine(ResourcePath, "static");
	public static readonly string JavaPath = Path.Combine(CachePath, "Java");
	public static readonly string Jre8Path = Path.Combine(JavaPath, "jre8");
	public static readonly string Jre17Path = Path.Combine(JavaPath, "jdk17");
	public static readonly string GamePath = Path.Combine(CachePath, "Game");
	public static readonly string GameBasePath = Path.Combine(GamePath, "Base");
	public static readonly string GameModsPath = Path.Combine(CachePath, "GameMods");
	public static readonly string CppGamePath = Path.Combine(CachePath, "CppGame");
}