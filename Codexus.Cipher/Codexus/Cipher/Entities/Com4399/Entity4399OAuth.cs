using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Com4399;

// Token: 0x020000A4 RID: 164
// TODO: [RequiredMember]
public class Entity4399OAuth
{
	// Token: 0x1700023F RID: 575
	// (get) Token: 0x060005FC RID: 1532 RVA: 0x0000AC0D File Offset: 0x00008E0D
	// (set) Token: 0x060005FD RID: 1533 RVA: 0x0000AC15 File Offset: 0x00008E15
	// TODO: [RequiredMember]
	[JsonPropertyName("DEVICE_IDENTIFIER")]
	public required string DeviceIdentifier { get; set; }

	// Token: 0x17000240 RID: 576
	// (get) Token: 0x060005FE RID: 1534 RVA: 0x0000AC1E File Offset: 0x00008E1E
	// (set) Token: 0x060005FF RID: 1535 RVA: 0x0000AC26 File Offset: 0x00008E26
	[JsonPropertyName("SCREEN_RESOLUTION")]
	public string ScreenResolution { get; set; } = "3200*1384";

	// Token: 0x17000241 RID: 577
	// (get) Token: 0x06000600 RID: 1536 RVA: 0x0000AC2F File Offset: 0x00008E2F
	// (set) Token: 0x06000601 RID: 1537 RVA: 0x0000AC37 File Offset: 0x00008E37
	[JsonPropertyName("DEVICE_MODEL")]
	public string DeviceModel { get; set; } = "Oppo A5 2022";

	// Token: 0x17000242 RID: 578
	// (get) Token: 0x06000602 RID: 1538 RVA: 0x0000AC40 File Offset: 0x00008E40
	// (set) Token: 0x06000603 RID: 1539 RVA: 0x0000AC48 File Offset: 0x00008E48
	[JsonPropertyName("DEVICE_MODEL_VERSION")]
	public string DeviceModelVersion { get; set; } = "14";

	// Token: 0x17000243 RID: 579
	// (get) Token: 0x06000604 RID: 1540 RVA: 0x0000AC51 File Offset: 0x00008E51
	// (set) Token: 0x06000605 RID: 1541 RVA: 0x0000AC59 File Offset: 0x00008E59
	[JsonPropertyName("SYSTEM_VERSION")]
	public string SystemVersion { get; set; } = "14";

	// Token: 0x17000244 RID: 580
	// (get) Token: 0x06000606 RID: 1542 RVA: 0x0000AC62 File Offset: 0x00008E62
	// (set) Token: 0x06000607 RID: 1543 RVA: 0x0000AC6A File Offset: 0x00008E6A
	[JsonPropertyName("PLATFORM_TYPE")]
	public string PlatformType { get; set; } = "Android";

	// Token: 0x17000245 RID: 581
	// (get) Token: 0x06000608 RID: 1544 RVA: 0x0000AC73 File Offset: 0x00008E73
	// (set) Token: 0x06000609 RID: 1545 RVA: 0x0000AC7B File Offset: 0x00008E7B
	[JsonPropertyName("SDK_VERSION")]
	public string SdkVersion { get; set; } = "3.12.2.503";

	// Token: 0x17000246 RID: 582
	// (get) Token: 0x0600060A RID: 1546 RVA: 0x0000AC84 File Offset: 0x00008E84
	// (set) Token: 0x0600060B RID: 1547 RVA: 0x0000AC8C File Offset: 0x00008E8C
	[JsonPropertyName("GAME_KEY")]
	public string GameKey { get; set; } = "115716";

	// Token: 0x17000247 RID: 583
	// (get) Token: 0x0600060C RID: 1548 RVA: 0x0000AC95 File Offset: 0x00008E95
	// (set) Token: 0x0600060D RID: 1549 RVA: 0x0000AC9D File Offset: 0x00008E9D
	[JsonPropertyName("GAME_VERSION")]
	public string GameVersion { get; set; } = "3.1.5.260925";

	// Token: 0x17000248 RID: 584
	// (get) Token: 0x0600060E RID: 1550 RVA: 0x0000ACA6 File Offset: 0x00008EA6
	// (set) Token: 0x0600060F RID: 1551 RVA: 0x0000ACAE File Offset: 0x00008EAE
	[JsonPropertyName("BID")]
	public string Bid { get; set; } = "com.netease.mc.m4399";

