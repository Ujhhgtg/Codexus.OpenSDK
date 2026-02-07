using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

// Token: 0x02000060 RID: 96
// TODO: [RequiredMember]
public class EntitySearchByIdsQuery
{
	// Token: 0x1700013E RID: 318
	// (get) Token: 0x060003AE RID: 942 RVA: 0x0000939F File Offset: 0x0000759F
	// (set) Token: 0x060003AF RID: 943 RVA: 0x000093A7 File Offset: 0x000075A7
	// TODO: [RequiredMember]
	[JsonPropertyName("item_id_list")]
	public List<ulong> ItemIdList { get; set; }

	// Token: 0x060003B0 RID: 944 RVA: 0x000093B0 File Offset: 0x000075B0
	public EntitySearchByIdsQuery()
	{
	}
}