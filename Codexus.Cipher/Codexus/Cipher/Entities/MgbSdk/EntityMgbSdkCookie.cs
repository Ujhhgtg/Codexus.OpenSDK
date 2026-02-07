using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MgbSdk;

public class EntityMgbSdkCookie
{
    [JsonPropertyName("timestamp")] public string Timestamp { get; set; }
    [JsonPropertyName("userid")] public string UserId { get; set; }
    [JsonPropertyName("realname")] public string RealName { get; set; } = "{\"realname_type\":\"0\"}";
    [JsonPropertyName("gameid")] public string GameId { get; set; }
    [JsonPropertyName("login_channel")] public string LoginChannel { get; set; }
    [JsonPropertyName("app_channel")] public string AppChannel { get; set; }
    [JsonPropertyName("platform")] public string Platform { get; set; } = "pc";
    [JsonPropertyName("sdkuid")] public string SdkUid { get; set; }
    [JsonPropertyName("sessionid")] public string SessionId { get; set; }
    [JsonPropertyName("sdk_version")] public string SdkVersion { get; set; } = "1.0.0";
    [JsonPropertyName("udid")] public string Udid { get; set; }
    [JsonPropertyName("deviceid")] public string DeviceId { get; set; }

    [JsonPropertyName("aim_info")]
    public string AimInfo { get; set; } = "{\"aim\":\"127.0.0.1\",\"tz\":\"+0800\",\"tzid\":\"\",\"country\":\"CN\"}";

    [JsonPropertyName("client_login_sn")] public string ClientLoginSn { get; set; }
    [JsonPropertyName("gas_token")] public string GasToken { get; set; } = string.Empty;
    [JsonPropertyName("source_platform")] public string SourcePlatform { get; set; } = "pc";
    [JsonPropertyName("ip")] public string Ip { get; set; } = "127.0.0.1";
}