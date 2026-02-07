using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Connection;

// Token: 0x0200009D RID: 157
// TODO: [RequiredMember]
public class EntityHandshake
{
	// Token: 0x17000235 RID: 565
	// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0000AB24 File Offset: 0x00008D24
	// (set) Token: 0x060005E2 RID: 1506 RVA: 0x0000AB2C File Offset: 0x00008D2C
	// TODO: [RequiredMember]
	[JsonPropertyName("handshakeBody")]
	public required string HandshakeBody { get; set; }

	// Token: 0x060005E3 RID: 1507 RVA: 0x0000AB35 File Offset: 0x00008D35
}