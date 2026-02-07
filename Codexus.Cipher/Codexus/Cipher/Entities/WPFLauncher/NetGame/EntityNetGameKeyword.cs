using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

// Token: 0x02000056 RID: 86
// TODO: [RequiredMember]
public class EntityNetGameKeyword
{
	// Token: 0x1700010B RID: 267
	// (get) Token: 0x0600033E RID: 830 RVA: 0x00008F04 File Offset: 0x00007104
	// (set) Token: 0x0600033F RID: 831 RVA: 0x00008F0C File Offset: 0x0000710C
	[JsonPropertyName("is_has")]
	public bool IsHas { get; set; }

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x06000340 RID: 832 RVA: 0x00008F15 File Offset: 0x00007115
	// (set) Token: 0x06000341 RID: 833 RVA: 0x00008F1D File Offset: 0x0000711D
	[JsonPropertyName("is_sync")]
	public int IsSync { get; set; }

	// Token: 0x1700010D RID: 269
	// (get) Token: 0x06000342 RID: 834 RVA: 0x00008F26 File Offset: 0x00007126
	// (set) Token: 0x06000343 RID: 835 RVA: 0x00008F2E File Offset: 0x0000712E
	[JsonPropertyName("item_type")]
	public int ItemType { get; set; } = 1;

	// Token: 0x1700010E RID: 270
	// (get) Token: 0x06000344 RID: 836 RVA: 0x00008F37 File Offset: 0x00007137
	// (set) Token: 0x06000345 RID: 837 RVA: 0x00008F3F File Offset: 0x0000713F
	// TODO: [RequiredMember]
	[JsonPropertyName("keyword")]
	public string Keyword { get; set; }

	// Token: 0x1700010F RID: 271
	// (get) Token: 0x06000346 RID: 838 RVA: 0x00008F48 File Offset: 0x00007148
	// (set) Token: 0x06000347 RID: 839 RVA: 0x00008F50 File Offset: 0x00007150
	[JsonPropertyName("length")]
	public int Length { get; set; } = 24;

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x06000348 RID: 840 RVA: 0x00008F59 File Offset: 0x00007159
	// (set) Token: 0x06000349 RID: 841 RVA: 0x00008F61 File Offset: 0x00007161
	[JsonPropertyName("master_type_id")]
	public string MasterTypeId { get; set; } = "2";

	// Token: 0x17000111 RID: 273
	// (get) Token: 0x0600034A RID: 842 RVA: 0x00008F6A File Offset: 0x0000716A
	// (set) Token: 0x0600034B RID: 843 RVA: 0x00008F72 File Offset: 0x00007172
	[JsonPropertyName("network_tag")]
	public bool NetworkTag { get; set; }

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x0600034C RID: 844 RVA: 0x00008F7B File Offset: 0x0000717B
	// (set) Token: 0x0600034D RID: 845 RVA: 0x00008F83 File Offset: 0x00007183
	[JsonPropertyName("offset")]
	public int Offset { get; set; }

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x0600034E RID: 846 RVA: 0x00008F8C File Offset: 0x0000718C
	// (set) Token: 0x0600034F RID: 847 RVA: 0x00008F94 File Offset: 0x00007194
	[JsonPropertyName("order")]
	public int Order { get; set; }

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x06000350 RID: 848 RVA: 0x00008F9D File Offset: 0x0000719D
	// (set) Token: 0x06000351 RID: 849 RVA: 0x00008FA5 File Offset: 0x000071A5
	[JsonPropertyName("secondary_type_id")]
	public string SecondaryTypeId { get; set; } = "";

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x06000352 RID: 850 RVA: 0x00008FAE File Offset: 0x000071AE
	// (set) Token: 0x06000353 RID: 851 RVA: 0x00008FB6 File Offset: 0x000071B6
	[JsonPropertyName("sort_type")]
	public int SortType { get; set; } = 2;

	// Token: 0x17000116 RID: 278
	// (get) Token: 0x06000354 RID: 852 RVA: 0x00008FBF File Offset: 0x000071BF
	// (set) Token: 0x06000355 RID: 853 RVA: 0x00008FC7 File Offset: 0x000071C7
	[JsonPropertyName("year")]
	public int Year { get; set; }

	// Token: 0x06000356 RID: 854 RVA: 0x00008FD0 File Offset: 0x000071D0
	public EntityNetGameKeyword()
	{
	}
}