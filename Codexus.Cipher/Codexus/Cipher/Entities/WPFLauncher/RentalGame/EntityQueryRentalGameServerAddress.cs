using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.RentalGame;

// Token: 0x02000045 RID: 69
public class EntityQueryRentalGameServerAddress
{
	// Token: 0x170000AB RID: 171
	// (get) Token: 0x06000272 RID: 626 RVA: 0x0000864E File Offset: 0x0000684E
	// (set) Token: 0x06000273 RID: 627 RVA: 0x00008656 File Offset: 0x00006856
	[JsonPropertyName("server_id")]
	public string ServerId { get; set; } = string.Empty;

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x06000274 RID: 628 RVA: 0x0000865F File Offset: 0x0000685F
	// (set) Token: 0x06000275 RID: 629 RVA: 0x00008667 File Offset: 0x00006867
	[JsonPropertyName("pwd")]
	public string Password { get; set; } = string.Empty;
}