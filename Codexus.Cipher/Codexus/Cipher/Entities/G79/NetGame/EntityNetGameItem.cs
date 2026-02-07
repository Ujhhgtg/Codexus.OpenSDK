using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.NetGame;

// Token: 0x02000097 RID: 151
// TODO: [RequiredMember]
public class EntityNetGameItem
{
	// Token: 0x17000215 RID: 533
	// (get) Token: 0x06000597 RID: 1431 RVA: 0x0000A823 File Offset: 0x00008A23
	// (set) Token: 0x06000598 RID: 1432 RVA: 0x0000A82B File Offset: 0x00008A2B
	// TODO: [RequiredMember]
	[JsonPropertyName("item_id")]
	public required string ItemId { get; set; }

	// Token: 0x17000216 RID: 534
	// (get) Token: 0x06000599 RID: 1433 RVA: 0x0000A834 File Offset: 0x00008A34
	// (set) Token: 0x0600059A RID: 1434 RVA: 0x0000A83C File Offset: 0x00008A3C
	// TODO: [RequiredMember]
	[JsonPropertyName("res_name")]
	public required string ResName { get; set; }

	// Token: 0x17000217 RID: 535
	// (get) Token: 0x0600059B RID: 1435 RVA: 0x0000A845 File Offset: 0x00008A45
	// (set) Token: 0x0600059C RID: 1436 RVA: 0x0000A84D File Offset: 0x00008A4D
	// TODO: [RequiredMember]
	[JsonPropertyName("brief")]
	public string Brief { get; set; }

	// Token: 0x17000218 RID: 536
	// (get) Token: 0x0600059D RID: 1437 RVA: 0x0000A856 File Offset: 0x00008A56
	// (set) Token: 0x0600059E RID: 1438 RVA: 0x0000A85E File Offset: 0x00008A5E
	// TODO: [RequiredMember]
	[JsonPropertyName("tag_names")]
	public List<string> TagNames { get; set; }

	// Token: 0x17000219 RID: 537
	// (get) Token: 0x0600059F RID: 1439 RVA: 0x0000A867 File Offset: 0x00008A67
	// (set) Token: 0x060005A0 RID: 1440 RVA: 0x0000A86F File Offset: 0x00008A6F
	// TODO: [RequiredMember]
	[JsonPropertyName("title_image_url")]
	public string TitleImageUrl { get; set; }

	// Token: 0x1700021A RID: 538
	// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0000A878 File Offset: 0x00008A78
	// (set) Token: 0x060005A2 RID: 1442 RVA: 0x0000A880 File Offset: 0x00008A80
	// TODO: [RequiredMember]
	[JsonPropertyName("new_recommend")]
	public int NewRecommend { get; set; }

	// Token: 0x1700021B RID: 539
	// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0000A889 File Offset: 0x00008A89
	// (set) Token: 0x060005A4 RID: 1444 RVA: 0x0000A891 File Offset: 0x00008A91
	// TODO: [RequiredMember]
	[JsonPropertyName("new_entrance_recommend")]
	public int NewEntranceRecommend { get; set; }

	// Token: 0x1700021C RID: 540
	// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0000A89A File Offset: 0x00008A9A
	// (set) Token: 0x060005A6 RID: 1446 RVA: 0x0000A8A2 File Offset: 0x00008AA2
	// TODO: [RequiredMember]
	[JsonPropertyName("new_recommend_time")]
	public int NewRecommendTime { get; set; }

	// Token: 0x1700021D RID: 541
	// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0000A8AB File Offset: 0x00008AAB
	// (set) Token: 0x060005A8 RID: 1448 RVA: 0x0000A8B3 File Offset: 0x00008AB3
	// TODO: [RequiredMember]
	[JsonPropertyName("order")]
	public string Order { get; set; }

	// Token: 0x1700021E RID: 542
	// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0000A8BC File Offset: 0x00008ABC
	// (set) Token: 0x060005AA RID: 1450 RVA: 0x0000A8C4 File Offset: 0x00008AC4
	// TODO: [RequiredMember]
	[JsonPropertyName("is_spigot")]
	public int IsSpigot { get; set; }

	// Token: 0x1700021F RID: 543
	// (get) Token: 0x060005AB RID: 1451 RVA: 0x0000A8CD File Offset: 0x00008ACD
	// (set) Token: 0x060005AC RID: 1452 RVA: 0x0000A8D5 File Offset: 0x00008AD5
	// TODO: [RequiredMember]
	[JsonPropertyName("stars")]
	public float Stars { get; set; }

