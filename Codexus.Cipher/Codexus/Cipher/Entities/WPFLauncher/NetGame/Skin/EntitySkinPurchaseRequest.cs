using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Skin;
// TODO: [RequiredMember]
public class EntitySkinPurchaseRequest
{
	// TODO: [RequiredMember]
	[JsonPropertyName("batch_count")]
	public int BatchCount { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("buy_path")]
	public string BuyPath { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("diamond")]
	public int Diamond { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("entity_id")]
	public int EntityId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("item_id")]
	public string ItemId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("item_level")]
	public int ItemLevel { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("last_play_time")]
	public int LastPlayTime { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("purchase_time")]
	public int PurchaseTime { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("total_play_time")]
	public int TotalPlayTime { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("user_id")]
	public string UserId { get; set; }

}