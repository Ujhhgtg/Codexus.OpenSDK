using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Codexus.Cipher.Entities;
using Codexus.Cipher.Entities.WPFLauncher.NetGame;
using Codexus.Cipher.Entities.WPFLauncher.NetGame.Skin;
using Codexus.Cipher.Entities.WPFLauncher.NetGame.Texture;
using Codexus.Cipher.Entities.WPFLauncher.RPC;
using Codexus.Cipher.Skip32;
using Codexus.Development.SDK.Manager;
using Codexus.Game.Launcher.Managers;
using Codexus.Game.Launcher.Services.Java.RPC.Events;
using Codexus.Game.Launcher.Utils;
using Serilog;

namespace Codexus.Game.Launcher.Services.Java.RPC;
public sealed class GameRpcService(int port, string serverIp, string serverPort, string roleName, string userId, string userToken, EnumGameVersion gameVersion)
{

	public void Connect(string skinPath, GetSkinList getSkinListFunc, GetComponents getComponentsFunc)
	{
		_dirSkinPath = skinPath;
		_getSkin = getSkinListFunc;
		_getComponents = getComponentsFunc;
		StartControlConnection(2);
		_mSocketCallbackFuc.RegisterReceiveCallback(18, OnCheckPlayerMsg);
		_mSocketCallbackFuc.RegisterReceiveCallback(19, OnPCycEntityCheck);
		_mSocketCallbackFuc.RegisterReceiveCallback(261, HandleAuthenticationNewVersion);
		_mSocketCallbackFuc.RegisterReceiveCallback(512, OnHeartBeat);
		_mSocketCallbackFuc.RegisterReceiveCallback(517, HandleAuthentication);
		_mSocketCallbackFuc.RegisterReceiveCallback(520, HandlePlayerSkin);
		_mSocketCallbackFuc.RegisterReceiveCallback(1298, HandleMsgFilterCheck);
		const ushort num = 4612;
		HandleLoginGameAction ??= HandleLoginGame;
		_mSocketCallbackFuc.RegisterReceiveCallback(num, HandleLoginGameAction);
	}

	private void StartControlConnection(int tryTimes)
	{
		try
		{
			_mMcControlListener = new TcpListener(IPAddress.Loopback, port);
			_mMcControlListener.Start();
			new Thread(ListenControlConnect).Start();
			Console.WriteLine();
			Log.Information("[RPC] Control connection started on port {Port}", port);
		}
		catch (Exception ex)
		{
			var flag = tryTimes > 0;
			if (flag)
			{
				StartControlConnection(tryTimes - 1);
			}
			else
			{
				CloseControlConnection();
			}
			Console.WriteLine();
			Log.Error(ex, "[RPC] Failed to start control connection after retries");
		}
	}

	public void CloseControlConnection()
	{
		_mIsNormalExit = true;
		var mMcControlListener = _mMcControlListener;
		mMcControlListener?.Stop();
		_mMcControlListener = null;
		CloseGameCleaning();
		Log.Information("[RPC] Control connection closed");
	}

	private void HandleAuthenticationNewVersion(byte[] data)
	{
		var flag = gameVersion > EnumGameVersion.V_1_18;
		if (flag)
		{
			var array = SimplePack.Pack(1799, serverIp, int.Parse(serverPort), roleName);
			var flag2 = array != null;
			if (flag2)
			{
				SendControlData(array);
			}
			Log.Information("[RPC] Sent new-version authentication to {ServerIP}:{ServerPort} | User: {UserID} | Role: {RoleName} | Protocol: {GameVersion}", serverIp, serverPort, userId, roleName, gameVersion);
		}
	}

	private void HandleAuthentication(byte[] data)
	{
		var array = SimplePack.Pack(1031, serverIp, int.Parse(serverPort), roleName, false);
		var flag = array != null;
		if (flag)
		{
			SendControlData(array);
		}
		Log.Information("[RPC] Sent authentication to {ServerIP}:{ServerPort} | User: {UserID} | Role: {RoleName} | Protocol: {GameVersion}", serverIp, serverPort, userId, roleName, gameVersion);
	}

	private void OnHeartBeat(byte[] data)
	{
		var array = SimplePack.Pack(512, "i'am wpflauncher");
		var flag = array != null;
		if (flag)
		{
			SendControlData(array);
		}
		Log.Information<string>("[RPC] Heartbeat {heartBeatMsg} sent", "i'am wpflauncher");
	}

