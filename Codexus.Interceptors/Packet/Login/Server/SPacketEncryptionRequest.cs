using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Enums;
using Codexus.Development.SDK.Event;
using Codexus.Development.SDK.Extensions;
using Codexus.Development.SDK.Manager;
using Codexus.Development.SDK.Packet;
using Codexus.Interceptors.Packet.Login.Client;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Security;
using Serilog;

namespace Codexus.Interceptors.Packet.Login.Server;
[RegisterPacket(EnumConnectionState.Login, EnumPacketDirection.ClientBound, 1, [
	EnumProtocolVersion.V1076,
	EnumProtocolVersion.V108X,
	EnumProtocolVersion.V1200,
	EnumProtocolVersion.V1122,
	EnumProtocolVersion.V1180,
	EnumProtocolVersion.V1210,
	EnumProtocolVersion.V1206
])]
public class SPacketEncryptionRequest : IPacket
{
	private string ServerId { get; set; } = "";
	private byte[] PublicKey { get; set; } = [];
	private byte[] VerifyToken { get; set; } = [];
	private bool ShouldAuthenticate { get; set; }
	public EnumProtocolVersion ClientProtocolVersion { get; set; }
	public void ReadFromBuffer(IByteBuffer buffer)
	{
		var flag = ClientProtocolVersion == EnumProtocolVersion.V1076;
		if (flag)
		{
			ServerId = buffer.ReadStringFromBuffer(20);
			PublicKey = buffer.ReadByteArrayFromBuffer(buffer.ReadShort());
			VerifyToken = buffer.ReadByteArrayFromBuffer(buffer.ReadShort());
		}
		else
		{
			ServerId = buffer.ReadStringFromBuffer(20);
			PublicKey = buffer.ReadByteArrayFromBuffer();
			VerifyToken = buffer.ReadByteArrayFromBuffer();
			var clientProtocolVersion = ClientProtocolVersion;
			var flag2 = clientProtocolVersion - EnumProtocolVersion.V1206 <= 1;
			if (flag2)
			{
				ShouldAuthenticate = buffer.ReadBoolean();
			}
		}
	}
	public void WriteToBuffer(IByteBuffer buffer)
	{
		var flag = ClientProtocolVersion == EnumProtocolVersion.V1076;
		if (flag)
		{
			buffer.WriteStringToBuffer(ServerId).WriteShort(PublicKey.Length).WriteBytes(PublicKey)
				.WriteShort(VerifyToken.Length)
				.WriteBytes(VerifyToken);
		}
		else
		{
			buffer.WriteStringToBuffer(ServerId).WriteByteArrayToBuffer(PublicKey).WriteByteArrayToBuffer(VerifyToken);
			var flag2 = ClientProtocolVersion == EnumProtocolVersion.V1210;
			if (flag2)
			{
				buffer.WriteBoolean(ShouldAuthenticate);
			}
		}
	}
	public bool HandlePacket(GameConnection connection)
	{
		var cipherKeyGenerator = new CipherKeyGenerator();
		cipherKeyGenerator.Init(new KeyGenerationParameters(new SecureRandom(), 128));
		var instance = SubjectPublicKeyInfo.GetInstance(PublicKey);
		var secretKey = cipherKeyGenerator.GenerateKey();
		bool flag3;
		using (var memoryStream = new MemoryStream(20))
		{
			memoryStream.Write(Encoding.GetEncoding("ISO-8859-1").GetBytes(ServerId));
			memoryStream.Write(secretKey);
			memoryStream.Write(PublicKey);
			memoryStream.Position = 0L;
			var text = memoryStream.ToSha1();
			var flag = !EventManager.Instance.TriggerEvent<EventEncryptionRequest>("channel_interceptor", new EventEncryptionRequest(connection, text)).IsCancelled;
			if (flag)
			{
				var onJoinServer = connection.OnJoinServer;
				if (onJoinServer != null)
				{
					onJoinServer(text);
				}
			}
			var pkcs1Encoding = new Pkcs1Encoding(new RsaEngine());
			pkcs1Encoding.Init(true, PublicKeyFactory.CreateKey(instance));
			var cpacketEncryptionResponse = new CPacketEncryptionResponse
			{
				ClientProtocolVersion = ClientProtocolVersion,
				SecretKeyEncrypted = pkcs1Encoding.ProcessBlock(secretKey, 0, secretKey.Length),
				VerifyTokenEncrypted = pkcs1Encoding.ProcessBlock(VerifyToken, 0, VerifyToken.Length)
			};
			var flag2 = connection.ServerChannel == null;
			if (flag2)
			{
				Log.Error("Server channel is null", []);
				flag3 = false;
			}
			else
			{
				connection.ServerChannel.Configuration.AutoRead = false;
				connection.ServerChannel.Configuration.SetOption(ChannelOption.AutoRead, false);
				connection.ServerChannel.WriteAndFlushAsync(cpacketEncryptionResponse).ContinueWith(delegate(Task channel)
				{
					var flag4 = !channel.IsCompletedSuccessfully;
					if (!flag4)
					{
						try
						{
							Log.Debug("Successfully sent encryption response to client", []);
							GameConnection.EnableEncryption(connection.ServerChannel, secretKey);
							connection.ServerChannel.Configuration.AutoRead = true;
							connection.ServerChannel.Configuration.SetOption(ChannelOption.AutoRead, true);
						}
						catch (Exception ex)
						{
							Log.Error(ex, "Failed to enable encryption", []);
						}
					}
				});
				flag3 = true;
			}
		}
		return flag3;
	}
}