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

namespace Codexus.Interceptors.Packet.Login.Server
{
	// Token: 0x02000005 RID: 5
	[RegisterPacket(EnumConnectionState.Login, EnumPacketDirection.ClientBound, 1, new EnumProtocolVersion[]
	{
		EnumProtocolVersion.V1076,
		EnumProtocolVersion.V108X,
		EnumProtocolVersion.V1200,
		EnumProtocolVersion.V1122,
		EnumProtocolVersion.V1180,
		EnumProtocolVersion.V1210,
		EnumProtocolVersion.V1206
	}, false)]
	public class SPacketEncryptionRequest : IPacket
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000263A File Offset: 0x0000083A
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002642 File Offset: 0x00000842
		private string ServerId { get; set; } = "";

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000264B File Offset: 0x0000084B
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002653 File Offset: 0x00000853
		private byte[] PublicKey { get; set; } = Array.Empty<byte>();

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000265C File Offset: 0x0000085C
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002664 File Offset: 0x00000864
		private byte[] VerifyToken { get; set; } = Array.Empty<byte>();

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000266D File Offset: 0x0000086D
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002675 File Offset: 0x00000875
		private bool ShouldAuthenticate { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000267E File Offset: 0x0000087E
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002686 File Offset: 0x00000886
		public EnumProtocolVersion ClientProtocolVersion { get; set; }

		// Token: 0x06000033 RID: 51 RVA: 0x00002690 File Offset: 0x00000890
		public void ReadFromBuffer(IByteBuffer buffer)
		{
			bool flag = this.ClientProtocolVersion == EnumProtocolVersion.V1076;
			if (flag)
			{
				this.ServerId = buffer.ReadStringFromBuffer(20);
				this.PublicKey = buffer.ReadByteArrayFromBuffer((int)buffer.ReadShort());
				this.VerifyToken = buffer.ReadByteArrayFromBuffer((int)buffer.ReadShort());
			}
			else
			{
				this.ServerId = buffer.ReadStringFromBuffer(20);
				this.PublicKey = buffer.ReadByteArrayFromBuffer();
				this.VerifyToken = buffer.ReadByteArrayFromBuffer();
				EnumProtocolVersion clientProtocolVersion = this.ClientProtocolVersion;
				bool flag2 = clientProtocolVersion - EnumProtocolVersion.V1206 <= 1;
				if (flag2)
				{
					this.ShouldAuthenticate = buffer.ReadBoolean();
				}
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002734 File Offset: 0x00000934
		public void WriteToBuffer(IByteBuffer buffer)
		{
			bool flag = this.ClientProtocolVersion == EnumProtocolVersion.V1076;
			if (flag)
			{
				buffer.WriteStringToBuffer(this.ServerId, 32767).WriteShort(this.PublicKey.Length).WriteBytes(this.PublicKey)
					.WriteShort(this.VerifyToken.Length)
					.WriteBytes(this.VerifyToken);
			}
			else
			{
				buffer.WriteStringToBuffer(this.ServerId, 32767).WriteByteArrayToBuffer(this.PublicKey).WriteByteArrayToBuffer(this.VerifyToken);
				bool flag2 = this.ClientProtocolVersion == EnumProtocolVersion.V1210;
				if (flag2)
				{
					buffer.WriteBoolean(this.ShouldAuthenticate);
				}
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027DC File Offset: 0x000009DC
		public bool HandlePacket(GameConnection connection)
		{
			CipherKeyGenerator cipherKeyGenerator = new CipherKeyGenerator();
			cipherKeyGenerator.Init(new KeyGenerationParameters(new SecureRandom(), 128));
			SubjectPublicKeyInfo instance = SubjectPublicKeyInfo.GetInstance(this.PublicKey);
			byte[] secretKey = cipherKeyGenerator.GenerateKey();
			bool flag3;
			using (MemoryStream memoryStream = new MemoryStream(20))
			{
				memoryStream.Write(Encoding.GetEncoding("ISO-8859-1").GetBytes(this.ServerId));
				memoryStream.Write(secretKey);
				memoryStream.Write(this.PublicKey);
				memoryStream.Position = 0L;
				string text = memoryStream.ToSha1();
				bool flag = !EventManager.Instance.TriggerEvent<EventEncryptionRequest>("channel_interceptor", new EventEncryptionRequest(connection, text)).IsCancelled;
				if (flag)
				{
					Action<string> onJoinServer = connection.OnJoinServer;
					if (onJoinServer != null)
					{
						onJoinServer(text);
					}
				}
				Pkcs1Encoding pkcs1Encoding = new Pkcs1Encoding(new RsaEngine());
				pkcs1Encoding.Init(true, (ICipherParameters)PublicKeyFactory.CreateKey(instance));
				CPacketEncryptionResponse cpacketEncryptionResponse = new CPacketEncryptionResponse
				{
					ClientProtocolVersion = this.ClientProtocolVersion,
					SecretKeyEncrypted = pkcs1Encoding.ProcessBlock(secretKey, 0, secretKey.Length),
					VerifyTokenEncrypted = pkcs1Encoding.ProcessBlock(this.VerifyToken, 0, this.VerifyToken.Length)
				};
				bool flag2 = connection.ServerChannel == null;
				if (flag2)
				{
					Log.Error("Server channel is null", Array.Empty<object>());
					flag3 = false;
				}
				else
				{
					connection.ServerChannel.Configuration.AutoRead = false;
					connection.ServerChannel.Configuration.SetOption<bool>(ChannelOption.AutoRead, false);
					connection.ServerChannel.WriteAndFlushAsync(cpacketEncryptionResponse).ContinueWith(delegate(Task channel)
					{
						bool flag4 = !channel.IsCompletedSuccessfully;
						if (!flag4)
						{
							try
							{
								Log.Debug("Successfully sent encryption response to client", Array.Empty<object>());
								GameConnection.EnableEncryption(connection.ServerChannel, secretKey);
								connection.ServerChannel.Configuration.AutoRead = true;
								connection.ServerChannel.Configuration.SetOption<bool>(ChannelOption.AutoRead, true);
							}
							catch (Exception ex)
							{
								Log.Error(ex, "Failed to enable encryption", Array.Empty<object>());
							}
						}
					});
					flag3 = true;
				}
			}
			return flag3;
		}
	}
}
