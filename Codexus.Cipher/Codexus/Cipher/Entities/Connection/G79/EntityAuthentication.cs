using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Connection.G79;

// Token: 0x0200009E RID: 158
// TODO: [RequiredMember]
public class EntityAuthentication
{
	// Token: 0x17000236 RID: 566
	// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0000AB3E File Offset: 0x00008D3E
	// (set) Token: 0x060005E5 RID: 1509 RVA: 0x0000AB46 File Offset: 0x00008D46
	// TODO: [RequiredMember]
	[JsonPropertyName("body")]
	public required string Body { get; set; }

	// Token: 0x060005E6 RID: 1510 RVA: 0x0000AB4F File Offset: 0x00008D4F
}