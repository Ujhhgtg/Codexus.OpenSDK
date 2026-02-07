using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Serilog;

namespace Codexus.Game.Launcher.Utils;
public partial class MemoryOptimizer : IDisposable
{
	[LibraryImport("kernel32.dll")]
	private static partial IntPtr OpenProcess(uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwProcessId);
	
	[LibraryImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static partial bool CloseHandle(IntPtr hObject);
	
	[LibraryImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static partial bool SetProcessWorkingSetSize(IntPtr hProcess, IntPtr dwMinimumWorkingSetSize, IntPtr dwMaximumWorkingSetSize);
	
	[DllImport("psapi.dll", ExactSpelling = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool EmptyWorkingSet(IntPtr hProcess);

	private MemoryOptimizer()
	{
		_optimizationTimer = new Timer(OptimizeCallback, null, TimeSpan.Zero, TimeSpan.FromMinutes(7L));
	}

	public static MemoryOptimizer GetInstance()
	{
		MemoryOptimizer memoryOptimizer2;
		using (Lock.EnterScope())
		{
			MemoryOptimizer memoryOptimizer;
			if ((memoryOptimizer = _instance) == null)
			{
				memoryOptimizer = _instance = new MemoryOptimizer();
			}
			memoryOptimizer2 = memoryOptimizer;
		}
		return memoryOptimizer2;
	}

	private void OptimizeCallback(object? state)
	{
		if ( _disposed || !RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		{
			Dispose();
		}
		else
		{
			try
			{
				var minecraftProcesses = GetMinecraftProcesses();
				var flag2 = minecraftProcesses.Count == 0;
				if (flag2)
				{
					Log.Information("[Memory Optimizer] No Minecraft processes found, stopping optimizer");
					Dispose();
				}
				else
				{
					foreach (var process in minecraftProcesses)
					{
						OptimizeProcess(process);
					}
					CleanupExitedProcesses();
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, "[Memory Optimizer] Error in optimization callback");
			}
		}
	}

	private static List<Process> GetMinecraftProcesses()
	{
		var list = new List<Process>();
		try
		{
			var processesByName = Process.GetProcessesByName("javaw");
			IEnumerable<Process> enumerable = processesByName;
			IsMinecraftProcessFunc ??= IsMinecraftProcess;
			list.AddRange(enumerable.Where(IsMinecraftProcessFunc));
		}
		catch (Exception ex)
		{
			Log.Error(ex, "[Memory Optimizer] Failed to get Minecraft processes");
		}
		return list;
	}

	private static bool IsMinecraftProcess(Process process)
	{
		bool flag;
		try
		{
			var commandLine = GetCommandLine(process);
			flag = !string.IsNullOrEmpty(commandLine) && SourceArray.Any(keyword => commandLine.Contains(keyword, StringComparison.OrdinalIgnoreCase));
		}
		catch (Exception ex)
		{
			Log.Warning(ex, "[Memory Optimizer] Failed to check if process is Minecraft: {ProcessId}", process.Id);
			flag = false;
		}
		return flag;
	}

	private static string? GetCommandLine(Process process)
	{
		try
		{
			var flag = !RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
			if (flag)
			{
				return null;
			}
			var defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(56, 1);
			defaultInterpolatedStringHandler.AppendLiteral("SELECT CommandLine FROM Win32_Process WHERE ProcessId = ");
			defaultInterpolatedStringHandler.AppendFormatted(process.Id);
			var managementObjectSearcher = new ManagementObjectSearcher(defaultInterpolatedStringHandler.ToStringAndClear());
			try
			{
				var managementObjectCollection = managementObjectSearcher.Get();
				try
				{
					var enumerator = managementObjectCollection.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							var managementBaseObject = enumerator.Current;
							var managementObject = (ManagementObject)(managementBaseObject is ManagementObject ? managementBaseObject : null);
							var flag2 = managementObject != null;
							if (flag2)
							{
								var obj = managementObject["CommandLine"];
								return (obj != null ? obj.ToString() : null) ?? "";
							}
						}
					}
					finally
					{
						enumerator.Dispose();
					}
				}
				finally
				{
					managementObjectCollection.Dispose();
				}
			}
			finally
			{
				managementObjectSearcher.Dispose();
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
		var disposed = _disposed;
		if (!disposed)
		{
			using (_lockObject.EnterScope())
			{
				try
				{
					var hasExited = process.HasExited;
					if (!hasExited)
					{
						process.Refresh();
						var workingSet = process.WorkingSet64;
						var num = workingSet / 1048576L;
						var intPtr = OpenProcess(1280U, false, (uint)process.Id);
						var flag = intPtr == IntPtr.Zero;
						if (!flag)
						{
							try
							{
								var flag2 = EmptyWorkingSet(intPtr);
								if (flag2)
								{
									var num2 = Math.Max(52428800L, workingSet / 4L);
									var num3 = Math.Max(num2 * 2L, workingSet);
									SetProcessWorkingSetSize(intPtr, new IntPtr(num2), new IntPtr(num3));
									Thread.Sleep(500);
									process.Refresh();
									var num4 = process.WorkingSet64 / 1048576L;
									Log.Information("[Memory Optimizer] Process ID: {ProcessId} - Memory Before: {BeforeMemory} MB, After: {AfterMemory} MB", process.Id, num, num4);
									_processedIds.Add(process.Id);
								}
							}
							finally
							{
								CloseHandle(intPtr);
							}
						}
					}
				}
				catch (Exception ex)
				{
					Log.Error(ex, "[Memory Optimizer] Failed to optimize process {ProcessId}", process.Id);
				}
			}
		}
	}

	private void CleanupExitedProcesses()
	{
		var disposed = _disposed;
		if (!disposed)
		{
			using (_lockObject.EnterScope())
			{
				var list = new List<int>();
				foreach (var num in _processedIds)
				{
					try
					{
						var processById = Process.GetProcessById(num);
						var hasExited = processById.HasExited;
						if (hasExited)
						{
							list.Add(num);
							processById.Dispose();
						}
					}
					catch (ArgumentException)
					{
						list.Add(num);
					}
					catch (Exception ex)
					{
						Log.Warning(ex, "[Memory Optimizer] Error checking process {ProcessId}", num);
						list.Add(num);
					}
				}
				foreach (var num2 in list)
				{
					_processedIds.Remove(num2);
				}
				var flag = list.Count > 0;
				if (flag)
				{
					Log.Information("[Memory Optimizer] Cleaned up {Count} exited processes", list.Count);
				}
			}
		}
	}

	public void Dispose()
	{
		var disposed = _disposed;
		if (!disposed)
		{
			using (Lock.EnterScope())
			{
				var flag = !_disposed;
				if (flag)
				{
					_disposed = true;
					var optimizationTimer = _optimizationTimer;
					if (optimizationTimer != null)
					{
						optimizationTimer.Dispose();
					}
					_optimizationTimer = null;
					_instance = null;
					Log.Information("[Memory Optimizer] Disposed");
				}
			}
		}
		GC.SuppressFinalize(this);
	}
	private const uint ProcessQueryInformation = 1024U;
	private const uint ProcessSetQuota = 256U;
	private static MemoryOptimizer? _instance;
	private static readonly Lock Lock = new();
	private Timer? _optimizationTimer;
	private readonly HashSet<int> _processedIds = [];
	private readonly Lock _lockObject = new();
	private bool _disposed;
	public static Func<Process, bool>? IsMinecraftProcessFunc;
    private static readonly string[] SourceArray = ["minecraft", "net.minecraft", "launchwrapper", "forge", "fabric", "quilt", "optifine", ".minecraft", "versions", "libraries"
    ];
}
