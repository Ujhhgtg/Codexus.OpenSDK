using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.Minecraft.Mods;

// Token: 0x02000072 RID: 114
// TODO: [RequiredMember]
public class EntitySearchByItemIdQuery
{
	// Token: 0x1700018B RID: 395
	// (get) Token: 0x06000456 RID: 1110 RVA: 0x000099B3 File Offset: 0x00007BB3
	// (set) Token: 0x06000457 RID: 1111 RVA: 0x000099BB File Offset: 0x00007BBB
	// TODO: [RequiredMember]
	[JsonPropertyName("item_id")]
	public string ItemId { get; set; }

	// Token: 0x1700018C RID: 396
	// (get) Token: 0x06000458 RID: 1112 RVA: 0x000099C4 File Offset: 0x00007BC4
	// (set) Token: 0x06000459 RID: 1113 RVA: 0x000099CC File Offset: 0x00007BCC
	// TODO: [RequiredMember]
	[JsonPropertyName("length")]
	public int Length { get; set; }

	// Token: 0x1700018D RID: 397
	// (get) Token: 0x0600045A RID: 1114 RVA: 0x000099D5 File Offset: 0x00007BD5
	// (set) Token: 0x0600045B RID: 1115 RVA: 0x000099DD File Offset: 0x00007BDD
	// TODO: [RequiredMember]
	[JsonPropertyName("offset")]
	public int Offset { get; set; }

	// Token: 0x0600045C RID: 1116 RVA: 0x000099E6 File Offset: 0x00007BE6
	public EntitySearchByItemIdQuery()
	{
	}
}