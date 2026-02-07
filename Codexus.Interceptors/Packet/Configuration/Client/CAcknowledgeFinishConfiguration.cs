using System;
using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Enums;
using Codexus.Development.SDK.Packet;
using DotNetty.Buffers;
using Serilog;

namespace Codexus.Interceptors.Packet.Configuration.Client
{
	// Token: 0x0200000E RID: 14
	[RegisterPacket(EnumConnectionState.Configuration, EnumPacketDirection.ServerBound, 3, new EnumProtocolVersion[]
	{
		EnumProtocolVersion.V1206,
		EnumProtocolVersion.V1210
	}, false)]
	public class CAcknowledgeFinishConfiguration : IPacket
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003460 File Offset: 0x00001660
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003468 File Offset: 0x00001668
		public EnumProtocolVersion ClientProtocolVersion { get; set; }

		// Token: 0x06000084 RID: 132 RVA: 0x00003471 File Offset: 0x00001671
		public void ReadFromBuffer(IByteBuffer buffer)
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003474 File Offset: 0x00001674
		public void WriteToBuffer(IByteBuffer buffer)
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003478 File Offset: 0x00001678
		public bool HandlePacket(GameConnection connection)
		{
			connection.State = EnumConnectionState.Play;
			Log.Debug("Finished configuration.", Array.Empty<object>());
			return false;
		}
	}
}
