using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Codexus.Cipher.Entities.MgbSdk;
using Codexus.Cipher.Utils.Http;

namespace Codexus.Cipher.Protocol;

// Token: 0x02000023 RID: 35
public class MgbSdk(string gameId) : IDisposable
{
	// Token: 0x060000CF RID: 207 RVA: 0x000050AF File Offset: 0x000032AF
	// public MgbSdk(string gameId)
	// {
	// }

	// Token: 0x060000D0 RID: 208 RVA: 0x000050D2 File Offset: 0x000032D2
	public void Dispose()
	{
		_sdk.Dispose();
		GC.SuppressFinalize(this);
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x000050E8 File Offset: 0x000032E8
	public string GenerateSAuth(string deviceId, string userid, string sdkUid, string sessionId, string timestamp, string channel, string platform = "pc")
	{
		var text = sessionId.ToUpper();
		return JsonSerializer.Serialize(new EntityMgbSdkCookie
		{
			Ip = InternalQuery.Gw,
			AimInfo = InternalQuery.ToAimInfo(),
			AppChannel = channel,
			ClientLoginSn = deviceId.ToUpper(),
			DeviceId = deviceId.ToUpper(),
			GameId = gameId,
			LoginChannel = channel,
			SdkUid = sdkUid,
			SessionId = text,
			Timestamp = timestamp,
			Platform = platform,
			SourcePlatform = platform,
			Udid = deviceId.ToUpper(),
			UserId = userid
		}, DefaultOptions);
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x000051A4 File Offset: 0x000033A4
	public async Task AuthSession(string cookie)
	{
		var httpResponseMessage2 = await _sdk.PostAsync("/" + gameId + "/sdk/uni_sauth", cookie);
		if (!httpResponseMessage2.IsSuccessStatusCode)
		{
			throw new HttpRequestException(httpResponseMessage2.ReasonPhrase);
		}
		var text = await httpResponseMessage2.Content.ReadAsStringAsync();
		var dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(text);
		if (dictionary["code"].ToString() != "200")
		{
			const string text2 = "Status: ";
			var obj = dictionary["status"];
			throw new HttpRequestException(text2 + obj);
		}
	}

	// Token: 0x0400005F RID: 95
	private readonly HttpWrapper _sdk = new("https://mgbsdk.matrix.netease.com");

	// Token: 0x04000060 RID: 96
	private static readonly JsonSerializerOptions DefaultOptions = new()
	{
		Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
	};
}