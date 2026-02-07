using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MPay;

// Token: 0x02000079 RID: 121
public class EntityMPayUser
{
	// Token: 0x1700019F RID: 415
	// (get) Token: 0x0600048D RID: 1165 RVA: 0x00009D5B File Offset: 0x00007F5B
	// (set) Token: 0x0600048E RID: 1166 RVA: 0x00009D63 File Offset: 0x00007F63
	[JsonPropertyName("ext_access_token")]
	public string ExtAccessToken { get; set; } = string.Empty;

	// Token: 0x170001A0 RID: 416
	// (get) Token: 0x0600048F RID: 1167 RVA: 0x00009D6C File Offset: 0x00007F6C
	// (set) Token: 0x06000490 RID: 1168 RVA: 0x00009D74 File Offset: 0x00007F74
	[JsonPropertyName("realname_verify_status")]
	public int RealNameVerifyStatus { get; set; }

	// Token: 0x170001A1 RID: 417
	// (get) Token: 0x06000491 RID: 1169 RVA: 0x00009D7D File Offset: 0x00007F7D
	// (set) Token: 0x06000492 RID: 1170 RVA: 0x00009D85 File Offset: 0x00007F85
	[JsonPropertyName("login_channel")]
	public string LoginChannel { get; set; } = string.Empty;

	// Token: 0x170001A2 RID: 418
	// (get) Token: 0x06000493 RID: 1171 RVA: 0x00009D8E File Offset: 0x00007F8E
	// (set) Token: 0x06000494 RID: 1172 RVA: 0x00009D96 File Offset: 0x00007F96
	[JsonPropertyName("realname_status")]
	public int RealNameStatus { get; set; }

	// Token: 0x170001A3 RID: 419
	// (get) Token: 0x06000495 RID: 1173 RVA: 0x00009D9F File Offset: 0x00007F9F
	// (set) Token: 0x06000496 RID: 1174 RVA: 0x00009DA7 File Offset: 0x00007FA7
	[JsonPropertyName("related_login_status")]
	public int RelatedLoginStatus { get; set; }

	// Token: 0x170001A4 RID: 420
	// (get) Token: 0x06000497 RID: 1175 RVA: 0x00009DB0 File Offset: 0x00007FB0
	// (set) Token: 0x06000498 RID: 1176 RVA: 0x00009DB8 File Offset: 0x00007FB8
	[JsonPropertyName("need_mask")]
	public bool NeedMask { get; set; }

	// Token: 0x170001A5 RID: 421
	// (get) Token: 0x06000499 RID: 1177 RVA: 0x00009DC1 File Offset: 0x00007FC1
	// (set) Token: 0x0600049A RID: 1178 RVA: 0x00009DC9 File Offset: 0x00007FC9
	[JsonPropertyName("mobile_bind_status")]
	public int MobileBindStatus { get; set; }

	// Token: 0x170001A6 RID: 422
	// (get) Token: 0x0600049B RID: 1179 RVA: 0x00009DD2 File Offset: 0x00007FD2
	// (set) Token: 0x0600049C RID: 1180 RVA: 0x00009DDA File Offset: 0x00007FDA
	[JsonPropertyName("mask_related_mobile")]
	public string MaskRelatedMobile { get; set; } = string.Empty;

	// Token: 0x170001A7 RID: 423
	// (get) Token: 0x0600049D RID: 1181 RVA: 0x00009DE3 File Offset: 0x00007FE3
	// (set) Token: 0x0600049E RID: 1182 RVA: 0x00009DEB File Offset: 0x00007FEB
	[JsonPropertyName("display_username")]
	public string DisplayUsername { get; set; } = string.Empty;

	// Token: 0x170001A8 RID: 424
	// (get) Token: 0x0600049F RID: 1183 RVA: 0x00009DF4 File Offset: 0x00007FF4
	// (set) Token: 0x060004A0 RID: 1184 RVA: 0x00009DFC File Offset: 0x00007FFC
	[JsonPropertyName("token")]
	public string Token { get; set; } = string.Empty;

	// Token: 0x170001A9 RID: 425
	// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00009E05 File Offset: 0x00008005
	// (set) Token: 0x060004A2 RID: 1186 RVA: 0x00009E0D File Offset: 0x0000800D
	[JsonPropertyName("client_username")]
	public string ClientUsername { get; set; } = string.Empty;

	// Token: 0x170001AA RID: 426
	// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00009E16 File Offset: 0x00008016
	// (set) Token: 0x060004A4 RID: 1188 RVA: 0x00009E1E File Offset: 0x0000801E
	[JsonPropertyName("avatar")]
	public string Avatar { get; set; } = string.Empty;

	// Token: 0x170001AB RID: 427
	// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00009E27 File Offset: 0x00008027
	// (set) Token: 0x060004A6 RID: 1190 RVA: 0x00009E2F File Offset: 0x0000802F
	[JsonPropertyName("need_aas")]
	public bool NeedAas { get; set; }

	// Token: 0x170001AC RID: 428
	// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00009E38 File Offset: 0x00008038
	// (set) Token: 0x060004A8 RID: 1192 RVA: 0x00009E40 File Offset: 0x00008040
	[JsonPropertyName("login_type")]
	public int LoginType { get; set; }

	// Token: 0x170001AD RID: 429
	// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00009E49 File Offset: 0x00008049
	// (set) Token: 0x060004AA RID: 1194 RVA: 0x00009E51 File Offset: 0x00008051
	[JsonPropertyName("nickname")]
	public string Nickname { get; set; } = string.Empty;

	// Token: 0x170001AE RID: 430
	// (get) Token: 0x060004AB RID: 1195 RVA: 0x00009E5A File Offset: 0x0000805A
	// (set) Token: 0x060004AC RID: 1196 RVA: 0x00009E62 File Offset: 0x00008062
	[JsonPropertyName("id")]
	public string Id { get; set; } = string.Empty;

	// Token: 0x170001AF RID: 431
	// (get) Token: 0x060004AD RID: 1197 RVA: 0x00009E6B File Offset: 0x0000806B
	// (set) Token: 0x060004AE RID: 1198 RVA: 0x00009E73 File Offset: 0x00008073
	[JsonPropertyName("pc_ext_info")]
	public EntityPcExtInfo EntityPcExtInfo { get; set; } = new();
}