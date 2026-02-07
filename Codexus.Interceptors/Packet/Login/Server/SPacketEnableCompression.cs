using System;
using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Enums;
using Codexus.Development.SDK.Extensions;
using Codexus.Development.SDK.Packet;
using DotNetty.Buffers;

namespace Codexus.Interceptors.Packet.Login.Server
{
	// Token: 0x02000004 RID: 4
	[RegisterPacket(EnumConnectionState.Login, EnumPacketDirection.ClientBound, 3, new EnumProtocolVersion[]
	{
		EnumProtocolVersion.V108X,
		EnumProtocolVersion.V1200,
		EnumProtocolVersion.V1122,
		EnumProtocolVersion.V1180,
		EnumProtocolVersion.V1210,
		EnumProtocolVersion.V1206
	}, false)]
	public class SPacketEnableCompression : IPacket
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000025C9 File Offset: 0x000007C9
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000025D1 File Offset: 0x000007D1
		private int CompressionThreshold { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000025DA File Offset: 0x000007DA
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000025E2 File Offset: 0x000007E2
		public EnumProtocolVersion ClientProtocolVersion { get; set; }

		// Token: 0x06000025 RID: 37 RVA: 0x000025EB File Offset: 0x000007EB
		public void ReadFromBuffer(IByteBuffer buffer)
		{
			this.CompressionThreshold = buffer.ReadVarIntFromBuffer();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025FB File Offset: 0x000007FB
		public void WriteToBuffer(IByteBuffer buffer)
		{
			buffer.WriteVarInt(this.CompressionThreshold);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000260C File Offset: 0x0000080C
		public bool HandlePacket(GameConnection connection)
		{
			GameConnection.EnableCompression(connection.ServerChannel, this.CompressionThreshold);
			return true;
		}
	}
}
