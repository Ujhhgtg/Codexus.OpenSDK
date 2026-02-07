using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace Codexus.Development.SDK.Utils;

// Token: 0x02000010 RID: 16
public class UdpBroadcaster : IDisposable
{
	// Token: 0x06000051 RID: 81 RVA: 0x00003324 File Offset: 0x00001524
	public UdpBroadcaster(string multicastAddress, int port, int targetPort, string serverIp, string roleName, bool is189Protocol)
	{
		_udpClient = new UdpClient();
		_udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
		_targetEndPoint = new IPEndPoint(IPAddress.Parse(multicastAddress), port);
		var flag = IsMulticastAddress(_targetEndPoint.Address);
		if (flag)
		{
			_udpClient.JoinMulticastGroup(_targetEndPoint.Address);
			_udpClient.MulticastLoopback = true;
		}
		else
		{
			_udpClient.EnableBroadcast = true;
		}
		_serverPort = targetPort;
		_is189Protocol = is189Protocol;
		_serverIp = serverIp;
		_roleName = roleName;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x000033D8 File Offset: 0x000015D8
	public void Dispose()
	{
		var cts = _cts;
		cts?.Dispose();
		_udpClient?.Close();
		_udpClient?.Dispose();
		GC.SuppressFinalize(this);
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00003414 File Offset: 0x00001614
	public async Task StartBroadcastingAsync()
	{
		_cts = new CancellationTokenSource();
		try
		{
			while (!_cts.IsCancellationRequested)
			{
				await SendMessageAsync();
				await Task.Delay(TimeSpan.FromSeconds(2L), _cts.Token);
			}
		}
		catch (OperationCanceledException ex)
		{
			Log.Error("Broadcasting operation cancelled, {exception}", [ex.Message]);
		}
		catch (Exception value)
		{
			var defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 1);
			defaultInterpolatedStringHandler.AppendLiteral("UDP Broadcast error: ");
			defaultInterpolatedStringHandler.AppendFormatted(value);
			Log.Error(defaultInterpolatedStringHandler.ToStringAndClear(), []);
		}
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00003458 File Offset: 0x00001658
	private async Task SendMessageAsync()
	{
		// Internal flag/state used for retry logic
		var retryState = 0; 

		try
		{
			var message = BuildMessage();
			var bytes = Encoding.UTF8.GetBytes(message);

			// Send the message asynchronously
			await _udpClient.SendAsync(bytes, bytes.Length, _targetEndPoint);
		}
		catch (SocketException ex) when (ex.SocketErrorCode == (SocketError)10065) 
		{
			// 10065 = HostUnreachable
			// If host is unreachable, set state to trigger a delay
			retryState = 1;
		}
		catch (Exception value)
		{
			// Log any other general exceptions
			Log.Error("UDP Send failed: {Exception}", value);
		}

		// If a retry/delay was flagged (e.g., host unreachable)
		if (retryState == 1)
		{
			// Wait for 5 seconds before allowing the state machine to complete
			await Task.Delay(5000, _cts.Token);
		}
	}

	// Token: 0x06000055 RID: 85 RVA: 0x0000349C File Offset: 0x0000169C
	private string BuildMessage()
	{
		string text;
		if (_is189Protocol)
		{
			var defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 3);
			defaultInterpolatedStringHandler.AppendLiteral("[MOTD] Codexus ");
			defaultInterpolatedStringHandler.AppendFormatted(_serverIp);
			defaultInterpolatedStringHandler.AppendLiteral(" -> ");
			defaultInterpolatedStringHandler.AppendFormatted(_roleName);
			defaultInterpolatedStringHandler.AppendLiteral("[/MOTD][AD]");
			defaultInterpolatedStringHandler.AppendFormatted(_serverPort);
			defaultInterpolatedStringHandler.AppendLiteral("[/AD]");
			text = defaultInterpolatedStringHandler.ToStringAndClear();
		}
		else
		{
			var defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(39, 3);
			defaultInterpolatedStringHandler2.AppendLiteral("[MOTD] §bCodexus §f");
			defaultInterpolatedStringHandler2.AppendFormatted(_serverIp);
			defaultInterpolatedStringHandler2.AppendLiteral(" -> ");
			defaultInterpolatedStringHandler2.AppendFormatted(_roleName);
			defaultInterpolatedStringHandler2.AppendLiteral("[/MOTD][AD]");
			defaultInterpolatedStringHandler2.AppendFormatted(_serverPort);
			defaultInterpolatedStringHandler2.AppendLiteral("[/AD]");
			text = defaultInterpolatedStringHandler2.ToStringAndClear();
		}
		return text;
	}

	// Token: 0x06000056 RID: 86 RVA: 0x0000359A File Offset: 0x0000179A
	public void Stop()
	{
		var cts = _cts;
		cts?.Cancel();
	}

	// Token: 0x06000057 RID: 87 RVA: 0x000035B0 File Offset: 0x000017B0
	private static bool IsMulticastAddress(IPAddress address)
	{
		var addressBytes = address.GetAddressBytes();
		var flag = address.AddressFamily == AddressFamily.InterNetwork && addressBytes[0] >= 224;
		return flag && addressBytes[0] <= 239;
	}

	// Token: 0x0400002C RID: 44
	private readonly bool _is189Protocol;

	// Token: 0x0400002D RID: 45
	private readonly string _roleName;

	// Token: 0x0400002E RID: 46
	private readonly string _serverIp;

	// Token: 0x0400002F RID: 47
	private readonly int _serverPort;

	// Token: 0x04000030 RID: 48
	private readonly IPEndPoint _targetEndPoint;

	// Token: 0x04000031 RID: 49
	private readonly UdpClient? _udpClient;

	// Token: 0x04000032 RID: 50
	private CancellationTokenSource? _cts;
}