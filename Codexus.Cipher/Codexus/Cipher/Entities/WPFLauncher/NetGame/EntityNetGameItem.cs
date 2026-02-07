using System.Text.Json.Serialization;
using Codexus.Cipher.Entities.Converter;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

// Token: 0x02000055 RID: 85
public class EntityNetGameItem
{
	// Token: 0x17000106 RID: 262
	// (get) Token: 0x06000333 RID: 819 RVA: 0x00008E6F File Offset: 0x0000706F
	// (set) Token: 0x06000334 RID: 820 RVA: 0x00008E77 File Offset: 0x00007077
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; } = string.Empty;

	// Token: 0x17000107 RID: 263
	// (get) Token: 0x06000335 RID: 821 RVA: 0x00008E80 File Offset: 0x00007080
	// (set) Token: 0x06000336 RID: 822 RVA: 0x00008E88 File Offset: 0x00007088
	[JsonPropertyName("brief_summary")]
	public string BriefSummary { get; set; } = string.Empty;

	// Token: 0x17000108 RID: 264
	// (get) Token: 0x06000337 RID: 823 RVA: 0x00008E91 File Offset: 0x00007091
	// (set) Token: 0x06000338 RID: 824 RVA: 0x00008E99 File Offset: 0x00007099
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	// Token: 0x17000109 RID: 265
	// (get) Token: 0x06000339 RID: 825 RVA: 0x00008EA2 File Offset: 0x000070A2
	// (set) Token: 0x0600033A RID: 826 RVA: 0x00008EAA File Offset: 0x000070AA
	[JsonPropertyName("online_count")]
	[JsonConverter(typeof(NetEaseIntConverter))]
	public string OnlineCount { get; set; } = string.Empty;

	// Token: 0x1700010A RID: 266
	// (get) Token: 0x0600033B RID: 827 RVA: 0x00008EB3 File Offset: 0x000070B3
	// (set) Token: 0x0600033C RID: 828 RVA: 0x00008EBB File Offset: 0x000070BB
	[JsonPropertyName("title_image_url")]
	public string TitleImageUrl { get; set; } = string.Empty;
}