using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher;
// TODO: [RequiredMember]
public class EntityAuthenticationData
{
	// TODO: [RequiredMember]
	[JsonPropertyName("sa_data")]
	public string SaData
	{
			
		get;
			
		set;
	}
	// TODO: [RequiredMember]
	[JsonPropertyName("sauth_json")]
	public string AuthJson
	{
			
		get;
			
		set;
	}
	// TODO: [RequiredMember]
	[JsonPropertyName("version")]
	public EntityAuthenticationVersion Version
	{
			
		get;
			
		set;
	}
	[JsonPropertyName("sdkuid")]
	public string SdkUid { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("aid")]
	public string Aid
	{
			
		get;
			
		set;
	}
	[JsonPropertyName("hasMessage")]
	public bool HasMessage { get; set; }
	[JsonPropertyName("hasGmail")]
	public bool HasGmail { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("otp_token")]
	public string OtpToken
	{
			
		get;
			
		set;
	}
	[JsonPropertyName("otp_pwd")]
	public string OtpPwd { get; set; }
	[JsonPropertyName("lock_time")]
	public int LockTime { get; set; }
	[JsonPropertyName("env")]
	public string Env { get; set; }
	[JsonPropertyName("min_engine_version")]
	public string MinEngineVersion { get; set; }
	[JsonPropertyName("min_patch_version")]
	public string MinPatchVersion { get; set; }
	[JsonPropertyName("verify_status")]
	public int VerifyStatus { get; set; }
	[JsonPropertyName("unisdk_login_json")]
	public string UniSdkLoginJson { get; set; }
	[JsonPropertyName("token")]
	public string Token { get; set; }
	[JsonPropertyName("is_register")]
	public bool IsRegister { get; set; } = true;
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; }

}