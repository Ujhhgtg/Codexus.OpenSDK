using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Extensions;
using Codexus.Development.SDK.Manager;
using Codexus.Development.SDK.Packet;
using Codexus.Development.SDK.Utils;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace Codexus.Development.SDK.Analysis;

// Token: 0x02000035 RID: 53
public class MessageSerializer : MessageToByteEncoder<IPacket>
{
	// Token: 0x0600012E RID: 302 RVA: 0x0000661C File Offset: 0x0000481C
	protected override void Encode(IChannelHandlerContext context, IPacket message, IByteBuffer output)
	{
		var gameConnection = context.Channel.GetAttribute<GameConnection>(ChannelAttribute.Connection).Get();
		var packetId = PacketManager.Instance.GetPacketId(gameConnection.ProtocolVersion, message);
		var flag = packetId != -1;
		if (flag)
		{
			output.WriteVarInt(packetId);
			message.WriteToBuffer(output);
		}
	}
}