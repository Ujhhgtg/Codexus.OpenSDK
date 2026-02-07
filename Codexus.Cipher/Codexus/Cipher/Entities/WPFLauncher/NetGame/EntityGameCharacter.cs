using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

// Token: 0x02000053 RID: 83
public class EntityGameCharacter
{
	// Token: 0x170000FE RID: 254
	// (get) Token: 0x06000321 RID: 801 RVA: 0x00008D97 File Offset: 0x00006F97
	// (set) Token: 0x06000322 RID: 802 RVA: 0x00008D9F File Offset: 0x00006F9F
	[JsonPropertyName("game_id")]
	public string GameId { get; set; } = string.Empty;

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x06000323 RID: 803 RVA: 0x00008DA8 File Offset: 0x00006FA8
	// (set) Token: 0x06000324 RID: 804 RVA: 0x00008DB0 File Offset: 0x00006FB0
	[JsonPropertyName("game_type")]
	public int GameType { get; set; } = 2;

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x06000325 RID: 805 RVA: 0x00008DB9 File Offset: 0x00006FB9
	// (set) Token: 0x06000326 RID: 806 RVA: 0x00008DC1 File Offset: 0x00006FC1
	[JsonPropertyName("user_id")]
	public string UserId { get; set; } = string.Empty;

	// Token: 0x17000101 RID: 257
	// (get) Token: 0x06000327 RID: 807 RVA: 0x00008DCA File Offset: 0x00006FCA
	// (set) Token: 0x06000328 RID: 808 RVA: 0x00008DD2 File Offset: 0x00006FD2
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x06000329 RID: 809 RVA: 0x00008DDB File Offset: 0x00006FDB
	// (set) Token: 0x0600032A RID: 810 RVA: 0x00008DE3 File Offset: 0x00006FE3
	[JsonPropertyName("create_time")]
	public int CreateTime { get; set; } = 555555;

	// Token: 0x17000103 RID: 259
	// (get) Token: 0x0600032B RID: 811 RVA: 0x00008DEC File Offset: 0x00006FEC
	// (set) Token: 0x0600032C RID: 812 RVA: 0x00008DF4 File Offset: 0x00006FF4
	[JsonPropertyName("expire_time")]
	public int ExpireTime { get; set; }
}