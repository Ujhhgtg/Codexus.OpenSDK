using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Codexus.Game.Launcher.Utils;
using Codexus.Game.Launcher.Utils.Progress;

namespace Codexus.Game.Launcher.Services.Bedrock;
public static class InstallerService
{

	public static async Task<bool> DownloadMinecraftAsync()
	{
		bool flag;
		try
		{
			var paths = GetInstallationPaths();
			var result = IsMinecraftInstalledAsync(paths).GetAwaiter().GetResult();
			if (result)
			{
				flag = true;
			}
			else
			{
				FileUtil.CleanDirectorySafe(paths.BasePath);
				using var progressBar = new SyncProgressBarUtil.ProgressBar(100);
				var progress = CreateProgressReporter(progressBar);
				var flag2 = await DownloadMinecraftPackage(paths.ArchivePath, progress);
				var flag3 = !flag2;
				if (flag3)
				{
					flag = false;
				}
				else
				{
					var flag4 = await ValidateDownloadedPackage(paths.ArchivePath, progress);
					if (!flag4)
					{
						flag = false;
					}
					else
					{
						await ExtractMinecraftPackage(paths.ArchivePath, paths.BasePath, progress);
						await SavePackageHash(paths.ArchivePath, paths.HashPath);
						await CleanupTemporaryFiles(paths.ArchivePath, progress);
						flag = true;
					}
				}
			}
		}
		catch (Exception innerException)
		{
			throw new InvalidOperationException("Failed to download and install Minecraft", innerException);
		}
		finally
		{
			SyncProgressBarUtil.ProgressBar.ClearCurrent();
		}
		return flag;
	}

	private static (string BasePath, string ArchivePath, string HashPath, string ExecutablePath) GetInstallationPaths()
	{
		var cppGamePath = PathUtil.CppGamePath;
		return (cppGamePath, Path.Combine(cppGamePath, "mc_base.7z"), Path.Combine(cppGamePath, "minecraft_pe.md5"), Path.Combine(cppGamePath, "windowsmc/Minecraft.Windows.exe"));
	}

	private static async Task<bool> IsMinecraftInstalledAsync((string BasePath, string ArchivePath, string HashPath, string ExecutablePath) paths)
	{
		var flag = !File.Exists(paths.ExecutablePath) || !File.Exists(paths.HashPath);
		bool flag2;
		if (flag)
		{
			flag2 = false;
		}
		else
		{
			try
			{
				var array = await File.ReadAllBytesAsync(paths.HashPath);
				flag2 = string.Equals(Convert.ToHexStringLower(array), "50ac5016023c295222b979565b9c707b", StringComparison.OrdinalIgnoreCase);
			}
			catch (Exception)
			{
				flag2 = false;
			}
		}
		return flag2;
	}

	private static IProgress<SyncProgressBarUtil.ProgressReport> CreateProgressReporter(SyncProgressBarUtil.ProgressBar progressBar)
	{
		return new Progress<SyncProgressBarUtil.ProgressReport>(delegate(SyncProgressBarUtil.ProgressReport update)
		{
			progressBar.Update(update.Percent, update.Message);
		});
	}

	private static async Task<bool> DownloadMinecraftPackage(string archivePath, IProgress<SyncProgressBarUtil.ProgressReport> progress)
	{
		return await DownloadUtil.DownloadAsync("https://x19.gdl.netease.com/release2_20250402_3.4.Win32.Netease.r20.OGL.Publish_3.4.5.273310_20250627104722.7z", archivePath, delegate(uint percentage)
		{
			progress.Report(new SyncProgressBarUtil.ProgressReport
			{
				Percent = (int)percentage,
				Message = "Downloading Minecraft Bedrock"
			});
		});
	}

	private static async Task<bool> ValidateDownloadedPackage(string archivePath, IProgress<SyncProgressBarUtil.ProgressReport> progress)
	{
		progress.Report(new SyncProgressBarUtil.ProgressReport
		{
			Percent = 0,
			Message = "Checking package validation"
		});
		var flag = await ValidatePackageAsync(archivePath, "50ac5016023c295222b979565b9c707b");
		var flag2 = !flag;
		bool flag3;
		if (flag2)
		{
			flag3 = false;
		}
		else
		{
			progress.Report(new SyncProgressBarUtil.ProgressReport
			{
				Percent = 100,
				Message = "Package validation succeeded"
			});
			flag3 = true;
		}
		return flag3;
	}

	private static async Task ExtractMinecraftPackage(string archivePath, string basePath, IProgress<SyncProgressBarUtil.ProgressReport> progress)
	{
		await CompressionUtil.Extract7ZAsync(archivePath, basePath, delegate(int percentage)
		{
			progress.Report(new SyncProgressBarUtil.ProgressReport
			{
				Percent = percentage,
				Message = "Extracting Minecraft Bedrock"
			});
		});
	}

	private static async Task SavePackageHash(string archivePath, string hashPath)
	{
		var array = await File.ReadAllBytesAsync(archivePath);
		var bytes = MD5.HashData(array);
		array = null;
		await File.WriteAllBytesAsync(hashPath, bytes);
	}

	private static async Task CleanupTemporaryFiles(string archivePath, IProgress<SyncProgressBarUtil.ProgressReport> progress)
	{
		progress.Report(new SyncProgressBarUtil.ProgressReport
		{
			Percent = 0,
			Message = "Cleaning up game resources"
		});
		FileUtil.DeleteFileSafe(archivePath);
		progress.Report(new SyncProgressBarUtil.ProgressReport
		{
			Percent = 100,
			Message = "Cleanup completed"
		});
		await Task.CompletedTask;
	}

	private static async Task<bool> ValidatePackageAsync(string filePath, string expectedMd5)
	{
		var flag = string.IsNullOrWhiteSpace(expectedMd5);
		if (flag)
		{
			throw new ArgumentException("Expected MD5 hash cannot be null or empty", "expectedMd5");
		}
		var flag2 = !File.Exists(filePath);
		if (flag2)
		{
			throw new FileNotFoundException("File not found: " + filePath, filePath);
		}
		var array = await File.ReadAllBytesAsync(filePath);
		return string.Equals(Convert.ToHexStringLower(MD5.HashData(array)), expectedMd5, StringComparison.OrdinalIgnoreCase);
	}
	private const string MinecraftArchiveName = "mc_base.7z";
	private const string HashFileName = "minecraft_pe.md5";
	private const string MinecraftExecutablePath = "windowsmc/Minecraft.Windows.exe";
}