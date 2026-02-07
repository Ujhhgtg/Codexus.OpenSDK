using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Codexus.Cipher.Entities.WPFLauncher.NetGame;
using Codexus.Cipher.Entities.WPFLauncher.NetGame.Mods;
using Codexus.Cipher.Entities.WPFLauncher.NetGame.Texture;
using Codexus.Cipher.Protocol;
using Codexus.Game.Launcher.Utils;
using Codexus.Game.Launcher.Utils.Progress;
using Serilog;

namespace Codexus.Game.Launcher.Services.Java;
public static class InstallerService
{

	public static async Task<bool> PrepareMinecraftClient(string userId, string userToken, WPFLauncher wpfLauncher, EnumGameVersion gameVersion)
	{
		var versionName = Enum.GetName(gameVersion);
		var md5Path = Path.Combine(PathUtil.GameBasePath, "GAME_BASE.MD5");
		var zipPath = Path.Combine(PathUtil.CachePath, "GameBase.7z");
		var versionMd5File = Path.Combine(PathUtil.GameBasePath, versionName + ".MD5");
		var versionZip = Path.Combine(PathUtil.CachePath, versionName + ".7z");
		var libMd5File = Path.Combine(PathUtil.GameBasePath, versionName + "_Lib.MD5");
		var libZip = Path.Combine(PathUtil.CachePath, versionName + "_Lib.7z");
		var minecraftClientLibs = wpfLauncher.GetMinecraftClientLibs(userId, userToken);
		var flag = minecraftClientLibs.Code != 0;
		if (flag)
		{
			throw new Exception("Failed to fetch base package: " + minecraftClientLibs.Message);
		}
		await ProcessPackage(minecraftClientLibs.Data.Url, zipPath, PathUtil.GameBasePath, md5Path, minecraftClientLibs.Data.Md5, "base package");
		var versionResult = wpfLauncher.GetMinecraftClientLibs(userId, userToken, gameVersion);
		if (versionResult.Code != 0)
		{
			throw new Exception("Failed to fetch " + versionName + " package: " + versionResult.Message);
		}
		await ProcessPackage(versionResult.Data.Url, versionZip, PathUtil.GameBasePath, versionMd5File, versionResult.Data.Md5, versionName + " package");
		await ProcessPackage(versionResult.Data.CoreLibUrl, libZip, PathUtil.CachePath, libMd5File, versionResult.Data.CoreLibMd5, versionName + " libraries");
		InstallCoreLibs(Path.Combine(PathUtil.CachePath, versionName + "_libs"), gameVersion);
		return true;
	}

	private static async Task ProcessPackage(string url, string zipPath, string extractTo, string md5Path, string md5, string label)
	{
		// 1. Check if the package is already up to date
		if (File.Exists(md5Path))
		{
			var currentMd5 = await File.ReadAllTextAsync(md5Path);
			if (currentMd5 == md5)
			{
				return; // Already installed and matches MD5
			}
		}

		// 2. Setup Progress Tracking
		var progressBar = new SyncProgressBarUtil.ProgressBar(100);
		IProgress<SyncProgressBarUtil.ProgressReport> uiProgress = new Progress<SyncProgressBarUtil.ProgressReport>(update =>
		{
			progressBar.Update(update.Percent, update.Message);
		});

		// 3. Download
		await DownloadUtil.DownloadAsync(url, zipPath, p =>
		{
			uiProgress.Report(new SyncProgressBarUtil.ProgressReport
			{
				Percent = (int)p,
				Message = $"Downloading {label}"
			});
		}, 8, CancellationToken.None);

		// 4. Extract
		CompressionUtil.Extract7Z(zipPath, extractTo, p =>
		{
			uiProgress.Report(new SyncProgressBarUtil.ProgressReport
			{
				Percent = p,
				Message = $"Extracting {label}"
			});
		});

		// 5. Finalize: Save MD5 marker and clean up
		await File.WriteAllTextAsync(md5Path, md5);
		FileUtil.DeleteFileSafe(zipPath);
	}

