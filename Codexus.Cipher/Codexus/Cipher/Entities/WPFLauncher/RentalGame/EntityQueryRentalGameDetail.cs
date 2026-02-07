using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.RentalGame;

// Token: 0x02000043 RID: 67
public class EntityQueryRentalGameDetail
{
	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x06000268 RID: 616 RVA: 0x000085E2 File Offset: 0x000067E2
	// (set) Token: 0x06000269 RID: 617 RVA: 0x000085EA File Offset: 0x000067EA
	[JsonPropertyName("server_id")]
	public string ServerId { get; set; } = string.Empty;
}