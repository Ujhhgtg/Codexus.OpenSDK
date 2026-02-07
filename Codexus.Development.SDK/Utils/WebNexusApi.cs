using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Codexus.Development.SDK.Entities;
using Serilog;

namespace Codexus.Development.SDK.Utils;
public sealed class WebNexusApi : IDisposable
{

	// public WebNexusApi(string nexusToken,  Action<string, int>? progressCallback = null)
	// {
	// 	_httpClient = new HttpClient
	// 	{
	// 		BaseAddress = new Uri("https://api.codexus.today/")
	// 	};
	// 	_httpClient.DefaultRequestHeaders.Accept.Clear();
	// 	_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
	// 	_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + nexusToken);
	// 	var action = progressCallback;
	// 	if (progressCallback == null && (action = WebNexusApi.func) == null)
	// 	{
	// 		action = (WebNexusApi.func = delegate
	// 		{
	// 		});
	// 	}
	// 	_progressCallback = action;
	// }
	public WebNexusApi(string nexusToken, Action<string, int>? progressCallback = null)
	{
		// Initialize HttpClient
		_httpClient = new HttpClient
		{
			BaseAddress = new Uri("https://api.codexus.today/")
		};

		// Set Accept header to application/json
		_httpClient.DefaultRequestHeaders.Accept.Clear();
		_httpClient.DefaultRequestHeaders.Accept.Add(
			new MediaTypeWithQualityHeaderValue("application/json")
		);

		// Set Authorization: Bearer <nexusToken>
		_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + nexusToken);

		// Assign progressCallback, or a default empty action if null
		// IL shows a cached compiler-generated lambda: (s, i) => { }
		_progressCallback = progressCallback ?? ((_, _) => { });
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	public async Task<string> ComputeAuthenticationBodyAsync(string serverId, long gameId, string gameVersion, string modInfo, string channel, int userId, string handshakeKey)
	{
		var requestData = new { serverId, gameId, gameVersion, modInfo, channel, userId, handshakeKey };
		return await PostAsync("/api/GameCipher/compute/authentication/body", requestData);
	}

	public async Task<string> ComputeHandshakeBodyAsync(int userId, string userToken, string base64Context, string channel, string gameVersion)
	{
		var requestData = new { userId, userToken, base64Context, channel, gameVersion };
		return await PostAsync("/api/GameCipher/compute/authentication/handshake", requestData);
	}

	public async Task<string> PeAccountConvert(string body)
	{
		var requestData = new { body };
		return await PostAsync("/api/PeGameCipher/account/convert", requestData);
	}

	public async Task<string> PeAuthentication(string clientKey, string displayName, string serverId, string gameType, uint userId, string userToken)
	{
		var requestData = new { clientKey, displayName, serverId, gameType, userId, userToken };
		return await PostAsync("/api/PeGameCipher/authentication", requestData);
	}

	public async Task<string> PeHttpEncryptAsync(string body)
	{
		var requestData = new { body };
		return await PostAsync("/api/PeGameCipher/crypto/encrypt", requestData);
	}

	public async Task<string> PeHttpDecryptAsync(string body)
	{
		var requestData = new { body };
		return await PostAsync("/api/PeGameCipher/crypto/decrypt", requestData);
	}

	public string PeMcpGetCheckNum(string dynamicPyCode, string dynamicCheckSalt, string gamePlayerId)
	{
		return JsonSerializer.Deserialize<BodyIn>(PostAsync("/api/PeZeroKnowledgeProof/zkp/get/check-num", new { dynamicPyCode, dynamicCheckSalt, gamePlayerId }).GetAwaiter().GetResult()).Body;
	}

	public string PeMcpGetStartType(string signature, string userId)
	{
		return JsonSerializer.Deserialize<BodyIn>(PostAsync("/api/PeZeroKnowledgeProof/zkp/get/start-type", new { signature, userId }).GetAwaiter().GetResult()).Body;
	}

	public IdCard GetRandomIdCard()
	{
		return JsonSerializer.Deserialize<IdCard>(GetAsync("/api/app/get/app/id-card").GetAwaiter().GetResult());
	}