	private static void InstallCoreLibs(string libPath, EnumGameVersion gameVersion)
	{
		var gameVersionFromEnum = GameVersionUtil.GetGameVersionFromEnum(gameVersion);
		var text = "forge-" + gameVersionFromEnum + "-";
		var text2 = "launchwrapper-";
		var text3 = "MercuriusUpdater-";
		var text4 = gameVersionFromEnum + ".jar";
		var text5 = gameVersionFromEnum + ".json";
		var flag = !Directory.Exists(libPath);
		if (!flag)
		{
			var files = Directory.GetFiles(libPath, "*", SearchOption.AllDirectories);
			foreach (var text6 in files)
			{
				var fileName = Path.GetFileName(text6);
				var flag2 = fileName.StartsWith(text);
				if (flag2)
				{
					text = Path.GetFileNameWithoutExtension(text6);
					var text7 = text.Replace("forge-", "");
					var text8 = Path.Combine(PathUtil.GameBasePath, ".minecraft", @"libraries\net\minecraftforge\forge", text7);
					var text9 = Path.Combine(text8, text + ".jar");
					var flag3 = !Directory.Exists(text8);
					if (flag3)
					{
						Directory.CreateDirectory(text8);
					}
					else
					{
						var flag4 = File.Exists(text9);
						if (flag4)
						{
							File.Delete(text9);
						}
					}
					File.Copy(text6, text9, true);
				}
				else
				{
					var flag5 = fileName.StartsWith(text2);
					if (flag5)
					{
						text2 = Path.GetFileNameWithoutExtension(text6);
						var text10 = text2.Replace("launchwrapper-", "");
						var text11 = Path.Combine(PathUtil.GameBasePath, ".minecraft", @"libraries\net\minecraft\launchwrapper", text10);
						var text12 = Path.Combine(text11, text2 + ".jar");
						var flag6 = !Directory.Exists(text11);
						if (flag6)
						{
							Directory.CreateDirectory(text11);
						}
						else
						{
							var flag7 = File.Exists(text12);
							if (flag7)
							{
								File.Delete(text12);
							}
						}
						File.Copy(text6, text12, true);
					}
					else
					{
						var flag8 = fileName.StartsWith(text3);
						if (flag8)
						{
							text3 = Path.GetFileNameWithoutExtension(text6);
							var text13 = text3.Replace("MercuriusUpdater-", "");
							var text14 = Path.Combine(PathUtil.GameBasePath, ".minecraft", @"libraries\net\minecraftforge\MercuriusUpdater", text13);
							var text15 = Path.Combine(text14, text3 + ".jar");
							var flag9 = !Directory.Exists(text14);
							if (flag9)
							{
								Directory.CreateDirectory(text14);
							}
							else
							{
								var flag10 = File.Exists(text15);
								if (flag10)
								{
									File.Delete(text15);
								}
							}
							File.Copy(text6, text15, true);
						}
						else
						{
							var flag11 = fileName.Equals(text4);
							if (flag11)
							{
								var text16 = Path.Combine(new[]
								{
									PathUtil.GameBasePath,
									".minecraft",
									"versions",
									gameVersionFromEnum,
									text4
								});
								File.Copy(text6, text16, true);
							}
							else
							{
								var flag12 = fileName.Equals(text5);
								if (flag12)
								{
									var text17 = Path.Combine(new[]
									{
										PathUtil.GameBasePath,
										".minecraft",
										"versions",
										gameVersionFromEnum,
										text5
									});
									File.Copy(text6, text17, true);
								}
								else
								{
									var flag13 = fileName.StartsWith("modlauncher-") && fileName.Contains("9.1.0");
									if (flag13)
									{
										var text18 = Path.Combine(new[]
										{
											PathUtil.GameBasePath,
											".minecraft",
											@"libraries\cpw\mods\modlauncher\9.1.0\modlauncher-9.1.0.jar"
										});
										File.Copy(text6, text18, true);
									}
									else
									{
										var flag14 = fileName.StartsWith("modlauncher-") && fileName.Contains("10.0.9");
										if (flag14)
										{
											var text19 = Path.Combine(new[]
											{
												PathUtil.GameBasePath,
												".minecraft",
												@"libraries\cpw\mods\modlauncher\10.0.9\modlauncher-10.0.9.jar"
											});
											File.Copy(text6, text19, true);
										}
										else
										{
											var flag15 = fileName.StartsWith("modlauncher-") && fileName.Contains("10.2.1");
											if (flag15)
											{
												var text20 = Path.Combine(new[]
												{
													PathUtil.GameBasePath,
													".minecraft",
													@"libraries\net\minecraftforge\modlauncher\10.2.1\modlauncher-10.2.1.jar"
												});
												File.Copy(text6, text20, true);
											}
										}
									}
								}
							}
						}
					}
				}
			}
			FileUtil.DeleteDirectorySafe(libPath);
		}
	}

