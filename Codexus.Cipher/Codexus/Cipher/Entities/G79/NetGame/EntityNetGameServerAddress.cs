using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.NetGame;

// Token: 0x02000099 RID: 153
// TODO: [RequiredMember]
public class EntityNetGameServerAddress
{
	// Token: 0x17000232 RID: 562
	// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0000AA22 File Offset: 0x00008C22
	// (set) Token: 0x060005D4 RID: 1492 RVA: 0x0000AA2A File Offset: 0x00008C2A
	// TODO: [RequiredMember]
	[JsonPropertyName("host")]
	public string Host { get; set; }

	// Token: 0x17000233 RID: 563
	// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0000AA33 File Offset: 0x00008C33
	// (set) Token: 0x060005D6 RID: 1494 RVA: 0x0000AA3B File Offset: 0x00008C3B
	// TODO: [RequiredMember]
	[JsonPropertyName("port")]
	public int Port { get; set; }

	// Token: 0x060005D7 RID: 1495 RVA: 0x0000AA44 File Offset: 0x00008C44
}