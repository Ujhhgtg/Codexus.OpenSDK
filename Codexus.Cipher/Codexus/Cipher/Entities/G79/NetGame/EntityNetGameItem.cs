using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.NetGame;
// TODO: [RequiredMember]
public class EntityNetGameItem
{
	// TODO: [RequiredMember]
	[JsonPropertyName("item_id")]
	public required string ItemId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("res_name")]
	public required string ResName { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("brief")]
	public string Brief { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("tag_names")]
	public List<string> TagNames { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("title_image_url")]
	public string TitleImageUrl { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("new_recommend")]
	public int NewRecommend { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("new_entrance_recommend")]
	public int NewEntranceRecommend { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("new_recommend_time")]
	public int NewRecommendTime { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("order")]
	public string Order { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("is_spigot")]
	public int IsSpigot { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("stars")]
	public float Stars { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("online_num")]
	public string OnlineNum { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("pic_url_list")]
	public List<string> PicUrlList { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("is_access_by_uid")]
	public int IsAccessByUid { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("opening_hour")]
	public string OpeningHour { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("sort_description")]
	public string SortDescription { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("is_show_online_count")]
	public int IsShowOnlineCount { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("sort")]
	public int Sort { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("is_fellow")]
	public int IsFellow { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("developer_id")]
	public int DeveloperId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("friend_play_num")]
	public int FriendPlayNum { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("week_play_num")]
	public int WeekPlayNum { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("recommend_sort_num")]
	public int RecommendSortNum { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("total_play_num")]
	public int TotalPlayNum { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("create_time")]
	public int CreateTime { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("running_status")]
	public string RunningStatus { get; set; }

}