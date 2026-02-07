using System;
using System.Text;
using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Enums;
using Codexus.Development.SDK.Extensions;
using Codexus.Development.SDK.Manager;
using Codexus.Development.SDK.Packet;
using Codexus.Development.SDK.Utils;
using Codexus.Interceptors.Event;
using DotNetty.Buffers;
using Serilog;

namespace Codexus.Interceptors.Packet.Handshake.Client
{
	// Token: 0x0200000A RID: 10
	[RegisterPacket(EnumConnectionState.Handshaking, EnumPacketDirection.ServerBound, 0, false)]
	public class CHandshake : IPacket
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003163 File Offset: 0x00001363
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000316B File Offset: 0x0000136B
		public int ProtocolVersion { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003174 File Offset: 0x00001374
		// (set) Token: 0x06000065 RID: 101 RVA: 0x0000317C File Offset: 0x0000137C
		public string ServerAddress { get; set; } = "";

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003185 File Offset: 0x00001385
		// (set) Token: 0x06000067 RID: 103 RVA: 0x0000318D File Offset: 0x0000138D
		public ushort ServerPort { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003196 File Offset: 0x00001396
		// (set) Token: 0x06000069 RID: 105 RVA: 0x0000319E File Offset: 0x0000139E
		public EnumConnectionState NextState { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000031A7 File Offset: 0x000013A7
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000031AF File Offset: 0x000013AF
		public EnumProtocolVersion ClientProtocolVersion { get; set; }

		// Token: 0x0600006C RID: 108 RVA: 0x000031B8 File Offset: 0x000013B8
		public void ReadFromBuffer(IByteBuffer buffer)
		{
			this.ProtocolVersion = buffer.ReadVarIntFromBuffer();
			this.ServerAddress = buffer.ReadStringFromBuffer(255);
			this.ServerPort = buffer.ReadUnsignedShort();
			this.NextState = (EnumConnectionState)buffer.ReadVarIntFromBuffer();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000031F4 File Offset: 0x000013F4
		public void WriteToBuffer(IByteBuffer buffer)
		{
			buffer.WriteVarInt(this.ProtocolVersion);
			buffer.WriteStringToBuffer(this.ServerAddress, 32767);
			buffer.WriteShort((int)this.ServerPort);
			buffer.WriteVarInt((int)this.NextState);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003230 File Offset: 0x00001430
		public bool HandlePacket(GameConnection connection)
		{
			bool isCancelled = EventManager.Instance.TriggerEvent<EventHandshake>(MessageChannels.AllVersions, new EventHandshake(connection, this)).IsCancelled;
			bool flag;
			if (isCancelled)
			{
				flag = true;
			}
			else
			{
				connection.ProtocolVersion = (EnumProtocolVersion)this.ProtocolVersion;
				connection.State = this.NextState;
				Log.Debug("Original address: {ServerAddress}", new object[] { Convert.ToBase64String(Encoding.UTF8.GetBytes(this.ServerAddress)) });
				this.ServerPort = (ushort)connection.ForwardPort;
				EnumProtocolVersion protocolVersion = connection.ProtocolVersion;
				string text = ((protocolVersion > EnumProtocolVersion.V1180) ? ((protocolVersion <= EnumProtocolVersion.V1206) ? (connection.ForwardAddress + "\0FML3\0") : (connection.ForwardAddress + "\0FORGE")) : ((protocolVersion <= EnumProtocolVersion.V1122) ? (connection.ForwardAddress + "\0FML\0") : (connection.ForwardAddress + "\0FML2\0")));
				this.ServerAddress = text;
				Log.Debug("New address: {ServerAddress}", new object[] { Convert.ToBase64String(Encoding.UTF8.GetBytes(this.ServerAddress)) });
				Log.Debug("Protocol version: {ProtocolVersion}, Next state: {State}, Address: {Address}", new object[]
				{
					connection.ProtocolVersion,
					connection.State,
					this.ServerAddress.Replace("\0", "|")
				});
				flag = false;
			}
			return flag;
		}
	}
}
