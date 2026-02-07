using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.RentalGame;

// Token: 0x02000095 RID: 149
// TODO: [RequiredMember]
public class EntityRentalGameServerAddressRequest
{
	// Token: 0x17000210 RID: 528
	// (get) Token: 0x0600058B RID: 1419 RVA: 0x0000A7BC File Offset: 0x000089BC
	// (set) Token: 0x0600058C RID: 1420 RVA: 0x0000A7C4 File Offset: 0x000089C4
	// TODO: [RequiredMember]
	[JsonPropertyName("server_id")]
	public string ServerId { get; set; }

	// Token: 0x17000211 RID: 529
	// (get) Token: 0x0600058D RID: 1421 RVA: 0x0000A7CD File Offset: 0x000089CD
	// (set) Token: 0x0600058E RID: 1422 RVA: 0x0000A7D5 File Offset: 0x000089D5
	// TODO: [RequiredMember]
	[JsonPropertyName("pwd")]
	public string Password { get; set; }

	// Token: 0x0600058F RID: 1423 RVA: 0x0000A7DE File Offset: 0x000089DE
}