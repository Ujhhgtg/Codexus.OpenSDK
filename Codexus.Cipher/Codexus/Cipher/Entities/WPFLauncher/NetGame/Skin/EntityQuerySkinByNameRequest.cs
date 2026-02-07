using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Skin;

// Token: 0x02000067 RID: 103
// TODO: [RequiredMember]
public class EntityQuerySkinByNameRequest
{
	// Token: 0x1700014F RID: 335
	// (get) Token: 0x060003D4 RID: 980 RVA: 0x00009510 File Offset: 0x00007710
	// (set) Token: 0x060003D5 RID: 981 RVA: 0x00009518 File Offset: 0x00007718
	// TODO: [RequiredMember]
	[JsonPropertyName("is_has")]
	public bool IsHas { get; set; }

	// Token: 0x17000150 RID: 336
	// (get) Token: 0x060003D6 RID: 982 RVA: 0x00009521 File Offset: 0x00007721
	// (set) Token: 0x060003D7 RID: 983 RVA: 0x00009529 File Offset: 0x00007729
	// TODO: [RequiredMember]
	[JsonPropertyName("is_sync")]
	public int IsSync { get; set; }

	// Token: 0x17000151 RID: 337
	// (get) Token: 0x060003D8 RID: 984 RVA: 0x00009532 File Offset: 0x00007732
	// (set) Token: 0x060003D9 RID: 985 RVA: 0x0000953A File Offset: 0x0000773A
	// TODO: [RequiredMember]
	[JsonPropertyName("item_type")]
	public int ItemType { get; set; }

	// Token: 0x17000152 RID: 338
	// (get) Token: 0x060003DA RID: 986 RVA: 0x00009543 File Offset: 0x00007743
	// (set) Token: 0x060003DB RID: 987 RVA: 0x0000954B File Offset: 0x0000774B
	// TODO: [RequiredMember]
	[JsonPropertyName("keyword")]
	public string Keyword { get; set; }

	// Token: 0x17000153 RID: 339
	// (get) Token: 0x060003DC RID: 988 RVA: 0x00009554 File Offset: 0x00007754
	// (set) Token: 0x060003DD RID: 989 RVA: 0x0000955C File Offset: 0x0000775C
	// TODO: [RequiredMember]
	[JsonPropertyName("length")]
	public int Length { get; set; }

	// Token: 0x17000154 RID: 340
	// (get) Token: 0x060003DE RID: 990 RVA: 0x00009565 File Offset: 0x00007765
	// (set) Token: 0x060003DF RID: 991 RVA: 0x0000956D File Offset: 0x0000776D
	// TODO: [RequiredMember]
	[JsonPropertyName("master_type_id")]
	public int MasterTypeId { get; set; }

	// Token: 0x17000155 RID: 341
	// (get) Token: 0x060003E0 RID: 992 RVA: 0x00009576 File Offset: 0x00007776
	// (set) Token: 0x060003E1 RID: 993 RVA: 0x0000957E File Offset: 0x0000777E
	// TODO: [RequiredMember]
	[JsonPropertyName("offset")]
	public int Offset { get; set; }

	// Token: 0x17000156 RID: 342
	// (get) Token: 0x060003E2 RID: 994 RVA: 0x00009587 File Offset: 0x00007787
	// (set) Token: 0x060003E3 RID: 995 RVA: 0x0000958F File Offset: 0x0000778F
	// TODO: [RequiredMember]
	[JsonPropertyName("price_type")]
	public int PriceType { get; set; }

	// Token: 0x17000157 RID: 343
	// (get) Token: 0x060003E4 RID: 996 RVA: 0x00009598 File Offset: 0x00007798
	// (set) Token: 0x060003E5 RID: 997 RVA: 0x000095A0 File Offset: 0x000077A0
	// TODO: [RequiredMember]
	[JsonPropertyName("secondary_type_id")]
	public string SecondaryTypeId { get; set; }

	// Token: 0x17000158 RID: 344
	// (get) Token: 0x060003E6 RID: 998 RVA: 0x000095A9 File Offset: 0x000077A9
	// (set) Token: 0x060003E7 RID: 999 RVA: 0x000095B1 File Offset: 0x000077B1
	// TODO: [RequiredMember]
	[JsonPropertyName("sort_type")]
	public int SortType { get; set; }

	// Token: 0x17000159 RID: 345
	// (get) Token: 0x060003E8 RID: 1000 RVA: 0x000095BA File Offset: 0x000077BA
	// (set) Token: 0x060003E9 RID: 1001 RVA: 0x000095C2 File Offset: 0x000077C2
	// TODO: [RequiredMember]
	[JsonPropertyName("year")]
	public int Year { get; set; }

	// Token: 0x060003EA RID: 1002 RVA: 0x000095CB File Offset: 0x000077CB
	public EntityQuerySkinByNameRequest()
	{
	}
}