using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.NetGame;

public class EntityNetGameItem
{
    [JsonPropertyName("item_id")] public required string ItemId { get; set; }
    [JsonPropertyName("res_name")] public required string ResName { get; set; }
    [JsonPropertyName("brief")] public string Brief { get; set; }
    [JsonPropertyName("tag_names")] public List<string> TagNames { get; set; }
    [JsonPropertyName("title_image_url")] public string TitleImageUrl { get; set; }
    [JsonPropertyName("new_recommend")] public int NewRecommend { get; set; }

    [JsonPropertyName("new_entrance_recommend")]
    public int NewEntranceRecommend { get; set; }

    [JsonPropertyName("new_recommend_time")]
    public int NewRecommendTime { get; set; }

    [JsonPropertyName("order")] public string Order { get; set; }
    [JsonPropertyName("is_spigot")] public int IsSpigot { get; set; }
    [JsonPropertyName("stars")] public float Stars { get; set; }
    [JsonPropertyName("entity_id")] public string EntityId { get; set; }
    [JsonPropertyName("online_num")] public string OnlineNum { get; set; }
    [JsonPropertyName("pic_url_list")] public List<string> PicUrlList { get; set; }
    [JsonPropertyName("is_access_by_uid")] public int IsAccessByUid { get; set; }
    [JsonPropertyName("opening_hour")] public string OpeningHour { get; set; }
    [JsonPropertyName("sort_description")] public string SortDescription { get; set; }

    [JsonPropertyName("is_show_online_count")]
    public int IsShowOnlineCount { get; set; }

    [JsonPropertyName("sort")] public int Sort { get; set; }
    [JsonPropertyName("is_fellow")] public int IsFellow { get; set; }
    [JsonPropertyName("developer_id")] public int DeveloperId { get; set; }
    [JsonPropertyName("friend_play_num")] public int FriendPlayNum { get; set; }
    [JsonPropertyName("week_play_num")] public int WeekPlayNum { get; set; }

    [JsonPropertyName("recommend_sort_num")]
    public int RecommendSortNum { get; set; }

    [JsonPropertyName("total_play_num")] public int TotalPlayNum { get; set; }
    [JsonPropertyName("create_time")] public int CreateTime { get; set; }
    [JsonPropertyName("running_status")] public string RunningStatus { get; set; }
}