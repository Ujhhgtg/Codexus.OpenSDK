using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.RentalGame;

// Token: 0x02000048 RID: 72
public class EntityRentalGamePlayerList
{
	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x060002C3 RID: 707 RVA: 0x000089EA File Offset: 0x00006BEA
	// (set) Token: 0x060002C4 RID: 708 RVA: 0x000089F2 File Offset: 0x00006BF2
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; } = string.Empty;

	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x060002C5 RID: 709 RVA: 0x000089FB File Offset: 0x00006BFB
	// (set) Token: 0x060002C6 RID: 710 RVA: 0x00008A03 File Offset: 0x00006C03
	[JsonPropertyName("server_id")]
	public string ServerId { get; set; } = string.Empty;

	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x060002C7 RID: 711 RVA: 0x00008A0C File Offset: 0x00006C0C
	// (set) Token: 0x060002C8 RID: 712 RVA: 0x00008A14 File Offset: 0x00006C14
	[JsonPropertyName("user_id")]
	public string UserId { get; set; } = string.Empty;

	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x060002C9 RID: 713 RVA: 0x00008A1D File Offset: 0x00006C1D
	// (set) Token: 0x060002CA RID: 714 RVA: 0x00008A25 File Offset: 0x00006C25
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x060002CB RID: 715 RVA: 0x00008A2E File Offset: 0x00006C2E
	// (set) Token: 0x060002CC RID: 716 RVA: 0x00008A36 File Offset: 0x00006C36
	[JsonPropertyName("create_ts")]
	public ulong CreateTs { get; set; }

	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x060002CD RID: 717 RVA: 0x00008A3F File Offset: 0x00006C3F
	// (set) Token: 0x060002CE RID: 718 RVA: 0x00008A47 File Offset: 0x00006C47
	[JsonPropertyName("delete_ts")]
	public ulong DeleteTs { get; set; }

	// Token: 0x170000D8 RID: 216
	// (get) Token: 0x060002CF RID: 719 RVA: 0x00008A50 File Offset: 0x00006C50
	// (set) Token: 0x060002D0 RID: 720 RVA: 0x00008A58 File Offset: 0x00006C58
	[JsonPropertyName("is_online")]
	public bool IsOnline { get; set; }

	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x060002D1 RID: 721 RVA: 0x00008A61 File Offset: 0x00006C61
	// (set) Token: 0x060002D2 RID: 722 RVA: 0x00008A69 File Offset: 0x00006C69
	[JsonPropertyName("status")]
	public EnumPlayerStatus Status { get; set; }
}