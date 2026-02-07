using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Com4399;

// Token: 0x020000A9 RID: 169
public class Entity4399VipInfo
{
	// Token: 0x17000273 RID: 627
	// (get) Token: 0x06000669 RID: 1641 RVA: 0x0000B173 File Offset: 0x00009373
	// (set) Token: 0x0600066A RID: 1642 RVA: 0x0000B17B File Offset: 0x0000937B
	[JsonPropertyName("level")]
	public int Level { get; set; }

	// Token: 0x17000274 RID: 628
	// (get) Token: 0x0600066B RID: 1643 RVA: 0x0000B184 File Offset: 0x00009384
	// (set) Token: 0x0600066C RID: 1644 RVA: 0x0000B18C File Offset: 0x0000938C
	[JsonPropertyName("score")]
	public int Score { get; set; }
}