using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

// Token: 0x02000058 RID: 88
public class EntityNetGameServerAddress
{
	// Token: 0x1700011D RID: 285
	// (get) Token: 0x06000364 RID: 868 RVA: 0x00009074 File Offset: 0x00007274
	// (set) Token: 0x06000365 RID: 869 RVA: 0x0000907C File Offset: 0x0000727C
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; } = string.Empty;

	// Token: 0x1700011E RID: 286
	// (get) Token: 0x06000366 RID: 870 RVA: 0x00009085 File Offset: 0x00007285
	// (set) Token: 0x06000367 RID: 871 RVA: 0x0000908D File Offset: 0x0000728D
	[JsonPropertyName("isp_enable")]
	public bool IspEnable { get; set; } = true;

	// Token: 0x1700011F RID: 287
	// (get) Token: 0x06000368 RID: 872 RVA: 0x00009096 File Offset: 0x00007296
	// (set) Token: 0x06000369 RID: 873 RVA: 0x0000909E File Offset: 0x0000729E
	[JsonPropertyName("ip")]
	public string Ip { get; set; } = string.Empty;

	// Token: 0x17000120 RID: 288
	// (get) Token: 0x0600036A RID: 874 RVA: 0x000090A7 File Offset: 0x000072A7
	// (set) Token: 0x0600036B RID: 875 RVA: 0x000090AF File Offset: 0x000072AF
	[JsonPropertyName("port")]
	public int Port { get; set; }

	// Token: 0x17000121 RID: 289
	// (get) Token: 0x0600036C RID: 876 RVA: 0x000090B8 File Offset: 0x000072B8
	// (set) Token: 0x0600036D RID: 877 RVA: 0x000090C0 File Offset: 0x000072C0
	[JsonPropertyName("game_status")]
	public int GameStatus { get; set; }

	// Token: 0x17000122 RID: 290
	// (get) Token: 0x0600036E RID: 878 RVA: 0x000090C9 File Offset: 0x000072C9
	// (set) Token: 0x0600036F RID: 879 RVA: 0x000090D1 File Offset: 0x000072D1
	[JsonPropertyName("announcement")]
	public string Announcement { get; set; } = string.Empty;

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x06000370 RID: 880 RVA: 0x000090DA File Offset: 0x000072DA
	// (set) Token: 0x06000371 RID: 881 RVA: 0x000090E2 File Offset: 0x000072E2
	[JsonPropertyName("in_whitelist")]
	public bool InWhitelist { get; set; }
}