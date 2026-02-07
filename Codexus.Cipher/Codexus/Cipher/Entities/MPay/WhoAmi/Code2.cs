using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MPay.WhoAmi;

// Token: 0x02000081 RID: 129
public class Code2
{
	// Token: 0x170001CC RID: 460
	// (get) Token: 0x060004EF RID: 1263 RVA: 0x0000A1EC File Offset: 0x000083EC
	// (set) Token: 0x060004F0 RID: 1264 RVA: 0x0000A1F4 File Offset: 0x000083F4
	[JsonPropertyName("iso_code")]
	public string IsoCode { get; set; }

	// Token: 0x170001CD RID: 461
	// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0000A1FD File Offset: 0x000083FD
	// (set) Token: 0x060004F2 RID: 1266 RVA: 0x0000A205 File Offset: 0x00008405
	[JsonPropertyName("names")]
	public Names Names { get; set; }
}