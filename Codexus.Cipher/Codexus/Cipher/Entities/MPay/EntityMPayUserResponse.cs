using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MPay;

// Token: 0x0200007A RID: 122
// TODO: [RequiredMember]
public class EntityMPayUserResponse
{
	// Token: 0x170001B0 RID: 432
	// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00009EFE File Offset: 0x000080FE
	// (set) Token: 0x060004B1 RID: 1201 RVA: 0x00009F06 File Offset: 0x00008106
	[JsonPropertyName("force_pwd")]
	public bool ForcePwd { get; set; }

	// Token: 0x170001B1 RID: 433
	// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00009F0F File Offset: 0x0000810F
	// (set) Token: 0x060004B3 RID: 1203 RVA: 0x00009F17 File Offset: 0x00008117
		
	[JsonPropertyName("verify_status")]
	public EntityVerifyStatus VerifyStatus
	{
			
		get;
			
		set;
	}

	// Token: 0x170001B2 RID: 434
	// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00009F20 File Offset: 0x00008120
	// (set) Token: 0x060004B5 RID: 1205 RVA: 0x00009F28 File Offset: 0x00008128
	// TODO: [RequiredMember]
	[JsonPropertyName("user")]
	public EntityMPayUser User { get; set; } = new();

	// Token: 0x060004B6 RID: 1206 RVA: 0x00009F31 File Offset: 0x00008131
}