	public static async Task<EntityModsList?> InstallGameMods(
		string userId,
		string userToken,
		EnumGameVersion gameVersion,
		WPFLauncher wpfLauncher,
		string gameId,
		bool isRental)
	{
		// 1. Initial Fetch
		var gameCoreResponse = await wpfLauncher.GetGameCoreModListAsync(userId, userToken, gameVersion, isRental);
		if (gameCoreResponse?.Data?.IidList == null) return null;

		// 2. Fetch Details
		var detailsResponse =
			await wpfLauncher.GetGameCoreModDetailsListAsync(userId, userToken, gameCoreResponse.Data.IidList);
		var modList = new EntityModsList();

		// Setup Progress Tracking
		var progressBar = new SyncProgressBarUtil.ProgressBar(100);
		IProgress<SyncProgressBarUtil.ProgressReport> progressReporter = new Progress<SyncProgressBarUtil.ProgressReport>(update =>
			progressBar.Update(update.Percent, update.Message));

		// 3. Process Initial Mod Info
		foreach (var modDetail in detailsResponse.Data)
		{
			foreach (var sub in modDetail.SubEntities)
			{
				// Simplified string interpolation
				var modIdentifier = $"{modDetail.ItemId}@{modDetail.MTypeId}@0.jar";

				modList.Mods.Add(new EntityModsInfo
				{
					ModPath = modIdentifier,
					Id = modIdentifier,
					Iid = modDetail.ItemId,
					Md5 = sub.JarMd5.ToUpper(),
					Name = "",
					Version = ""
				});
			}
		}

		// 4. Download and Extract Core Mods
		var corePath = Path.Combine(PathUtil.GameModsPath, gameId);
		if (Directory.Exists(corePath)) Directory.Delete(corePath, true);

		var total = detailsResponse.Total;
		for (var i = 0; i < detailsResponse.Data.Length; i++)
		{
			var mod = detailsResponse.Data[i];
			var firstSub = mod.SubEntities[0];
			var displayIdx = i + 1;

			var fileName = Path.GetFileNameWithoutExtension(firstSub.ResName);
			var jarName = $"{fileName}@{mod.MTypeId}@{mod.EntityId}.jar";
			var jarPath = Path.Combine(corePath, jarName);
			var archivePath = Path.Combine(corePath, firstSub.ResName);
			var extractDir = Path.Combine(corePath, fileName);

			// Check if download is necessary
			if (!File.Exists(jarPath) || !FileUtil.ComputeMd5FromFile(jarPath)
				    .Equals(firstSub.JarMd5, StringComparison.OrdinalIgnoreCase))
			{
				// Download
				await DownloadUtil.DownloadAsync(firstSub.ResUrl, archivePath, dp =>
				{
					progressReporter.Report(new SyncProgressBarUtil.ProgressReport
					{
						Percent = (int)dp,
						Message = $"Downloading core mod {displayIdx}/{total}"
					});
				}, 8, CancellationToken.None);

				// Extract
				CompressionUtil.Extract7Z(archivePath, extractDir, p =>
				{
					progressReporter.Report(new SyncProgressBarUtil.ProgressReport
					{
						Percent = p * 100 / total,
						Message = $"Extracting core mod {displayIdx}/{total}"
					});
				});

				// Clean up extraction
				foreach (var extractedJar in FileUtil.EnumerateFiles(extractDir, "jar"))
				{
					FileUtil.CopyFileSafe(extractedJar, jarPath);
				}

				FileUtil.DeleteDirectorySafe(extractDir);
				FileUtil.DeleteFileSafe(archivePath);
			}
		}

		progressReporter.Report(new SyncProgressBarUtil.ProgressReport { Percent = 100, Message = "Core mods ready" });

		// 5. Process Net Game Components
		var cacheDir = Path.Combine(PathUtil.CachePath, "Game", gameId);
		var cacheArchive = cacheDir + ".7z";
		var md5FilePath = Path.Combine(cacheDir, $"{gameId}.MD5");
		var jsonFilePath = Path.Combine(cacheDir, $"{gameId}.json");

		FileUtil.CreateDirectorySafe(cacheDir);

		var netCompResponse = wpfLauncher.GetNetGameComponentDownloadList(userId, userToken, gameId);
		if (netCompResponse?.Data != null && netCompResponse.Code == 0)
		{
			var comp = netCompResponse.Data.SubEntities[0];
			var needsRefresh = !File.Exists(md5FilePath) || (await File.ReadAllTextAsync(md5FilePath) != comp.ResMd5);

			if (!needsRefresh && File.Exists(jsonFilePath))
			{
				var cachedList = JsonSerializer.Deserialize<EntityModsList>(await File.ReadAllTextAsync(jsonFilePath));
				if (cachedList?.Mods != null) modList.Mods.AddRange(cachedList.Mods);
			}
			else
			{
				FileUtil.DeleteFileSafe(cacheArchive);
				await DownloadUtil.DownloadAsync(comp.ResUrl, cacheArchive,
					p =>
					{
						progressReporter.Report(new SyncProgressBarUtil.ProgressReport
							{ Percent = (int)p, Message = "Downloading game assets" });
					}, 8, CancellationToken.None);

				FileUtil.DeleteDirectorySafe(cacheDir);
				CompressionUtil.Extract7Z(cacheArchive, cacheDir,
					p =>
					{
						progressReporter.Report(new SyncProgressBarUtil.ProgressReport
							{ Percent = p, Message = "Extracting game assets" });
					});

				// Index new mods
				var serverModsList = new EntityModsList();
				var modFiles = FileUtil.EnumerateFiles(Path.Combine(cacheDir, ".minecraft", "mods"), "jar");

				foreach (var path in modFiles)
				{
					var name = Path.GetFileName(path);
					var bytes = await File.ReadAllBytesAsync(path);
					var hash = Convert.ToHexString(MD5.HashData(bytes)).ToUpper();

					serverModsList.Mods.Add(new EntityModsInfo
					{
						ModPath = name,
						Id = name,
						Iid = name.Split('@')[0],
						Md5 = hash,
						Name = "",
						Version = ""
					});
				}

				modList.Mods.AddRange(serverModsList.Mods);
				await File.WriteAllTextAsync(md5FilePath, comp.ResMd5);
				await File.WriteAllTextAsync(jsonFilePath, JsonSerializer.Serialize(serverModsList));
				FileUtil.DeleteFileSafe(cacheArchive);
			}
		}

		progressReporter.Report(new SyncProgressBarUtil.ProgressReport
			{ Percent = 100, Message = "Game assets ready" });
		SyncProgressBarUtil.ProgressBar.ClearCurrent();

		return modList;
	}

