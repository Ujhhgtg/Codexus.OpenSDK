using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MPay.WhoAmi;

// Token: 0x02000083 RID: 131
public class Code4
{
	// Token: 0x170001D0 RID: 464
	// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0000A242 File Offset: 0x00008442
	// (set) Token: 0x060004FA RID: 1274 RVA: 0x0000A24A File Offset: 0x0000844A
	[JsonPropertyName("id")]
	public int Id { get; set; }

	// Token: 0x170001D1 RID: 465
	// (get) Token: 0x060004FB RID: 1275 RVA: 0x0000A253 File Offset: 0x00008453
	// (set) Token: 0x060004FC RID: 1276 RVA: 0x0000A25B File Offset: 0x0000845B
	[JsonPropertyName("names")]
	public Names Names { get; set; }
}