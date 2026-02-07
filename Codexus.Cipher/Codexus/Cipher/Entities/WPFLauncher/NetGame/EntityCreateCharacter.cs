using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

// Token: 0x02000051 RID: 81
// TODO: [RequiredMember]
public class EntityCreateCharacter
{
	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x0600030D RID: 781 RVA: 0x00008CC4 File Offset: 0x00006EC4
	// (set) Token: 0x0600030E RID: 782 RVA: 0x00008CCC File Offset: 0x00006ECC
	// TODO: [RequiredMember]
	[JsonPropertyName("game_id")]
	public string GameId { get; set; }

	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x0600030F RID: 783 RVA: 0x00008CD5 File Offset: 0x00006ED5
	// (set) Token: 0x06000310 RID: 784 RVA: 0x00008CDD File Offset: 0x00006EDD
	[JsonPropertyName("game_type")]
	public int GameType { get; set; } = 2;

	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x06000311 RID: 785 RVA: 0x00008CE6 File Offset: 0x00006EE6
	// (set) Token: 0x06000312 RID: 786 RVA: 0x00008CEE File Offset: 0x00006EEE
	// TODO: [RequiredMember]
	[JsonPropertyName("user_id")]
	public string UserId { get; set; }

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x06000313 RID: 787 RVA: 0x00008CF7 File Offset: 0x00006EF7
	// (set) Token: 0x06000314 RID: 788 RVA: 0x00008CFF File Offset: 0x00006EFF
	// TODO: [RequiredMember]
	[JsonPropertyName("name")]
	public string Name { get; set; }

	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x06000315 RID: 789 RVA: 0x00008D08 File Offset: 0x00006F08
	// (set) Token: 0x06000316 RID: 790 RVA: 0x00008D10 File Offset: 0x00006F10
	[JsonPropertyName("create_time")]
	public int CreateTime { get; set; } = 555555;

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x06000317 RID: 791 RVA: 0x00008D19 File Offset: 0x00006F19
	// (set) Token: 0x06000318 RID: 792 RVA: 0x00008D21 File Offset: 0x00006F21
	[JsonPropertyName("expire_time")]
	public int ExpireTime { get; set; }

	// Token: 0x06000319 RID: 793 RVA: 0x00008D2A File Offset: 0x00006F2A
}