using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

// Token: 0x02000052 RID: 82
public class EntityDetailsVideo
{
	// Token: 0x170000FB RID: 251
	// (get) Token: 0x0600031A RID: 794 RVA: 0x00008D45 File Offset: 0x00006F45
	// (set) Token: 0x0600031B RID: 795 RVA: 0x00008D4D File Offset: 0x00006F4D
	[JsonPropertyName("cover")]
	public string Cover { get; set; } = string.Empty;

	// Token: 0x170000FC RID: 252
	// (get) Token: 0x0600031C RID: 796 RVA: 0x00008D56 File Offset: 0x00006F56
	// (set) Token: 0x0600031D RID: 797 RVA: 0x00008D5E File Offset: 0x00006F5E
	[JsonPropertyName("size")]
	public int Size { get; set; }

	// Token: 0x170000FD RID: 253
	// (get) Token: 0x0600031E RID: 798 RVA: 0x00008D67 File Offset: 0x00006F67
	// (set) Token: 0x0600031F RID: 799 RVA: 0x00008D6F File Offset: 0x00006F6F
	[JsonPropertyName("url")]
	public string Url { get; set; } = string.Empty;
}