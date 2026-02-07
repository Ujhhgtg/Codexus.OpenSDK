using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Codexus.Cipher.Connection.ChaCha;
using Codexus.Cipher.Entities.Connection;
using Codexus.Cipher.Extensions;
using Codexus.Development.SDK.Utils;
using Serilog;

namespace Codexus.Cipher.Connection;

public static class NetEaseConnection
{
	private static readonly byte[] TokenKey =
	[
		0xAC, 0x24, 0x9C, 0x69, 0xC7, 0x2C, 0xB3, 0xB4, 0x4E, 0xC0,
		0xCC, 0x6C, 0x54, 0x3A, 0x81, 0x95
	];

	private static readonly byte[] ChaChaNonce = "163 NetEase\n"u8.ToArray();

	public static int RandomAuthPort()
	{
		return new[] { 10200, 10600, 10400, 10000 }[Random.Shared.Next(0, 3)];
	}

	public static async Task CreateAuthenticatorAsync(string serverId, string gameId, string gameVersion, string modInfo, string nexusToken, int userId, string userToken, string authAddress, int authPort, Action handleSuccess, Func<string, string, int, string, byte[], string, byte[]>? buildEstablishing = null, Func<string, ChaChaOfSalsa, string, long, string, string, string, int, byte[], byte[]>? buildJoinServerMessage = null)
	{
		buildEstablishing ??= DefaultBuildEstablishing;
		buildJoinServerMessage ??= DefaultBuildJoinServerMessage;
		var client = new TcpClient();
		try
		{
			await client.ConnectAsync(IPAddress.Parse(authAddress), authPort);
			if (!client.Connected)
			{
				throw new TimeoutException($"Connecting to server {authAddress}:{authPort} timed out");
			}
			Log.Information("Connected to server {Address}:{Port}", authAddress, authPort);
			var stream = client.GetStream();
			using var details = await stream.ReadSteamWithInt16Async();
			var arg = details.ToArray();
			var remoteKey = new byte[16];
			var array = new byte[256];
			details.Position = 0L;
			details.ReadExactly(remoteKey);
			details.ReadExactly(array);
			await stream.WriteAsync(buildEstablishing(nexusToken, gameVersion, userId, userToken, arg, "netease"));
			using var statusStream = await stream.ReadSteamWithInt16Async();
			var b = (byte)statusStream.ReadByte();
			if (b != 0)
			{
				var reference = b;
				throw new Exception("Establishing error: " + Convert.ToHexString(new ReadOnlySpan<byte>(in reference)));
			}
			Log.Information("Establishing successfully");
			var array3 = Encoding.ASCII.GetBytes(userToken).Xor(TokenKey);
			var arg2 = new ChaChaOfSalsa(array3.CombineWith(remoteKey), ChaChaNonce, encryption: true);
			var decrypt = new ChaChaOfSalsa(remoteKey.CombineWith(array3), ChaChaNonce, encryption: false);
			await stream.WriteAsync(buildJoinServerMessage(nexusToken, arg2, serverId, long.Parse(gameId), gameVersion, modInfo, "netease", userId, remoteKey));
			using var memoryStream = await stream.ReadSteamWithInt16Async();
			var (b2, array5) = decrypt.UnpackMessage(memoryStream.ToArray());
			if (b2 != 9 || array5[0] != 0)
			{
				throw new Exception("Authentication of message failed: " + array5[0]);
			}
			handleSuccess();
		}
		catch (HttpRequestException ex)
		{
			client.Close();
			if (ex.StatusCode == HttpStatusCode.Unauthorized)
			{
				Log.Error("Access token is invalid or expired.");
			}
		}
		catch (Exception ex2)
		{
			client.Close();
			throw new Exception("Failed to create connection: " + ex2.Message, ex2);
		}
	}

	private static byte[] DefaultBuildEstablishing(string nexusToken, string gameVersion, int userId, string userToken, byte[] context, string channel)
	{
		Log.Information("Building establishing message");
		return Convert.FromBase64String(JsonSerializer.Deserialize<EntityHandshake>(new WebNexusApi(nexusToken).ComputeHandshakeBodyAsync(userId, userToken, Convert.ToBase64String(context), channel, gameVersion).GetAwaiter().GetResult())!.HandshakeBody);
	}

	private static byte[] DefaultBuildJoinServerMessage(string nexusToken, ChaChaOfSalsa cipher, string serverId, long gameId, string gameVersion, string modInfo, string channel, int userId, byte[] handshakeKey)
	{
		Log.Information("Building join server message");
		return cipher.PackMessage(9, Convert.FromBase64String(JsonSerializer.Deserialize<Dictionary<string, string>>(new WebNexusApi(nexusToken).ComputeAuthenticationBodyAsync(serverId, gameId, gameVersion, modInfo, channel, userId, Convert.ToBase64String(handshakeKey)).GetAwaiter().GetResult())!["authBody"]));
	}
}