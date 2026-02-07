using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MPay.WhoAmi;

// Token: 0x02000088 RID: 136
public class Names
{
	// Token: 0x170001E7 RID: 487
	// (get) Token: 0x0600052C RID: 1324 RVA: 0x0000A417 File Offset: 0x00008617
	// (set) Token: 0x0600052D RID: 1325 RVA: 0x0000A41F File Offset: 0x0000861F
	[JsonPropertyName("en")]
	public string En { get; set; }
}