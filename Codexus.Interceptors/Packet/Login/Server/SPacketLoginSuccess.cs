using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Entities;
using Codexus.Development.SDK.Enums;
using Codexus.Development.SDK.Event;
using Codexus.Development.SDK.Extensions;
using Codexus.Development.SDK.Manager;
using Codexus.Development.SDK.Packet;
using Codexus.Development.SDK.Utils;
using DotNetty.Buffers;
using Serilog;

namespace Codexus.Interceptors.Packet.Login.Server
{
	// Token: 0x02000006 RID: 6
	[RegisterPacket(EnumConnectionState.Login, EnumPacketDirection.ClientBound, 2, new EnumProtocolVersion[]
	{
		EnumProtocolVersion.V1076,
		EnumProtocolVersion.V108X,
		EnumProtocolVersion.V1200,
		EnumProtocolVersion.V1122,
		EnumProtocolVersion.V1180,
		EnumProtocolVersion.V1210,
		EnumProtocolVersion.V1206
	}, false)]
	public class SPacketLoginSuccess : IPacket
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002A16 File Offset: 0x00000C16
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002A1E File Offset: 0x00000C1E
		private string Uuid { get; set; } = "";

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002A27 File Offset: 0x00000C27
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002A2F File Offset: 0x00000C2F
		private byte[] Guid { get; set; } = new byte[16];

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A38 File Offset: 0x00000C38
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002A40 File Offset: 0x00000C40
		private string Username { get; set; } = "";

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002A49 File Offset: 0x00000C49
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002A51 File Offset: 0x00000C51
		[Nullable(new byte[] { 2, 0 })]
		private List<Property> Properties
		{
			[return: Nullable(new byte[] { 2, 0 })]
			get;
			[param: Nullable(new byte[] { 2, 0 })]
			set;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002A5A File Offset: 0x00000C5A
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002A62 File Offset: 0x00000C62
		private bool StrictErrorHandling { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002A6B File Offset: 0x00000C6B
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002A73 File Offset: 0x00000C73
		public EnumProtocolVersion ClientProtocolVersion { get; set; }

		// Token: 0x06000043 RID: 67 RVA: 0x00002A7C File Offset: 0x00000C7C
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
					buffer.ReadBytes(this.Guid);
					this.Username = buffer.ReadStringFromBuffer(16);
					this.Properties = buffer.ReadProperties();
					return;
				}
				if (enumProtocolVersion - EnumProtocolVersion.V1206 > 1)
				{
					return;
				}
				buffer.ReadBytes(this.Guid);
				this.Username = buffer.ReadStringFromBuffer(16);
				this.Properties = buffer.ReadProperties();
				this.StrictErrorHandling = buffer.ReadBoolean();
				return;
			}
			this.Uuid = buffer.ReadStringFromBuffer(36);
			this.Username = buffer.ReadStringFromBuffer(16);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B64 File Offset: 0x00000D64
		public void WriteToBuffer(IByteBuffer buffer)
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
					buffer.WriteBytes(this.Guid);
					buffer.WriteStringToBuffer(this.Username, 16);
					buffer.WriteProperties(this.Properties);
					return;
				}
				if (enumProtocolVersion - EnumProtocolVersion.V1206 > 1)
				{
					return;
				}
				buffer.WriteBytes(this.Guid);
				buffer.WriteStringToBuffer(this.Username, 16);
				buffer.WriteProperties(this.Properties);
				buffer.WriteBoolean(this.StrictErrorHandling);
				return;
			}
			buffer.WriteStringToBuffer(this.Uuid, 36);
			buffer.WriteStringToBuffer(this.Username, 16);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002C4C File Offset: 0x00000E4C
		public bool HandlePacket(GameConnection connection)
		{
			EnumProtocolVersion clientProtocolVersion = this.ClientProtocolVersion;
			EnumProtocolVersion enumProtocolVersion = clientProtocolVersion;
			if (enumProtocolVersion <= EnumProtocolVersion.V1122)
			{
				if (enumProtocolVersion != EnumProtocolVersion.V1076 && enumProtocolVersion != EnumProtocolVersion.V108X && enumProtocolVersion != EnumProtocolVersion.V1122)
				{
					goto IL_00A3;
				}
			}
			else if (enumProtocolVersion != EnumProtocolVersion.V1180)
			{
				if (enumProtocolVersion != EnumProtocolVersion.V1200 && enumProtocolVersion - EnumProtocolVersion.V1206 > 1)
				{
					goto IL_00A3;
				}
				Log.Information("{0}({1}) 加入了服务器", new object[]
				{
					this.Username,
					new Guid(this.Guid, true)
				});
				goto IL_00A3;
			}
			Log.Information("{0}({1}) 加入了服务器", new object[] { this.Username, this.Uuid });
			IL_00A3:
			connection.Uuid = this.Guid;
			bool flag = this.ClientProtocolVersion >= EnumProtocolVersion.V1206;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool isCancelled = EventManager.Instance.TriggerEvent<EventLoginSuccess>(MessageChannels.AllVersions, new EventLoginSuccess(connection)).IsCancelled;
				if (isCancelled)
				{
					flag2 = true;
				}
				else
				{
					connection.State = EnumConnectionState.Play;
					flag2 = false;
				}
			}
			return flag2;
		}
	}
}