	// Token: 0x17000220 RID: 544
	// (get) Token: 0x060005AD RID: 1453 RVA: 0x0000A8DE File Offset: 0x00008ADE
	// (set) Token: 0x060005AE RID: 1454 RVA: 0x0000A8E6 File Offset: 0x00008AE6
	// TODO: [RequiredMember]
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; }

	// Token: 0x17000221 RID: 545
	// (get) Token: 0x060005AF RID: 1455 RVA: 0x0000A8EF File Offset: 0x00008AEF
	// (set) Token: 0x060005B0 RID: 1456 RVA: 0x0000A8F7 File Offset: 0x00008AF7
	// TODO: [RequiredMember]
	[JsonPropertyName("online_num")]
	public string OnlineNum { get; set; }

	// Token: 0x17000222 RID: 546
	// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0000A900 File Offset: 0x00008B00
	// (set) Token: 0x060005B2 RID: 1458 RVA: 0x0000A908 File Offset: 0x00008B08
	// TODO: [RequiredMember]
	[JsonPropertyName("pic_url_list")]
	public List<string> PicUrlList { get; set; }

	// Token: 0x17000223 RID: 547
	// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0000A911 File Offset: 0x00008B11
	// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0000A919 File Offset: 0x00008B19
	// TODO: [RequiredMember]
	[JsonPropertyName("is_access_by_uid")]
	public int IsAccessByUid { get; set; }

	// Token: 0x17000224 RID: 548
	// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0000A922 File Offset: 0x00008B22
	// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0000A92A File Offset: 0x00008B2A
	// TODO: [RequiredMember]
	[JsonPropertyName("opening_hour")]
	public string OpeningHour { get; set; }

	// Token: 0x17000225 RID: 549
	// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0000A933 File Offset: 0x00008B33
	// (set) Token: 0x060005B8 RID: 1464 RVA: 0x0000A93B File Offset: 0x00008B3B
	// TODO: [RequiredMember]
	[JsonPropertyName("sort_description")]
	public string SortDescription { get; set; }

	// Token: 0x17000226 RID: 550
	// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0000A944 File Offset: 0x00008B44
	// (set) Token: 0x060005BA RID: 1466 RVA: 0x0000A94C File Offset: 0x00008B4C
	// TODO: [RequiredMember]
	[JsonPropertyName("is_show_online_count")]
	public int IsShowOnlineCount { get; set; }

	// Token: 0x17000227 RID: 551
	// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000A955 File Offset: 0x00008B55
	// (set) Token: 0x060005BC RID: 1468 RVA: 0x0000A95D File Offset: 0x00008B5D
	// TODO: [RequiredMember]
	[JsonPropertyName("sort")]
	public int Sort { get; set; }

	// Token: 0x17000228 RID: 552
	// (get) Token: 0x060005BD RID: 1469 RVA: 0x0000A966 File Offset: 0x00008B66
	// (set) Token: 0x060005BE RID: 1470 RVA: 0x0000A96E File Offset: 0x00008B6E
	// TODO: [RequiredMember]
	[JsonPropertyName("is_fellow")]
	public int IsFellow { get; set; }

	// Token: 0x17000229 RID: 553
	// (get) Token: 0x060005BF RID: 1471 RVA: 0x0000A977 File Offset: 0x00008B77
	// (set) Token: 0x060005C0 RID: 1472 RVA: 0x0000A97F File Offset: 0x00008B7F
	// TODO: [RequiredMember]
	[JsonPropertyName("developer_id")]
	public int DeveloperId { get; set; }

	// Token: 0x1700022A RID: 554
	// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0000A988 File Offset: 0x00008B88
	// (set) Token: 0x060005C2 RID: 1474 RVA: 0x0000A990 File Offset: 0x00008B90
	// TODO: [RequiredMember]
	[JsonPropertyName("friend_play_num")]
	public int FriendPlayNum { get; set; }

	// Token: 0x1700022B RID: 555
	// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0000A999 File Offset: 0x00008B99
	// (set) Token: 0x060005C4 RID: 1476 RVA: 0x0000A9A1 File Offset: 0x00008BA1
	// TODO: [RequiredMember]
	[JsonPropertyName("week_play_num")]
	public int WeekPlayNum { get; set; }

	// Token: 0x1700022C RID: 556
	// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000A9AA File Offset: 0x00008BAA
	// (set) Token: 0x060005C6 RID: 1478 RVA: 0x0000A9B2 File Offset: 0x00008BB2
	// TODO: [RequiredMember]
	[JsonPropertyName("recommend_sort_num")]
	public int RecommendSortNum { get; set; }

	// Token: 0x1700022D RID: 557
	// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0000A9BB File Offset: 0x00008BBB
	// (set) Token: 0x060005C8 RID: 1480 RVA: 0x0000A9C3 File Offset: 0x00008BC3
	// TODO: [RequiredMember]
	[JsonPropertyName("total_play_num")]
	public int TotalPlayNum { get; set; }

	// Token: 0x1700022E RID: 558
	// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0000A9CC File Offset: 0x00008BCC
	// (set) Token: 0x060005CA RID: 1482 RVA: 0x0000A9D4 File Offset: 0x00008BD4
	// TODO: [RequiredMember]
	[JsonPropertyName("create_time")]
	public int CreateTime { get; set; }

	// Token: 0x1700022F RID: 559
	// (get) Token: 0x060005CB RID: 1483 RVA: 0x0000A9DD File Offset: 0x00008BDD
	// (set) Token: 0x060005CC RID: 1484 RVA: 0x0000A9E5 File Offset: 0x00008BE5
	// TODO: [RequiredMember]
	[JsonPropertyName("running_status")]
	public string RunningStatus { get; set; }

	// Token: 0x060005CD RID: 1485 RVA: 0x0000A9EE File Offset: 0x00008BEE
	public EntityNetGameItem()
	{
	}
}