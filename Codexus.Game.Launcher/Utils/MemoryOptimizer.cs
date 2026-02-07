using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading;
using Serilog;

namespace Codexus.Game.Launcher.Utils;

public partial class MemoryOptimizer : IDisposable
{
	private static MemoryOptimizer? _instance;

	private static readonly Lock Lock = new();

	private Timer? _optimizationTimer;

	private readonly HashSet<int> _processedIds = [];

	private readonly Lock _lockObject = new();

	private bool _disposed;
    private static readonly string[] SourceArray = ["minecraft", "net.minecraft", "launchwrapper", "forge", "fabric", "quilt", "optifine", ".minecraft", "versions", "libraries"];

    [LibraryImport("kernel32.dll")]
	private static partial nint OpenProcess(uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwProcessId);

	[LibraryImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static partial bool CloseHandle(nint hObject);

	[LibraryImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static partial bool SetProcessWorkingSetSize(nint hProcess, nint dwMinimumWorkingSetSize, nint dwMaximumWorkingSetSize);

	[LibraryImport("psapi.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static partial bool EmptyWorkingSet(nint hProcess);

	private MemoryOptimizer()
	{
		_optimizationTimer = new Timer(OptimizeCallback, null, TimeSpan.Zero, TimeSpan.FromMinutes(7L));
	}

	public static MemoryOptimizer GetInstance()
	{
		using (Lock.EnterScope())
		{
			return _instance ??= new MemoryOptimizer();
		}
	}

	private void OptimizeCallback(object? state)
	{
		if (_disposed || !RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		{
			Dispose();
			return;
		}
		try
		{
			var minecraftProcesses = GetMinecraftProcesses();
			if (minecraftProcesses.Count == 0)
			{
				Log.Information("[Memory Optimizer] No Minecraft processes found, stopping optimizer");
				Dispose();
				return;
			}
			foreach (var item in minecraftProcesses)
			{
				OptimizeProcess(item);
			}
			CleanupExitedProcesses();
		}
		catch (Exception exception)
		{
			Log.Error(exception, "[Memory Optimizer] Error in optimization callback");
		}
	}

	private static List<Process> GetMinecraftProcesses()
	{
		List<Process> list = [];
		try
		{
			list.AddRange(Process.GetProcessesByName("javaw").Where(IsMinecraftProcess));
		}
		catch (Exception exception)
		{
			Log.Error(exception, "[Memory Optimizer] Failed to get Minecraft processes");
		}
		return list;
	}

	private static bool IsMinecraftProcess(Process process)
	{
		try
		{
			var commandLine = GetCommandLine(process);
			return !string.IsNullOrEmpty(commandLine) && SourceArray.Any(keyword => commandLine.Contains(keyword, StringComparison.OrdinalIgnoreCase));
		}
		catch (Exception exception)
		{
			Log.Warning(exception, "[Memory Optimizer] Failed to check if process is Minecraft: {ProcessId}", process.Id);
			return false;
		}
	}

	private static string? GetCommandLine(Process process)
	{
		try
		{
			if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				return null;
			}
			using var managementObjectSearcher = new ManagementObjectSearcher($"SELECT CommandLine FROM Win32_Process WHERE ProcessId = {process.Id}");
			using var managementObjectCollection = managementObjectSearcher.Get();
			foreach (var item in managementObjectCollection)
			{
				var managementObject = (ManagementObject?)(item is ManagementObject ? item : null);
				if (managementObject != null)
				{
					return managementObject["CommandLine"]?.ToString() ?? "";
				}
			}
		}
		catch
		{
			try
			{
				return process.StartInfo.Arguments;
			}
			catch
			{
				Log.Error("[Memory Optimizer] Failed to get CommandLine arguments");
				return null;
			}
		}
		return "";
	}

	private void OptimizeProcess(Process process)
	{
		if (_disposed)
		{
			return;
		}
		using (_lockObject.EnterScope())
		{
			try
			{
				if (process.HasExited)
				{
					return;
				}
				process.Refresh();
				var workingSet = process.WorkingSet64;
				var propertyValue = workingSet / 1048576;
				var num = OpenProcess(1280u, bInheritHandle: false, (uint)process.Id);
				if (num == IntPtr.Zero)
				{
					return;
				}
				try
				{
					if (EmptyWorkingSet(num))
					{
						var num2 = Math.Max(52428800L, workingSet / 4);
						_ = SetProcessWorkingSetSize(dwMaximumWorkingSetSize: new IntPtr(Math.Max(num2 * 2, workingSet)), hProcess: num, dwMinimumWorkingSetSize: new IntPtr(num2));
						Thread.Sleep(500);
						process.Refresh();
						Log.Information(propertyValue2: process.WorkingSet64 / 1048576, messageTemplate: "[Memory Optimizer] Process ID: {ProcessId} - Memory Before: {BeforeMemory} MB, After: {AfterMemory} MB", propertyValue0: process.Id, propertyValue1: propertyValue);
						_processedIds.Add(process.Id);
					}
				}
				finally
				{
					_ = CloseHandle(num);
				}
			}
			catch (Exception exception)
			{
				Log.Error(exception, "[Memory Optimizer] Failed to optimize process {ProcessId}", process.Id);
			}
		}
	}

	private void CleanupExitedProcesses()
	{
		if (_disposed)
		{
			return;
		}
		using (_lockObject.EnterScope())
		{
			List<int> list = [];
			foreach (var processedId in _processedIds)
			{
				try
				{
					var processById = Process.GetProcessById(processedId);
					if (processById.HasExited)
					{
						list.Add(processedId);
						processById.Dispose();
					}
				}
				catch (ArgumentException)
				{
					list.Add(processedId);
				}
				catch (Exception exception)
				{
					Log.Warning(exception, "[Memory Optimizer] Error checking process {ProcessId}", processedId);
					list.Add(processedId);
				}
			}
			foreach (var item in list)
			{
				_processedIds.Remove(item);
			}
			if (list.Count > 0)
			{
				Log.Information("[Memory Optimizer] Cleaned up {Count} exited processes", list.Count);
			}
		}
	}

	public void Dispose()
	{
		if (_disposed)
		{
			return;
		}
		using (Lock.EnterScope())
		{
			if (!_disposed)
			{
				_disposed = true;
				_optimizationTimer?.Dispose();
				_optimizationTimer = null;
				_instance = null;
				Log.Information("[Memory Optimizer] Disposed");
			}
		}
		GC.SuppressFinalize(this);
	}
}
