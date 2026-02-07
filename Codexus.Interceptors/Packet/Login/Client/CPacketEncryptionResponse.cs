using System;
using System.Runtime.CompilerServices;
using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Enums;
using Codexus.Development.SDK.Extensions;
using Codexus.Development.SDK.Packet;
using DotNetty.Buffers;

namespace Codexus.Interceptors.Packet.Login.Client
{
	// Token: 0x02000007 RID: 7
	[RequiredMember]
	[RegisterPacket(EnumConnectionState.Login, EnumPacketDirection.ServerBound, 1, new EnumProtocolVersion[]
	{
		EnumProtocolVersion.V1076,
		EnumProtocolVersion.V108X,
		EnumProtocolVersion.V1200,
		EnumProtocolVersion.V1122,
		EnumProtocolVersion.V1180,
		EnumProtocolVersion.V1210,
		EnumProtocolVersion.V1206
	}, false)]
	public class CPacketEncryptionResponse : IPacket
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002D80 File Offset: 0x00000F80
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002D88 File Offset: 0x00000F88
		[RequiredMember]
		public byte[] SecretKeyEncrypted { get; set; } = Array.Empty<byte>();

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002D91 File Offset: 0x00000F91
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002D99 File Offset: 0x00000F99
		[RequiredMember]
		public byte[] VerifyTokenEncrypted { get; set; } = Array.Empty<byte>();

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002DA2 File Offset: 0x00000FA2
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002DAA File Offset: 0x00000FAA
		public EnumProtocolVersion ClientProtocolVersion { get; set; }

		// Token: 0x0600004D RID: 77 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public void ReadFromBuffer(IByteBuffer buffer)
		{
			bool flag = this.ClientProtocolVersion == EnumProtocolVersion.V1076;
			if (flag)
			{
				this.SecretKeyEncrypted = buffer.ReadByteArrayFromBuffer((int)buffer.ReadShort());
				this.VerifyTokenEncrypted = buffer.ReadByteArrayFromBuffer((int)buffer.ReadShort());
			}
			else
			{
				this.SecretKeyEncrypted = buffer.ReadByteArrayFromBuffer();
				this.VerifyTokenEncrypted = buffer.ReadByteArrayFromBuffer();
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E18 File Offset: 0x00001018
		public void WriteToBuffer(IByteBuffer buffer)
		{
			bool flag = this.ClientProtocolVersion == EnumProtocolVersion.V1076;
			if (flag)
			{
				buffer.WriteShort(this.SecretKeyEncrypted.Length).WriteBytes(this.SecretKeyEncrypted).WriteShort(this.VerifyTokenEncrypted.Length)
					.WriteBytes(this.VerifyTokenEncrypted);
			}
			else
			{
				buffer.WriteByteArrayToBuffer(this.SecretKeyEncrypted).WriteByteArrayToBuffer(this.VerifyTokenEncrypted);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E84 File Offset: 0x00001084
		public bool HandlePacket(GameConnection connection)
		{
			return false;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E97 File Offset: 0x00001097
		[Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
		[CompilerFeatureRequired("RequiredMembers")]
		public CPacketEncryptionResponse()
		{
		}
	}
}
