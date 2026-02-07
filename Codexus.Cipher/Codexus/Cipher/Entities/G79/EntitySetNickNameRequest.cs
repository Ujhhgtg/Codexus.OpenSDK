using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79;

// Token: 0x02000090 RID: 144
// TODO: [RequiredMember]
public class EntitySetNickNameRequest
{
	// Token: 0x17000204 RID: 516
	// (get) Token: 0x0600056E RID: 1390 RVA: 0x0000A6C3 File Offset: 0x000088C3
	// (set) Token: 0x0600056F RID: 1391 RVA: 0x0000A6CB File Offset: 0x000088CB
	// TODO: [RequiredMember]
	[JsonPropertyName("name")]
	public string Name { get; set; }

	// Token: 0x17000205 RID: 517
	// (get) Token: 0x06000570 RID: 1392 RVA: 0x0000A6D4 File Offset: 0x000088D4
	// (set) Token: 0x06000571 RID: 1393 RVA: 0x0000A6DC File Offset: 0x000088DC
	// TODO: [RequiredMember]
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; }

	// Token: 0x06000572 RID: 1394 RVA: 0x0000A6E5 File Offset: 0x000088E5
}