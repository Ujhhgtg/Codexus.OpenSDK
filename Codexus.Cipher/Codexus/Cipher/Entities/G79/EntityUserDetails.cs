using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79;

// Token: 0x02000091 RID: 145
// TODO: [RequiredMember]
public class EntityUserDetails
{
	// Token: 0x17000206 RID: 518
	// (get) Token: 0x06000573 RID: 1395 RVA: 0x0000A6EE File Offset: 0x000088EE
	// (set) Token: 0x06000574 RID: 1396 RVA: 0x0000A6F6 File Offset: 0x000088F6
	// TODO: [RequiredMember]
	[JsonPropertyName("name")]
	public string Name { get; set; }

	// Token: 0x06000575 RID: 1397 RVA: 0x0000A6FF File Offset: 0x000088FF
}