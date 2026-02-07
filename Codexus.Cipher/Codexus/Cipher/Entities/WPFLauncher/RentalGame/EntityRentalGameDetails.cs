using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.RentalGame;

// Token: 0x02000047 RID: 71
public class EntityRentalGameDetails
{
	// Token: 0x170000BF RID: 191
	// (get) Token: 0x0600029C RID: 668 RVA: 0x00008825 File Offset: 0x00006A25
	// (set) Token: 0x0600029D RID: 669 RVA: 0x0000882D File Offset: 0x00006A2D
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; } = string.Empty;

	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x0600029E RID: 670 RVA: 0x00008836 File Offset: 0x00006A36
	// (set) Token: 0x0600029F RID: 671 RVA: 0x0000883E File Offset: 0x00006A3E
	[JsonPropertyName("owner_id")]
	public string OwnerId { get; set; } = string.Empty;

	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x060002A0 RID: 672 RVA: 0x00008847 File Offset: 0x00006A47
	// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000884F File Offset: 0x00006A4F
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x060002A2 RID: 674 RVA: 0x00008858 File Offset: 0x00006A58
	// (set) Token: 0x060002A3 RID: 675 RVA: 0x00008860 File Offset: 0x00006A60
	[JsonPropertyName("brief_summary")]
	public string BriefSummary { get; set; } = string.Empty;

	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x060002A4 RID: 676 RVA: 0x00008869 File Offset: 0x00006A69
	// (set) Token: 0x060002A5 RID: 677 RVA: 0x00008871 File Offset: 0x00006A71
	[JsonPropertyName("icon_index")]
	public uint IconIndex { get; set; }

	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000887A File Offset: 0x00006A7A
	// (set) Token: 0x060002A7 RID: 679 RVA: 0x00008882 File Offset: 0x00006A82
	[JsonPropertyName("begin_time")]
	public ulong BeginTime { get; set; }

	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000888B File Offset: 0x00006A8B
	// (set) Token: 0x060002A9 RID: 681 RVA: 0x00008893 File Offset: 0x00006A93
	[JsonPropertyName("mc_version")]
	public string McVersion { get; set; } = string.Empty;

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x060002AA RID: 682 RVA: 0x0000889C File Offset: 0x00006A9C
	// (set) Token: 0x060002AB RID: 683 RVA: 0x000088A4 File Offset: 0x00006AA4
	[JsonPropertyName("capacity")]
	public uint Capacity { get; set; }

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x060002AC RID: 684 RVA: 0x000088AD File Offset: 0x00006AAD
	// (set) Token: 0x060002AD RID: 685 RVA: 0x000088B5 File Offset: 0x00006AB5
	[JsonPropertyName("world_id")]
	public string WorldId { get; set; } = string.Empty;

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x060002AE RID: 686 RVA: 0x000088BE File Offset: 0x00006ABE
	// (set) Token: 0x060002AF RID: 687 RVA: 0x000088C6 File Offset: 0x00006AC6
	[JsonPropertyName("player_count")]
	public uint PlayerCount { get; set; }

	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x060002B0 RID: 688 RVA: 0x000088CF File Offset: 0x00006ACF
	// (set) Token: 0x060002B1 RID: 689 RVA: 0x000088D7 File Offset: 0x00006AD7
	[JsonPropertyName("image_url")]
	public string ImageUrl { get; set; } = string.Empty;

	// Token: 0x170000CA RID: 202
	// (get) Token: 0x060002B2 RID: 690 RVA: 0x000088E0 File Offset: 0x00006AE0
	// (set) Token: 0x060002B3 RID: 691 RVA: 0x000088E8 File Offset: 0x00006AE8
	[JsonPropertyName("status")]
	public EnumServerStatus Status { get; set; }

	// Token: 0x170000CB RID: 203
	// (get) Token: 0x060002B4 RID: 692 RVA: 0x000088F1 File Offset: 0x00006AF1
	// (set) Token: 0x060002B5 RID: 693 RVA: 0x000088F9 File Offset: 0x00006AF9
	[JsonPropertyName("visibility")]
	public EnumVisibilityStatus Visibility { get; set; }

	// Token: 0x170000CC RID: 204
	// (get) Token: 0x060002B6 RID: 694 RVA: 0x00008902 File Offset: 0x00006B02
	// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000890A File Offset: 0x00006B0A
	[JsonPropertyName("has_pwd")]
	public string HasPassword { get; set; } = string.Empty;

	// Token: 0x170000CD RID: 205
	// (get) Token: 0x060002B8 RID: 696 RVA: 0x00008913 File Offset: 0x00006B13
	// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000891B File Offset: 0x00006B1B
	[JsonPropertyName("server_type")]
	public EnumServerType ServerType { get; set; }

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x060002BA RID: 698 RVA: 0x00008924 File Offset: 0x00006B24
	// (set) Token: 0x060002BB RID: 699 RVA: 0x0000892C File Offset: 0x00006B2C
	[JsonPropertyName("active_components")]
	public List<string> ActiveComponents
	{
		get;
		set;
	} = [];

	// Token: 0x170000CF RID: 207
	// (get) Token: 0x060002BC RID: 700 RVA: 0x00008935 File Offset: 0x00006B35
	// (set) Token: 0x060002BD RID: 701 RVA: 0x0000893D File Offset: 0x00006B3D
	[JsonPropertyName("update_active_components")]
	public List<string> UpdateActiveComponents
	{
		get;
		set;
	} = [];

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x060002BE RID: 702 RVA: 0x00008946 File Offset: 0x00006B46
	// (set) Token: 0x060002BF RID: 703 RVA: 0x0000894E File Offset: 0x00006B4E
		
	[JsonPropertyName("server_ip")]
	public string ServerIp
	{
			
		get;
			
		set;
	}

	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x060002C0 RID: 704 RVA: 0x00008957 File Offset: 0x00006B57
	// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000895F File Offset: 0x00006B5F
	[JsonPropertyName("server_port")]
	public int? ServerPort { get; set; }
}