	private static void OnPCycEntityCheck(byte[] data)
	{
		Log.Information<string>("[RPC] PCyc Entity {entity} sent", "[]");
	}

	private void HandlePlayerSkin(byte[] content)
	{
		Log.Information<string>("[RPC] Event received -> {event}", "Send Player Skin");
		var entityOtherEnterWorldMsg = new EntityOtherEnterWorldMsg();
		new SimpleUnpack(content).Unpack<EntityOtherEnterWorldMsg>(ref entityOtherEnterWorldMsg);
		TaskManager.Instance.GetFactory().StartNew(() => ProcessPlayerSkin(entityOtherEnterWorldMsg));
	}

	// private async Task ProcessPlayerSkin(EntityOtherEnterWorldMsg msg)
	// {
	// 	var getSkin = _getSkin;
	// 	List<EntityUserGameTexture> list = (getSkin != null ? getSkin(userId, this.<userToken>P, new EntityUserGameTextureRequest
	// 	{
	// 		UserId = _skip32Cipher.ComputeUserIdFromUuid(msg.Uuid).ToString(),
	// 		ClientType = EnumGameClientType.Java
	// 	}) : null);
	// 	var filePath = string.Empty;
	// 	var skinMode = EnumSkinMode.Default;
	// 	var flag = list != null;
	// 	if (flag)
	// 	{
	// 		foreach (var item in list.Where((EntityUserGameTexture s) => s.SkinId.Length > 5))
	// 		{
	// 			skinMode = (EnumSkinMode)item.SkinMode;
	// 			var tempPath = Path.Combine(_dirSkinPath, "skin_" + item.SkinId + ".png");
	// 			var flag2 = File.Exists(tempPath) && FileUtil.IsFileReadable(tempPath);
	// 			if (flag2)
	// 			{
	// 				filePath = tempPath;
	// 				break;
	// 			}
	// 			try
	// 			{
	// 				var instance = IUserManager.Instance;
	// 				EntityAvailableUser entityAvailableUser = instance != null ? instance.GetAvailableUser(userId : null);
	// 				var flag3 = entityAvailableUser == null;
	// 				if (flag3)
	// 				{
	// 					continue;
	// 				}
	// 				var getComponents = _getComponents;
	// 				Entity<EntityComponentDownloadInfoResponse> entity = (getComponents != null ? getComponents(userId, entityAvailableUser.AccessToken, item.SkinId) : null);
	// 				var flag4 = entity != null && entity.Code != 0;
	// 				if (flag4)
	// 				{
	// 					continue;
	// 				}
	// 				var entity2 = entity;
	// 				string text2;
	// 				if (entity2 == null)
	// 				{
	// 					text2 = null;
	// 				}
	// 				else
	// 				{
	// 					var data = entity2.Data;
	// 					if (data == null)
	// 					{
	// 						text2 = null;
	// 					}
	// 					else
	// 					{
	// 						text2 = data.SubEntities.Select((EntityComponentDownloadInfoResponseSub sub) => sub.ResUrl).FirstOrDefault<string>();
	// 					}
	// 				}
	// 				var text = text2;
	// 				var flag5 = text != null;
	// 				if (flag5)
	// 				{
	// 					Directory.CreateDirectory(_dirSkinPath);
	// 					var text3 = tempPath;
	// 					var array2 = await _httpClient.GetByteArrayAsync(text);
	// 					await FileUtil.WriteFileSafelyAsync(text3, array2);
	// 					text3 = null;
	// 					array2 = null;
	// 					if (File.Exists(tempPath) && FileUtil.IsFileReadable(tempPath))
	// 					{
	// 						filePath = tempPath;
	// 						break;
	// 					}
	// 				}
	// 				entityAvailableUser = null;
	// 				entity = null;
	// 				text = null;
	// 			}
	// 			catch (Exception exception)
	// 			{
	// 				Log.Error(exception, "[RPC] Failed to handle skin for player {Name}", msg.Name);
	// 				try
	// 				{
	// 					if (File.Exists(tempPath))
	// 					{
	// 						File.Delete(tempPath);
	// 					}
	// 				}
	// 				catch
	// 				{
	// 					Log.Error<string>(exception, "[RPC] Failed to delete temp file {Path}", filePath);
	// 				}
	// 			}
	// 			tempPath = null;
	// 			item = null;
	// 		}
	// 		IEnumerator<EntityUserGameTexture> enumerator = null;
	// 	}
	// 	Log.Information("[RPC] Sending skin data for {Name}: {Path}", msg.Name, filePath);
	// 	var array = SimplePack.Pack(520, msg.Name, filePath, string.Empty, skinMode);
	// 	if (array != null)
	// 	{
	// 		SendControlData(array);
	// 	}
	// }
	private async Task ProcessPlayerSkin(EntityOtherEnterWorldMsg msg)
	{
		var filePath = string.Empty;
		var skinMode = EnumSkinMode.Default;

		var getSkin = _getSkin;
		if (getSkin == null)
		{
			SendSkin(msg, filePath, skinMode);
			return;
		}

		var skins = getSkin(
			userId,
			userToken,
			new EntityUserGameTextureRequest
			{
				UserId = _skip32Cipher
					.ComputeUserIdFromUuid(msg.Uuid)
					.ToString(),
				ClientType = EnumGameClientType.Java
			});

		if (skins == null)
		{
			SendSkin(msg, filePath, skinMode);
			return;
		}

		foreach (var skin in skins)
		{
			if (skin?.SkinId is not { Length: > 5 })
			{
				continue;
			}

			skinMode = (EnumSkinMode)skin.SkinMode;
			var tempPath = Path.Combine(_dirSkinPath, $"skin_{skin.SkinId}.png");

			if (File.Exists(tempPath) && FileUtil.IsFileReadable(tempPath))
			{
				filePath = tempPath;
				break;
			}

			try
			{
				var userManager = IUserManager.Instance;
				var availableUser = userManager?.GetAvailableUser(userId);
				if (availableUser == null)
				{
					continue;
				}

				var getComponents = _getComponents;
				if (getComponents == null)
				{
					continue;
				}

				var response = getComponents(
					userId,
					availableUser.AccessToken,
					skin.SkinId);

				if (response is not { Code: 0 })
				{
					continue;
				}

				var url = response.Data?
					.SubEntities?
					.FirstOrDefault()?
					.ResUrl;

				if (string.IsNullOrEmpty(url))
				{
					continue;
				}

				Directory.CreateDirectory(_dirSkinPath);

				var bytes = await _httpClient.GetByteArrayAsync(url);
				await FileUtil.WriteFileSafelyAsync(tempPath, bytes);

				if (File.Exists(tempPath) && FileUtil.IsFileReadable(tempPath))
				{
					filePath = tempPath;
					break;
				}
			}
			catch (Exception ex)
			{
				Log.Error(
					ex,
					"[RPC] Failed to handle skin for player {Name}",
					msg.Name);

				try
				{
					if (File.Exists(tempPath))
					{
						File.Delete(tempPath);
					}
				}
				catch
				{
					Log.Error(
						ex,
						"[RPC] Failed to delete temp file {Path}",
						tempPath);
				}
			}
		}

		SendSkin(msg, filePath, skinMode);
	}

