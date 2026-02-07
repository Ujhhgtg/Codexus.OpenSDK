using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MPay.WhoAmi;

// Token: 0x02000087 RID: 135
public class Isp
{
	// Token: 0x170001E5 RID: 485
	// (get) Token: 0x06000527 RID: 1319 RVA: 0x0000A3EC File Offset: 0x000085EC
	// (set) Token: 0x06000528 RID: 1320 RVA: 0x0000A3F4 File Offset: 0x000085F4
	[JsonPropertyName("id")]
	public int Id { get; set; }

	// Token: 0x170001E6 RID: 486
	// (get) Token: 0x06000529 RID: 1321 RVA: 0x0000A3FD File Offset: 0x000085FD
	// (set) Token: 0x0600052A RID: 1322 RVA: 0x0000A405 File Offset: 0x00008605
	[JsonPropertyName("names")]
	public Names Names { get; set; }
}