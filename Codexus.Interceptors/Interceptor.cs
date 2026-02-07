using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Codexus.Development.SDK.Analysis;
using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Entities;
using Codexus.Development.SDK.Event;
using Codexus.Development.SDK.Manager;
using Codexus.Development.SDK.Utils;
using Codexus.Interceptors.Handlers;
using DotNetty.Buffers;
using DotNetty.Common.Concurrency;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Serilog;

namespace Codexus.Interceptors
{
	// Token: 0x02000002 RID: 2
	public class Interceptor
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public Guid Identifier { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		public string LocalAddress { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002071 File Offset: 0x00000271
		public int LocalPort { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000207A File Offset: 0x0000027A
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002082 File Offset: 0x00000282
		public string NickName { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000208B File Offset: 0x0000028B
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002093 File Offset: 0x00000293
		public string ForwardAddress { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000209C File Offset: 0x0000029C
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020A4 File Offset: 0x000002A4
		public int ForwardPort { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020AD File Offset: 0x000002AD
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020B5 File Offset: 0x000002B5
		public string ServerName { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020BE File Offset: 0x000002BE
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000020C6 File Offset: 0x000002C6
		public string ServerVersion { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000020CF File Offset: 0x000002CF
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000020D7 File Offset: 0x000002D7
		[Nullable(2)]
		private IChannel Channel
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000020E0 File Offset: 0x000002E0
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000020E8 File Offset: 0x000002E8
		[Nullable(2)]
		private UdpBroadcaster UdpBroadcaster
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000020F4 File Offset: 0x000002F4
		public Interceptor(MultithreadEventLoopGroup acceptorGroup, MultithreadEventLoopGroup workerGroup, string serverName, string serverVersion, string forwardAddress, int forwardPort, string localAddress, int localPort, string nickName)
		{
			this.acceptorGroup = acceptorGroup;
			this.workerGroup = workerGroup;
			this.ActiveChannels = new ConcurrentDictionary<IChannelId, IChannel>();
			this.Identifier = Guid.NewGuid();
			this.LocalAddress = localAddress;
			this.LocalPort = localPort;
			this.NickName = nickName;
			this.ForwardAddress = forwardAddress;
			this.ForwardPort = forwardPort;
			this.ServerName = serverName;
			this.ServerVersion = serverVersion;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000216C File Offset: 0x0000036C
		public static Interceptor CreateInterceptor(EntitySocks5 socks5, string modInfo, string gameId, string serverName, string serverVersion, string forwardAddress, int forwardPort, string nickName, string userId, string userToken, [Nullable(new byte[] { 2, 0 })] Action<string> onJoinServer = null, string localAddress = "127.0.0.1", int localPort = 6445)
		{
			EventCreateInterceptor eventCreateInterceptor = EventManager.Instance.TriggerEvent<EventCreateInterceptor>("channel_interceptor", new EventCreateInterceptor(localPort));
			bool isCancelled = eventCreateInterceptor.IsCancelled;
			if (isCancelled)
			{
				throw new InvalidOperationException("Create Interceptor cancelled");
			}
			int availablePort = NetworkUtil.GetAvailablePort(eventCreateInterceptor.Port, 35565, true);
			MultithreadEventLoopGroup multithreadEventLoopGroup = new MultithreadEventLoopGroup();
			MultithreadEventLoopGroup multithreadEventLoopGroup2 = new MultithreadEventLoopGroup();
			Interceptor interceptor = new Interceptor(multithreadEventLoopGroup, multithreadEventLoopGroup2, serverName, serverVersion, forwardAddress, forwardPort, localAddress, availablePort, nickName);
			ServerBootstrap serverBootstrap = new ServerBootstrap().Group(multithreadEventLoopGroup, multithreadEventLoopGroup2).Channel<TcpServerSocketChannel>().Option<bool>(ChannelOption.SoReuseaddr, true)
				.Option<bool>(ChannelOption.SoReuseport, true)
				.Option<bool>(ChannelOption.TcpNodelay, true)
				.Option<bool>(ChannelOption.SoKeepalive, true)
				.Option<IByteBufferAllocator>(ChannelOption.Allocator, (IByteBufferAllocator)PooledByteBufferAllocator.Default)
				.Option<int>(ChannelOption.SoSndbuf, 1048576)
				.Option<int>(ChannelOption.SoRcvbuf, 1048576)
				.Option<int>(ChannelOption.WriteBufferHighWaterMark, 1048576)
				.Option<TimeSpan>(ChannelOption.ConnectTimeout, TimeSpan.FromSeconds(10.0))
				.ChildHandler(new ActionChannelInitializer<IChannel>(delegate(IChannel channel)
				{
					channel.Pipeline.AddLast("splitter", new MessageDeserializer21Bit()).AddLast("handler", new ServerHandler(interceptor, socks5, modInfo, gameId, forwardAddress, forwardPort, nickName, userId, userToken, onJoinServer)).AddLast("pre-encoder", new MessageSerializer21Bit())
						.AddLast("encoder", new MessageSerializer());
				}))
				.LocalAddress(IPAddress.Any, availablePort);
			string text = "请通过{Address}游玩";
			object[] array = new object[1];
			int num = 0;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler.AppendFormatted(localAddress);
			defaultInterpolatedStringHandler.AppendLiteral(":");
			defaultInterpolatedStringHandler.AppendFormatted<int>(availablePort);
			array[num] = defaultInterpolatedStringHandler.ToStringAndClear();
			Log.Information(text, array);
			Log.Information("您的名字:{Name}", new object[] { nickName });
			interceptor.UdpBroadcaster = new UdpBroadcaster("224.0.2.60", 4445, availablePort, forwardAddress, nickName, serverVersion.Contains("1.8.") || serverVersion.Contains("1.7."));
			serverBootstrap.BindAsync().ContinueWith(delegate(Task<IChannel> task)
			{
				bool isCompletedSuccessfully = task.IsCompletedSuccessfully;
				if (isCompletedSuccessfully)
				{
					interceptor.Channel = task.Result;
				}
			}).ContinueWith<Task>((Task _) => interceptor.UdpBroadcaster.StartBroadcastingAsync());
			return interceptor;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023CC File Offset: 0x000005CC
		public async Task ChangeForwardAddressAsync(string newAddress, int newPort)
		{
			this.ForwardAddress = newAddress;
			this.ForwardPort = newPort;
			UdpBroadcaster udpBroadcaster = this.UdpBroadcaster;
			if (udpBroadcaster != null)
			{
				udpBroadcaster.Stop();
			}
			this.UdpBroadcaster = new UdpBroadcaster("224.0.2.60", 4445, this.LocalPort, this.ForwardAddress, this.NickName, this.ServerVersion.Contains("1.8.") || this.ServerVersion.Contains("1.7."));
			await this.UdpBroadcaster.StartBroadcastingAsync();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002420 File Offset: 0x00000620
		public void ShutdownAsync()
		{
			try
			{
				UdpBroadcaster udpBroadcaster = this.UdpBroadcaster;
				if (udpBroadcaster != null)
				{
					udpBroadcaster.Stop();
				}
				foreach (KeyValuePair<IChannelId, IChannel> keyValuePair in this.ActiveChannels)
				{
					IChannelId channelId;
					IChannel channel;
					keyValuePair.Deconstruct(out channelId, out channel);
					IChannel channel2 = channel;
					GameConnection gameConnection = channel2.GetAttribute<GameConnection>(ChannelAttribute.Connection).Get();
					channel2.GetAttribute<GameConnection>(ChannelAttribute.Connection).Remove();
					gameConnection.Shutdown();
				}
				IChannel channel3 = this.Channel;
				if (channel3 != null)
				{
					channel3.CloseAsync();
				}
				((AbstractEventExecutorGroup)this.acceptorGroup).ShutdownGracefullyAsync();
				((AbstractEventExecutorGroup)this.workerGroup).ShutdownGracefullyAsync();
			}
			catch
			{
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002500 File Offset: 0x00000700
		public static void EnsureLoaded()
		{
			bool flag = typeof(Interceptor).Assembly.GetName().Name == null;
			if (flag)
			{
				throw new InvalidOperationException("Should never call CheckIsLoaded()");
			}
		}

		// Token: 0x04000001 RID: 1
		public readonly ConcurrentDictionary<IChannelId, IChannel> ActiveChannels;

		// Token: 0x0400000C RID: 12
		private IEventLoopGroup acceptorGroup;

		// Token: 0x0400000D RID: 13
		private IEventLoopGroup workerGroup;
	}
}
