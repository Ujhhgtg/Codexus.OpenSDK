using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Connection.G79;

// Token: 0x020000A1 RID: 161
// TODO: [RequiredMember]
public class EntityZkpGetCheckNum
{
	// Token: 0x17000239 RID: 569
	// (get) Token: 0x060005ED RID: 1517 RVA: 0x0000AB8C File Offset: 0x00008D8C
	// (set) Token: 0x060005EE RID: 1518 RVA: 0x0000AB94 File Offset: 0x00008D94
	// TODO: [RequiredMember]
	[JsonPropertyName("body")]
	public required string Body { get; set; }

	// Token: 0x060005EF RID: 1519 RVA: 0x0000AB9D File Offset: 0x00008D9D
}