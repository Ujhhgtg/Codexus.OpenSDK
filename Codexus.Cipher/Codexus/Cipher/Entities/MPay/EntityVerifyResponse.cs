using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MPay;

// Token: 0x0200007E RID: 126
public class EntityVerifyResponse
{
	// Token: 0x170001C3 RID: 451
	// (get) Token: 0x060004DA RID: 1242 RVA: 0x0000A122 File Offset: 0x00008322
	// (set) Token: 0x060004DB RID: 1243 RVA: 0x0000A12A File Offset: 0x0000832A
	[JsonPropertyName("reason")]
	public string Reason { get; set; } = string.Empty;

	// Token: 0x170001C4 RID: 452
	// (get) Token: 0x060004DC RID: 1244 RVA: 0x0000A133 File Offset: 0x00008333
	// (set) Token: 0x060004DD RID: 1245 RVA: 0x0000A13B File Offset: 0x0000833B
	[JsonPropertyName("code")]
	public int Code { get; set; }

	// Token: 0x170001C5 RID: 453
	// (get) Token: 0x060004DE RID: 1246 RVA: 0x0000A144 File Offset: 0x00008344
	// (set) Token: 0x060004DF RID: 1247 RVA: 0x0000A14C File Offset: 0x0000834C
	[JsonPropertyName("verify_url")]
	public string VerifyUrl { get; set; } = string.Empty;
}