using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher;
// TODO: [RequiredMember]
public class EntityX19Cookie
{
	[JsonPropertyName("gameid")]
	public string GameId { get; set; } = "x19";
	[JsonPropertyName("login_channel")]
	public string LoginChannel { get; set; } = "netease";
	[JsonPropertyName("app_channel")]
	public string AppChannel { get; set; } = "netease";
	[JsonPropertyName("platform")]
	public string Platform { get; set; } = "pc";
	// TODO: [RequiredMember]
	[JsonPropertyName("sdkuid")]
	public string SdkUid { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("sessionid")]
	public string SessionId { get; set; }
	[JsonPropertyName("sdk_version")]
	public string SdkVersion { get; set; } = "4.2.0";
	// TODO: [RequiredMember]
	[JsonPropertyName("udid")]
	public string Udid { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("deviceid")]
	public string DeviceId { get; set; }
	[JsonPropertyName("aim_info")]
	public string AimInfo { get; set; } = "{\"aim\":\"127.0.0.1\",\"country\":\"CN\",\"tz\":\"+0800\",\"tzid\":\"\"}";

}