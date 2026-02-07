using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Codexus.Cipher.Utils.Http;
public class HttpWrapper(
	string domain = "",
	Action<HttpWrapper.HttpWrapperBuilder> extension = null,
	HttpClientHandler handler = null,
	Version version = null)
	: IDisposable
{

	public void Dispose()
	{
		_httpClient.Dispose();
		GC.SuppressFinalize(this);
	}

	public HttpClient GetClient()
	{
		return _httpClient;
	}

	public async Task<HttpResponseMessage> PostAsync(string url, string body, Action<HttpWrapperBuilder> block)
	{
		return await PostAsync(url, body, "application/json", block);
	}

	public async Task<HttpResponseMessage> PostAsync(string url, string body, string contentType = "application/json", Action<HttpWrapperBuilder> block = null)
	{
		HttpResponseMessage httpResponseMessage2;
		using var request = new HttpRequestMessage(HttpMethod.Post, _domain + url);
		var flag = version != null;
		if (flag)
		{
			request.Version = version;
		}
		request.Content = new StringContent(body, Encoding.UTF8, contentType);
		var flag2 = block == null;
		if (flag2)
		{
			var httpResponseMessage = await _httpClient.SendAsync(request);
			httpResponseMessage2 = httpResponseMessage;
		}
		else
		{
			var builder = new HttpWrapperBuilder(_domain, url, body);
			var extension1 = extension;
			if (extension1 != null)
			{
				extension1(builder);
			}
			block(builder);
			foreach (var header in builder.GetHeaders())
			{
				request.Headers.TryAddWithoutValidation(header.Key, header.Value);
			}
			var enumerator = default(Dictionary<string, string>.Enumerator);
			var httpResponseMessage3 = await _httpClient.SendAsync(request);
			httpResponseMessage2 = httpResponseMessage3;
		}

		return httpResponseMessage2;
	}

	public async Task<HttpResponseMessage> PostAsync(string url, byte[] body,  Action<HttpWrapperBuilder> block = null)
	{
		HttpResponseMessage httpResponseMessage2;
		using var request = new HttpRequestMessage(HttpMethod.Post, _domain + url);
		var flag = version != null;
		if (flag)
		{
			request.Version = version;
		}
		request.Content = new ByteArrayContent(body);
		var flag2 = block == null;
		if (flag2)
		{
			var httpResponseMessage = await _httpClient.SendAsync(request);
			httpResponseMessage2 = httpResponseMessage;
		}
		else
		{
			var builder = new HttpWrapperBuilder(_domain, url, Encoding.UTF8.GetString(body));
			var extension1 = extension;
			if (extension1 != null)
			{
				extension1(builder);
			}
			block(builder);
			foreach (var header in builder.GetHeaders())
			{
				request.Headers.TryAddWithoutValidation(header.Key, header.Value);
			}
			var enumerator = default(Dictionary<string, string>.Enumerator);
			var httpResponseMessage3 = await _httpClient.SendAsync(request);
			httpResponseMessage2 = httpResponseMessage3;
		}

		return httpResponseMessage2;
	}

	public async Task<HttpResponseMessage> GetAsync(string url,  Action<HttpWrapperBuilder> block = null)
	{
		var request = new HttpRequestMessage
		{
			Method = HttpMethod.Get,
			RequestUri = new Uri(_domain + url)
		};
		var flag = version != null;
		if (flag)
		{
			request.Version = version;
		}
		var flag2 = block == null;
		HttpResponseMessage httpResponseMessage2;
		if (flag2)
		{
			var httpResponseMessage = await _httpClient.SendAsync(request);
			httpResponseMessage2 = httpResponseMessage;
		}
		else
		{
			var builder = new HttpWrapperBuilder(_domain, url, "");
			var extension1 = extension;
			if (extension1 != null)
			{
				extension1(builder);
			}
			block(builder);
			foreach (var header in builder.GetHeaders())
			{
				request.Headers.TryAddWithoutValidation(header.Key, header.Value);
			}
			var enumerator = default(Dictionary<string, string>.Enumerator);
			var httpResponseMessage3 = await _httpClient.SendAsync(request);
			httpResponseMessage2 = httpResponseMessage3;
		}
		return httpResponseMessage2;
	}
	private readonly HttpClient _httpClient = new(handler ?? new HttpClientHandler());
	private readonly string _domain = domain;
	public class HttpWrapperBuilder(string domain, string url, string body)
	{
		public string Domain => domain;
		public string Url => url;
		public string Body => body;

		public HttpWrapperBuilder AddHeader(Dictionary<string, string> headers)
		{
			foreach (var keyValuePair in headers)
			{
				_headers.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return this;
		}

		public HttpWrapperBuilder AddHeader(string key, string value)
		{
			_headers.Add(key, value);
			return this;
		}

		public HttpWrapperBuilder UserAgent(string userAgent)
		{
			_headers.Add("User-Agent", userAgent);
			return this;
		}

		public Dictionary<string, string> GetHeaders()
		{
			return _headers;
		}
		private readonly Dictionary<string, string> _headers = new();
	}
}