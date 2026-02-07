using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Utils;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;

namespace Codexus.Development.SDK.Handlers;

// Token: 0x0200001F RID: 31
public class ClientHandler(GameConnection connection) : ChannelHandlerAdapter
{
	// Token: 0x060000B7 RID: 183 RVA: 0x00005537 File Offset: 0x00003737
	public override void ChannelActive(IChannelHandlerContext context)
	{
		context.Channel.GetAttribute(ChannelAttribute.Connection).Set(connection);
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00005556 File Offset: 0x00003756
	public override void ChannelRead(IChannelHandlerContext context, object message)
	{
		connection.OnServerReceived((IByteBuffer)message);
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x0000556B File Offset: 0x0000376B
	public override void ChannelInactive(IChannelHandlerContext context)
	{
		context.Channel.GetAttribute(ChannelAttribute.Connection).Remove();
	}
}