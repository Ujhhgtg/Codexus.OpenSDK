using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79;

// Token: 0x0200008F RID: 143
// TODO: [RequiredMember]
public class EntitySetNickName
{
	// Token: 0x17000203 RID: 515
	// (get) Token: 0x0600056B RID: 1387 RVA: 0x0000A6A9 File Offset: 0x000088A9
	// (set) Token: 0x0600056C RID: 1388 RVA: 0x0000A6B1 File Offset: 0x000088B1
	// TODO: [RequiredMember]
	[JsonPropertyName("name")]
	public required string Name { get; set; }

	// Token: 0x0600056D RID: 1389 RVA: 0x0000A6BA File Offset: 0x000088BA
}