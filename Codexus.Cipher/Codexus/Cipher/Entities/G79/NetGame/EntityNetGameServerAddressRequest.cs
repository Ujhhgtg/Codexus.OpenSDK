using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.NetGame;

// Token: 0x0200009A RID: 154
// TODO: [RequiredMember]
public class EntityNetGameServerAddressRequest
{
	// Token: 0x17000234 RID: 564
	// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0000AA4D File Offset: 0x00008C4D
	// (set) Token: 0x060005D9 RID: 1497 RVA: 0x0000AA55 File Offset: 0x00008C55
	// TODO: [RequiredMember]
	[JsonPropertyName("item_id")]
	public string ItemId { get; set; }

	// Token: 0x060005DA RID: 1498 RVA: 0x0000AA5E File Offset: 0x00008C5E
}