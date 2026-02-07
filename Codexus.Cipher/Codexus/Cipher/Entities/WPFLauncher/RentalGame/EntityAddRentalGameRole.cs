using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.RentalGame;

// Token: 0x0200003F RID: 63
public class EntityAddRentalGameRole
{
	// Token: 0x1700009B RID: 155
	// (get) Token: 0x0600024C RID: 588 RVA: 0x000084BB File Offset: 0x000066BB
	// (set) Token: 0x0600024D RID: 589 RVA: 0x000084C3 File Offset: 0x000066C3
	[JsonPropertyName("server_id")]
	public string ServerId { get; set; } = string.Empty;

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x0600024E RID: 590 RVA: 0x000084CC File Offset: 0x000066CC
	// (set) Token: 0x0600024F RID: 591 RVA: 0x000084D4 File Offset: 0x000066D4
	[JsonPropertyName("user_id")]
	public string UserId { get; set; } = string.Empty;

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x06000250 RID: 592 RVA: 0x000084DD File Offset: 0x000066DD
	// (set) Token: 0x06000251 RID: 593 RVA: 0x000084E5 File Offset: 0x000066E5
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x06000252 RID: 594 RVA: 0x000084EE File Offset: 0x000066EE
	// (set) Token: 0x06000253 RID: 595 RVA: 0x000084F6 File Offset: 0x000066F6
	[JsonPropertyName("create_ts")]
	public int CreateTs { get; set; }

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x06000254 RID: 596 RVA: 0x000084FF File Offset: 0x000066FF
	// (set) Token: 0x06000255 RID: 597 RVA: 0x00008507 File Offset: 0x00006707
	[JsonPropertyName("is_online")]
	public bool IsOnline { get; set; }

	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x06000256 RID: 598 RVA: 0x00008510 File Offset: 0x00006710
	// (set) Token: 0x06000257 RID: 599 RVA: 0x00008518 File Offset: 0x00006718
	[JsonPropertyName("status")]
	public int Status { get; set; }
}