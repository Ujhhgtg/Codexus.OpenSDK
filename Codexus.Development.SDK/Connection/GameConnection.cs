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

// Token: 0x02000030 RID: 48
public class GameConnection : IConnection
{
	// Token: 0x17000041 RID: 65
	// (get) Token: 0x06000102 RID: 258 RVA: 0x00005F4B File Offset: 0x0000414B
	// (set) Token: 0x06000103 RID: 259 RVA: 0x00005F53 File Offset: 0x00004153
	public string NickName { get; set; }

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x06000104 RID: 260 RVA: 0x00005F5C File Offset: 0x0000415C
	// (set) Token: 0x06000105 RID: 261 RVA: 0x00005F64 File Offset: 0x00004164
	public EnumProtocolVersion ProtocolVersion { get; set; }

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x06000106 RID: 262 RVA: 0x00005F6D File Offset: 0x0000416D
	// (set) Token: 0x06000107 RID: 263 RVA: 0x00005F75 File Offset: 0x00004175
	public EnumConnectionState State { get; set; }

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x06000108 RID: 264 RVA: 0x00005F7E File Offset: 0x0000417E
	// (set) Token: 0x06000109 RID: 265 RVA: 0x00005F86 File Offset: 0x00004186
		
	public Action<string> OnJoinServer
	{
		get;
		set;
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x0600010A RID: 266 RVA: 0x00005F8F File Offset: 0x0000418F
	public MultithreadEventLoopGroup TaskGroup { get; }

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x0600010B RID: 267 RVA: 0x00005F97 File Offset: 0x00004197
	// (set) Token: 0x0600010C RID: 268 RVA: 0x00005F9F File Offset: 0x0000419F
	public GameSession Session { get; set; }

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x0600010D RID: 269 RVA: 0x00005FA8 File Offset: 0x000041A8
	public string GameId { get; }

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x0600010E RID: 270 RVA: 0x00005FB0 File Offset: 0x000041B0
	public string ModInfo { get; }

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x0600010F RID: 271 RVA: 0x00005FB8 File Offset: 0x000041B8
	public int ForwardPort { get; }

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x06000110 RID: 272 RVA: 0x00005FC0 File Offset: 0x000041C0
	public string ForwardAddress { get; }

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x06000111 RID: 273 RVA: 0x00005FC8 File Offset: 0x000041C8
	// (set) Token: 0x06000112 RID: 274 RVA: 0x00005FD0 File Offset: 0x000041D0
	public byte[] Uuid { get; set; }

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x06000113 RID: 275 RVA: 0x00005FD9 File Offset: 0x000041D9
	// (set) Token: 0x06000114 RID: 276 RVA: 0x00005FE1 File Offset: 0x000041E1
	public Guid InterceptorId { get; set; }

	// Token: 0x06000115 RID: 277 RVA: 0x00005FEC File Offset: 0x000041EC
	public GameConnection(EntitySocks5 socks5, string modInfo, string gameId, string forwardAddress, int forwardPort, string nickName, string userId, string userToken, IChannel channel,  Action<string> onJoinServer)
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

	// Token: 0x06000116 RID: 278 RVA: 0x00006074 File Offset: 0x00004274
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

	// Token: 0x06000117 RID: 279 RVA: 0x00006198 File Offset: 0x00004398
	public void OnServerReceived(IByteBuffer buffer)
	{
		HandlePacketReceived(buffer, EnumPacketDirection.ClientBound, delegate(object data)
		{
			ClientChannel.WriteAndFlushAsync(data);
		});
	}

	// Token: 0x06000118 RID: 280 RVA: 0x000061B0 File Offset: 0x000043B0
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

	// Token: 0x06000119 RID: 281 RVA: 0x000061C8 File Offset: 0x000043C8
	public void Shutdown()
	{
		EventManager.Instance.TriggerEvent<EventConnectionClosed>("channel_connection", new EventConnectionClosed(this));
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

	// Token: 0x0600011A RID: 282 RVA: 0x00006240 File Offset: 0x00004440
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

	// Token: 0x0600011B RID: 283 RVA: 0x00006350 File Offset: 0x00004550
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

	// Token: 0x0600011C RID: 284 RVA: 0x00006464 File Offset: 0x00004664
	public static void EnableEncryption(IChannel channel, byte[] secretKey)
	{
		channel.Pipeline.AddBefore("splitter", "decrypt", new NettyEncryptionDecoder(secretKey)).AddBefore("pre-encoder", "encrypt", new NettyEncryptionEncoder(secretKey));
	}

	// Token: 0x04000078 RID: 120
	public readonly IChannel ClientChannel;

	// Token: 0x04000079 RID: 121
	private bool _initialized;

	// Token: 0x0400007A RID: 122
	private MultithreadEventLoopGroup _workerGroup;

	// Token: 0x0400007B RID: 123
	public IChannel ServerChannel;

	// Token: 0x0400007C RID: 124
	private readonly EntitySocks5 _socks5;
}