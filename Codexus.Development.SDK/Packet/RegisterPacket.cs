using System;
using Codexus.Development.SDK.Enums;

namespace Codexus.Development.SDK.Packet;

// Token: 0x02000017 RID: 23
[AttributeUsage(AttributeTargets.Class)]
public class RegisterPacket(EnumConnectionState state, EnumPacketDirection direction, int[] packetIds, EnumProtocolVersion[] versions, bool skip = false) : Attribute
{
	// Token: 0x17000022 RID: 34
	// (get) Token: 0x0600007B RID: 123 RVA: 0x00003CDE File Offset: 0x00001EDE
	public EnumConnectionState State { get; } = state;

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x0600007C RID: 124 RVA: 0x00003CE6 File Offset: 0x00001EE6
	public EnumPacketDirection Direction { get; } = direction;

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x0600007D RID: 125 RVA: 0x00003CEE File Offset: 0x00001EEE
	public int[] PacketIds { get; } = packetIds;

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x0600007E RID: 126 RVA: 0x00003CF6 File Offset: 0x00001EF6
	public EnumProtocolVersion[] Versions { get; } = versions;

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x0600007F RID: 127 RVA: 0x00003CFE File Offset: 0x00001EFE
	public bool Skip { get; } = skip;

	// Token: 0x06000080 RID: 128 RVA: 0x00003D06 File Offset: 0x00001F06
	public RegisterPacket(EnumConnectionState state, EnumPacketDirection direction, int packetId, bool skip = false)
		: this(state, direction, [packetId], Enum.GetValues<EnumProtocolVersion>(), skip)
	{
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00003D23 File Offset: 0x00001F23
	public RegisterPacket(EnumConnectionState state, EnumPacketDirection direction, int[] packetIds, bool skip = false)
		: this(state, direction, packetIds, Enum.GetValues<EnumProtocolVersion>(), skip)
	{
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00003D37 File Offset: 0x00001F37
	public RegisterPacket(EnumConnectionState state, EnumPacketDirection direction, int packetId, EnumProtocolVersion version, bool skip = false)
		: this(state, direction, [packetId], [version], skip)
	{
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00003D5A File Offset: 0x00001F5A
	public RegisterPacket(EnumConnectionState state, EnumPacketDirection direction, int[] packetIds, EnumProtocolVersion version, bool skip = false)
		: this(state, direction, packetIds, [version], skip)
	{
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00003D74 File Offset: 0x00001F74
	public RegisterPacket(EnumConnectionState state, EnumPacketDirection direction, int packetId, EnumProtocolVersion[] versions, bool skip = false)
		: this(state, direction, [packetId], versions, skip)
	{
	}
}