	public string ComputeCaptchaAsync(byte[] image)
	{
		return JsonSerializer.Deserialize<BodyIn>(PostAsync("/api/app/compute/captcha", new
		{
			body = Convert.ToBase64String(image)
		}).GetAwaiter().GetResult()).Body;
	}

	public async Task<string> GetAppVersionAsync(string appId, string appSecret)
	{
		var requestData = new { appId, appSecret };
		return await PostAsync("/api/app/get/app/version", requestData);
	}

	public byte[] DownloadFile(string url, string pluginId)
	{
		return DownloadFileAsync(url, pluginId).GetAwaiter().GetResult();
	}

	// private Task<string> PostAsync<T>(string endpoint, T requestData, Dictionary<string, string>? headers = null)
	// {
	// 	WebNexusApi.<PostAsync>d__18<T> <PostAsync>d__ = new WebNexusApi.<PostAsync>d__18<T>();
	// 		<PostAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
	// 		<PostAsync>d__.<>4__this = this;
	// 		<PostAsync>d__.endpoint = endpoint;
	// 		<PostAsync>d__.requestData = requestData;
	// 		<PostAsync>d__.headers = headers;
	// 		<PostAsync>d__.<>1__state = -1;
	// 		<PostAsync>d__.<>t__builder.Start<WebNexusApi.<PostAsync>d__18<T>>(ref <PostAsync>d__);
	// 	return <PostAsync>d__.<>t__builder.Task;
	// }
	private async Task<string> PostAsync<T>(
		string endpoint,
		T requestData,
		Dictionary<string, string>? headers = null)
	{
		try
		{
			// Serialize request body
			var stringContent = new StringContent(
				JsonSerializer.Serialize(requestData),
				Encoding.UTF8,
				"application/json");

			// Apply headers to content (note: content headers, not request headers)
			if (headers != null)
			{
				foreach (var header in headers)
				{
					stringContent.Headers.Add(header.Key, header.Value);
				}
			}

			// POST request
			var response = await _httpClient.PostAsync(endpoint, stringContent);

			// Read response body
			var text = await response.Content.ReadAsStringAsync();

			// Special handling for unauthorized
			if (response.StatusCode == HttpStatusCode.Unauthorized)
			{
				Log.Warning(
					"Warning: Your subscription time has expired, or the AccessToken has expired, try refreshing.");
			}

			// Success path
			if (response.IsSuccessStatusCode)
			{
				return text;
			}

			// Failure path
			Log.Error(
				"Request failed with status code {StatusCode}. Response: {Data}",
				response.StatusCode,
				text);

			throw new Exception(
				$"Request failed with status code {response.StatusCode}. Response: {text}");
		}
		catch (Exception ex)
		{
			// Wrap any other exception
			throw new Exception("Error processing API response: " + ex.Message);
		}
	}

