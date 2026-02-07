using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.RentalGame;

// Token: 0x02000092 RID: 146
// TODO: [RequiredMember]
public class EntityRentalGame
{
	// Token: 0x17000207 RID: 519
	// (get) Token: 0x06000576 RID: 1398 RVA: 0x0000A708 File Offset: 0x00008908
	// (set) Token: 0x06000577 RID: 1399 RVA: 0x0000A710 File Offset: 0x00008910
	// TODO: [RequiredMember]
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; }

	// Token: 0x17000208 RID: 520
	// (get) Token: 0x06000578 RID: 1400 RVA: 0x0000A719 File Offset: 0x00008919
	// (set) Token: 0x06000579 RID: 1401 RVA: 0x0000A721 File Offset: 0x00008921
	// TODO: [RequiredMember]
	[JsonPropertyName("name")]
	public string Name { get; set; }

	// Token: 0x17000209 RID: 521
	// (get) Token: 0x0600057A RID: 1402 RVA: 0x0000A72A File Offset: 0x0000892A
	// (set) Token: 0x0600057B RID: 1403 RVA: 0x0000A732 File Offset: 0x00008932
	// TODO: [RequiredMember]
	[JsonPropertyName("player_count")]
	public int PlayerCount { get; set; }

	// Token: 0x1700020A RID: 522
	// (get) Token: 0x0600057C RID: 1404 RVA: 0x0000A73B File Offset: 0x0000893B
	// (set) Token: 0x0600057D RID: 1405 RVA: 0x0000A743 File Offset: 0x00008943
	// TODO: [RequiredMember]
	[JsonPropertyName("server_name")]
	public string ServerName { get; set; }

	// Token: 0x0600057E RID: 1406 RVA: 0x0000A74C File Offset: 0x0000894C
}