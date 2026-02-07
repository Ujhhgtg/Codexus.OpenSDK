using System;
using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Enums;
using Codexus.Development.SDK.Extensions;
using Codexus.Development.SDK.Packet;
using DotNetty.Buffers;
using Serilog;

namespace Codexus.Interceptors.Packet.Login.Client
{
	// Token: 0x02000009 RID: 9
	[RegisterPacket(EnumConnectionState.Login, EnumPacketDirection.ServerBound, 0, new EnumProtocolVersion[]
	{
		EnumProtocolVersion.V1076,
		EnumProtocolVersion.V108X,
		EnumProtocolVersion.V1200,
		EnumProtocolVersion.V1122,
		EnumProtocolVersion.V1180,
		EnumProtocolVersion.V1210,
		EnumProtocolVersion.V1206
	}, false)]
	public class CPacketLoginStart : IPacket
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002F27 File Offset: 0x00001127
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002F2F File Offset: 0x0000112F
		private string Profile { get; set; } = "";

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002F38 File Offset: 0x00001138
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002F40 File Offset: 0x00001140
		private bool HasPlayerUuid { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002F49 File Offset: 0x00001149
		private byte[] Uuid { get; } = new byte[16];

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002F51 File Offset: 0x00001151
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002F59 File Offset: 0x00001159
		public EnumProtocolVersion ClientProtocolVersion { get; set; }

		// Token: 0x0600005E RID: 94 RVA: 0x00002F64 File Offset: 0x00001164
		public void ReadFromBuffer(IByteBuffer buffer)
		{
			EnumProtocolVersion clientProtocolVersion = this.ClientProtocolVersion;
			EnumProtocolVersion enumProtocolVersion = clientProtocolVersion;
			if (enumProtocolVersion <= EnumProtocolVersion.V1122)
			{
				if (enumProtocolVersion != EnumProtocolVersion.V1076 && enumProtocolVersion != EnumProtocolVersion.V108X && enumProtocolVersion != EnumProtocolVersion.V1122)
				{
					return;
				}
			}
			else if (enumProtocolVersion != EnumProtocolVersion.V1180)
			{
				if (enumProtocolVersion == EnumProtocolVersion.V1200)
				{
					this.Profile = buffer.ReadStringFromBuffer(16);
					this.HasPlayerUuid = buffer.ReadBoolean();
					bool hasPlayerUuid = this.HasPlayerUuid;
					if (hasPlayerUuid)
					{
						buffer.ReadBytes(this.Uuid);
					}
					return;
				}
				if (enumProtocolVersion - EnumProtocolVersion.V1206 > 1)
				{
					return;
				}
				this.Profile = buffer.ReadStringFromBuffer(16);
				buffer.ReadBytes(this.Uuid);
				return;
			}
			this.Profile = buffer.ReadStringFromBuffer(16);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000302C File Offset: 0x0000122C
		public void WriteToBuffer(IByteBuffer buffer)
		{
			EnumProtocolVersion clientProtocolVersion = this.ClientProtocolVersion;
			EnumProtocolVersion enumProtocolVersion = clientProtocolVersion;
			if (enumProtocolVersion <= EnumProtocolVersion.V1122)
			{
				if (enumProtocolVersion == EnumProtocolVersion.V1076 || enumProtocolVersion == EnumProtocolVersion.V108X || enumProtocolVersion == EnumProtocolVersion.V1122)
				{
					buffer.WriteStringToBuffer(this.Profile, 16);
				}
			}
			else if (enumProtocolVersion != EnumProtocolVersion.V1180)
			{
				if (enumProtocolVersion != EnumProtocolVersion.V1200)
				{
					if (enumProtocolVersion - EnumProtocolVersion.V1206 <= 1)
					{
						buffer.WriteStringToBuffer(this.Profile, 16);
						buffer.WriteBytes(this.Uuid);
					}
				}
				else
				{
					buffer.WriteStringToBuffer(this.Profile, 16);
					buffer.WriteBoolean(this.HasPlayerUuid);
					bool hasPlayerUuid = this.HasPlayerUuid;
					if (hasPlayerUuid)
					{
						buffer.WriteBytes(this.Uuid);
					}
				}
			}
			else
			{
				buffer.WriteStringToBuffer(this.Profile, 32767);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003108 File Offset: 0x00001308
		public bool HandlePacket(GameConnection connection)
		{
			this.Profile = connection.NickName;
			Log.Debug("{NickName} trying to login...", new object[] { this.Profile });
			return false;
		}
	}
}
