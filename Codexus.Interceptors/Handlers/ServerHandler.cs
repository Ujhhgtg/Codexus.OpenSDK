using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Entities;
using Codexus.Development.SDK.Utils;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;

namespace Codexus.Interceptors.Handlers
{
	// Token: 0x0200000F RID: 15
	public class ServerHandler : ChannelHandlerAdapter
	{
		// Token: 0x06000088 RID: 136 RVA: 0x000034AC File Offset: 0x000016AC
		public ServerHandler(Interceptor interceptor, EntitySocks5 socks5, string modInfo, string gameId, string forwardAddress, int forwardPort, string nickName, string userId, string userToken, [Nullable(new byte[] { 2, 0 })] Action<string> onJoinServer)
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003510 File Offset: 0x00001710
		public override void ChannelActive(IChannelHandlerContext context)
		{
			this.<interceptor>P.ActiveChannels.TryAdd(context.Channel.Id, context.Channel);
			IChannel channel = context.Channel;
			GameConnection gameConnection = new GameConnection(this.<socks5>P, this.<modInfo>P, this.<gameId>P, this.<forwardAddress>P, this.<forwardPort>P, this.<nickName>P, this.<userId>P, this.<userToken>P, channel, this.<onJoinServer>P);
			gameConnection.InterceptorId = this.<interceptor>P.Identifier;
			channel.GetAttribute<GameConnection>(ChannelAttribute.Connection).Set(gameConnection);
			gameConnection.Prepare();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000035AF File Offset: 0x000017AF
		public override void ChannelRead(IChannelHandlerContext context, object message)
		{
			context.Channel.GetAttribute<GameConnection>(ChannelAttribute.Connection).Get().OnClientReceived((IByteBuffer)message);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000035D4 File Offset: 0x000017D4
		public override void ChannelInactive(IChannelHandlerContext context)
		{
			IChannel channel;
			this.<interceptor>P.ActiveChannels.TryRemove(context.Channel.Id, out channel);
			context.Channel.GetAttribute<GameConnection>(ChannelAttribute.Connection).Get().Shutdown();
		}

		// Token: 0x0400002E RID: 46
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Interceptor <interceptor>P = interceptor;

		// Token: 0x0400002F RID: 47
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EntitySocks5 <socks5>P = socks5;

		// Token: 0x04000030 RID: 48
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <modInfo>P = modInfo;

		// Token: 0x04000031 RID: 49
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <gameId>P = gameId;

		// Token: 0x04000032 RID: 50
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <forwardAddress>P = forwardAddress;

		// Token: 0x04000033 RID: 51
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <forwardPort>P = forwardPort;

		// Token: 0x04000034 RID: 52
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <nickName>P = nickName;

		// Token: 0x04000035 RID: 53
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <userId>P = userId;

		// Token: 0x04000036 RID: 54
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <userToken>P = userToken;

		// Token: 0x04000037 RID: 55
		[Nullable(new byte[] { 2, 0 })]
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<string> <onJoinServer>P = onJoinServer;
	}
}
