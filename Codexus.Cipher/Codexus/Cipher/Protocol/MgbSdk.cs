using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Codexus.Cipher.Entities.MgbSdk;
using Codexus.Cipher.Utils.Http;

namespace Codexus.Cipher.Protocol;

public class MgbSdk(string gameId) : IDisposable
{
    // public MgbSdk(string gameId)
    // {
    // }

    public void Dispose()
    {
        _sdk.Dispose();
        GC.SuppressFinalize(this);
    }

    public string GenerateSAuth(string deviceId, string userid, string sdkUid, string sessionId, string timestamp,
        string channel, string platform = "pc")
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

    public async Task AuthSession(string cookie)
    {
        var httpResponseMessage2 = await _sdk.PostAsync("/" + gameId + "/sdk/uni_sauth", cookie);
        if (!httpResponseMessage2.IsSuccessStatusCode)
            throw new HttpRequestException(httpResponseMessage2.ReasonPhrase);
        var text = await httpResponseMessage2.Content.ReadAsStringAsync();
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(text);
        if (dictionary["code"].ToString() != "200")
        {
            const string text2 = "Status: ";
            var obj = dictionary["status"];
            throw new HttpRequestException(text2 + obj);
        }
    }

    private readonly HttpWrapper _sdk = new("https://mgbsdk.matrix.netease.com");

    private static readonly JsonSerializerOptions DefaultOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
}