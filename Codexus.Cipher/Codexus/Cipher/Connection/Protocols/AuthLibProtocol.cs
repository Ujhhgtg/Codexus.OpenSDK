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

	// public AuthLibProtocol(IPAddress address, int port, string modList, string version, string accessToken)
	// {
	// }

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
		var disposed = _disposed;
		if (!disposed)
		{
			if (disposing)
			{
				_cts.Cancel();
				var listener = _listener;
				listener?.Stop();
				try
				{
					var acceptLoopTask = _acceptLoopTask;
					acceptLoopTask?.Wait(TimeSpan.FromSeconds(5L));
				}
				catch (Exception ex)
				{
					Log.Error("Authentication failed. {Message}", ex.Message);
				}
				_cts.Dispose();
			}
			_disposed = true;
		}
	}

	public void Start()
	{
		var disposed = _disposed;
		if (disposed)
		{
			throw new ObjectDisposedException("AuthLibProtocol");
		}
		_listener = new TcpListener(address, port);
		_listener.Start();
		_acceptLoopTask = AcceptLoopAsync(_cts.Token);
	}

	public void Stop()
	{
		var flag = !_disposed;
		if (flag)
		{
			Dispose();
		}
	}

	// 	private Task AcceptLoopAsync(CancellationToken token)
	// 	{
	// 		/*
	// An exception occurred when decompiling this method (06000679)
	//
	// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Threading.Tasks.Task Codexus.Cipher.Connection.Protocols.AuthLibProtocol::AcceptLoopAsync(System.Threading.CancellationToken)
	//
	// ---> System.Collections.Generic.KeyNotFoundException: The given key 'IL_0014:' was not present in the dictionary.
	// at ICSharpCode.Decompiler.ILAst.SimpleControlFlow.SimplifyShortCircuit(List`1 body, ILBasicBlock head, Int32 pos) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\SimpleControlFlow.cs:line 270
	// at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 277
	// at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
	// at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 88
	// --- End of inner exception stack trace ---
	// at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 92
	// at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind)
	// */;
	// 	}
	private async Task AcceptLoopAsync(CancellationToken token)
	{
		// This is the "loop" logic found between IL_00EA and IL_010C
		while (!token.IsCancellationRequested && !_disposed)
		{
			try
			{
				// IL_002F: Accepts the next client connection asynchronously
				var client = await _listener.AcceptTcpClientAsync(token).ConfigureAwait(false);

				// IL_00B2: Hand off the client to a separate handler task
				// Note: The 'pop' in IL_00B7 suggests the task isn't awaited here
				_ = HandleClientAsync(client, token);
			}
			catch (ObjectDisposedException)
			{
				// IL_00C2: If the listener is closed, break the loop gracefully
				break;
			}
			catch (Exception ex)
			{
				// IL_00D1 - IL_00E1: Log the error and continue the loop
				Log.Warning("Accept loop error: {Message}", ex.Message);
			}
		}
	}

	private static async Task ReadExactAsync(NetworkStream stream, byte[] buffer, int offset, int count, CancellationToken token)
	{
		int num;
		for (var read = 0; read < count; read += num)
		{
			var num2 = await stream.ReadAsync(buffer.AsMemory(offset + read, count - read), token).ConfigureAwait(false);
			num = num2;
			if (num == 0)
			{
				throw new EndOfStreamException();
			}
		}
	}

	// private async Task HandleClientAsync(TcpClient client, CancellationToken token)
	// {
	// 	using (client)
	// 	{
	// 		var cDisplayClass17 = new c__DisplayClass17_0();
	// 		var stream = client.GetStream();
	// 		object obj = null;
	// 		object obj3;
	// 		try
	// 		{
	// 			cDisplayClass17.responseCode = 1U;
	// 			object obj2 = null;
	// 			try
	// 			{
	// 				try
	// 				{
	// 					var lenBuf = new byte[4];
	// 					await ReadExactAsync(stream, lenBuf, 0, 4, token).ConfigureAwait(false);
	// 					var num = BitConverter.ToInt32(lenBuf, 0);
	// 					var gameIdBuf = new byte[num];
	// 					await ReadExactAsync(stream, gameIdBuf, 0, num, token).ConfigureAwait(false);
	// 					var gameId = Encoding.UTF8.GetString(gameIdBuf);
	// 					await ReadExactAsync(stream, lenBuf, 0, 4, token).ConfigureAwait(false);
	// 					var num2 = BitConverter.ToInt32(lenBuf, 0);
	// 					var userIdBuf = new byte[num2];
	// 					await ReadExactAsync(stream, userIdBuf, 0, num2, token).ConfigureAwait(false);
	// 					var userId = Encoding.UTF8.GetString(userIdBuf);
	// 					await ReadExactAsync(stream, lenBuf, 0, 4, token).ConfigureAwait(false);
	// 					var num3 = BitConverter.ToInt32(lenBuf, 0);
	// 					var certBuf = new byte[num3];
	// 					await ReadExactAsync(stream, certBuf, 0, num3, token).ConfigureAwait(false);
	// 					var text = Encoding.Unicode.GetString(certBuf);
	// 					var instance = IUserManager.Instance;
	// 					var entityAvailableUser = instance != null ? instance.GetAvailableUser(userId) : null;
	// 					if (entityAvailableUser == null)
	// 					{
	// 						throw new Exception("User not found");
	// 					}
	// 					if (!string.IsNullOrEmpty(text))
	// 					{
	// 						await NetEaseConnection.CreateAuthenticatorAsync(text, gameId, version, modList_P, accessToken_P, int.Parse(userId), entityAvailableUser.AccessToken, "45.253.165.190", NetEaseConnection.RandomAuthPort(), cDisplayClass17.HandleClientAsync_b__0).ConfigureAwait(false);
	// 					}
	// 					lenBuf = null;
	// 					gameIdBuf = null;
	// 					gameId = null;
	// 					userIdBuf = null;
	// 					userId = null;
	// 					certBuf = null;
	// 					text = null;
	// 					entityAvailableUser = null;
	// 				}
	// 				catch (Exception ex)
	// 				{
	// 					Log.Warning("Client handling error: {Message}", ex.Message);
	// 				}
	// 			}
	// 			catch (object obj2)
	// 			{
	// 			}
	// 			try
	// 			{
	// 				var bytes = BitConverter.GetBytes(cDisplayClass17.responseCode);
	// 				await stream.WriteAsync(bytes, token).ConfigureAwait(false);
	// 				bytes = null;
	// 			}
	// 			catch (Exception ex2)
	// 			{
	// 				Log.Warning("Response writing error: {Message}", ex2.Message);
	// 			}
	// 			obj3 = obj2;
	// 			if (obj3 != null)
	// 			{
	// 				var ex3 = obj3 as Exception;
	// 				if (ex3 == null)
	// 				{
	// 					throw obj3;
	// 				}
	// 				ExceptionDispatchInfo.Capture(ex3).Throw();
	// 			}
	// 			obj2 = null;
	// 		}
	// 		catch (object obj)
	// 		{
	// 		}
	// 		if (stream != null)
	// 		{
	// 			await stream.DisposeAsync();
	// 		}
	// 		obj3 = obj;
	// 		if (obj3 != null)
	// 		{
	// 			var ex3 = obj3 as Exception;
	// 			if (ex3 == null)
	// 			{
	// 				throw obj3;
	// 			}
	// 			ExceptionDispatchInfo.Capture(ex3).Throw();
	// 		}
	// 		obj = null;
	// 		cDisplayClass17 = null;
	// 		stream = null;
	// 	}
	// 	TcpClient tcpClient = null;
	// }
	private async Task HandleClientAsync(TcpClient client, CancellationToken token)
	{
	    using (client)
	    await using (var stream = client.GetStream())
	    {
	        uint responseCode = 1; // Default error code (1 = Error, 0 = Success)

	        try
	        {
	            // 1. Read Game ID (UTF8)
	            var gameId = await ReadPrefixedStringAsync(stream, Encoding.UTF8, token);
	            
	            // 2. Read User ID (UTF8)
	            var userId = await ReadPrefixedStringAsync(stream, Encoding.UTF8, token);
	            
	            // 3. Read Certificate (Unicode/UTF16)
	            var certificate = await ReadPrefixedStringAsync(stream, Encoding.Unicode, token);

	            // 4. Validate User
	            var user = IUserManager.Instance?.GetAvailableUser(userId);
	            if (user == null) throw new Exception("User not found");

	            // 5. Connect to the NetEase Auth Server
	            if (!string.IsNullOrEmpty(certificate))
	            {
	                await NetEaseConnection.CreateAuthenticatorAsync(
	                    certificate, gameId, version, modList, accessToken, 
	                    int.Parse(userId), user.AccessToken, 
	                    "45.253.165.190", NetEaseConnection.RandomAuthPort(),
	                    () => { responseCode = 0; }
	                ).ConfigureAwait(false);
	            }
	        }
	        catch (Exception ex)
	        {
	            Log.Warning("Client handling error: {Message}", ex.Message);
	        }

	        // 6. Final Step: Send the response code back to the client
	        try
	        {
	            var response = BitConverter.GetBytes(responseCode);
	            await stream.WriteAsync(response, token).ConfigureAwait(false);
	        }
	        catch (Exception ex)
	        {
	            Log.Warning("Response writing error: {Message}", ex.Message);
	        }
	    }
	}

	// Helper method to clean up the repetitive "Read length -> Read bytes -> Convert to string" logic
	private async Task<string> ReadPrefixedStringAsync(NetworkStream stream, Encoding encoding, CancellationToken token)
	{
	    var lenBuf = new byte[4];
	    await ReadExactAsync(stream, lenBuf, 0, 4, token).ConfigureAwait(false);
	    var length = BitConverter.ToInt32(lenBuf, 0);

	    var buffer = new byte[length];
	    await ReadExactAsync(stream, buffer, 0, length, token).ConfigureAwait(false);
	    return encoding.GetString(buffer);
	}
	private readonly CancellationTokenSource _cts = new();
		
	private TcpListener _listener;
		
	private Task _acceptLoopTask;
	private bool _disposed;
	// [CompilerGenerated]
	// private sealed class c__DisplayClass17_0
	// {
	// 	// Token: 0x060007DC RID: 2012
	// 	internal void HandleClientAsync_b__0()
	// 	{
	// 		responseCode = 0U;
	// 	}
	//
	// 	// Token: 0x04000756 RID: 1878
	// 	public uint responseCode;
	// }
}