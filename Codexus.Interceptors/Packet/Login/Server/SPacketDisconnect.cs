using System;
using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Enums;
using Codexus.Development.SDK.Extensions;
using Codexus.Development.SDK.Packet;
using DotNetty.Buffers;
using Serilog;

namespace Codexus.Interceptors.Packet.Login.Server
{
	// Token: 0x02000003 RID: 3
	[RegisterPacket(EnumConnectionState.Login, EnumPacketDirection.ClientBound, 0, false)]
	public class SPacketDisconnect : IPacket
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000253A File Offset: 0x0000073A
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002542 File Offset: 0x00000742
		public string Reason { get; set; } = "";

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000254B File Offset: 0x0000074B
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002553 File Offset: 0x00000753
		public EnumProtocolVersion ClientProtocolVersion { get; set; }

		// Token: 0x0600001D RID: 29 RVA: 0x0000255C File Offset: 0x0000075C
		public void ReadFromBuffer(IByteBuffer buffer)
		{
			this.Reason = buffer.ReadStringFromBuffer(32767);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002571 File Offset: 0x00000771
		public void WriteToBuffer(IByteBuffer buffer)
		{
			buffer.WriteStringToBuffer(this.Reason, 32767);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002588 File Offset: 0x00000788
		public bool HandlePacket(GameConnection connection)
		{
			Log.Debug("Disconnect reason: {Reason}", new object[] { this.Reason });
			return false;
		}
	}
}
