using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.RentalGame;

// Token: 0x02000040 RID: 64
public class EntityDeleteRentalGameRole
{
	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x06000259 RID: 601 RVA: 0x0000854B File Offset: 0x0000674B
	// (set) Token: 0x0600025A RID: 602 RVA: 0x00008553 File Offset: 0x00006753
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; } = string.Empty;
}