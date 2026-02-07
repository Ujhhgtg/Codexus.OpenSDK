using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Serilog;

namespace Codexus.Cipher.Extensions.Com4399Extensions;

// Token: 0x0200002E RID: 46
public class CaptchaHttpServer
{
	// Token: 0x06000159 RID: 345 RVA: 0x000075C0 File Offset: 0x000057C0
	public CaptchaHttpServer(int port)
	{
		Port = port;
		var defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 1);
		defaultInterpolatedStringHandler.AppendLiteral("http://127.0.0.1:");
		defaultInterpolatedStringHandler.AppendFormatted(port);
		defaultInterpolatedStringHandler.AppendLiteral("/");
		var text = defaultInterpolatedStringHandler.ToStringAndClear();
		_httpListener = new HttpListener();
		_httpListener.Prefixes.Add(text);
	}

	// Token: 0x0600015A RID: 346 RVA: 0x00007630 File Offset: 0x00005830
	// 	public Task StartAsync()
	// 	{
	// 		var isRunning = _isRunning;
	// 		Task task;
	// 		if (isRunning)
	// 		{
	// 			task = Task.CompletedTask;
	// 		}
	// 		else
	// 		{
	// 			_httpListener.Start();
	// 			_isRunning = true;
	// 			Task.Run(async delegate
	// 			{
	// 				/*
	// An exception occurred when decompiling this method (06000161)
	//
	// ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Threading.Tasks.Task Codexus.Cipher.Extensions.Com4399Extensions.CaptchaHttpServer::<StartAsync>b__4_0()
	//
	// ---> System.Collections.Generic.KeyNotFoundException: The given key 'IL_0014:' was not present in the dictionary.
	// at ICSharpCode.Decompiler.ILAst.SimpleControlFlow.SimplifyShortCircuit(List`1 body, ILBasicBlock head, Int32 pos) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\SimpleControlFlow.cs:line 270
	// at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 277
	// at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
	// at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 88
	// --- End of inner exception stack trace ---
	// at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 92
	// at ICSharpCode.Decompiler.Ast.Transforms.DelegateConstruction.HandleAnonymousMethod(ObjectCreateExpression objectCreateExpression, Expression target, IMethod methodRef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\Transforms\DelegateConstruction.cs:line 237
	// */;
	// 			});
	// 			task = Task.CompletedTask;
	// 		}
	// 		return task;
	// 	}

	public Task StartAsync()
	{
		if (_isRunning)
		{
			return Task.CompletedTask;
		}

		_httpListener.Start();
		_isRunning = true;

		// This launches the background loop (the state machine you provided)
		Task.Run(async () =>
		{
			while (_isRunning && _httpListener.IsListening)
			{
				try
				{
					// Wait for a browser to connect (e.g., http://127.0.0.1:port/)
					var context = await _httpListener.GetContextAsync();

					// Process the request on yet another thread so the loop stays responsive
					_ = Task.Run(() => HandleHttpRequest(context));
				}
				catch (HttpListenerException)
				{
					// Usually happens when the listener is stopped/closed
					break;
				}
				catch (Exception ex)
				{
					Log.Error("Error processing request: {Message}", ex.Message);
				}
			}
		});

		return Task.CompletedTask;
	}
	
	// Token: 0x0600015B RID: 347 RVA: 0x00007680 File Offset: 0x00005880
	public void Stop()
	{
		var isRunning = _isRunning;
		if (isRunning)
		{
			_isRunning = false;
			_httpListener.Stop();
		}
	}

	// Token: 0x0600015C RID: 348 RVA: 0x000076B0 File Offset: 0x000058B0
	private void HandleHttpRequest(HttpListenerContext context)
	{
		try
		{
			var request = context.Request;
			var response = context.Response;
			var flag = request.Url == null;
			if (flag)
			{
				response.StatusCode = 400;
				response.Close();
			}
			else
			{
				var text = request.Url.AbsolutePath.Trim('/').ToLowerInvariant();
				var flag2 = text == null || text.Length != 0;
				if (flag2)
				{
					if (text != "index.html")
					{
						if (text != "img.json")
						{
							if (text != "resolve")
							{
								goto IL_0112;
							}
							var flag3 = request.HttpMethod == "POST";
							if (flag3)
							{
								HandleResolveRequest(request, response);
								return;
							}
							goto IL_0112;
						}
						else
						{
							var flag4 = request.HttpMethod == "GET";
							if (flag4)
							{
								HandleImageJsonRequest(response);
								return;
							}
							goto IL_0112;
						}
					}
				}
				var flag5 = request.HttpMethod == "GET";
				if (flag5)
				{
					HandleIndexRequest(response);
					return;
				}
				IL_0112:
				response.StatusCode = 404;
				WriteStringToResponse(response, "Not Found");
			}
		}
		catch (Exception ex)
		{
			try
			{
				context.Response.StatusCode = 500;
				WriteStringToResponse(context.Response, ex.ToString());
			}
			catch
			{
				Log.Error("Server Error");
			}
		}
		finally
		{
			context.Response.Close();
		}
	}

	// Token: 0x0600015D RID: 349 RVA: 0x00007888 File Offset: 0x00005A88
	private static void HandleIndexRequest(HttpListenerResponse response)
	{
		var text = CaptchaHandler.CurrentCaptchaType == "jigsaw" ? CaptchaResources.JigsawCaptchaHtml : CaptchaResources.ClickCaptchaHtml;
		response.ContentType = "text/html; charset=utf-8";
		WriteStringToResponse(response, text);
	}

	// Token: 0x0600015E RID: 350 RVA: 0x000078C8 File Offset: 0x00005AC8
	private static void HandleImageJsonRequest(HttpListenerResponse response)
	{
		response.ContentType = "application/json";
		Dictionary<string, string> dictionary2;
		if (CaptchaHandler.CurrentCaptchaType == "jigsaw")
		{
			var dictionary = new Dictionary<string, string>();
			dictionary.Add("verificationImage", "data:image/png;base64," + CaptchaHandler.BackgroundImageBase64);
			dictionary2 = dictionary;
			dictionary.Add("sliderImage", "data:image/png;base64," + CaptchaHandler.SliderImageBase64);
		}
		else
		{
			var dictionary3 = new Dictionary<string, string>();
			dictionary3.Add("img", "data:image/png;base64," + CaptchaHandler.BackgroundImageBase64);
			dictionary2 = dictionary3;
			dictionary3.Add("text", CaptchaHandler.ClickableText);
		}
		var dictionary4 = dictionary2;
		WriteStringToResponse(response, JsonSerializer.Serialize(dictionary4));
	}

	// Token: 0x0600015F RID: 351 RVA: 0x00007970 File Offset: 0x00005B70
	private void HandleResolveRequest(HttpListenerRequest request, HttpListenerResponse response)
	{
		response.ContentType = "application/json";
		using var streamReader = new StreamReader(request.InputStream, request.ContentEncoding);
		using var jsonDocument = JsonDocument.Parse(streamReader.ReadToEnd());
		CaptchaHandler.SetCaptchaResult(jsonDocument.RootElement.GetProperty("data").GetString() ?? "");
		WriteStringToResponse(response, "{\"result\":\"resolved\"}");
		Task.Delay(1000).ContinueWith(delegate
		{
			Stop();
		});
	}

	// Token: 0x06000160 RID: 352 RVA: 0x00007A38 File Offset: 0x00005C38
	private static void WriteStringToResponse(HttpListenerResponse response, string content)
	{
		var bytes = Encoding.UTF8.GetBytes(content);
		response.ContentLength64 = bytes.Length;
		response.OutputStream.Write(bytes, 0, bytes.Length);
	}

	// Token: 0x0400007F RID: 127
	private readonly HttpListener _httpListener;

	// Token: 0x04000080 RID: 128
	private bool _isRunning;

	// Token: 0x04000081 RID: 129
	public readonly int Port;
}