using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

// Token: 0x0200004F RID: 79
// TODO: [RequiredMember]
public class EntityComponentDownloadInfoResponse
{
	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x060002EB RID: 747 RVA: 0x00008BA2 File Offset: 0x00006DA2
	// (set) Token: 0x060002EC RID: 748 RVA: 0x00008BAA File Offset: 0x00006DAA
	// TODO: [RequiredMember]
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; }

	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x060002ED RID: 749 RVA: 0x00008BB3 File Offset: 0x00006DB3
	// (set) Token: 0x060002EE RID: 750 RVA: 0x00008BBB File Offset: 0x00006DBB
	// TODO: [RequiredMember]
	[JsonPropertyName("item_id")]
	public string ItemId { get; set; }

	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x060002EF RID: 751 RVA: 0x00008BC4 File Offset: 0x00006DC4
	// (set) Token: 0x060002F0 RID: 752 RVA: 0x00008BCC File Offset: 0x00006DCC
	// TODO: [RequiredMember]
	[JsonPropertyName("user_id")]
	public string UserId { get; set; }

	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x060002F1 RID: 753 RVA: 0x00008BD5 File Offset: 0x00006DD5
	// (set) Token: 0x060002F2 RID: 754 RVA: 0x00008BDD File Offset: 0x00006DDD
	// TODO: [RequiredMember]
	[JsonPropertyName("itype")]
	public int IType { get; set; }

	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x060002F3 RID: 755 RVA: 0x00008BE6 File Offset: 0x00006DE6
	// (set) Token: 0x060002F4 RID: 756 RVA: 0x00008BEE File Offset: 0x00006DEE
	// TODO: [RequiredMember]
	[JsonPropertyName("mtypeid")]
	public int MTypeId { get; set; }

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x060002F5 RID: 757 RVA: 0x00008BF7 File Offset: 0x00006DF7
	// (set) Token: 0x060002F6 RID: 758 RVA: 0x00008BFF File Offset: 0x00006DFF
	// TODO: [RequiredMember]
	[JsonPropertyName("stypeid")]
	public int STypeId { get; set; }

	// Token: 0x170000EB RID: 235
	// (get) Token: 0x060002F7 RID: 759 RVA: 0x00008C08 File Offset: 0x00006E08
	// (set) Token: 0x060002F8 RID: 760 RVA: 0x00008C10 File Offset: 0x00006E10
	// TODO: [RequiredMember]
	[JsonPropertyName("sub_entities")]
	public List<EntityComponentDownloadInfoResponseSub> SubEntities { get; set; }

	// Token: 0x170000EC RID: 236
	// (get) Token: 0x060002F9 RID: 761 RVA: 0x00008C19 File Offset: 0x00006E19
	// (set) Token: 0x060002FA RID: 762 RVA: 0x00008C21 File Offset: 0x00006E21
	// TODO: [RequiredMember]
	[JsonPropertyName("sub_mod_list")]
	public List<ulong> SubModList { get; set; }

	// Token: 0x060002FB RID: 763 RVA: 0x00008C2A File Offset: 0x00006E2A
	public EntityComponentDownloadInfoResponse()
	{
	}
}