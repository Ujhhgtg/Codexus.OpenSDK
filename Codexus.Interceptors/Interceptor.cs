using System;
using System.Collections.Concurrent;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Codexus.Development.SDK.Analysis;
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

namespace Codexus.Interceptors;
public class Interceptor(
	MultithreadEventLoopGroup acceptorGroup,
	MultithreadEventLoopGroup workerGroup,
	string serverName,
	string serverVersion,
	string forwardAddress,
	int forwardPort,
	string localAddress,
	int localPort,
	string nickName)
{
	public Guid Identifier { get; } = Guid.NewGuid();
	public string LocalAddress { get; set; } = localAddress;
	public int LocalPort { get; set; } = localPort;
	public string NickName { get; set; } = nickName;
	public string ForwardAddress { get; private set; } = forwardAddress;
	public int ForwardPort { get; private set; } = forwardPort;
	public string ServerName { get; set; } = serverName;
	public string ServerVersion { get; set; } = serverVersion;

	private IChannel? Channel
	{
		get;
		set;
	}
	private UdpBroadcaster? UdpBroadcaster
	{
		get;
		set;
	}

	public static Interceptor CreateInterceptor(EntitySocks5 socks5, string modInfo, string gameId, string serverName, string serverVersion, string forwardAddress, int forwardPort, string nickName, string userId, string userToken, Action<string>? onJoinServer = null, string localAddress = "127.0.0.1", int localPort = 6445)
	{
		var eventCreateInterceptor = EventManager.Instance.TriggerEvent<EventCreateInterceptor>("channel_interceptor", new EventCreateInterceptor(localPort));
		var isCancelled = eventCreateInterceptor.IsCancelled;
		if (isCancelled)
		{
			throw new InvalidOperationException("Create Interceptor cancelled");
		}
		var availablePort = NetworkUtil.GetAvailablePort(eventCreateInterceptor.Port, 35565, true);
		var multithreadEventLoopGroup = new MultithreadEventLoopGroup();
		var multithreadEventLoopGroup2 = new MultithreadEventLoopGroup();
		var interceptor = new Interceptor(multithreadEventLoopGroup, multithreadEventLoopGroup2, serverName, serverVersion, forwardAddress, forwardPort, localAddress, availablePort, nickName);
		var serverBootstrap = new ServerBootstrap().Group(multithreadEventLoopGroup, multithreadEventLoopGroup2).Channel<TcpServerSocketChannel>().Option(ChannelOption.SoReuseaddr, true)
			.Option(ChannelOption.SoReuseport, true)
			.Option(ChannelOption.TcpNodelay, true)
			.Option(ChannelOption.SoKeepalive, true)
			.Option<IByteBufferAllocator>(ChannelOption.Allocator, PooledByteBufferAllocator.Default)
			.Option(ChannelOption.SoSndbuf, 1048576)
			.Option(ChannelOption.SoRcvbuf, 1048576)
			.Option(ChannelOption.WriteBufferHighWaterMark, 1048576)
			.Option(ChannelOption.ConnectTimeout, TimeSpan.FromSeconds(10.0))
			.ChildHandler(new ActionChannelInitializer<IChannel>(delegate(IChannel channel)
			{
				channel.Pipeline.AddLast("splitter", new MessageDeserializer21Bit()).AddLast("handler", new ServerHandler(interceptor, socks5, modInfo, gameId, forwardAddress, forwardPort, nickName, userId, userToken, onJoinServer)).AddLast("pre-encoder", new MessageSerializer21Bit())
					.AddLast("encoder", new MessageSerializer());
			}))
			.LocalAddress(IPAddress.Any, availablePort);
		var text = "请通过{Address}游玩";
		var array = new object[1];
		var num = 0;
		var defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
		defaultInterpolatedStringHandler.AppendFormatted(localAddress);
		defaultInterpolatedStringHandler.AppendLiteral(":");
		defaultInterpolatedStringHandler.AppendFormatted(availablePort);
		array[num] = defaultInterpolatedStringHandler.ToStringAndClear();
		Log.Information(text, array);
		Log.Information("您的名字:{Name}", [nickName]);
		interceptor.UdpBroadcaster = new UdpBroadcaster("224.0.2.60", 4445, availablePort, forwardAddress, nickName, serverVersion.Contains("1.8.") || serverVersion.Contains("1.7."));
		serverBootstrap.BindAsync().ContinueWith(delegate(Task<IChannel> task)
		{
			var isCompletedSuccessfully = task.IsCompletedSuccessfully;
			if (isCompletedSuccessfully)
			{
				interceptor.Channel = task.Result;
			}
		}).ContinueWith(_ => interceptor.UdpBroadcaster.StartBroadcastingAsync());
		return interceptor;
	}
	public async Task ChangeForwardAddressAsync(string newAddress, int newPort)
	{
		ForwardAddress = newAddress;
		ForwardPort = newPort;
		UdpBroadcaster?.Stop();
		UdpBroadcaster = new UdpBroadcaster("224.0.2.60", 4445, LocalPort, ForwardAddress, NickName, ServerVersion.Contains("1.8.") || ServerVersion.Contains("1.7."));
		await UdpBroadcaster.StartBroadcastingAsync();
	}
	public void ShutdownAsync()
	{
		try
		{
			UdpBroadcaster?.Stop();
			foreach (var keyValuePair in ActiveChannels)
			{
				keyValuePair.Deconstruct(out _, out var channel);
				var gameConnection = channel.GetAttribute(ChannelAttribute.Connection).Get();
				channel.GetAttribute(ChannelAttribute.Connection).Remove();
				gameConnection.Shutdown();
			}
			Channel?.CloseAsync();
			((AbstractEventExecutorGroup)_acceptorGroup).ShutdownGracefullyAsync();
			((AbstractEventExecutorGroup)_workerGroup).ShutdownGracefullyAsync();
		}
		catch
		{
		}
	}
	public static void EnsureLoaded()
	{
		var flag = typeof(Interceptor).Assembly.GetName().Name == null;
		if (flag)
		{
			throw new InvalidOperationException("Should never call CheckIsLoaded()");
		}
	}
	
	public readonly ConcurrentDictionary<IChannelId, IChannel> ActiveChannels = new();
	private readonly IEventLoopGroup _acceptorGroup = acceptorGroup;
	private readonly IEventLoopGroup _workerGroup = workerGroup;
}