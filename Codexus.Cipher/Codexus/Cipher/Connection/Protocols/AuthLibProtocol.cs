using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Codexus.Development.SDK.Manager;
using Serilog;

namespace Codexus.Cipher.Connection.Protocols;

public class AuthLibProtocol(IPAddress address, int port, string modList, string version, string accessToken) : IDisposable
{
	private readonly CancellationTokenSource _cts = new();

	private TcpListener? _listener;

	private Task? _acceptLoopTask;

	private bool _disposed;

	~AuthLibProtocol()
	{
		Dispose(false);
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (_disposed)
		{
			return;
		}
		if (disposing)
		{
			_cts.Cancel();
			_listener?.Stop();
			try
			{
				_acceptLoopTask?.Wait(TimeSpan.FromSeconds(5L));
			}
			catch (Exception ex)
			{
				Log.Error("Authentication failed. {Message}", ex.Message);
			}
			_cts.Dispose();
		}
		_disposed = true;
	}

	public void Start()
	{
		if (_disposed)
		{
			throw new ObjectDisposedException("AuthLibProtocol");
		}
		_listener = new TcpListener(address, port);
		_listener.Start();
		_acceptLoopTask = AcceptLoopAsync(_cts.Token);
	}

	public void Stop()
	{
		if (!_disposed)
		{
			Dispose();
		}
	}

	private async Task AcceptLoopAsync(CancellationToken token)
	{
		while (!token.IsCancellationRequested && !_disposed)
		{
			try
			{
				HandleClientAsync(await _listener!.AcceptTcpClientAsync(token).ConfigureAwait(continueOnCapturedContext: false), token);
			}
			catch (ObjectDisposedException)
			{
				break;
			}
			catch (Exception ex2)
			{
				Log.Warning("Accept loop error: {Message}", ex2.Message);
				break;
			}
		}
	}

	private static async Task ReadExactAsync(NetworkStream stream, byte[] buffer, int offset, int count, CancellationToken token)
	{
		int num;
		for (var read = 0; read < count; read += num)
		{
			num = await stream.ReadAsync(buffer.AsMemory(offset + read, count - read), token).ConfigureAwait(continueOnCapturedContext: false);
			if (num == 0)
			{
				throw new EndOfStreamException();
			}
		}
	}

	private async Task HandleClientAsync(TcpClient client, CancellationToken token)
	{
		using (client)
		{
			await using var stream = client.GetStream();
			var responseCode = 1u;
			try
			{
				var lenBuf = new byte[4];
				await ReadExactAsync(stream, lenBuf, 0, 4, token).ConfigureAwait(continueOnCapturedContext: false);
				var num = BitConverter.ToInt32(lenBuf, 0);
				var gameIdBuf = new byte[num];
				await ReadExactAsync(stream, gameIdBuf, 0, num, token).ConfigureAwait(continueOnCapturedContext: false);
				var gameId = Encoding.UTF8.GetString(gameIdBuf);
				await ReadExactAsync(stream, lenBuf, 0, 4, token).ConfigureAwait(continueOnCapturedContext: false);
				var num2 = BitConverter.ToInt32(lenBuf, 0);
				var userIdBuf = new byte[num2];
				await ReadExactAsync(stream, userIdBuf, 0, num2, token).ConfigureAwait(continueOnCapturedContext: false);
				var userId = Encoding.UTF8.GetString(userIdBuf);
				await ReadExactAsync(stream, lenBuf, 0, 4, token).ConfigureAwait(continueOnCapturedContext: false);
				var num3 = BitConverter.ToInt32(lenBuf, 0);
				var certBuf = new byte[num3];
				await ReadExactAsync(stream, certBuf, 0, num3, token).ConfigureAwait(continueOnCapturedContext: false);
				var text = Encoding.Unicode.GetString(certBuf);
				var entityAvailableUser = IUserManager.Instance?.GetAvailableUser(userId);
				if (entityAvailableUser == null)
				{
					throw new Exception("User not found");
				}
				if (!string.IsNullOrEmpty(text))
				{
					await NetEaseConnection.CreateAuthenticatorAsync(text, gameId, version, modList, accessToken, int.Parse(userId), entityAvailableUser.AccessToken, "45.253.165.190", NetEaseConnection.RandomAuthPort(),
						() => { responseCode = 0u; }).ConfigureAwait(continueOnCapturedContext: false);
				}
			}
			catch (Exception ex)
			{
				Log.Warning(ex, "Client handling error");
			}
			finally
			{
				try
				{
					await stream.WriteAsync(BitConverter.GetBytes(responseCode), token).ConfigureAwait(continueOnCapturedContext: false);
				}
				catch (Exception ex)
				{
					Log.Warning(ex, "Response writing error");
				}
			}
		}
	}
}