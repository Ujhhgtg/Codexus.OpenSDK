using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MPay;

// Token: 0x0200007B RID: 123
public class EntityPcExtInfo
{
	// Token: 0x170001B3 RID: 435
	// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00009F45 File Offset: 0x00008145
	// (set) Token: 0x060004B8 RID: 1208 RVA: 0x00009F4D File Offset: 0x0000814D
	[JsonPropertyName("src_jf_game_id")]
	public string SrcJfGameId { get; set; } = string.Empty;

	// Token: 0x170001B4 RID: 436
	// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00009F56 File Offset: 0x00008156
	// (set) Token: 0x060004BA RID: 1210 RVA: 0x00009F5E File Offset: 0x0000815E
	[JsonPropertyName("src_app_channel")]
	public string SrcAppChannel { get; set; } = string.Empty;

	// Token: 0x170001B5 RID: 437
	// (get) Token: 0x060004BB RID: 1211 RVA: 0x00009F67 File Offset: 0x00008167
	// (set) Token: 0x060004BC RID: 1212 RVA: 0x00009F6F File Offset: 0x0000816F
	[JsonPropertyName("from_game_id")]
	public string FromGameId { get; set; } = string.Empty;

	// Token: 0x170001B6 RID: 438
	// (get) Token: 0x060004BD RID: 1213 RVA: 0x00009F78 File Offset: 0x00008178
	// (set) Token: 0x060004BE RID: 1214 RVA: 0x00009F80 File Offset: 0x00008180
	[JsonPropertyName("src_client_type")]
	public int SrcClientType { get; set; }

	// Token: 0x170001B7 RID: 439
	// (get) Token: 0x060004BF RID: 1215 RVA: 0x00009F89 File Offset: 0x00008189
	// (set) Token: 0x060004C0 RID: 1216 RVA: 0x00009F91 File Offset: 0x00008191
	[JsonPropertyName("src_udid")]
	public string SrcUdid { get; set; } = string.Empty;

	// Token: 0x170001B8 RID: 440
	// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00009F9A File Offset: 0x0000819A
	// (set) Token: 0x060004C2 RID: 1218 RVA: 0x00009FA2 File Offset: 0x000081A2
	[JsonPropertyName("src_sdk_version")]
	public string SrcSdkVersion { get; set; } = string.Empty;

	// Token: 0x170001B9 RID: 441
	// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00009FAB File Offset: 0x000081AB
	// (set) Token: 0x060004C4 RID: 1220 RVA: 0x00009FB3 File Offset: 0x000081B3
	[JsonPropertyName("src_pay_channel")]
	public string SrcPayChannel { get; set; } = string.Empty;

	// Token: 0x170001BA RID: 442
	// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00009FBC File Offset: 0x000081BC
	// (set) Token: 0x060004C6 RID: 1222 RVA: 0x00009FC4 File Offset: 0x000081C4
	[JsonPropertyName("src_client_ip")]
	public string SrcClientIp { get; set; } = string.Empty;

	// Token: 0x170001BB RID: 443
	// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00009FCD File Offset: 0x000081CD
	// (set) Token: 0x060004C8 RID: 1224 RVA: 0x00009FD5 File Offset: 0x000081D5
	[JsonPropertyName("extra_unisdk_data")]
	public string ExtraUnisdkData { get; set; } = string.Empty;
}