	private void SendSkin(
		EntityOtherEnterWorldMsg msg,
		string filePath,
		EnumSkinMode skinMode)
	{
		Log.Information(
			"[RPC] Sending skin data for {Name}: {Path}",
			msg.Name,
			filePath);

		var payload = SimplePack.Pack(520, msg.Name, filePath, string.Empty, skinMode);

		if (payload != null)
		{
			SendControlData(payload);
		}
	}

	private static void HandleLoginGame(byte[] data)
	{
		Log.Information<string>("[RPC] Event received -> {event}", "Login Game");
	}

	private void HandleMsgFilterCheck(byte[] data)
	{
		var array = SimplePack.Pack(1298, false, 0L, 0L);
		var flag = array != null;
		if (flag)
		{
			SendControlData(array);
		}
		Log.Information<string>("[RPC] Event received -> {event}", "Filter Message Check");
	}

	private void OnCheckPlayerMsg(byte[] data)
	{
		EntityCheckPlayerMessage entityCheckPlayerMessage = null;
		new SimpleUnpack(data).Unpack(ref entityCheckPlayerMessage);
		var flag = entityCheckPlayerMessage != null;
		if (flag)
		{
			var array = SimplePack.Pack(18, entityCheckPlayerMessage.Length, entityCheckPlayerMessage.Message);
			var flag2 = array != null;
			if (flag2)
			{
				SendControlData(array);
			}
		}
		Log.Information<string, string>("[RPC] Event received -> {event} with data {Message}", "Player Message Check", entityCheckPlayerMessage != null ? entityCheckPlayerMessage.Message : null);
	}

