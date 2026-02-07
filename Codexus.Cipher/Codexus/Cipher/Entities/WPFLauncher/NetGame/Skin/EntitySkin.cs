using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Skin;

// Token: 0x02000068 RID: 104
public class EntitySkin
{
	// Token: 0x1700015A RID: 346
	// (get) Token: 0x060003EB RID: 1003 RVA: 0x000095D4 File Offset: 0x000077D4
	// (set) Token: 0x060003EC RID: 1004 RVA: 0x000095DC File Offset: 0x000077DC
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; } = string.Empty;

	// Token: 0x1700015B RID: 347
	// (get) Token: 0x060003ED RID: 1005 RVA: 0x000095E5 File Offset: 0x000077E5
	// (set) Token: 0x060003EE RID: 1006 RVA: 0x000095ED File Offset: 0x000077ED
	[JsonPropertyName("brief_summary")]
	public string BriefSummary { get; set; } = string.Empty;

	// Token: 0x1700015C RID: 348
	// (get) Token: 0x060003EF RID: 1007 RVA: 0x000095F6 File Offset: 0x000077F6
	// (set) Token: 0x060003F0 RID: 1008 RVA: 0x000095FE File Offset: 0x000077FE
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	// Token: 0x1700015D RID: 349
	// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00009607 File Offset: 0x00007807
	// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0000960F File Offset: 0x0000780F
	[JsonPropertyName("title_image_url")]
	public string TitleImageUrl { get; set; } = string.Empty;

	// Token: 0x1700015E RID: 350
	// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00009618 File Offset: 0x00007818
	// (set) Token: 0x060003F4 RID: 1012 RVA: 0x00009620 File Offset: 0x00007820
	[JsonPropertyName("like_num")]
	public int LikeNum { get; set; }
}