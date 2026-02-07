using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MPay.WhoAmi;

// Token: 0x02000080 RID: 128
public class Code1
{
	// Token: 0x170001CA RID: 458
	// (get) Token: 0x060004EA RID: 1258 RVA: 0x0000A1C1 File Offset: 0x000083C1
	// (set) Token: 0x060004EB RID: 1259 RVA: 0x0000A1C9 File Offset: 0x000083C9
	[JsonPropertyName("code")]
	public string Code { get; set; }

	// Token: 0x170001CB RID: 459
	// (get) Token: 0x060004EC RID: 1260 RVA: 0x0000A1D2 File Offset: 0x000083D2
	// (set) Token: 0x060004ED RID: 1261 RVA: 0x0000A1DA File Offset: 0x000083DA
	[JsonPropertyName("names")]
	public Names Names { get; set; }
}