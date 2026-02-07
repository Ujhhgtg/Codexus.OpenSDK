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

public class CaptchaHttpServer
{
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
        if (_isRunning) return Task.CompletedTask;

        _httpListener.Start();
        _isRunning = true;

        // This launches the background loop (the state machine you provided)
        Task.Run(async () =>
        {
            while (_isRunning && _httpListener.IsListening)
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
        });

        return Task.CompletedTask;
    }

    public void Stop()
    {
        var isRunning = _isRunning;
        if (isRunning)
        {
            _isRunning = false;
            _httpListener.Stop();
        }
    }

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
                    if (text != "index.html")
                    {
                        if (text != "img.json")
                        {
                            if (text != "resolve") goto IL_0112;
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

    private static void HandleIndexRequest(HttpListenerResponse response)
    {
        var text = CaptchaHandler.CurrentCaptchaType == "jigsaw"
            ? CaptchaResources.JigsawCaptchaHtml
            : CaptchaResources.ClickCaptchaHtml;
        response.ContentType = "text/html; charset=utf-8";
        WriteStringToResponse(response, text);
    }

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

    private void HandleResolveRequest(HttpListenerRequest request, HttpListenerResponse response)
    {
        response.ContentType = "application/json";
        using var streamReader = new StreamReader(request.InputStream, request.ContentEncoding);
        using var jsonDocument = JsonDocument.Parse(streamReader.ReadToEnd());
        CaptchaHandler.SetCaptchaResult(jsonDocument.RootElement.GetProperty("data").GetString() ?? "");
        WriteStringToResponse(response, "{\"result\":\"resolved\"}");
        Task.Delay(1000).ContinueWith(delegate { Stop(); });
    }

    private static void WriteStringToResponse(HttpListenerResponse response, string content)
    {
        var bytes = Encoding.UTF8.GetBytes(content);
        response.ContentLength64 = bytes.Length;
        response.OutputStream.Write(bytes, 0, bytes.Length);
    }

    private readonly HttpListener _httpListener;
    private bool _isRunning;
    public readonly int Port;
}