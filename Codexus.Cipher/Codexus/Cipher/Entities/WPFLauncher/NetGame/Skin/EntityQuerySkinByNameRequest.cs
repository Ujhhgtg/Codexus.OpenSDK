using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Skin;

public class EntityQuerySkinByNameRequest
{
    [JsonPropertyName("is_has")] public bool IsHas { get; set; }
    [JsonPropertyName("is_sync")] public int IsSync { get; set; }
    [JsonPropertyName("item_type")] public int ItemType { get; set; }
    [JsonPropertyName("keyword")] public string Keyword { get; set; }
    [JsonPropertyName("length")] public int Length { get; set; }
    [JsonPropertyName("master_type_id")] public int MasterTypeId { get; set; }
    [JsonPropertyName("offset")] public int Offset { get; set; }
    [JsonPropertyName("price_type")] public int PriceType { get; set; }

    [JsonPropertyName("secondary_type_id")]
    public string SecondaryTypeId { get; set; }

    [JsonPropertyName("sort_type")] public int SortType { get; set; }
    [JsonPropertyName("year")] public int Year { get; set; }
}