using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

// Token: 0x02000057 RID: 87
// TODO: [RequiredMember]
public class EntityNetGameRequest
{
	// Token: 0x17000117 RID: 279
	// (get) Token: 0x06000357 RID: 855 RVA: 0x00009005 File Offset: 0x00007205
	// (set) Token: 0x06000358 RID: 856 RVA: 0x0000900D File Offset: 0x0000720D
	// TODO: [RequiredMember]
	[JsonPropertyName("available_mc_versions")]
	public string[] AvailableMcVersions { get; set; }

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x06000359 RID: 857 RVA: 0x00009016 File Offset: 0x00007216
	// (set) Token: 0x0600035A RID: 858 RVA: 0x0000901E File Offset: 0x0000721E
	// TODO: [RequiredMember]
	[JsonPropertyName("item_type")]
	public int ItemType { get; set; }

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x0600035B RID: 859 RVA: 0x00009027 File Offset: 0x00007227
	// (set) Token: 0x0600035C RID: 860 RVA: 0x0000902F File Offset: 0x0000722F
	// TODO: [RequiredMember]
	[JsonPropertyName("length")]
	public int Length { get; set; }

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x0600035D RID: 861 RVA: 0x00009038 File Offset: 0x00007238
	// (set) Token: 0x0600035E RID: 862 RVA: 0x00009040 File Offset: 0x00007240
	// TODO: [RequiredMember]
	[JsonPropertyName("offset")]
	public int Offset { get; set; }

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x0600035F RID: 863 RVA: 0x00009049 File Offset: 0x00007249
	// (set) Token: 0x06000360 RID: 864 RVA: 0x00009051 File Offset: 0x00007251
	// TODO: [RequiredMember]
	[JsonPropertyName("master_type_id")]
	public string MasterTypeId { get; set; }

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x06000361 RID: 865 RVA: 0x0000905A File Offset: 0x0000725A
	// (set) Token: 0x06000362 RID: 866 RVA: 0x00009062 File Offset: 0x00007262
	// TODO: [RequiredMember]
	[JsonPropertyName("secondary_type_id")]
	public string SecondaryTypeId { get; set; }

	// Token: 0x06000363 RID: 867 RVA: 0x0000906B File Offset: 0x0000726B
	public EntityNetGameRequest()
	{
	}
}