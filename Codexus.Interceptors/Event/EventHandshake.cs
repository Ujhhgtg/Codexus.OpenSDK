using System;
using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Manager;
using Codexus.Interceptors.Packet.Handshake.Client;

namespace Codexus.Interceptors.Event
{
	// Token: 0x02000010 RID: 16
	public class EventHandshake : EventArgsBase
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000361B File Offset: 0x0000181B
		public CHandshake Handshake { get; }

		// Token: 0x0600008D RID: 141 RVA: 0x00003623 File Offset: 0x00001823
		public EventHandshake(GameConnection connection, CHandshake handshake)
			: base(connection)
		{
			this.Handshake = handshake;
		}
	}
}
