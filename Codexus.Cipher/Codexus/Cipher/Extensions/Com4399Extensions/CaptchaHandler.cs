// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Net;
// using System.Runtime.CompilerServices;
// using System.Text.Json;
// using System.Threading;
// using System.Threading.Tasks;
// using Codexus.Development.SDK.Utils;
// using Serilog;
//
// namespace Codexus.Cipher.Extensions.Com4399Extensions;
//
// // Token: 0x0200002D RID: 45
// public static class CaptchaHandler
// {
// 	// Token: 0x1700002A RID: 42
// 	// (get) Token: 0x06000148 RID: 328 RVA: 0x00007389 File Offset: 0x00005589
// 	// (set) Token: 0x06000149 RID: 329 RVA: 0x00007390 File Offset: 0x00005590
// 	public static string BackgroundImageBase64 { get; private set; } = "";
//
// 	// Token: 0x1700002B RID: 43
// 	// (get) Token: 0x0600014A RID: 330 RVA: 0x00007398 File Offset: 0x00005598
// 	// (set) Token: 0x0600014B RID: 331 RVA: 0x0000739F File Offset: 0x0000559F
// 	public static string SliderImageBase64 { get; private set; } = "";
//
// 	// Token: 0x1700002C RID: 44
// 	// (get) Token: 0x0600014C RID: 332 RVA: 0x000073A7 File Offset: 0x000055A7
// 	// (set) Token: 0x0600014D RID: 333 RVA: 0x000073AE File Offset: 0x000055AE
// 	public static string ClickableText { get; private set; } = "";
//
// 	// Token: 0x1700002D RID: 45
// 	// (get) Token: 0x0600014E RID: 334 RVA: 0x000073B6 File Offset: 0x000055B6
// 	// (set) Token: 0x0600014F RID: 335 RVA: 0x000073BD File Offset: 0x000055BD
// 	public static string CurrentCaptchaType { get; private set; } = "jigsaw";
//
// 	// Token: 0x06000150 RID: 336 RVA: 0x000073C5 File Offset: 0x000055C5
// 	public static void SetCaptchaResult(string data)
// 	{
// 		var captchaTaskCompletionSource = _captchaTaskCompletionSource;
// 		captchaTaskCompletionSource?.SetResult(data);
// 	}
//
// 	// Token: 0x06000151 RID: 337 RVA: 0x000073DC File Offset: 0x000055DC
// 	private static async Task<string> WaitForCaptchaCompletionAsync(int timeoutSeconds = 300)
// 	{
// 		_captchaTaskCompletionSource = new TaskCompletionSource<string>();
// 		_cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds));
// 		_cancellationTokenSource.Token.Register(delegate
// 		{
// 			var captchaTaskCompletionSource = _captchaTaskCompletionSource;
// 			captchaTaskCompletionSource?.TrySetCanceled();
// 		});
// 		string text2;
// 		try
// 		{
// 			var text = await _captchaTaskCompletionSource.Task;
// 			text2 = text;
// 		}
// 		catch (OperationCanceledException)
// 		{
// 			var defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 1);
// 			defaultInterpolatedStringHandler.AppendLiteral("Captcha verification timeout after ");
// 			defaultInterpolatedStringHandler.AppendFormatted(timeoutSeconds);
// 			defaultInterpolatedStringHandler.AppendLiteral(" seconds");
// 			throw new TimeoutException(defaultInterpolatedStringHandler.ToStringAndClear());
// 		}
// 		return text2;
// 	}
//
// 	// Token: 0x06000152 RID: 338 RVA: 0x00007420 File Offset: 0x00005620
// 	private static void ResetCaptchaState()
// 	{
// 		BackgroundImageBase64 = "";
// 		SliderImageBase64 = "";
// 		ClickableText = "";
// 	}
//
// 	// Token: 0x06000153 RID: 339 RVA: 0x00007444 File Offset: 0x00005644
// 	[DebuggerStepThrough]
// 	public static Task<string> HandleLoginCaptchaAsync(string resultJson)
// 	{
// 		CaptchaHandler.HandleLoginCaptchaAsync_d__21 handleLoginCaptchaAsync_d__ = new CaptchaHandler.HandleLoginCaptchaAsync_d__21();
// 		handleLoginCaptchaAsync_d__.__t__builder = AsyncTaskMethodBuilder<string>.Create();
// 		handleLoginCaptchaAsync_d__.resultJson = resultJson;
// 		handleLoginCaptchaAsync_d__.__1__state = -1;
// 		handleLoginCaptchaAsync_d__.__t__builder.Start<CaptchaHandler.HandleLoginCaptchaAsync_d__21>(ref handleLoginCaptchaAsync_d__);
// 		return handleLoginCaptchaAsync_d__.__t__builder.Task;
// 	}
//
// 	// Token: 0x06000154 RID: 340 RVA: 0x00007488 File Offset: 0x00005688
// 	[DebuggerStepThrough]
// 	private static Task<string> HandleJigsawCaptchaAsync(string captchaUrl)
// 	{
// 		CaptchaHandler.HandleJigsawCaptchaAsync_d__22 handleJigsawCaptchaAsync_d__ = new CaptchaHandler.HandleJigsawCaptchaAsync_d__22();
// 		handleJigsawCaptchaAsync_d__.__t__builder = AsyncTaskMethodBuilder<string>.Create();
// 		handleJigsawCaptchaAsync_d__.captchaUrl = captchaUrl;
// 		handleJigsawCaptchaAsync_d__.__1__state = -1;
// 		handleJigsawCaptchaAsync_d__.__t__builder.Start<CaptchaHandler.HandleJigsawCaptchaAsync_d__22>(ref handleJigsawCaptchaAsync_d__);
// 		return handleJigsawCaptchaAsync_d__.__t__builder.Task;
// 	}
//
// 	// Token: 0x06000155 RID: 341 RVA: 0x000074CC File Offset: 0x000056CC
// 	[DebuggerStepThrough]
// 	private static Task<string> HandleClickCaptchaAsync(string captchaUrl)
// 	{
// 		CaptchaHandler.HandleClickCaptchaAsync_d__23 handleClickCaptchaAsync_d__ = new CaptchaHandler.HandleClickCaptchaAsync_d__23();
// 		handleClickCaptchaAsync_d__.__t__builder = AsyncTaskMethodBuilder<string>.Create();
// 		handleClickCaptchaAsync_d__.captchaUrl = captchaUrl;
// 		handleClickCaptchaAsync_d__.__1__state = -1;
// 		handleClickCaptchaAsync_d__.__t__builder.Start<CaptchaHandler.HandleClickCaptchaAsync_d__23>(ref handleClickCaptchaAsync_d__);
// 		return handleClickCaptchaAsync_d__.__t__builder.Task;
// 	}
//
// 	// Token: 0x06000156 RID: 342 RVA: 0x00007510 File Offset: 0x00005710
// 	private static string BuildCaptchaParameter(string token, string captchaId)
// 	{
// 		return JsonSerializer.Serialize<Dictionary<string, string>>(new Dictionary<string, string>
// 		{
// 			{ "v_token", token },
// 			{ "captcha_id", captchaId },
// 			{ "type", "0" }
// 		}, null);
// 	}
//
// 	// Token: 0x06000157 RID: 343 RVA: 0x00007558 File Offset: 0x00005758
// 	private static async Task<CaptchaHttpServer> StartCaptchaServerAsync()
// 	{
// 		var port = NetworkUtil.GetAvailablePort();
// 		CaptchaHttpServer captchaHttpServer;
// 		for (;;)
// 		{
// 			try
// 			{
// 				var server = new CaptchaHttpServer(port);
// 				await server.StartAsync();
// 				Log.Information("Captcha HTTP server started on port {Port}", port);
// 				var defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 1);
// 				defaultInterpolatedStringHandler.AppendLiteral("http://127.0.0.1:");
// 				defaultInterpolatedStringHandler.AppendFormatted(port);
// 				defaultInterpolatedStringHandler.AppendLiteral("/");
// 				var fileName = defaultInterpolatedStringHandler.ToStringAndClear();
// 				Process.Start(new ProcessStartInfo
// 				{
// 					FileName = fileName,
// 					UseShellExecute = true
// 				});
// 				captchaHttpServer = server;
// 				break;
// 			}
// 			catch (HttpListenerException)
// 			{
// 				port = NetworkUtil.GetAvailablePort();
// 			}
// 		}
// 		return captchaHttpServer;
// 	}
//
// 	// Token: 0x04000079 RID: 121
// 	private static TaskCompletionSource<string> _captchaTaskCompletionSource;
//
// 	// Token: 0x0400007A RID: 122
// 		
// 	private static CancellationTokenSource _cancellationTokenSource;
// }

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Codexus.Development.SDK.Utils;
using Serilog;

