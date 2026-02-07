using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MPay.WhoAmi;

// Token: 0x02000082 RID: 130
public class Code3
{
	// Token: 0x170001CE RID: 462
	// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000A217 File Offset: 0x00008417
	// (set) Token: 0x060004F5 RID: 1269 RVA: 0x0000A21F File Offset: 0x0000841F
	[JsonPropertyName("iso_code")]
	public string IsoCode { get; set; }

	// Token: 0x170001CF RID: 463
	// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0000A228 File Offset: 0x00008428
	// (set) Token: 0x060004F7 RID: 1271 RVA: 0x0000A230 File Offset: 0x00008430
	[JsonPropertyName("names")]
	public Names Names { get; set; }
}