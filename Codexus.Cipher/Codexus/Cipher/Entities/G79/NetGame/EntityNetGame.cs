using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.NetGame;

// Token: 0x02000096 RID: 150
// TODO: [RequiredMember]
public class EntityNetGame
{
	// Token: 0x17000212 RID: 530
	// (get) Token: 0x06000590 RID: 1424 RVA: 0x0000A7E7 File Offset: 0x000089E7
	// (set) Token: 0x06000591 RID: 1425 RVA: 0x0000A7EF File Offset: 0x000089EF
	// TODO: [RequiredMember]
	[JsonPropertyName("res")]
	public required List<EntityNetGameItem> Res { get; set; }

	// Token: 0x17000213 RID: 531
	// (get) Token: 0x06000592 RID: 1426 RVA: 0x0000A7F8 File Offset: 0x000089F8
	// (set) Token: 0x06000593 RID: 1427 RVA: 0x0000A800 File Offset: 0x00008A00
	// TODO: [RequiredMember]
	[JsonPropertyName("tag")]
	public required int Tag { get; set; }

	// Token: 0x17000214 RID: 532
	// (get) Token: 0x06000594 RID: 1428 RVA: 0x0000A809 File Offset: 0x00008A09
	// (set) Token: 0x06000595 RID: 1429 RVA: 0x0000A811 File Offset: 0x00008A11
	// TODO: [RequiredMember]
	[JsonPropertyName("campaign_id")]
	public required int CampaignId { get; set; }

	// Token: 0x06000596 RID: 1430 RVA: 0x0000A81A File Offset: 0x00008A1A
}