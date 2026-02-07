// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Net;
// using System.Net.Http;
// using System.Net.Sockets;
// using System.Runtime.CompilerServices;
// using System.Text;
// using System.Text.Json;
// using System.Threading.Tasks;
// using Codexus.Cipher.Connection.ChaCha;
// using Codexus.Cipher.Entities.Connection;
// using Codexus.Cipher.Extensions;
// using Codexus.Development.SDK.Utils;
// using Serilog;
//
// namespace Codexus.Cipher.Connection
// {
// 	// Token: 0x020000AA RID: 170
// 	public static class NetEaseConnection
// 	{
// 		// Token: 0x0600066E RID: 1646 RVA: 0x0000B1A0 File Offset: 0x000093A0
// 		public static int RandomAuthPort()
// 		{
// 			var array = new[] { 10200, 10600, 10400, 10000 };
// 			return array[new Random().Next(0, array.Length)];
// 		}
//
// 		// Token: 0x0600066F RID: 1647 RVA: 0x0000B1D4 File Offset: 0x000093D4
// 		public static async Task CreateAuthenticatorAsync(string serverId, string gameId, string gameVersion, string modInfo, string nexusToken, int userId, string userToken, string authAddress, int authPort, Action handleSuccess, [Nullable(new byte[] { 2, 0, 0, 0, 0, 0, 0 })] Func<string, string, int, string, byte[], string, byte[]> buildEstablishing = null, [Nullable(new byte[] { 2, 0, 0, 0, 0, 0, 0, 0, 0 })] Func<string, ChaChaOfSalsa, string, long, string, string, string, int, byte[], byte[]> buildJoinServerMessage = null)
// 		{
// 			var flag = buildEstablishing == null;
// 			if (flag)
// 			{
// 				Func<string, string, int, string, byte[], string, byte[]> func;
// 				if ((func = SomeCompilerGeneratedGarbage.DefaultBuildEstablishing) == null)
// 				{
// 					func = SomeCompilerGeneratedGarbage.DefaultBuildEstablishing = DefaultBuildEstablishing;
// 				}
// 				buildEstablishing = func;
// 			}
// 			var flag2 = buildJoinServerMessage == null;
// 			if (flag2)
// 			{
// 				Func<string, ChaChaOfSalsa, string, long, string, string, string, int, byte[], byte[]> func2;
// 				if ((func2 = SomeCompilerGeneratedGarbage.DefaultBuildJoinServerMessage) == null)
// 				{
// 					func2 = SomeCompilerGeneratedGarbage.DefaultBuildJoinServerMessage = DefaultBuildJoinServerMessage;
// 				}
// 				buildJoinServerMessage = func2;
// 			}
// 			var client = new TcpClient();
// 			try
// 			{
// 				await client.ConnectAsync(IPAddress.Parse(authAddress), authPort);
// 				if (!client.Connected)
// 				{
// 					var defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(32, 2);
// 					defaultInterpolatedStringHandler.AppendLiteral("Connecting to server ");
// 					defaultInterpolatedStringHandler.AppendFormatted(authAddress);
// 					defaultInterpolatedStringHandler.AppendLiteral(":");
// 					defaultInterpolatedStringHandler.AppendFormatted(authPort);
// 					defaultInterpolatedStringHandler.AppendLiteral(" timed out");
// 					throw new TimeoutException(defaultInterpolatedStringHandler.ToStringAndClear());
// 				}
// 				Log.Information("Connected to server {Address}:{Port}", authAddress, authPort);
// 				var stream = client.GetStream();
// 				var memoryStream2 = await stream.ReadSteamWithInt16Async();
// 				var details = memoryStream2;
// 				memoryStream2 = null;
// 				byte[] arg;
// 				byte[] remoteKey;
// 				byte[] array;
// 				byte[] array2;
// 				MemoryStream statusStream;
// 				byte[] array3;
// 				ChaChaOfSalsa arg2;
// 				ChaChaOfSalsa decrypt;
// 				byte[] array4;
// 				MemoryStream memoryStream;
// 				byte[] data;
// 				byte[] array5;
// 				try
// 				{
// 					arg = details.ToArray();
// 					remoteKey = new byte[16];
// 					array = new byte[256];
// 					details.Position = 0L;
// 					details.ReadExactly(remoteKey);
// 					details.ReadExactly(array);
// 					array2 = buildEstablishing(nexusToken, gameVersion, userId, userToken, arg, "netease");
// 					await stream.WriteAsync(array2);
// 					var memoryStream3 = await stream.ReadSteamWithInt16Async();
// 					statusStream = memoryStream3;
// 					memoryStream3 = null;
// 					try
// 					{
// 						var b = (byte)statusStream.ReadByte();
// 						if (b > 0)
// 						{
// 							var reference = b;
// 							throw new Exception("Establishing error: " + Convert.ToHexString(new ReadOnlySpan<byte>(ref reference)));
// 						}
// 						Log.Information("Establishing successfully");
// 						array3 = Encoding.ASCII.GetBytes(userToken).Xor(TokenKey);
// 						arg2 = new ChaChaOfSalsa(array3.CombineWith(remoteKey), ChaChaNonce, true);
// 						decrypt = new ChaChaOfSalsa(remoteKey.CombineWith(array3), ChaChaNonce, false);
// 						array4 = buildJoinServerMessage(nexusToken, arg2, serverId, long.Parse(gameId), gameVersion, modInfo, "netease", userId, remoteKey);
// 						await stream.WriteAsync(array4);
// 						var memoryStream4 = await stream.ReadSteamWithInt16Async();
// 						memoryStream = memoryStream4;
// 						memoryStream4 = null;
// 						try
// 						{
// 							data = memoryStream.ToArray();
// 							var valueTuple = decrypt.UnpackMessage(data);
// 							var b2 = valueTuple.Item1;
// 							array5 = valueTuple.Item2;
// 							if (b2 != 9 || array5[0] > 0)
// 							{
// 								throw new Exception("Authentication of message failed: " + array5[0]);
// 							}
// 							handleSuccess();
// 						}
// 						finally
// 						{
// 							if (memoryStream != null)
// 							{
// 								((IDisposable)memoryStream).Dispose();
// 							}
// 						}
// 					}
// 					finally
// 					{
// 						if (statusStream != null)
// 						{
// 							((IDisposable)statusStream).Dispose();
// 						}
// 					}
// 				}
// 				finally
// 				{
// 					if (details != null)
// 					{
// 						((IDisposable)details).Dispose();
// 					}
// 				}
// 				stream = null;
// 				details = null;
// 				arg = null;
// 				remoteKey = null;
// 				array = null;
// 				array2 = null;
// 				statusStream = null;
// 				array3 = null;
// 				arg2 = null;
// 				decrypt = null;
// 				array4 = null;
// 				memoryStream = null;
// 				data = null;
// 				array5 = null;
// 			}
// 			catch (HttpRequestException ex)
// 			{
// 				client.Close();
// 				if (ex.StatusCode.GetValueOrDefault() == HttpStatusCode.Unauthorized)
// 				{
// 					Log.Error("Access token is invalid or expired.");
// 				}
// 			}
// 			catch (Exception ex2)
// 			{
// 				client.Close();
// 				throw new Exception("Failed to create connection: " + ex2.Message, ex2);
// 			}
// 		}
//
// 		// Token: 0x06000670 RID: 1648 RVA: 0x0000B270 File Offset: 0x00009470
// 		private static byte[] DefaultBuildEstablishing(string nexusToken, string gameVersion, int userId, string userToken, byte[] context, string channel)
// 		{
// 			Log.Information("Building establishing message");
// 			return Convert.FromBase64String(JsonSerializer.Deserialize<EntityHandshake>(new WebNexusApi(nexusToken).ComputeHandshakeBodyAsync(userId, userToken, Convert.ToBase64String(context), channel, gameVersion).GetAwaiter().GetResult()).HandshakeBody);
// 		}
//
// 		// Token: 0x06000671 RID: 1649 RVA: 0x0000B2C4 File Offset: 0x000094C4
// 		private static byte[] DefaultBuildJoinServerMessage(string nexusToken, ChaChaOfSalsa cipher, string serverId, long gameId, string gameVersion, string modInfo, string channel, int userId, byte[] handshakeKey)
// 		{
// 			Log.Information("Building join server message");
// 			var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(new WebNexusApi(nexusToken).ComputeAuthenticationBodyAsync(serverId, gameId, gameVersion, modInfo, channel, userId, Convert.ToBase64String(handshakeKey)).GetAwaiter().GetResult());
// 			return cipher.PackMessage(9, Convert.FromBase64String(dictionary["authBody"]));
// 		}
//
// 		// Token: 0x04000312 RID: 786
// 		private static readonly byte[] TokenKey = new byte[]
// 		{
// 			172, 36, 156, 105, 199, 44, 179, 180, 78, 192,
// 			204, 108, 84, 58, 129, 149
// 		};
//
// 		// Token: 0x04000313 RID: 787
// 		private static readonly byte[] ChaChaNonce = new byte[] {
// 			0x31, 0x36, 0x33, 0x20, 0x4E, 0x65, 0x74, 0x45, 0x61, 0x73, 0x65, 0x0A, 0x00
// 		};
//
// 		// Token: 0x0200012B RID: 299
// 		[CompilerGenerated]
// 		private static class SomeCompilerGeneratedGarbage
// 		{
// 			// Token: 0x0400072C RID: 1836
// 			public static Func<string, string, int, string, byte[], string, byte[]> DefaultBuildEstablishing;
//
// 			// Token: 0x0400072D RID: 1837
// 			public static Func<string, ChaChaOfSalsa, string, long, string, string, string, int, byte[], byte[]> DefaultBuildJoinServerMessage;
// 		}
// 	}
// }

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
    private static readonly byte[] TokenKey = [172, 36, 156, 105, 199, 44, 179, 180, 78, 192, 204, 108, 84, 58, 129, 149
    ];
    private static readonly byte[] ChaChaNonce = "163 NetEase\n"u8.ToArray();

    public static int RandomAuthPort() => new Random().Next(0, 4) switch {
        0 => 10200, 1 => 10600, 2 => 10400, _ => 10000
    };

    public static async Task CreateAuthenticatorAsync(
        string serverId, string gameId, string gameVersion, string modInfo, string nexusToken, 
        int userId, string userToken, string authAddress, int authPort, 
        Action handleSuccess,
        Func<string, string, int, string, byte[], string, byte[]> buildEstablishing = null,
        Func<string, ChaChaOfSalsa, string, long, string, string, string, int, byte[], byte[]> buildJoinServerMessage = null)
    {
        // Set defaults if null
        buildEstablishing ??= DefaultBuildEstablishing;
        buildJoinServerMessage ??= DefaultBuildJoinServerMessage;

        using var client = new TcpClient();
        try
        {
            await client.ConnectAsync(IPAddress.Parse(authAddress), authPort);
            if (!client.Connected)
                throw new TimeoutException($"Connecting to server {authAddress}:{authPort} timed out");

            Log.Information("Connected to server {Address}:{Port}", authAddress, authPort);
            var stream = client.GetStream();

            // Phase 1: Handshake / Establishing
            using var details = await stream.ReadSteamWithInt16Async();
            var remoteKey = new byte[16];
            details.Position = 0;
            details.ReadExactly(remoteKey);
            // The next 256 bytes are skipped/ignored in the logic despite being read

            var establishMsg = buildEstablishing(nexusToken, gameVersion, userId, userToken, details.ToArray(), "netease");
            await stream.WriteAsync(establishMsg);

            // Phase 2: Check Status
            using var statusStream = await stream.ReadSteamWithInt16Async();
            var errorCode = (byte)statusStream.ReadByte();
            if (errorCode > 0)
                throw new Exception($"Establishing error: {errorCode:X2}");

            Log.Information("Establishing successfully");

            // Phase 3: Setup Encryption (ChaCha20)
            var userTokenXor = Encoding.ASCII.GetBytes(userToken).Xor(TokenKey);
            var encryptor = new ChaChaOfSalsa(userTokenXor.CombineWith(remoteKey), ChaChaNonce, true);
            var decryptor = new ChaChaOfSalsa(remoteKey.CombineWith(userTokenXor), ChaChaNonce, false);

            // Phase 4: Join Server
            var joinMsg = buildJoinServerMessage(nexusToken, encryptor, serverId, long.Parse(gameId), gameVersion, modInfo, "netease", userId, remoteKey);
            await stream.WriteAsync(joinMsg);

            // Phase 5: Final Authentication Result
            using var responseStream = await stream.ReadSteamWithInt16Async();
            var (msgType, payload) = decryptor.UnpackMessage(responseStream.ToArray());

            if (msgType != 9 || payload[0] > 0)
                throw new Exception($"Authentication of message failed: {payload[0]}");

            handleSuccess();
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            Log.Error("Access token is invalid or expired.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to create connection: {Message}", ex.Message);
            throw;
        }
    }

    private static byte[] DefaultBuildEstablishing(string nexusToken, string gameVersion, int userId, string userToken, byte[] context, string channel)
    {
        Log.Information("Building establishing message");
        var api = new WebNexusApi(nexusToken);
        var result = api.ComputeHandshakeBodyAsync(userId, userToken, Convert.ToBase64String(context), channel, gameVersion).GetAwaiter().GetResult();
        return Convert.FromBase64String(JsonSerializer.Deserialize<EntityHandshake>(result).HandshakeBody);
    }

    private static byte[] DefaultBuildJoinServerMessage(string nexusToken, ChaChaOfSalsa cipher, string serverId, long gameId, string gameVersion, string modInfo, string channel, int userId, byte[] handshakeKey)
    {
        Log.Information("Building join server message");
        var api = new WebNexusApi(nexusToken);
        var result = api.ComputeAuthenticationBodyAsync(serverId, gameId, gameVersion, modInfo, channel, userId, Convert.ToBase64String(handshakeKey)).GetAwaiter().GetResult();
        var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(result);
        return cipher.PackMessage(9, Convert.FromBase64String(dict["authBody"]));
    }
}