	private static void InstallCustomMods(string mods)
	{
		FileUtil.EnumerateFiles(PathUtil.CustomModsPath, "jar").ToList<string>().ForEach(delegate(string f)
		{
			FileUtil.CopyFileSafe(f, Path.Combine(mods, Path.GetFileName(f)));
		});
	}

	public static string PrepareGameRuntime(string userId, string gameId, string roleName, EnumGType gameType)
	{
		var text = HashUtil.GenerateGameRuntimeId(gameId, roleName);
		var text2 = Path.Combine(PathUtil.GamePath, "Runtime", text);
		var text3 = Path.Combine(text2, ".minecraft");
		var flag = !Directory.Exists(text2);
		if (flag)
		{
			Directory.CreateDirectory(text2);
		}
		var flag2 = gameType == EnumGType.NetGame;
		if (flag2)
		{
			var text4 = Path.Combine(text3, "mods");
			FileUtil.DeleteDirectorySafe(text4);
			FileUtil.CreateDirectorySafe(text4);
			FileUtil.CopyDirectory(Path.Combine(PathUtil.CachePath, "Game", gameId, ".minecraft"), text3, false);
			InstallCustomMods(text4);
		}
		var text5 = Path.Combine(text3, "assets");
		var text6 = Path.Combine(PathUtil.GameBasePath, ".minecraft", "assets");
		var flag3 = Directory.Exists(text5);
		if (flag3)
		{
			FileUtil.DeleteDirectorySafe(text5);
		}
		FileUtil.CreateSymbolicLinkSafe(text5, text6);
		return text2;
	}

	public static void InstallCoreMods(string gameId, string targetModsPath)
	{
		var text = Path.Combine(PathUtil.GameModsPath, gameId);
		var flag = Directory.Exists(text);
		if (flag)
		{
			FileUtil.CreateDirectorySafe(targetModsPath);
			var array = FileUtil.EnumerateFiles(text);
			foreach (var text2 in array)
			{
				var text3 = Path.Combine(targetModsPath, Path.GetRelativePath(text, text2));
				FileUtil.CreateDirectorySafe(Path.GetDirectoryName(text3));
				FileUtil.CopyFileSafe(text2, text3);
			}
		}
	}

	public static void InstallNativeDll(EnumGameVersion gameVersion)
	{
		try
		{
			var text = Path.Combine(PathUtil.ResourcePath, "api-ms-win-crt-utility-l1-1-1.dll");
			var text2 = Path.Combine(new[]
			{
				PathUtil.GameBasePath,
				".minecraft",
				"versions",
				GameVersionUtil.GetGameVersionFromEnum(gameVersion),
				"natives",
				"runtime"
			});
			var flag = !Directory.Exists(text2);
			if (flag)
			{
				FileUtil.CreateDirectorySafe(text2);
			}
			var flag2 = !File.Exists(text);
			if (flag2)
			{
				throw new Exception("Native dll not found: " + text);
			}
			var text3 = Path.Combine(text2, "api-ms-win-crt-utility-l1-1-1.dll");
			FileUtil.CopyFileSafe(text, text3);
		}
		catch (Exception ex)
		{
			var text4 = "Failed to install native dll:";
			Log.Error(text4 + (ex != null ? ex.ToString() : null));
		}
	}
}