	// public async Task<string> GetAsync(string endpoint, Dictionary<string, string>? headers = null)
	// {
	// 	string text3;
	// 	try
	// 	{
	// 		var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, endpoint);
	// 		var flag = headers != null;
	// 		if (flag)
	// 		{
	// 			foreach (var header in headers)
	// 			{
	// 				httpRequestMessage.Headers.Add(header.Key, header.Value);
	// 				header = default;
	// 			}
	// 			var enumerator = default(Dictionary<string, string>.Enumerator);
	// 		}
	// 		var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
	// 		var response = httpResponseMessage;
	// 		try
	// 		{
	// 			var text2 = await response.Content.ReadAsStringAsync();
	// 			var text = text2;
	// 			if (response.StatusCode == HttpStatusCode.Unauthorized)
	// 			{
	// 				Log.Warning("Warning: Your subscription time has expired, or the AccessToken has expired, try refreshing.", []);
	// 			}
	// 			if (!response.IsSuccessStatusCode)
	// 			{
	// 				var defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(44, 2);
	// 				defaultInterpolatedStringHandler.AppendLiteral("Request failed with status code ");
	// 				defaultInterpolatedStringHandler.AppendFormatted(response.StatusCode);
	// 				defaultInterpolatedStringHandler.AppendLiteral(". Response: ");
	// 				defaultInterpolatedStringHandler.AppendFormatted(text);
	// 				throw new HttpRequestException(defaultInterpolatedStringHandler.ToStringAndClear());
	// 			}
	// 			text3 = text;
	// 		}
	// 		finally
	// 		{
	// 			if (response != null)
	// 			{
	// 				((IDisposable)response).Dispose();
	// 			}
	// 		}
	// 	}
	// 	catch (Exception ex2)
	// 	{
	// 		throw new Exception("Error processing API response: " + ex2.Message);
	// 	}
	// 	return text3;
	// }
	public async Task<string> GetAsync(
		string endpoint,
		Dictionary<string, string>? headers = null)
	{
		try
		{
			using var httpRequestMessage = new HttpRequestMessage(
				HttpMethod.Get,
				endpoint);

			if (headers != null)
			{
				foreach (var header in headers)
				{
					httpRequestMessage.Headers.Add(header.Key, header.Value);
				}
			}

			using var response = await _httpClient.SendAsync(httpRequestMessage);

			var text = await response.Content.ReadAsStringAsync();

			if (response.StatusCode == HttpStatusCode.Unauthorized)
			{
				Log.Warning(
					"Warning: Your subscription time has expired, or the AccessToken has expired, try refreshing.");
			}

			if (!response.IsSuccessStatusCode)
			{
				throw new HttpRequestException(
					$"Request failed with status code {response.StatusCode}. Response: {text}");
			}

			return text;
		}
		catch (Exception ex)
		{
			throw new Exception("Error processing API response: " + ex.Message);
		}
	}