namespace Codexus.Cipher.Extensions.Com4399Extensions;

public static class CaptchaHandler
{
    // Properties used by the UI/Web Server to show the images
    public static string BackgroundImageBase64 { get; private set; } = "";
    public static string SliderImageBase64 { get; private set; } = "";
    public static string ClickableText { get; private set; } = "";
    public static string CurrentCaptchaType { get; private set; } = "jigsaw";

    private static TaskCompletionSource<string> _captchaTaskCompletionSource;
    private static CancellationTokenSource _cancellationTokenSource;

    /// <summary>
    /// Called by the local HTTP server when the user finishes solving the captcha.
    /// </summary>
    public static void SetCaptchaResult(string data)
    {
        _captchaTaskCompletionSource?.TrySetResult(data);
    }

    public static async Task<string> HandleLoginCaptchaAsync(string resultJson)
    {
        // resultJson likely contains the initial captcha token or URL from the API
        // Here you would parse resultJson and decide which handler to call.
        // Simplified logic:
        ResetCaptchaState();
            
        // Logic to launch server and wait
        await StartCaptchaServerAsync();
        return await WaitForCaptchaCompletionAsync();
    }

    private static async Task<string> WaitForCaptchaCompletionAsync(int timeoutSeconds = 300)
    {
        _captchaTaskCompletionSource = new TaskCompletionSource<string>();
        _cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds));

        // Link the cancellation token to the TaskCompletionSource
        using (_cancellationTokenSource.Token.Register(() => _captchaTaskCompletionSource.TrySetCanceled()))
        {
            try
            {
                return await _captchaTaskCompletionSource.Task;
            }
            catch (OperationCanceledException)
            {
                throw new TimeoutException($"Captcha verification timeout after {timeoutSeconds} seconds");
            }
        }
    }

    private static async Task<CaptchaHttpServer> StartCaptchaServerAsync()
    {
        var port = NetworkUtil.GetAvailablePort();
        while (true)
        {
            try
            {
                var server = new CaptchaHttpServer(port);
                await server.StartAsync();
                    
                Log.Information("Captcha HTTP server started on port {Port}", port);
                    
                var url = $"http://127.0.0.1:{port}/";
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
                    
                return server;
            }
            catch (HttpListenerException)
            {
                port = NetworkUtil.GetAvailablePort(); // Try another port if busy
            }
        }
    }

    private static void ResetCaptchaState()
    {
        BackgroundImageBase64 = "";
        SliderImageBase64 = "";
        ClickableText = "";
    }

    private static string BuildCaptchaParameter(string token, string captchaId)
    {
        var payload = new Dictionary<string, string>
        {
            { "v_token", token },
            { "captcha_id", captchaId },
            { "type", "0" }
        };
        return JsonSerializer.Serialize(payload);
    }
}