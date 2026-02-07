using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Enums;
using DotNetty.Buffers;

namespace Codexus.Development.SDK.Packet;

// Token: 0x02000016 RID: 22
public interface IPacket
{
	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000075 RID: 117
	// (set) Token: 0x06000076 RID: 118
	EnumProtocolVersion ClientProtocolVersion { get; set; }

	// Token: 0x06000077 RID: 119
	void ReadFromBuffer(IByteBuffer buffer);

	// Token: 0x06000078 RID: 120
	void WriteToBuffer(IByteBuffer buffer);

	// Token: 0x06000079 RID: 121
	bool HandlePacket(GameConnection connection);
}