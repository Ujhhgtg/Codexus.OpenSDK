using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Skin;

// Token: 0x0200006A RID: 106
// TODO: [RequiredMember]
public class EntitySkinPurchaseRequest
{
	// Token: 0x17000164 RID: 356
	// (get) Token: 0x06000401 RID: 1025 RVA: 0x000096BC File Offset: 0x000078BC
	// (set) Token: 0x06000402 RID: 1026 RVA: 0x000096C4 File Offset: 0x000078C4
	// TODO: [RequiredMember]
	[JsonPropertyName("batch_count")]
	public int BatchCount { get; set; }

	// Token: 0x17000165 RID: 357
	// (get) Token: 0x06000403 RID: 1027 RVA: 0x000096CD File Offset: 0x000078CD
	// (set) Token: 0x06000404 RID: 1028 RVA: 0x000096D5 File Offset: 0x000078D5
	// TODO: [RequiredMember]
	[JsonPropertyName("buy_path")]
	public string BuyPath { get; set; }

	// Token: 0x17000166 RID: 358
	// (get) Token: 0x06000405 RID: 1029 RVA: 0x000096DE File Offset: 0x000078DE
	// (set) Token: 0x06000406 RID: 1030 RVA: 0x000096E6 File Offset: 0x000078E6
	// TODO: [RequiredMember]
	[JsonPropertyName("diamond")]
	public int Diamond { get; set; }

	// Token: 0x17000167 RID: 359
	// (get) Token: 0x06000407 RID: 1031 RVA: 0x000096EF File Offset: 0x000078EF
	// (set) Token: 0x06000408 RID: 1032 RVA: 0x000096F7 File Offset: 0x000078F7
	// TODO: [RequiredMember]
	[JsonPropertyName("entity_id")]
	public int EntityId { get; set; }

	// Token: 0x17000168 RID: 360
	// (get) Token: 0x06000409 RID: 1033 RVA: 0x00009700 File Offset: 0x00007900
	// (set) Token: 0x0600040A RID: 1034 RVA: 0x00009708 File Offset: 0x00007908
	// TODO: [RequiredMember]
	[JsonPropertyName("item_id")]
	public string ItemId { get; set; }

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x0600040B RID: 1035 RVA: 0x00009711 File Offset: 0x00007911
	// (set) Token: 0x0600040C RID: 1036 RVA: 0x00009719 File Offset: 0x00007919
	// TODO: [RequiredMember]
	[JsonPropertyName("item_level")]
	public int ItemLevel { get; set; }

	// Token: 0x1700016A RID: 362
	// (get) Token: 0x0600040D RID: 1037 RVA: 0x00009722 File Offset: 0x00007922
	// (set) Token: 0x0600040E RID: 1038 RVA: 0x0000972A File Offset: 0x0000792A
	// TODO: [RequiredMember]
	[JsonPropertyName("last_play_time")]
	public int LastPlayTime { get; set; }

	// Token: 0x1700016B RID: 363
	// (get) Token: 0x0600040F RID: 1039 RVA: 0x00009733 File Offset: 0x00007933
	// (set) Token: 0x06000410 RID: 1040 RVA: 0x0000973B File Offset: 0x0000793B
	// TODO: [RequiredMember]
	[JsonPropertyName("purchase_time")]
	public int PurchaseTime { get; set; }

	// Token: 0x1700016C RID: 364
	// (get) Token: 0x06000411 RID: 1041 RVA: 0x00009744 File Offset: 0x00007944
	// (set) Token: 0x06000412 RID: 1042 RVA: 0x0000974C File Offset: 0x0000794C
	// TODO: [RequiredMember]
	[JsonPropertyName("total_play_time")]
	public int TotalPlayTime { get; set; }

	// Token: 0x1700016D RID: 365
	// (get) Token: 0x06000413 RID: 1043 RVA: 0x00009755 File Offset: 0x00007955
	// (set) Token: 0x06000414 RID: 1044 RVA: 0x0000975D File Offset: 0x0000795D
	// TODO: [RequiredMember]
	[JsonPropertyName("user_id")]
	public string UserId { get; set; }

	// Token: 0x06000415 RID: 1045 RVA: 0x00009766 File Offset: 0x00007966
}