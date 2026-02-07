using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher;

// Token: 0x02000034 RID: 52
// TODO: [RequiredMember]
public class EntityAuthenticationData
{
	// Token: 0x17000035 RID: 53
	// (get) Token: 0x06000175 RID: 373 RVA: 0x00007B92 File Offset: 0x00005D92
	// (set) Token: 0x06000176 RID: 374 RVA: 0x00007B9A File Offset: 0x00005D9A// TODO: [RequiredMember]
	// TODO: [RequiredMember]
	[JsonPropertyName("sa_data")]
	public string SaData
	{
			
		get;
			
		set;
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x06000177 RID: 375 RVA: 0x00007BA3 File Offset: 0x00005DA3
	// (set) Token: 0x06000178 RID: 376 RVA: 0x00007BAB File Offset: 0x00005DAB// TODO: [RequiredMember]
	// TODO: [RequiredMember]
	[JsonPropertyName("sauth_json")]
	public string AuthJson
	{
			
		get;
			
		set;
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x06000179 RID: 377 RVA: 0x00007BB4 File Offset: 0x00005DB4
	// (set) Token: 0x0600017A RID: 378 RVA: 0x00007BBC File Offset: 0x00005DBC// TODO: [RequiredMember]
	// TODO: [RequiredMember]
	[JsonPropertyName("version")]
	public EntityAuthenticationVersion Version
	{
			
		get;
			
		set;
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x0600017B RID: 379 RVA: 0x00007BC5 File Offset: 0x00005DC5
	// (set) Token: 0x0600017C RID: 380 RVA: 0x00007BCD File Offset: 0x00005DCD
	[JsonPropertyName("sdkuid")]
	public string SdkUid { get; set; }

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x0600017D RID: 381 RVA: 0x00007BD6 File Offset: 0x00005DD6
	// (set) Token: 0x0600017E RID: 382 RVA: 0x00007BDE File Offset: 0x00005DDE// TODO: [RequiredMember]
	// TODO: [RequiredMember]
	[JsonPropertyName("aid")]
	public string Aid
	{
			
		get;
			
		set;
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x0600017F RID: 383 RVA: 0x00007BE7 File Offset: 0x00005DE7
	// (set) Token: 0x06000180 RID: 384 RVA: 0x00007BEF File Offset: 0x00005DEF
	[JsonPropertyName("hasMessage")]
	public bool HasMessage { get; set; }

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x06000181 RID: 385 RVA: 0x00007BF8 File Offset: 0x00005DF8
	// (set) Token: 0x06000182 RID: 386 RVA: 0x00007C00 File Offset: 0x00005E00
	[JsonPropertyName("hasGmail")]
	public bool HasGmail { get; set; }

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000183 RID: 387 RVA: 0x00007C09 File Offset: 0x00005E09
	// (set) Token: 0x06000184 RID: 388 RVA: 0x00007C11 File Offset: 0x00005E11// TODO: [RequiredMember]
	// TODO: [RequiredMember]
	[JsonPropertyName("otp_token")]
	public string OtpToken
	{
			
		get;
			
		set;
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x06000185 RID: 389 RVA: 0x00007C1A File Offset: 0x00005E1A
	// (set) Token: 0x06000186 RID: 390 RVA: 0x00007C22 File Offset: 0x00005E22
	[JsonPropertyName("otp_pwd")]
	public string OtpPwd { get; set; }

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x06000187 RID: 391 RVA: 0x00007C2B File Offset: 0x00005E2B
	// (set) Token: 0x06000188 RID: 392 RVA: 0x00007C33 File Offset: 0x00005E33
	[JsonPropertyName("lock_time")]
	public int LockTime { get; set; }

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x06000189 RID: 393 RVA: 0x00007C3C File Offset: 0x00005E3C
	// (set) Token: 0x0600018A RID: 394 RVA: 0x00007C44 File Offset: 0x00005E44
	[JsonPropertyName("env")]
	public string Env { get; set; }

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x0600018B RID: 395 RVA: 0x00007C4D File Offset: 0x00005E4D
	// (set) Token: 0x0600018C RID: 396 RVA: 0x00007C55 File Offset: 0x00005E55
	[JsonPropertyName("min_engine_version")]
	public string MinEngineVersion { get; set; }

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x0600018D RID: 397 RVA: 0x00007C5E File Offset: 0x00005E5E
	// (set) Token: 0x0600018E RID: 398 RVA: 0x00007C66 File Offset: 0x00005E66
	[JsonPropertyName("min_patch_version")]
	public string MinPatchVersion { get; set; }

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x0600018F RID: 399 RVA: 0x00007C6F File Offset: 0x00005E6F
	// (set) Token: 0x06000190 RID: 400 RVA: 0x00007C77 File Offset: 0x00005E77
	[JsonPropertyName("verify_status")]
	public int VerifyStatus { get; set; }

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x06000191 RID: 401 RVA: 0x00007C80 File Offset: 0x00005E80
	// (set) Token: 0x06000192 RID: 402 RVA: 0x00007C88 File Offset: 0x00005E88
	[JsonPropertyName("unisdk_login_json")]
	public string UniSdkLoginJson { get; set; }

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x06000193 RID: 403 RVA: 0x00007C91 File Offset: 0x00005E91
	// (set) Token: 0x06000194 RID: 404 RVA: 0x00007C99 File Offset: 0x00005E99
	[JsonPropertyName("token")]
	public string Token { get; set; }

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x06000195 RID: 405 RVA: 0x00007CA2 File Offset: 0x00005EA2
	// (set) Token: 0x06000196 RID: 406 RVA: 0x00007CAA File Offset: 0x00005EAA
	[JsonPropertyName("is_register")]
	public bool IsRegister { get; set; } = true;

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x06000197 RID: 407 RVA: 0x00007CB3 File Offset: 0x00005EB3
	// (set) Token: 0x06000198 RID: 408 RVA: 0x00007CBB File Offset: 0x00005EBB
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; }

	// Token: 0x06000199 RID: 409 RVA: 0x00007CC4 File Offset: 0x00005EC4
	public EntityAuthenticationData()
	{
	}
}