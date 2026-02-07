using System.Collections.Generic;
using System.Text.Json.Serialization;
using Codexus.Cipher.Utils;

namespace Codexus.Cipher.Entities.WPFLauncher.RentalGame;

// Token: 0x02000046 RID: 70
public class EntityRentalGame
{
	// Token: 0x170000AD RID: 173
	// (get) Token: 0x06000277 RID: 631 RVA: 0x0000868F File Offset: 0x0000688F
	// (set) Token: 0x06000278 RID: 632 RVA: 0x00008697 File Offset: 0x00006897
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; } = string.Empty;

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x06000279 RID: 633 RVA: 0x000086A0 File Offset: 0x000068A0
	// (set) Token: 0x0600027A RID: 634 RVA: 0x000086A8 File Offset: 0x000068A8
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x0600027B RID: 635 RVA: 0x000086B1 File Offset: 0x000068B1
	// (set) Token: 0x0600027C RID: 636 RVA: 0x000086B9 File Offset: 0x000068B9
	[JsonPropertyName("server_name")]
	public string ServerName { get; set; } = string.Empty;

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x0600027D RID: 637 RVA: 0x000086C2 File Offset: 0x000068C2
	// (set) Token: 0x0600027E RID: 638 RVA: 0x000086CA File Offset: 0x000068CA
	[JsonPropertyName("visibility")]
	public EnumVisibilityStatus Visibility { get; set; }

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x0600027F RID: 639 RVA: 0x000086D3 File Offset: 0x000068D3
	// (set) Token: 0x06000280 RID: 640 RVA: 0x000086DB File Offset: 0x000068DB
	[JsonPropertyName("has_pwd")]
	public string HasPassword { get; set; } = string.Empty;

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x06000281 RID: 641 RVA: 0x000086E4 File Offset: 0x000068E4
	// (set) Token: 0x06000282 RID: 642 RVA: 0x000086EC File Offset: 0x000068EC
	[JsonPropertyName("server_type")]
	public EnumServerType ServerType { get; set; }

	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x06000283 RID: 643 RVA: 0x000086F5 File Offset: 0x000068F5
	// (set) Token: 0x06000284 RID: 644 RVA: 0x000086FD File Offset: 0x000068FD
	[JsonPropertyName("status")]
	public EnumServerStatus Status { get; set; }

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x06000285 RID: 645 RVA: 0x00008706 File Offset: 0x00006906
	// (set) Token: 0x06000286 RID: 646 RVA: 0x0000870E File Offset: 0x0000690E
	[JsonPropertyName("capacity")]
	public uint Capacity { get; set; }

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x06000287 RID: 647 RVA: 0x00008717 File Offset: 0x00006917
	// (set) Token: 0x06000288 RID: 648 RVA: 0x0000871F File Offset: 0x0000691F
	[JsonPropertyName("mc_version")]
	public string McVersion { get; set; } = string.Empty;

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x06000289 RID: 649 RVA: 0x00008728 File Offset: 0x00006928
	// (set) Token: 0x0600028A RID: 650 RVA: 0x00008730 File Offset: 0x00006930
	[JsonPropertyName("owner_id")]
	public long OwnerId { get; set; }

	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x0600028B RID: 651 RVA: 0x00008739 File Offset: 0x00006939
	// (set) Token: 0x0600028C RID: 652 RVA: 0x00008741 File Offset: 0x00006941
	[JsonPropertyName("player_count")]
	public uint PlayerCount { get; set; }

	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x0600028D RID: 653 RVA: 0x0000874A File Offset: 0x0000694A
	// (set) Token: 0x0600028E RID: 654 RVA: 0x00008752 File Offset: 0x00006952
	[JsonConverter(typeof(JsonConventer.SingleOrArrayConverter<string>))]
	[JsonPropertyName("image_url")]
	public List<string> ImageUrl { get; set; }

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x0600028F RID: 655 RVA: 0x0000875B File Offset: 0x0000695B
	// (set) Token: 0x06000290 RID: 656 RVA: 0x00008763 File Offset: 0x00006963
	[JsonPropertyName("world_id")]
	public string WorldId { get; set; } = string.Empty;

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x06000291 RID: 657 RVA: 0x0000876C File Offset: 0x0000696C
	// (set) Token: 0x06000292 RID: 658 RVA: 0x00008774 File Offset: 0x00006974
	[JsonPropertyName("min_level")]
	public string MinLevel { get; set; } = string.Empty;

	// Token: 0x170000BB RID: 187
	// (get) Token: 0x06000293 RID: 659 RVA: 0x0000877D File Offset: 0x0000697D
	// (set) Token: 0x06000294 RID: 660 RVA: 0x00008785 File Offset: 0x00006985
	[JsonPropertyName("pvp")]
	public bool IsPvpEnabled { get; set; }

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x06000295 RID: 661 RVA: 0x0000878E File Offset: 0x0000698E
	// (set) Token: 0x06000296 RID: 662 RVA: 0x00008796 File Offset: 0x00006996
	[JsonPropertyName("like_num")]
	public uint LikeCount { get; set; }

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x06000297 RID: 663 RVA: 0x0000879F File Offset: 0x0000699F
	// (set) Token: 0x06000298 RID: 664 RVA: 0x000087A7 File Offset: 0x000069A7
	[JsonPropertyName("icon_index")]
	public uint IconIndex { get; set; }

	// Token: 0x170000BE RID: 190
	// (get) Token: 0x06000299 RID: 665 RVA: 0x000087B0 File Offset: 0x000069B0
	// (set) Token: 0x0600029A RID: 666 RVA: 0x000087B8 File Offset: 0x000069B8
		
	[JsonPropertyName("offset")]
	public string Offset
	{
			
		get;
			
		set;
	}
}