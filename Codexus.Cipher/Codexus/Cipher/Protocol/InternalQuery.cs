using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Codexus.Cipher.Entities.MPay.WhoAmi;
using Codexus.Cipher.Utils.Http;

namespace Codexus.Cipher.Protocol;

public static class InternalQuery
{
    public static string Gw { get; private set; } = "";
    public static EntityWhoAmi WhoAmi { get; private set; } = new();

    public static void Initialize()
    {
        WhoAmi = GetWhoAmi().GetAwaiter().GetResult();
        Gw = GetGw().GetAwaiter().GetResult();
    }

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

    private static async Task<string> GetGw()
    {
        var httpResponseMessage = await Client.GetAsync("http://nstool.netease.com/internalquery",
            delegate(HttpWrapper.HttpWrapperBuilder builder)
            {
                builder.UserAgent(
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
            });
        httpResponseMessage.EnsureSuccessStatusCode();
        var text = await httpResponseMessage.Content.ReadAsStringAsync();
        var dictionary = new Dictionary<string, string>();
        var array = text.Split('\n');
        foreach (var str in array)
        {
            var array2 = str.Split('=');
            if (array2.Length == 2) dictionary[array2[0].Trim()] = array2[1].Trim();
        }

        return dictionary["gw"];
    }

    private static async Task<EntityWhoAmi> GetWhoAmi()
    {
        var httpResponseMessage = await Client.GetAsync("https://whoami.nie.netease.com/v6",
            delegate(HttpWrapper.HttpWrapperBuilder builder)
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

    private static readonly HttpWrapper Client = new();

    private static readonly JsonSerializerOptions DefaultOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
}