	// private async Task<byte[]> DownloadFileAsync(string endpoint, string id, Dictionary<string, string>? headers = null)
	// {
	// 	var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, endpoint);
	// 	var flag = headers != null;
	// 	if (flag)
	// 	{
	// 		foreach (var header in headers)
	// 		{
	// 			httpRequestMessage.Headers.Add(header.Key, header.Value);
	// 			header = default;
	// 		}
	// 		var enumerator = default(Dictionary<string, string>.Enumerator);
	// 	}
	// 	byte[] array;
	// 	try
	// 	{
	// 		var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage, HttpCompletionOption.ResponseHeadersRead);
	// 		var response = httpResponseMessage;
	// 		try
	// 		{
	// 			if (response.StatusCode == HttpStatusCode.Unauthorized)
	// 			{
	// 				Log.Warning("Warning: Your subscription time has expired,\nor the AccessToken has expired, try refreshing.", []);
	// 			}
	// 			if (!response.IsSuccessStatusCode)
	// 			{
	// 				var defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(44, 2);
	// 				defaultInterpolatedStringHandler.AppendLiteral("Request failed with status code ");
	// 				defaultInterpolatedStringHandler.AppendFormatted(response.StatusCode);
	// 				defaultInterpolatedStringHandler.AppendLiteral(". Response: ");
	// 				defaultInterpolatedStringHandler.AppendFormatted<HttpContent>(response.Content);
	// 				throw new HttpRequestException(defaultInterpolatedStringHandler.ToStringAndClear());
	// 			}
	// 			var totalBytes = response.Content.Headers.ContentLength.GetValueOrDefault(-1L);
	// 			var stream = await response.Content.ReadAsStreamAsync();
	// 			var contentStream = stream;
	// 			object obj = null;
	// 			byte[] result;
	// 			try
	// 			{
	// 				byte[] buffer;
	// 				using (var memoryStream = new MemoryStream())
	// 				{
	// 					buffer = new byte[8192];
	// 					var totalBytesRead = 0L;
	// 					for (;;)
	// 					{
	// 						var num = await contentStream.ReadAsync(buffer);
	// 						int bytesRead;
	// 						if ((bytesRead = num) <= 0)
	// 						{
	// 							break;
	// 						}
	// 						await memoryStream.WriteAsync(buffer.AsMemory(0, bytesRead));
	// 						totalBytesRead += bytesRead;
	// 						if (totalBytes > 0L)
	// 						{
	// 							var arg = (int)(totalBytesRead * 100L / totalBytes);
	// 							_progressCallback(id, arg);
	// 						}
	// 					}
	// 					if (totalBytes <= 0L)
	// 					{
	// 						_progressCallback(id, 100);
	// 					}
	// 					result = memoryStream.ToArray();
	// 				}
	// 				MemoryStream memoryStream = null;
	// 				buffer = null;
	// 			}
	// 			catch
	// 			{
	// 			}
	// 			if (contentStream != null)
	// 			{
	// 				await contentStream.DisposeAsync();
	// 			}
	// 			var obj2 = obj;
	// 			if (obj2 != null)
	// 			{
	// 				var ex2 = obj2 as Exception;
	// 				if (ex2 == null)
	// 				{
	// 					throw obj2;
	// 				}
	// 				ExceptionDispatchInfo.Capture(ex2).Throw();
	// 			}
	// 			obj = null;
	// 			contentStream = null;
	// 			array = result;
	// 		}
	// 		finally
	// 		{
	// 			if (response != null)
	// 			{
	// 				((IDisposable)response).Dispose();
	// 			}
	// 		}
	// 	}
	// 	catch (HttpRequestException ex)
	// 	{
	// 		throw new Exception("Error processing API response: " + ex.Message);
	// 	}
	// 	return array;
	// }
	private async Task<byte[]> DownloadFileAsync(
	    string endpoint,
	    string id,
	    Dictionary<string, string>? headers = null)
	{
	    HttpRequestMessage? httpRequestMessage = null;
	    HttpResponseMessage? response = null;

	    try
	    {
	        httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, endpoint);

	        if (headers != null)
	        {
	            foreach (var header in headers)
	            {
	                httpRequestMessage.Headers.Add(header.Key, header.Value);
	            }
	        }

	        response = await _httpClient.SendAsync(
	            httpRequestMessage,
	            HttpCompletionOption.ResponseHeadersRead);

	        if (response.StatusCode == HttpStatusCode.Unauthorized)
	        {
	            Log.Warning(
	                "Warning: Your subscription time has expired,\n" +
	                "or the AccessToken has expired, try refreshing.");
	        }

	        if (!response.IsSuccessStatusCode)
	        {
	            throw new HttpRequestException(
	                $"Request failed with status code {response.StatusCode}. Response: {response.Content}");
	        }

	        var totalBytes =
	            response.Content.Headers.ContentLength ?? -1;

	        await using var contentStream =
	            await response.Content.ReadAsStreamAsync();

	        using var memoryStream = new MemoryStream();

	        var buffer = new byte[8192];
	        long totalBytesRead = 0;

	        while (true)
	        {
	            var bytesRead = await contentStream.ReadAsync(buffer);

	            if (bytesRead <= 0)
	            {
	                break;
	            }

	            await memoryStream.WriteAsync(
	                buffer.AsMemory(0, bytesRead));

	            totalBytesRead += bytesRead;

	            if (totalBytes > 0)
	            {
	                var progress =
	                    (int)(totalBytesRead * 100 / totalBytes);

	                _progressCallback?.Invoke(id, progress);
	            }
	        }

	        if (totalBytes <= 0)
	        {
	            _progressCallback?.Invoke(id, 100);
	        }

	        return memoryStream.ToArray();
	    }
	    catch (Exception ex)
	    {
	        throw new Exception(
	            "Error processing API response: " + ex.Message);
	    }
	    finally
	    {
	        response?.Dispose();
	        httpRequestMessage?.Dispose();
	    }
	}

	private void Dispose(bool disposing)
	{
		var flag = !_disposed;
		if (flag)
		{
			if (disposing)
			{
				_httpClient.Dispose();
			}
			_disposed = true;
		}
	}

	~WebNexusApi()
	{
		Dispose(false);
	}
	private const string BaseUrl = "https://api.codexus.today/";
	private readonly HttpClient _httpClient;
	private readonly Action<string, int> _progressCallback;
	private bool _disposed;
}