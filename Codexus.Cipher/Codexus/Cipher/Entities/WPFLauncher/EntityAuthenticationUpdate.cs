using System.Text.Json.Serialization;
using Codexus.Cipher.Entities.Converter;

namespace Codexus.Cipher.Entities.WPFLauncher;

// Token: 0x02000037 RID: 55
public class EntityAuthenticationUpdate
{
	// Token: 0x1700006C RID: 108
	// (get) Token: 0x060001E6 RID: 486 RVA: 0x000080B1 File Offset: 0x000062B1
	// (set) Token: 0x060001E7 RID: 487 RVA: 0x000080B9 File Offset: 0x000062B9
	[JsonPropertyName("sa_data")]
	public object SaData { get; set; }

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x060001E8 RID: 488 RVA: 0x000080C2 File Offset: 0x000062C2
	// (set) Token: 0x060001E9 RID: 489 RVA: 0x000080CA File Offset: 0x000062CA
	[JsonPropertyName("sauth_json")]
	public object SauthJson { get; set; }

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x060001EA RID: 490 RVA: 0x000080D3 File Offset: 0x000062D3
	// (set) Token: 0x060001EB RID: 491 RVA: 0x000080DB File Offset: 0x000062DB
	[JsonPropertyName("version")]
	public string Version { get; set; }

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x060001EC RID: 492 RVA: 0x000080E4 File Offset: 0x000062E4
	// (set) Token: 0x060001ED RID: 493 RVA: 0x000080EC File Offset: 0x000062EC
	[JsonPropertyName("sdkuid")]
	public string SdkUid { get; set; }

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x060001EE RID: 494 RVA: 0x000080F5 File Offset: 0x000062F5
	// (set) Token: 0x060001EF RID: 495 RVA: 0x000080FD File Offset: 0x000062FD
	[JsonPropertyName("aid")]
	[JsonConverter(typeof(NetEaseIntConverter))]
	public string Aid { get; set; }

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x060001F0 RID: 496 RVA: 0x00008106 File Offset: 0x00006306
	// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000810E File Offset: 0x0000630E
	[JsonPropertyName("hasMessage")]
	public bool HasMessage { get; set; }

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x060001F2 RID: 498 RVA: 0x00008117 File Offset: 0x00006317
	// (set) Token: 0x060001F3 RID: 499 RVA: 0x0000811F File Offset: 0x0000631F
	[JsonPropertyName("hasGmail")]
	public bool HasGmail { get; set; }

	// Token: 0x17000073 RID: 115
	// (get) Token: 0x060001F4 RID: 500 RVA: 0x00008128 File Offset: 0x00006328
	// (set) Token: 0x060001F5 RID: 501 RVA: 0x00008130 File Offset: 0x00006330
	[JsonPropertyName("otp_token")]
	public string OtpToken { get; set; }

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x060001F6 RID: 502 RVA: 0x00008139 File Offset: 0x00006339
	// (set) Token: 0x060001F7 RID: 503 RVA: 0x00008141 File Offset: 0x00006341
	[JsonPropertyName("otp_pwd")]
	public string OtpPwd { get; set; }

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000814A File Offset: 0x0000634A
	// (set) Token: 0x060001F9 RID: 505 RVA: 0x00008152 File Offset: 0x00006352
	[JsonPropertyName("lock_time")]
	public long LockTime { get; set; }

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x060001FA RID: 506 RVA: 0x0000815B File Offset: 0x0000635B
	// (set) Token: 0x060001FB RID: 507 RVA: 0x00008163 File Offset: 0x00006363
	[JsonPropertyName("env")]
	public string Env { get; set; }

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x060001FC RID: 508 RVA: 0x0000816C File Offset: 0x0000636C
	// (set) Token: 0x060001FD RID: 509 RVA: 0x00008174 File Offset: 0x00006374
	[JsonPropertyName("min_engine_version")]
	public string MinEngineVersion { get; set; }

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x060001FE RID: 510 RVA: 0x0000817D File Offset: 0x0000637D
	// (set) Token: 0x060001FF RID: 511 RVA: 0x00008185 File Offset: 0x00006385
	[JsonPropertyName("min_patch_version")]
	public string MinPatchVersion { get; set; }

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x06000200 RID: 512 RVA: 0x0000818E File Offset: 0x0000638E
	// (set) Token: 0x06000201 RID: 513 RVA: 0x00008196 File Offset: 0x00006396
	[JsonPropertyName("verify_status")]
	public int VerifyStatus { get; set; }

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x06000202 RID: 514 RVA: 0x0000819F File Offset: 0x0000639F
	// (set) Token: 0x06000203 RID: 515 RVA: 0x000081A7 File Offset: 0x000063A7
	[JsonPropertyName("unisdk_login_json")]
	public object UniSdkLoginJson { get; set; }

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x06000204 RID: 516 RVA: 0x000081B0 File Offset: 0x000063B0
	// (set) Token: 0x06000205 RID: 517 RVA: 0x000081B8 File Offset: 0x000063B8
	[JsonPropertyName("token")]
	public string Token { get; set; }

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x06000206 RID: 518 RVA: 0x000081C1 File Offset: 0x000063C1
	// (set) Token: 0x06000207 RID: 519 RVA: 0x000081C9 File Offset: 0x000063C9
	[JsonPropertyName("is_register")]
	public bool IsRegister { get; set; } = true;

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x06000208 RID: 520 RVA: 0x000081D2 File Offset: 0x000063D2
	// (set) Token: 0x06000209 RID: 521 RVA: 0x000081DA File Offset: 0x000063DA
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; }
}