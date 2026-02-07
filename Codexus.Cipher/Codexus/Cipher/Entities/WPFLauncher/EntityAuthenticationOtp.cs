using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher;

// Token: 0x02000036 RID: 54
public class EntityAuthenticationOtp
{
	// Token: 0x1700005C RID: 92
	// (get) Token: 0x060001C5 RID: 453 RVA: 0x00007F11 File Offset: 0x00006111
	// (set) Token: 0x060001C6 RID: 454 RVA: 0x00007F19 File Offset: 0x00006119
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; } = "";

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x060001C7 RID: 455 RVA: 0x00007F22 File Offset: 0x00006122
	// (set) Token: 0x060001C8 RID: 456 RVA: 0x00007F2A File Offset: 0x0000612A
	[JsonPropertyName("account")]
	public string Account { get; set; } = "";

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x060001C9 RID: 457 RVA: 0x00007F33 File Offset: 0x00006133
	// (set) Token: 0x060001CA RID: 458 RVA: 0x00007F3B File Offset: 0x0000613B
	[JsonPropertyName("token")]
	public string Token { get; set; } = "";

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x060001CB RID: 459 RVA: 0x00007F44 File Offset: 0x00006144
	// (set) Token: 0x060001CC RID: 460 RVA: 0x00007F4C File Offset: 0x0000614C
	[JsonPropertyName("sead")]
	public string Sead { get; set; } = "";

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x060001CD RID: 461 RVA: 0x00007F55 File Offset: 0x00006155
	// (set) Token: 0x060001CE RID: 462 RVA: 0x00007F5D File Offset: 0x0000615D
	[JsonPropertyName("hasMessage")]
	public bool HasMessage { get; set; }

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x060001CF RID: 463 RVA: 0x00007F66 File Offset: 0x00006166
	// (set) Token: 0x060001D0 RID: 464 RVA: 0x00007F6E File Offset: 0x0000616E
	[JsonPropertyName("aid")]
	public string Aid { get; set; } = "";

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x060001D1 RID: 465 RVA: 0x00007F77 File Offset: 0x00006177
	// (set) Token: 0x060001D2 RID: 466 RVA: 0x00007F7F File Offset: 0x0000617F
	[JsonPropertyName("sdkuid")]
	public string SdkUid { get; set; } = "";

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x060001D3 RID: 467 RVA: 0x00007F88 File Offset: 0x00006188
	// (set) Token: 0x060001D4 RID: 468 RVA: 0x00007F90 File Offset: 0x00006190
	[JsonPropertyName("access_token")]
	public string AccessToken { get; set; } = "";

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x060001D5 RID: 469 RVA: 0x00007F99 File Offset: 0x00006199
	// (set) Token: 0x060001D6 RID: 470 RVA: 0x00007FA1 File Offset: 0x000061A1
	[JsonPropertyName("unisdk_login_json")]
	public string UniSdkLoginJson { get; set; } = "";

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x060001D7 RID: 471 RVA: 0x00007FAA File Offset: 0x000061AA
	// (set) Token: 0x060001D8 RID: 472 RVA: 0x00007FB2 File Offset: 0x000061B2
	[JsonPropertyName("verify_status")]
	public int VerifyStatus { get; set; }

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x060001D9 RID: 473 RVA: 0x00007FBB File Offset: 0x000061BB
	// (set) Token: 0x060001DA RID: 474 RVA: 0x00007FC3 File Offset: 0x000061C3
	[JsonPropertyName("hasGmail")]
	public bool HasGmail { get; set; }

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x060001DB RID: 475 RVA: 0x00007FCC File Offset: 0x000061CC
	// (set) Token: 0x060001DC RID: 476 RVA: 0x00007FD4 File Offset: 0x000061D4
	[JsonPropertyName("is_register")]
	public bool IsRegister { get; set; }

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x060001DD RID: 477 RVA: 0x00007FDD File Offset: 0x000061DD
	// (set) Token: 0x060001DE RID: 478 RVA: 0x00007FE5 File Offset: 0x000061E5
	[JsonPropertyName("env")]
	public string Env { get; set; } = "";

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x060001DF RID: 479 RVA: 0x00007FEE File Offset: 0x000061EE
	// (set) Token: 0x060001E0 RID: 480 RVA: 0x00007FF6 File Offset: 0x000061F6
	[JsonPropertyName("last_server_up_time")]
	public long LastServerUpTime { get; set; }

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x060001E1 RID: 481 RVA: 0x00007FFF File Offset: 0x000061FF
	// (set) Token: 0x060001E2 RID: 482 RVA: 0x00008007 File Offset: 0x00006207
	[JsonPropertyName("min_engine_version")]
	public string MinEngineVersion { get; set; } = "";

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x060001E3 RID: 483 RVA: 0x00008010 File Offset: 0x00006210
	// (set) Token: 0x060001E4 RID: 484 RVA: 0x00008018 File Offset: 0x00006218
	[JsonPropertyName("min_patch_version")]
	public string MinPatchVersion { get; set; } = "";
}