	// Token: 0x17000249 RID: 585
	// (get) Token: 0x06000610 RID: 1552 RVA: 0x0000ACB7 File Offset: 0x00008EB7
	// (set) Token: 0x06000611 RID: 1553 RVA: 0x0000ACBF File Offset: 0x00008EBF
	[JsonPropertyName("RUNTIME")]
	public string Runtime { get; set; } = "Origin";

	// Token: 0x1700024A RID: 586
	// (get) Token: 0x06000612 RID: 1554 RVA: 0x0000ACC8 File Offset: 0x00008EC8
	// (set) Token: 0x06000613 RID: 1555 RVA: 0x0000ACD0 File Offset: 0x00008ED0
	[JsonPropertyName("CANAL_IDENTIFIER")]
	public string CanalIdentifier { get; set; } = string.Empty;

	// Token: 0x1700024B RID: 587
	// (get) Token: 0x06000614 RID: 1556 RVA: 0x0000ACD9 File Offset: 0x00008ED9
	// (set) Token: 0x06000615 RID: 1557 RVA: 0x0000ACE1 File Offset: 0x00008EE1
	// TODO: [RequiredMember]
	[JsonPropertyName("UDID")]
	public required string Udid { get; set; }

	// Token: 0x1700024C RID: 588
	// (get) Token: 0x06000616 RID: 1558 RVA: 0x0000ACEA File Offset: 0x00008EEA
	// (set) Token: 0x06000617 RID: 1559 RVA: 0x0000ACF2 File Offset: 0x00008EF2
	[JsonPropertyName("DEBUG")]
	public string Debug { get; set; } = "false";

	// Token: 0x1700024D RID: 589
	// (get) Token: 0x06000618 RID: 1560 RVA: 0x0000ACFB File Offset: 0x00008EFB
	// (set) Token: 0x06000619 RID: 1561 RVA: 0x0000AD03 File Offset: 0x00008F03
	[JsonPropertyName("NETWORK_TYPE")]
	public string NetworkType { get; set; } = "WIFI";

	// Token: 0x1700024E RID: 590
	// (get) Token: 0x0600061A RID: 1562 RVA: 0x0000AD0C File Offset: 0x00008F0C
	// (set) Token: 0x0600061B RID: 1563 RVA: 0x0000AD14 File Offset: 0x00008F14
	[JsonPropertyName("GAME_BOX_VERSION")]
	public string GameBoxVersion { get; set; } = string.Empty;

	// Token: 0x1700024F RID: 591
	// (get) Token: 0x0600061C RID: 1564 RVA: 0x0000AD1D File Offset: 0x00008F1D
	// (set) Token: 0x0600061D RID: 1565 RVA: 0x0000AD25 File Offset: 0x00008F25
	[JsonPropertyName("VIP_INFO")]
	public string VipInfo { get; set; } = string.Empty;

	// Token: 0x17000250 RID: 592
	// (get) Token: 0x0600061E RID: 1566 RVA: 0x0000AD2E File Offset: 0x00008F2E
	// (set) Token: 0x0600061F RID: 1567 RVA: 0x0000AD36 File Offset: 0x00008F36
	[JsonPropertyName("TEAM")]
	public int Team { get; set; } = 2;

	// Token: 0x17000251 RID: 593
	// (get) Token: 0x06000620 RID: 1568 RVA: 0x0000AD3F File Offset: 0x00008F3F
	// (set) Token: 0x06000621 RID: 1569 RVA: 0x0000AD47 File Offset: 0x00008F47
	// TODO: [RequiredMember]
	[JsonPropertyName("DEVICE_IDENTIFIER_SM")]
	public required string DeviceIdentifierSm { get; set; }

	// Token: 0x17000252 RID: 594
	// (get) Token: 0x06000622 RID: 1570 RVA: 0x0000AD50 File Offset: 0x00008F50
	// (set) Token: 0x06000623 RID: 1571 RVA: 0x0000AD58 File Offset: 0x00008F58
	[JsonPropertyName("UID")]
	public string Uid { get; set; } = string.Empty;
}