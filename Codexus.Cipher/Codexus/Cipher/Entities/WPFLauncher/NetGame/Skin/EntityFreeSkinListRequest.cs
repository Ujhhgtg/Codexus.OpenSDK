using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Skin;

// Token: 0x02000066 RID: 102
// TODO: [RequiredMember]
public class EntityFreeSkinListRequest
{
	// Token: 0x17000148 RID: 328
	// (get) Token: 0x060003C5 RID: 965 RVA: 0x00009490 File Offset: 0x00007690
	// (set) Token: 0x060003C6 RID: 966 RVA: 0x00009498 File Offset: 0x00007698
	// TODO: [RequiredMember]
	[JsonPropertyName("is_has")]
	public bool IsHas { get; set; }

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x060003C7 RID: 967 RVA: 0x000094A1 File Offset: 0x000076A1
	// (set) Token: 0x060003C8 RID: 968 RVA: 0x000094A9 File Offset: 0x000076A9
	// TODO: [RequiredMember]
	[JsonPropertyName("item_type")]
	public int ItemType { get; set; }

	// Token: 0x1700014A RID: 330
	// (get) Token: 0x060003C9 RID: 969 RVA: 0x000094B2 File Offset: 0x000076B2
	// (set) Token: 0x060003CA RID: 970 RVA: 0x000094BA File Offset: 0x000076BA
	// TODO: [RequiredMember]
	[JsonPropertyName("length")]
	public int Length { get; set; }

	// Token: 0x1700014B RID: 331
	// (get) Token: 0x060003CB RID: 971 RVA: 0x000094C3 File Offset: 0x000076C3
	// (set) Token: 0x060003CC RID: 972 RVA: 0x000094CB File Offset: 0x000076CB
	// TODO: [RequiredMember]
	[JsonPropertyName("master_type_id")]
	public int MasterTypeId { get; set; }

	// Token: 0x1700014C RID: 332
	// (get) Token: 0x060003CD RID: 973 RVA: 0x000094D4 File Offset: 0x000076D4
	// (set) Token: 0x060003CE RID: 974 RVA: 0x000094DC File Offset: 0x000076DC
	// TODO: [RequiredMember]
	[JsonPropertyName("offset")]
	public int Offset { get; set; }

	// Token: 0x1700014D RID: 333
	// (get) Token: 0x060003CF RID: 975 RVA: 0x000094E5 File Offset: 0x000076E5
	// (set) Token: 0x060003D0 RID: 976 RVA: 0x000094ED File Offset: 0x000076ED
	// TODO: [RequiredMember]
	[JsonPropertyName("price_type")]
	public int PriceType { get; set; }

	// Token: 0x1700014E RID: 334
	// (get) Token: 0x060003D1 RID: 977 RVA: 0x000094F6 File Offset: 0x000076F6
	// (set) Token: 0x060003D2 RID: 978 RVA: 0x000094FE File Offset: 0x000076FE
	// TODO: [RequiredMember]
	[JsonPropertyName("secondary_type_id")]
	public int SecondaryTypeId { get; set; }

	// Token: 0x060003D3 RID: 979 RVA: 0x00009507 File Offset: 0x00007707
}