using System;
using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Enums;
using Codexus.Development.SDK.Event;
using Codexus.Development.SDK.Manager;
using Codexus.Development.SDK.Packet;
using Codexus.Development.SDK.Utils;
using DotNetty.Buffers;
using Serilog;

namespace Codexus.Interceptors.Packet.Login.Client
{
	// Token: 0x02000008 RID: 8
	[RegisterPacket(EnumConnectionState.Login, EnumPacketDirection.ServerBound, 3, new EnumProtocolVersion[]
	{
		EnumProtocolVersion.V1210,
		EnumProtocolVersion.V1206
	}, false)]
	public class CPacketLoginAcknowledged : IPacket
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002EB6 File Offset: 0x000010B6
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002EBE File Offset: 0x000010BE
		public EnumProtocolVersion ClientProtocolVersion { get; set; }

		// Token: 0x06000053 RID: 83 RVA: 0x00002EC7 File Offset: 0x000010C7
		public void ReadFromBuffer(IByteBuffer buffer)
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002ECA File Offset: 0x000010CA
		public void WriteToBuffer(IByteBuffer buffer)
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002ED0 File Offset: 0x000010D0
		public bool HandlePacket(GameConnection connection)
		{
			bool isCancelled = EventManager.Instance.TriggerEvent<EventLoginSuccess>(MessageChannels.AllVersions, new EventLoginSuccess(connection)).IsCancelled;
			bool flag;
			if (isCancelled)
			{
				flag = true;
			}
			else
			{
				connection.State = EnumConnectionState.Configuration;
				Log.Debug("Configuration started.", Array.Empty<object>());
				flag = false;
			}
			return flag;
		}
	}
}
