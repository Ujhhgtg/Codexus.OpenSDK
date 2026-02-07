using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Skin;

public class EntitySkinPurchaseRequest
{
    [JsonPropertyName("batch_count")] public int BatchCount { get; set; }
    [JsonPropertyName("buy_path")] public string BuyPath { get; set; }
    [JsonPropertyName("diamond")] public int Diamond { get; set; }
    [JsonPropertyName("entity_id")] public int EntityId { get; set; }
    [JsonPropertyName("item_id")] public string ItemId { get; set; }
    [JsonPropertyName("item_level")] public int ItemLevel { get; set; }
    [JsonPropertyName("last_play_time")] public int LastPlayTime { get; set; }
    [JsonPropertyName("purchase_time")] public int PurchaseTime { get; set; }
    [JsonPropertyName("total_play_time")] public int TotalPlayTime { get; set; }
    [JsonPropertyName("user_id")] public string UserId { get; set; }
}