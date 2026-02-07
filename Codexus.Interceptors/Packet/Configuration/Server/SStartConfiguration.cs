using System;
using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Enums;
using Codexus.Development.SDK.Packet;
using DotNetty.Buffers;
using Serilog;

namespace Codexus.Interceptors.Packet.Configuration.Server
{
	// Token: 0x0200000C RID: 12
	[RegisterPacket(EnumConnectionState.Play, EnumPacketDirection.ClientBound, 105, new EnumProtocolVersion[]
	{
		EnumProtocolVersion.V1206,
		EnumProtocolVersion.V1210
	}, false)]
	public class SStartConfiguration : IPacket
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000033E0 File Offset: 0x000015E0
		// (set) Token: 0x06000077 RID: 119 RVA: 0x000033E8 File Offset: 0x000015E8
		public EnumProtocolVersion ClientProtocolVersion { get; set; }

		// Token: 0x06000078 RID: 120 RVA: 0x000033F1 File Offset: 0x000015F1
		public void ReadFromBuffer(IByteBuffer buffer)
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000033F4 File Offset: 0x000015F4
		public void WriteToBuffer(IByteBuffer buffer)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000033F8 File Offset: 0x000015F8
		public bool HandlePacket(GameConnection connection)
		{
			connection.State = EnumConnectionState.Configuration;
			Log.Debug("Starting configuration.", Array.Empty<object>());
			return false;
		}
	}
}
