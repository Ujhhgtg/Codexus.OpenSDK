using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Codexus.Development.SDK.Analysis;
using Codexus.Development.SDK.Entities;
using Codexus.Development.SDK.Enums;
using Codexus.Development.SDK.Event;
using Codexus.Development.SDK.Extensions;
using Codexus.Development.SDK.Handlers;
using Codexus.Development.SDK.Manager;
using Codexus.Development.SDK.Utils;
using DotNetty.Buffers;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using OpenTl.Netty.Socks.Handlers;
using Serilog;

namespace Codexus.Development.SDK.Connection;
public class GameConnection : IConnection
{
	public string NickName { get; set; }
	public EnumProtocolVersion ProtocolVersion { get; set; }
	public EnumConnectionState State { get; set; }
		
	public Action<string>? OnJoinServer
	{
		get;
		set;
	}
	public MultithreadEventLoopGroup TaskGroup { get; }
	public GameSession Session { get; set; }
	public string GameId { get; }
	public string ModInfo { get; }
	public int ForwardPort { get; }
	public string ForwardAddress { get; }
	public byte[] Uuid { get; set; }
	public Guid InterceptorId { get; set; }

	public GameConnection(EntitySocks5 socks5, string modInfo, string gameId, string forwardAddress, int forwardPort, string nickName, string userId, string userToken, IChannel channel, Action<string>? onJoinServer)
	{
		_socks5 = socks5;
		ClientChannel = channel;
		NickName = nickName;
		ProtocolVersion = EnumProtocolVersion.None;
		OnJoinServer = onJoinServer;
		TaskGroup = new MultithreadEventLoopGroup();
		Session = new GameSession(nickName, userId, userToken);
		GameId = gameId;
		ModInfo = modInfo;
		ForwardPort = forwardPort;
		ForwardAddress = forwardAddress;
		Uuid = new byte[16];
	}

	public void Prepare()
	{
		_initialized = false;
		var flag = _workerGroup != null;
		if (flag)
		{
			Shutdown();
		}
		_workerGroup = new MultithreadEventLoopGroup();
		var bootstrap = new Bootstrap().Group(_workerGroup).Channel<TcpSocketChannel>().Option(ChannelOption.TcpNodelay, true)
			.Option(ChannelOption.SoKeepalive, true)
			.Option<IByteBufferAllocator>(ChannelOption.Allocator, PooledByteBufferAllocator.Default)
			.Option(ChannelOption.SoSndbuf, 1048576)
			.Option(ChannelOption.SoRcvbuf, 1048576)
			.Option(ChannelOption.WriteBufferHighWaterMark, 1048576)
			.Option(ChannelOption.ConnectTimeout, TimeSpan.FromSeconds(30.0))
			.Handler(new ActionChannelInitializer<IChannel>(delegate(IChannel channel)
			{
				var enabled = _socks5.Enabled;
				if (enabled)
				{
					var flag3 = !IPAddress.TryParse(_socks5.Address, out var ipaddress);
					if (flag3)
					{
						ipaddress = Dns.GetHostAddressesAsync(_socks5.Address).GetAwaiter().GetResult()
							.First();
					}
					channel.Pipeline.AddLast("socks5", new Socks5ProxyHandler(new IPEndPoint(ipaddress, _socks5.Port), _socks5.Username, _socks5.Password));
				}
				channel.Pipeline.AddLast("splitter", new MessageDeserializer21Bit());
				channel.Pipeline.AddLast("handler", new ClientHandler(this)).AddLast("pre-encoder", new MessageSerializer21Bit()).AddLast("encoder", new MessageSerializer());
			}));
		Task.Run(async delegate
		{
			var finalAddress = EventManager.Instance.TriggerEvent("channel_connection", new EventParseAddress(this, ForwardAddress, ForwardPort));
			var channel2 = await (IPAddress.TryParse(finalAddress.Address, out var address) ? bootstrap.ConnectAsync(address, finalAddress.Port) : bootstrap.ConnectAsync(finalAddress.Address, finalAddress.Port)).ContinueWith<IChannel>(delegate(Task<IChannel> channel)
			{
				var flag4 = !channel.IsFaulted;
				IChannel channel3;
				if (flag4)
				{
					channel3 = channel.Result;
				}
				else
				{
					Log.Error(channel.Exception, "Failed to connect to remote server {Address}:{Port}", [finalAddress.Address, finalAddress.Port
					]);
					channel3 = null;
				}
				return channel3;
			});
			var serverChannel = channel2;
			channel2 = null;
			ServerChannel = serverChannel;
			_initialized = true;
		});
		while (!_initialized)
		{
			Thread.Sleep(100);
		}
		var flag2 = ServerChannel == null;
		if (flag2)
		{
			Shutdown();
		}
	}

