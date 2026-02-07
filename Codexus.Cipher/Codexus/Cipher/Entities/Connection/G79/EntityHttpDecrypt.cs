using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Connection.G79;

// Token: 0x0200009F RID: 159
// TODO: [RequiredMember]
public class EntityHttpDecrypt
{
	// Token: 0x17000237 RID: 567
	// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0000AB58 File Offset: 0x00008D58
	// (set) Token: 0x060005E8 RID: 1512 RVA: 0x0000AB60 File Offset: 0x00008D60
	// TODO: [RequiredMember]
	[JsonPropertyName("body")]
	public string Body { get; set; }

	// Token: 0x060005E9 RID: 1513 RVA: 0x0000AB69 File Offset: 0x00008D69
}