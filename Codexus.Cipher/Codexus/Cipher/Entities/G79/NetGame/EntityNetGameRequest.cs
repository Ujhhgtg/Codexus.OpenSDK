using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.NetGame;

// Token: 0x02000098 RID: 152
// TODO: [RequiredMember]
public class EntityNetGameRequest
{
	// Token: 0x17000230 RID: 560
	// (get) Token: 0x060005CE RID: 1486 RVA: 0x0000A9F7 File Offset: 0x00008BF7
	// (set) Token: 0x060005CF RID: 1487 RVA: 0x0000A9FF File Offset: 0x00008BFF
	// TODO: [RequiredMember]
	[JsonPropertyName("version")]
	public string Version { get; set; }

	// Token: 0x17000231 RID: 561
	// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0000AA08 File Offset: 0x00008C08
	// (set) Token: 0x060005D1 RID: 1489 RVA: 0x0000AA10 File Offset: 0x00008C10
	// TODO: [RequiredMember]
	[JsonPropertyName("channel_id")]
	public int ChannelId { get; set; }

	// Token: 0x060005D2 RID: 1490 RVA: 0x0000AA19 File Offset: 0x00008C19
	public EntityNetGameRequest()
	{
	}
}