	private void ListenControlConnect()
	{
		try
		{
			while (!_mIsNormalExit)
			{
				var mMcControlListener = _mMcControlListener;
				var tcpClient = mMcControlListener != null ? mMcControlListener.AcceptTcpClient() : null;
				var flag = tcpClient == null;
				object obj;
				if (flag)
				{
					obj = null;
				}
				else
				{
					var remoteEndPoint = tcpClient.Client.RemoteEndPoint;
					var flag2 = remoteEndPoint == null;
					if (flag2)
					{
						obj = null;
					}
					else
					{
						var text = remoteEndPoint.ToString();
						obj = text != null ? text.Split(':')[0] : null;
					}
				}
				var flag3 = (string)obj != IPAddress.Loopback.ToString();
				if (flag3)
				{
					tcpClient?.Close();
				}
				else
				{
					Client = tcpClient;
					var stream = tcpClient.GetStream();
					_reader = new BinaryReader(stream);
					_writer = new BinaryWriter(stream);
					SendCacheControlData();
					new Thread(OnRecvControlData).Start();
					var text2 = "[RPC] Accepted control connection from {RemoteEndPoint}";
					var remoteEndPoint2 = tcpClient.Client.RemoteEndPoint;
					Log.Information<string>(text2, remoteEndPoint2 != null ? remoteEndPoint2.ToString() : null);
				}
			}
		}
		catch
		{
			Log.Error("[RPC] Failed to listen control connection");
		}
	}

	private void SendControlData(byte[] message)
	{
		var flag = _writer == null;
		if (flag)
		{
			_sendCache.Add(message);
		}
		else
		{
			var array = BitConverter.GetBytes((ushort)message.Length).Concat(message).ToArray();
			try
			{
				var writer = _writer;
				writer?.Write(array);
				var writer2 = _writer;
				writer2?.Flush();
			}
			catch (Exception ex)
			{
				Log.Error(ex, "[RPC] Failed to send control data");
			}
		}
	}

	private void OnRecvControlData()
	{
		while (!_mIsNormalExit)
		{
			try
			{
				var flag = _reader != null;
				if (flag)
				{
					var num = _reader.ReadUInt16();
					var array = _reader.ReadBytes(num);
					HandleMcControlMessage(array);
				}
			}
			catch (EndOfStreamException)
			{
				break;
			}
			catch (IOException)
			{
				break;
			}
			catch (Exception ex)
			{
				Log.Error(ex, "[RPC] Error receiving control data");
				var flag2 = !_mIsNormalExit;
				if (flag2)
				{
					CloseGameCleaning();
				}
				break;
			}
		}
	}

	private void HandleMcControlMessage(byte[] message)
	{
		var num = BitConverter.ToUInt16(message, 0);
		var array = message.Skip(2).ToArray();
		var flag = !_mIsLaunchIdxReady && num == 261;
		if (flag)
		{
			_mIsLaunchIdxReady = true;
			Log.Information("[RPC] Launch index ready, executed ready actions");
		}
		_mSocketCallbackFuc.InvokeCallback(num, array);
	}

	private void CloseGameCleaning()
	{
		_mIsLaunchIdxReady = false;
		Log.Information("[RPC] Cleaned up game resources");
	}

	private void SendCacheControlData()
	{
		foreach (var array in _sendCache)
		{
			SendControlData(array);
		}
		_sendCache.Clear();
	}
	private readonly HttpClient _httpClient = new();
	private readonly SocketCallback _mSocketCallbackFuc = new();
	private readonly List<byte[]> _sendCache = [];
	private readonly Skip32Cipher _skip32Cipher = new("SaintSteve\0"u8.ToArray());
	private string _dirSkinPath = string.Empty;
	private GetComponents? _getComponents;
	private GetSkinList? _getSkin;
	private bool _mIsLaunchIdxReady;
	private bool _mIsNormalExit;
	private TcpListener? _mMcControlListener;
	private BinaryReader? _reader;
	private BinaryWriter? _writer;
	public TcpClient? Client;
	public delegate Entity<EntityComponentDownloadInfoResponse> GetComponents(string userId, string userToken, string str);
	public delegate List<EntityUserGameTexture> GetSkinList(string userId, string userToken, EntityUserGameTextureRequest entity);
	public static Action<byte[]>? HandleLoginGameAction;
}
