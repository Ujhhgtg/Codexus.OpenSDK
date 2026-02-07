using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace Codexus.Development.SDK.Utils;

public class UdpBroadcaster : IDisposable
{
    public UdpBroadcaster(string multicastAddress, int port, int targetPort, string serverIp, string roleName,
        bool is189Protocol)
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

    public void Dispose()
    {
        var cts = _cts;
        cts?.Dispose();
        _udpClient?.Close();
        _udpClient?.Dispose();
        GC.SuppressFinalize(this);
    }

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
            // Wait for 5 seconds before allowing the state machine to complete
            await Task.Delay(5000, _cts.Token);
    }

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

    public void Stop()
    {
        var cts = _cts;
        cts?.Cancel();
    }

    private static bool IsMulticastAddress(IPAddress address)
    {
        var addressBytes = address.GetAddressBytes();
        var flag = address.AddressFamily == AddressFamily.InterNetwork && addressBytes[0] >= 224;
        return flag && addressBytes[0] <= 239;
    }

    private readonly bool _is189Protocol;
    private readonly string _roleName;
    private readonly string _serverIp;
    private readonly int _serverPort;
    private readonly IPEndPoint _targetEndPoint;
    private readonly UdpClient? _udpClient;
    private CancellationTokenSource? _cts;
}