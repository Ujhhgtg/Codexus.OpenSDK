using System;
using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Enums;
using Codexus.Development.SDK.Packet;
using DotNetty.Buffers;

namespace Codexus.Interceptors.Packet.Configuration.Client
{
	// Token: 0x0200000D RID: 13
	[RegisterPacket(EnumConnectionState.Play, EnumPacketDirection.ServerBound, 12, new EnumProtocolVersion[]
	{
		EnumProtocolVersion.V1206,
		EnumProtocolVersion.V1210
	}, false)]
	public class CAcknowledgeConfiguration : IPacket
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000342C File Offset: 0x0000162C
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003434 File Offset: 0x00001634
		public EnumProtocolVersion ClientProtocolVersion { get; set; }

		// Token: 0x0600007E RID: 126 RVA: 0x0000343D File Offset: 0x0000163D
		public void ReadFromBuffer(IByteBuffer buffer)
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003440 File Offset: 0x00001640
		public void WriteToBuffer(IByteBuffer buffer)
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003444 File Offset: 0x00001644
		public bool HandlePacket(GameConnection connection)
		{
			return false;
		}
	}
}
