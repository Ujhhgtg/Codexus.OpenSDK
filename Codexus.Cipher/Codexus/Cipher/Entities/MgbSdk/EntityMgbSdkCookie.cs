using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MgbSdk;
// TODO: [RequiredMember]
public class EntityMgbSdkCookie
{
	// TODO: [RequiredMember]
	[JsonPropertyName("timestamp")]
	public string Timestamp { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("userid")]
	public string UserId { get; set; }
	[JsonPropertyName("realname")]
	public string RealName { get; set; } = "{\"realname_type\":\"0\"}";
	// TODO: [RequiredMember]
	[JsonPropertyName("gameid")]
	public string GameId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("login_channel")]
	public string LoginChannel { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("app_channel")]
	public string AppChannel { get; set; }
	[JsonPropertyName("platform")]
	public string Platform { get; set; } = "pc";
	// TODO: [RequiredMember]
	[JsonPropertyName("sdkuid")]
	public string SdkUid { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("sessionid")]
	public string SessionId { get; set; }
	[JsonPropertyName("sdk_version")]
	public string SdkVersion { get; set; } = "1.0.0";
	// TODO: [RequiredMember]
	[JsonPropertyName("udid")]
	public string Udid { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("deviceid")]
	public string DeviceId { get; set; }
	[JsonPropertyName("aim_info")]
	public string AimInfo { get; set; } = "{\"aim\":\"127.0.0.1\",\"tz\":\"+0800\",\"tzid\":\"\",\"country\":\"CN\"}";
	// TODO: [RequiredMember]
	[JsonPropertyName("client_login_sn")]
	public string ClientLoginSn { get; set; }
	[JsonPropertyName("gas_token")]
	public string GasToken { get; set; } = string.Empty;
	[JsonPropertyName("source_platform")]
	public string SourcePlatform { get; set; } = "pc";
	[JsonPropertyName("ip")]
	public string Ip { get; set; } = "127.0.0.1";

}