using System;
using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Enums;
using Codexus.Development.SDK.Packet;
using DotNetty.Buffers;

namespace Codexus.Interceptors.Packet.Configuration.Server
{
	// Token: 0x0200000B RID: 11
	[RegisterPacket(EnumConnectionState.Configuration, EnumPacketDirection.ClientBound, 3, new EnumProtocolVersion[]
	{
		EnumProtocolVersion.V1206,
		EnumProtocolVersion.V1210
	}, false)]
	public class SFinishConfiguration : IPacket
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000033AB File Offset: 0x000015AB
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000033B3 File Offset: 0x000015B3
		public EnumProtocolVersion ClientProtocolVersion { get; set; }

		// Token: 0x06000072 RID: 114 RVA: 0x000033BC File Offset: 0x000015BC
		public void ReadFromBuffer(IByteBuffer buffer)
		{
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000033BF File Offset: 0x000015BF
		public void WriteToBuffer(IByteBuffer buffer)
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000033C4 File Offset: 0x000015C4
		public bool HandlePacket(GameConnection connection)
		{
			return false;
		}
	}
}
