using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher;

// Token: 0x0200003B RID: 59
// TODO: [RequiredMember]
public class EntityX19Cookie
{
	// Token: 0x17000089 RID: 137
	// (get) Token: 0x06000224 RID: 548 RVA: 0x00008300 File Offset: 0x00006500
	// (set) Token: 0x06000225 RID: 549 RVA: 0x00008308 File Offset: 0x00006508
	[JsonPropertyName("gameid")]
	public string GameId { get; set; } = "x19";

	// Token: 0x1700008A RID: 138
	// (get) Token: 0x06000226 RID: 550 RVA: 0x00008311 File Offset: 0x00006511
	// (set) Token: 0x06000227 RID: 551 RVA: 0x00008319 File Offset: 0x00006519
	[JsonPropertyName("login_channel")]
	public string LoginChannel { get; set; } = "netease";

	// Token: 0x1700008B RID: 139
	// (get) Token: 0x06000228 RID: 552 RVA: 0x00008322 File Offset: 0x00006522
	// (set) Token: 0x06000229 RID: 553 RVA: 0x0000832A File Offset: 0x0000652A
	[JsonPropertyName("app_channel")]
	public string AppChannel { get; set; } = "netease";

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x0600022A RID: 554 RVA: 0x00008333 File Offset: 0x00006533
	// (set) Token: 0x0600022B RID: 555 RVA: 0x0000833B File Offset: 0x0000653B
	[JsonPropertyName("platform")]
	public string Platform { get; set; } = "pc";

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x0600022C RID: 556 RVA: 0x00008344 File Offset: 0x00006544
	// (set) Token: 0x0600022D RID: 557 RVA: 0x0000834C File Offset: 0x0000654C
	// TODO: [RequiredMember]
	[JsonPropertyName("sdkuid")]
	public string SdkUid { get; set; }

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x0600022E RID: 558 RVA: 0x00008355 File Offset: 0x00006555
	// (set) Token: 0x0600022F RID: 559 RVA: 0x0000835D File Offset: 0x0000655D
	// TODO: [RequiredMember]
	[JsonPropertyName("sessionid")]
	public string SessionId { get; set; }

	// Token: 0x1700008F RID: 143
	// (get) Token: 0x06000230 RID: 560 RVA: 0x00008366 File Offset: 0x00006566
	// (set) Token: 0x06000231 RID: 561 RVA: 0x0000836E File Offset: 0x0000656E
	[JsonPropertyName("sdk_version")]
	public string SdkVersion { get; set; } = "4.2.0";

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x06000232 RID: 562 RVA: 0x00008377 File Offset: 0x00006577
	// (set) Token: 0x06000233 RID: 563 RVA: 0x0000837F File Offset: 0x0000657F
	// TODO: [RequiredMember]
	[JsonPropertyName("udid")]
	public string Udid { get; set; }

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x06000234 RID: 564 RVA: 0x00008388 File Offset: 0x00006588
	// (set) Token: 0x06000235 RID: 565 RVA: 0x00008390 File Offset: 0x00006590
	// TODO: [RequiredMember]
	[JsonPropertyName("deviceid")]
	public string DeviceId { get; set; }

	// Token: 0x17000092 RID: 146
	// (get) Token: 0x06000236 RID: 566 RVA: 0x00008399 File Offset: 0x00006599
	// (set) Token: 0x06000237 RID: 567 RVA: 0x000083A1 File Offset: 0x000065A1
	[JsonPropertyName("aim_info")]
	public string AimInfo { get; set; } = "{\"aim\":\"127.0.0.1\",\"country\":\"CN\",\"tz\":\"+0800\",\"tzid\":\"\"}";

	// Token: 0x06000238 RID: 568 RVA: 0x000083AC File Offset: 0x000065AC
}