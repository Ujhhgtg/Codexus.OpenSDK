using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Codexus.Cipher.Entities.MPay.WhoAmi;
using Codexus.Cipher.Utils.Http;

namespace Codexus.Cipher.Protocol;

// Token: 0x02000022 RID: 34
public static class InternalQuery
{
	// Token: 0x17000023 RID: 35
	// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004F09 File Offset: 0x00003109
	// (set) Token: 0x060000C7 RID: 199 RVA: 0x00004F10 File Offset: 0x00003110
	public static string Gw { get; private set; } = "";

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004F18 File Offset: 0x00003118
	// (set) Token: 0x060000C9 RID: 201 RVA: 0x00004F1F File Offset: 0x0000311F
	public static EntityWhoAmi WhoAmi { get; private set; } = new();

	// Token: 0x060000CA RID: 202 RVA: 0x00004F28 File Offset: 0x00003128
	public static void Initialize()
	{
		WhoAmi = GetWhoAmi().GetAwaiter().GetResult();
		Gw = GetGw().GetAwaiter().GetResult();
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00004F68 File Offset: 0x00003168
	public static string ToAimInfo()
	{
		var geoLocationData = JsonSerializer.Deserialize<GeoLocationData>(Convert.FromBase64String(WhoAmi.Payload));
		return JsonSerializer.Serialize(new EntityAimInfo
		{
			Code1 = geoLocationData.Code1,
			Code2 = geoLocationData.Code2,
			Code3 = geoLocationData.Code3,
			Code4 = geoLocationData.Code4,
			Isp = geoLocationData.Isp,
			Aim = geoLocationData.Ip
		}, DefaultOptions);
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00004FF4 File Offset: 0x000031F4
	private static async Task<string> GetGw()
	{
		var httpResponseMessage = await Client.GetAsync("http://nstool.netease.com/internalquery", delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.UserAgent("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
		});
		httpResponseMessage.EnsureSuccessStatusCode();
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		var dictionary = new Dictionary<string, string>();
		var array = text.Split('\n');
		foreach (var str in array)
		{
			var array2 = str.Split('=');
			if (array2.Length == 2)
			{
				dictionary[array2[0].Trim()] = array2[1].Trim();
			}
		}
		return dictionary["gw"];
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00005034 File Offset: 0x00003234
	private static async Task<EntityWhoAmi> GetWhoAmi()
	{
		var httpResponseMessage = await Client.GetAsync("https://whoami.nie.netease.com/v6", delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader("X-AUTH-PRODUCT", "g0");
			builder.AddHeader("X-AUTH-TOKEN", "token.efa8zUW6sxjR");
			builder.AddHeader("X-IPDB-LOCALE", "en");
			builder.AddHeader("X-PROJECT_CODE", "x19");
		});
		httpResponseMessage.EnsureSuccessStatusCode();
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<EntityWhoAmi>(text, DefaultOptions);
	}

	// Token: 0x0400005A RID: 90
	private static readonly HttpWrapper Client = new();

	// Token: 0x0400005B RID: 91
	private static readonly JsonSerializerOptions DefaultOptions = new()
	{
		Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
	};
}