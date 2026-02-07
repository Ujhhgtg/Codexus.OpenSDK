using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Codexus.Cipher.Utils.Http;

public class HttpWrapper(
	string domain = "",
	Action<HttpWrapper.HttpWrapperBuilder>? extension = null,
	HttpClientHandler? handler = null,
	Version? version = null)
	: IDisposable
{
	public class HttpWrapperBuilder(string domain, string url, string body)
	{
		private readonly Dictionary<string, string> _headers = new();

		public string Domain => domain;

		public string Url => url;

		public string Body => body;

		public HttpWrapperBuilder AddHeader(Dictionary<string, string> headers)
		{
			foreach (var header in headers)
			{
				_headers.Add(header.Key, header.Value);
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
	}

	private readonly HttpClient _httpClient = new(handler ?? new HttpClientHandler());

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

	public async Task<HttpResponseMessage> PostAsync(string url, string body, string contentType = "application/json", Action<HttpWrapperBuilder>? block = null)
	{
		using var request = new HttpRequestMessage(HttpMethod.Post, domain + url);
		if (version != null)
		{
			request.Version = version;
		}
		request.Content = new StringContent(body, Encoding.UTF8, contentType);
		if (block == null)
		{
			return await _httpClient.SendAsync(request);
		}
		var builder = new HttpWrapperBuilder(domain, url, body);
		extension?.Invoke(builder);
		block(builder);
		foreach (var header in builder.GetHeaders())
		{
			request.Headers.TryAddWithoutValidation(header.Key, header.Value);
		}
		return await _httpClient.SendAsync(request);
	}

	public async Task<HttpResponseMessage> PostAsync(string url, byte[] body, Action<HttpWrapperBuilder>? block = null)
	{
		using var request = new HttpRequestMessage(HttpMethod.Post, domain + url);
		if (version != null)
		{
			request.Version = version;
		}
		request.Content = new ByteArrayContent(body);
		if (block == null)
		{
			return await _httpClient.SendAsync(request);
		}
		var builder = new HttpWrapperBuilder(domain, url, Encoding.UTF8.GetString(body));
		extension?.Invoke(builder);
		block(builder);
		foreach (var header in builder.GetHeaders())
		{
			request.Headers.TryAddWithoutValidation(header.Key, header.Value);
		}
		return await _httpClient.SendAsync(request);
	}

	public async Task<HttpResponseMessage> GetAsync(string url, Action<HttpWrapperBuilder>? block = null)
	{
		var request = new HttpRequestMessage
		{
			Method = HttpMethod.Get,
			RequestUri = new Uri(domain + url)
		};
		if (version != null)
		{
			request.Version = version;
		}
		if (block == null)
		{
			return await _httpClient.SendAsync(request);
		}
		var builder = new HttpWrapperBuilder(domain, url, "");
		extension?.Invoke(builder);
		block(builder);
		foreach (var header in builder.GetHeaders())
		{
			request.Headers.TryAddWithoutValidation(header.Key, header.Value);
		}
		return await _httpClient.SendAsync(request);
	}
}
