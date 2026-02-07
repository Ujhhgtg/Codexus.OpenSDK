using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Skin;
// TODO: [RequiredMember]
public class EntityFreeSkinListRequest
{
	// TODO: [RequiredMember]
	[JsonPropertyName("is_has")]
	public bool IsHas { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("item_type")]
	public int ItemType { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("length")]
	public int Length { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("master_type_id")]
	public int MasterTypeId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("offset")]
	public int Offset { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("price_type")]
	public int PriceType { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("secondary_type_id")]
	public int SecondaryTypeId { get; set; }

}