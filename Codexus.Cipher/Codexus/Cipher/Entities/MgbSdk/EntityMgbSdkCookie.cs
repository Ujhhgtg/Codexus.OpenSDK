using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MgbSdk;

// Token: 0x02000089 RID: 137
// TODO: [RequiredMember]
public class EntityMgbSdkCookie
{
	// Token: 0x170001E8 RID: 488
	// (get) Token: 0x0600052F RID: 1327 RVA: 0x0000A431 File Offset: 0x00008631
	// (set) Token: 0x06000530 RID: 1328 RVA: 0x0000A439 File Offset: 0x00008639
	// TODO: [RequiredMember]
	[JsonPropertyName("timestamp")]
	public string Timestamp { get; set; }

	// Token: 0x170001E9 RID: 489
	// (get) Token: 0x06000531 RID: 1329 RVA: 0x0000A442 File Offset: 0x00008642
	// (set) Token: 0x06000532 RID: 1330 RVA: 0x0000A44A File Offset: 0x0000864A
	// TODO: [RequiredMember]
	[JsonPropertyName("userid")]
	public string UserId { get; set; }

	// Token: 0x170001EA RID: 490
	// (get) Token: 0x06000533 RID: 1331 RVA: 0x0000A453 File Offset: 0x00008653
	// (set) Token: 0x06000534 RID: 1332 RVA: 0x0000A45B File Offset: 0x0000865B
	[JsonPropertyName("realname")]
	public string RealName { get; set; } = "{\"realname_type\":\"0\"}";

	// Token: 0x170001EB RID: 491
	// (get) Token: 0x06000535 RID: 1333 RVA: 0x0000A464 File Offset: 0x00008664
	// (set) Token: 0x06000536 RID: 1334 RVA: 0x0000A46C File Offset: 0x0000866C
	// TODO: [RequiredMember]
	[JsonPropertyName("gameid")]
	public string GameId { get; set; }

	// Token: 0x170001EC RID: 492
	// (get) Token: 0x06000537 RID: 1335 RVA: 0x0000A475 File Offset: 0x00008675
	// (set) Token: 0x06000538 RID: 1336 RVA: 0x0000A47D File Offset: 0x0000867D
	// TODO: [RequiredMember]
	[JsonPropertyName("login_channel")]
	public string LoginChannel { get; set; }

	// Token: 0x170001ED RID: 493
	// (get) Token: 0x06000539 RID: 1337 RVA: 0x0000A486 File Offset: 0x00008686
	// (set) Token: 0x0600053A RID: 1338 RVA: 0x0000A48E File Offset: 0x0000868E
	// TODO: [RequiredMember]
	[JsonPropertyName("app_channel")]
	public string AppChannel { get; set; }

	// Token: 0x170001EE RID: 494
	// (get) Token: 0x0600053B RID: 1339 RVA: 0x0000A497 File Offset: 0x00008697
	// (set) Token: 0x0600053C RID: 1340 RVA: 0x0000A49F File Offset: 0x0000869F
	[JsonPropertyName("platform")]
	public string Platform { get; set; } = "pc";

	// Token: 0x170001EF RID: 495
	// (get) Token: 0x0600053D RID: 1341 RVA: 0x0000A4A8 File Offset: 0x000086A8
	// (set) Token: 0x0600053E RID: 1342 RVA: 0x0000A4B0 File Offset: 0x000086B0
	// TODO: [RequiredMember]
	[JsonPropertyName("sdkuid")]
	public string SdkUid { get; set; }

	// Token: 0x170001F0 RID: 496
	// (get) Token: 0x0600053F RID: 1343 RVA: 0x0000A4B9 File Offset: 0x000086B9
	// (set) Token: 0x06000540 RID: 1344 RVA: 0x0000A4C1 File Offset: 0x000086C1
	// TODO: [RequiredMember]
	[JsonPropertyName("sessionid")]
	public string SessionId { get; set; }

	// Token: 0x170001F1 RID: 497
	// (get) Token: 0x06000541 RID: 1345 RVA: 0x0000A4CA File Offset: 0x000086CA
	// (set) Token: 0x06000542 RID: 1346 RVA: 0x0000A4D2 File Offset: 0x000086D2
	[JsonPropertyName("sdk_version")]
	public string SdkVersion { get; set; } = "1.0.0";

	// Token: 0x170001F2 RID: 498
	// (get) Token: 0x06000543 RID: 1347 RVA: 0x0000A4DB File Offset: 0x000086DB
	// (set) Token: 0x06000544 RID: 1348 RVA: 0x0000A4E3 File Offset: 0x000086E3
	// TODO: [RequiredMember]
	[JsonPropertyName("udid")]
	public string Udid { get; set; }

	// Token: 0x170001F3 RID: 499
	// (get) Token: 0x06000545 RID: 1349 RVA: 0x0000A4EC File Offset: 0x000086EC
	// (set) Token: 0x06000546 RID: 1350 RVA: 0x0000A4F4 File Offset: 0x000086F4
	// TODO: [RequiredMember]
	[JsonPropertyName("deviceid")]
	public string DeviceId { get; set; }

	// Token: 0x170001F4 RID: 500
	// (get) Token: 0x06000547 RID: 1351 RVA: 0x0000A4FD File Offset: 0x000086FD
	// (set) Token: 0x06000548 RID: 1352 RVA: 0x0000A505 File Offset: 0x00008705
	[JsonPropertyName("aim_info")]
	public string AimInfo { get; set; } = "{\"aim\":\"127.0.0.1\",\"tz\":\"+0800\",\"tzid\":\"\",\"country\":\"CN\"}";

	// Token: 0x170001F5 RID: 501
	// (get) Token: 0x06000549 RID: 1353 RVA: 0x0000A50E File Offset: 0x0000870E
	// (set) Token: 0x0600054A RID: 1354 RVA: 0x0000A516 File Offset: 0x00008716
	// TODO: [RequiredMember]
	[JsonPropertyName("client_login_sn")]
	public string ClientLoginSn { get; set; }

	// Token: 0x170001F6 RID: 502
	// (get) Token: 0x0600054B RID: 1355 RVA: 0x0000A51F File Offset: 0x0000871F
	// (set) Token: 0x0600054C RID: 1356 RVA: 0x0000A527 File Offset: 0x00008727
	[JsonPropertyName("gas_token")]
	public string GasToken { get; set; } = string.Empty;

	// Token: 0x170001F7 RID: 503
	// (get) Token: 0x0600054D RID: 1357 RVA: 0x0000A530 File Offset: 0x00008730
	// (set) Token: 0x0600054E RID: 1358 RVA: 0x0000A538 File Offset: 0x00008738
	[JsonPropertyName("source_platform")]
	public string SourcePlatform { get; set; } = "pc";

	// Token: 0x170001F8 RID: 504
	// (get) Token: 0x0600054F RID: 1359 RVA: 0x0000A541 File Offset: 0x00008741
	// (set) Token: 0x06000550 RID: 1360 RVA: 0x0000A549 File Offset: 0x00008749
	[JsonPropertyName("ip")]
	public string Ip { get; set; } = "127.0.0.1";

	// Token: 0x06000551 RID: 1361 RVA: 0x0000A554 File Offset: 0x00008754
}