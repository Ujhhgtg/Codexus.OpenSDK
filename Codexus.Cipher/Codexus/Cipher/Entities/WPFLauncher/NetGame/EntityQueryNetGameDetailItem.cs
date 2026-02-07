using System;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

// Token: 0x0200005A RID: 90
public class EntityQueryNetGameDetailItem
{
	// Token: 0x17000129 RID: 297
	// (get) Token: 0x0600037E RID: 894 RVA: 0x0000918D File Offset: 0x0000738D
	// (set) Token: 0x0600037F RID: 895 RVA: 0x00009195 File Offset: 0x00007395
	[JsonPropertyName("brief_image_urls")]
	public string[] BriefImageUrls { get; set; } = Array.Empty<string>();

	// Token: 0x1700012A RID: 298
	// (get) Token: 0x06000380 RID: 896 RVA: 0x0000919E File Offset: 0x0000739E
	// (set) Token: 0x06000381 RID: 897 RVA: 0x000091A6 File Offset: 0x000073A6
	[JsonPropertyName("detail_description")]
	public string DetailDescription { get; set; } = string.Empty;

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x06000382 RID: 898 RVA: 0x000091AF File Offset: 0x000073AF
	// (set) Token: 0x06000383 RID: 899 RVA: 0x000091B7 File Offset: 0x000073B7
	[JsonPropertyName("developer_name")]
	public string DeveloperName { get; set; } = string.Empty;

	// Token: 0x1700012C RID: 300
	// (get) Token: 0x06000384 RID: 900 RVA: 0x000091C0 File Offset: 0x000073C0
	// (set) Token: 0x06000385 RID: 901 RVA: 0x000091C8 File Offset: 0x000073C8
	[JsonPropertyName("developer_urs")]
	public string DeveloperUrs { get; set; } = string.Empty;

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x06000386 RID: 902 RVA: 0x000091D1 File Offset: 0x000073D1
	// (set) Token: 0x06000387 RID: 903 RVA: 0x000091D9 File Offset: 0x000073D9
	[JsonPropertyName("publish_time")]
	public int PublishTime { get; set; }

	// Token: 0x1700012E RID: 302
	// (get) Token: 0x06000388 RID: 904 RVA: 0x000091E2 File Offset: 0x000073E2
	// (set) Token: 0x06000389 RID: 905 RVA: 0x000091EA File Offset: 0x000073EA
	[JsonPropertyName("video_info_list")]
	public EntityDetailsVideo[] VideoInfoList { get; set; } = Array.Empty<EntityDetailsVideo>();

	// Token: 0x1700012F RID: 303
	// (get) Token: 0x0600038A RID: 906 RVA: 0x000091F3 File Offset: 0x000073F3
	// (set) Token: 0x0600038B RID: 907 RVA: 0x000091FB File Offset: 0x000073FB
	[JsonPropertyName("mc_version_list")]
	public EntityMcVersion[] McVersionList { get; set; } = Array.Empty<EntityMcVersion>();

	// Token: 0x17000130 RID: 304
	// (get) Token: 0x0600038C RID: 908 RVA: 0x00009204 File Offset: 0x00007404
	// (set) Token: 0x0600038D RID: 909 RVA: 0x0000920C File Offset: 0x0000740C
	[JsonPropertyName("server_address")]
	public string ServerAddress { get; set; } = string.Empty;

	// Token: 0x17000131 RID: 305
	// (get) Token: 0x0600038E RID: 910 RVA: 0x00009215 File Offset: 0x00007415
	// (set) Token: 0x0600038F RID: 911 RVA: 0x0000921D File Offset: 0x0000741D
	[JsonPropertyName("server_port")]
	public int ServerPort { get; set; }
}