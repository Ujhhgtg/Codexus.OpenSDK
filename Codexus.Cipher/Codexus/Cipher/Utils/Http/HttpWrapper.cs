using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Codexus.Cipher.Utils.Http;

// Token: 0x02000015 RID: 21
public class HttpWrapper(
	string domain = "",
	Action<HttpWrapper.HttpWrapperBuilder> extension = null,
	HttpClientHandler handler = null,
	Version version = null)
	: IDisposable
{
	// Token: 0x06000071 RID: 113 RVA: 0x0000338C File Offset: 0x0000158C
	public void Dispose()
	{
		_httpClient.Dispose();
		GC.SuppressFinalize(this);
	}

	// Token: 0x06000072 RID: 114 RVA: 0x000033A2 File Offset: 0x000015A2
	public HttpClient GetClient()
	{
		return _httpClient;
	}

	// Token: 0x06000073 RID: 115 RVA: 0x000033AC File Offset: 0x000015AC
	public async Task<HttpResponseMessage> PostAsync(string url, string body, Action<HttpWrapperBuilder> block)
	{
		return await PostAsync(url, body, "application/json", block);
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00003408 File Offset: 0x00001608
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

	// Token: 0x06000075 RID: 117 RVA: 0x0000346C File Offset: 0x0000166C
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

	// Token: 0x06000076 RID: 118 RVA: 0x000034C8 File Offset: 0x000016C8
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

	// Token: 0x0400003E RID: 62
	private readonly HttpClient _httpClient = new(handler ?? new HttpClientHandler());

	// Token: 0x0400003F RID: 63
	private readonly string _domain = domain;

	// Token: 0x04000040 RID: 64

	// Token: 0x04000041 RID: 65

	// Token: 0x020000B8 RID: 184
	public class HttpWrapperBuilder(string domain, string url, string body)
	{

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0000C554 File Offset: 0x0000A754
		public string Domain => domain;

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0000C55C File Offset: 0x0000A75C
		public string Url => url;

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0000C564 File Offset: 0x0000A764
		public string Body => body;

		// Token: 0x0600069B RID: 1691 RVA: 0x0000C56C File Offset: 0x0000A76C
		public HttpWrapperBuilder AddHeader(Dictionary<string, string> headers)
		{
			foreach (var keyValuePair in headers)
			{
				_headers.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return this;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		public HttpWrapperBuilder AddHeader(string key, string value)
		{
			_headers.Add(key, value);
			return this;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0000C5FC File Offset: 0x0000A7FC
		public HttpWrapperBuilder UserAgent(string userAgent)
		{
			_headers.Add("User-Agent", userAgent);
			return this;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0000C624 File Offset: 0x0000A824
		public Dictionary<string, string> GetHeaders()
		{
			return _headers;
		}

		// Token: 0x04000369 RID: 873
		private readonly Dictionary<string, string> _headers = new();
	}
}