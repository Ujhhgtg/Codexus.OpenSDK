using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.RentalGame;

// Token: 0x02000044 RID: 68
public class EntityQueryRentalGamePlayerList
{
	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x0600026B RID: 619 RVA: 0x00008607 File Offset: 0x00006807
	// (set) Token: 0x0600026C RID: 620 RVA: 0x0000860F File Offset: 0x0000680F
	[JsonPropertyName("server_id")]
	public string ServerId { get; set; } = string.Empty;

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x0600026D RID: 621 RVA: 0x00008618 File Offset: 0x00006818
	// (set) Token: 0x0600026E RID: 622 RVA: 0x00008620 File Offset: 0x00006820
	[JsonPropertyName("offset")]
	public int Offset { get; set; }

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x0600026F RID: 623 RVA: 0x00008629 File Offset: 0x00006829
	// (set) Token: 0x06000270 RID: 624 RVA: 0x00008631 File Offset: 0x00006831
	[JsonPropertyName("length")]
	public int Length { get; set; }
}