using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MPay.WhoAmi;

// Token: 0x02000085 RID: 133
public class EntityWhoAmi
{
	// Token: 0x170001DB RID: 475
	// (get) Token: 0x06000511 RID: 1297 RVA: 0x0000A330 File Offset: 0x00008530
	// (set) Token: 0x06000512 RID: 1298 RVA: 0x0000A338 File Offset: 0x00008538
	[JsonPropertyName("payload")]
	public string Payload { get; set; }

	// Token: 0x170001DC RID: 476
	// (get) Token: 0x06000513 RID: 1299 RVA: 0x0000A341 File Offset: 0x00008541
	// (set) Token: 0x06000514 RID: 1300 RVA: 0x0000A349 File Offset: 0x00008549
	[JsonPropertyName("sig")]
	public string Signature { get; set; }

	// Token: 0x170001DD RID: 477
	// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000A352 File Offset: 0x00008552
	// (set) Token: 0x06000516 RID: 1302 RVA: 0x0000A35A File Offset: 0x0000855A
	[JsonPropertyName("status")]
	public int Status { get; set; }

	// Token: 0x170001DE RID: 478
	// (get) Token: 0x06000517 RID: 1303 RVA: 0x0000A363 File Offset: 0x00008563
	// (set) Token: 0x06000518 RID: 1304 RVA: 0x0000A36B File Offset: 0x0000856B
	[JsonPropertyName("why")]
	public string Why { get; set; }
}