	public void OnServerReceived(IByteBuffer buffer)
	{
		HandlePacketReceived(buffer, EnumPacketDirection.ClientBound, delegate(object data)
		{
			ClientChannel.WriteAndFlushAsync(data);
		});
	}

	public void OnClientReceived(IByteBuffer buffer)
	{
		HandlePacketReceived(buffer, EnumPacketDirection.ServerBound, delegate(object data)
		{
			var serverChannel = ServerChannel;
			if (serverChannel != null)
			{
				serverChannel.WriteAndFlushAsync(data);
			}
		});
	}

	public void Shutdown()
	{
		EventManager.Instance.TriggerEvent("channel_connection", new EventConnectionClosed(this));
		Log.Debug("Shutting down connection...", []);
		TaskGroup.ShutdownGracefullyAsync();
		ClientChannel.CloseAsync();
		var serverChannel = ServerChannel;
		if (serverChannel != null)
		{
			serverChannel.CloseAsync();
		}
		var workerGroup = _workerGroup;
		var flag = workerGroup != null;
		if (flag)
		{
			workerGroup.ShutdownGracefullyAsync();
		}
	}

	private void HandlePacketReceived(IByteBuffer buffer, EnumPacketDirection direction, Action<object> onRedirect)
	{
		buffer.MarkReaderIndex();
		var num = buffer.ReadVarIntFromBuffer();
		var packet = PacketManager.Instance.BuildPacket(State, direction, ProtocolVersion, num);
		var flag = packet == null;
		if (flag)
		{
			buffer.ResetReaderIndex();
			onRedirect(buffer);
		}
		else
		{
			var metadata = PacketManager.Instance.GetMetadata(packet);
			var flag2 = metadata != null && metadata.Skip;
			if (flag2)
			{
				buffer.ResetReaderIndex();
				onRedirect(buffer);
			}
			else
			{
				packet.ClientProtocolVersion = ProtocolVersion;
				try
				{
					packet.ReadFromBuffer(buffer);
				}
				catch (Exception ex)
				{
					Log.Error(ex, "Cannot read packet from buffer, direction: {Direction}, Id: {Id}, Packet: {Packet}, ProtocolVersion: {ProtocolVersion}", direction, num, packet, ProtocolVersion);
					throw;
				}
				var flag3 = packet.HandlePacket(this);
				if (flag3)
				{
					buffer.ResetReaderIndex();
				}
				else
				{
					buffer.ResetReaderIndex();
					onRedirect(packet);
				}
			}
		}
	}

	public static void EnableCompression(IChannel channel, int threshold)
	{
		var flag = threshold < 0;
		if (flag)
		{
			var flag2 = channel.Pipeline.Get("decompress") is NettyCompressionDecoder;
			if (flag2)
			{
				channel.Pipeline.Remove("decompress");
			}
			var flag3 = channel.Pipeline.Get("compress") is NettyCompressionEncoder;
			if (flag3)
			{
				channel.Pipeline.Remove("compress");
			}
		}
		else
		{
			var nettyCompressionDecoder = channel.Pipeline.Get("decompress") as NettyCompressionDecoder;
			var flag4 = nettyCompressionDecoder != null;
			if (flag4)
			{
				nettyCompressionDecoder.Threshold = threshold;
			}
			else
			{
				channel.Pipeline.AddAfter("splitter", "decompress", new NettyCompressionDecoder(threshold));
			}
			var nettyCompressionEncoder = channel.Pipeline.Get("compress") as NettyCompressionEncoder;
			var flag5 = nettyCompressionEncoder != null;
			if (flag5)
			{
				nettyCompressionEncoder.Threshold = threshold;
			}
			else
			{
				channel.Pipeline.AddBefore("encoder", "compress", new NettyCompressionEncoder(threshold));
			}
		}
	}

	public static void EnableEncryption(IChannel channel, byte[] secretKey)
	{
		channel.Pipeline.AddBefore("splitter", "decrypt", new NettyEncryptionDecoder(secretKey)).AddBefore("pre-encoder", "encrypt", new NettyEncryptionEncoder(secretKey));
	}
	public readonly IChannel ClientChannel;
	private bool _initialized;
	private MultithreadEventLoopGroup _workerGroup;
	public IChannel ServerChannel;
	private readonly EntitySocks5 _socks5;
}