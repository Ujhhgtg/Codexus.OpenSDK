using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Skin;
public class EntitySkin
{
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; } = string.Empty;
	[JsonPropertyName("brief_summary")]
	public string BriefSummary { get; set; } = string.Empty;
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;
	[JsonPropertyName("title_image_url")]
	public string TitleImageUrl { get; set; } = string.Empty;
	[JsonPropertyName("like_num")]
	public int LikeNum { get; set; }
}