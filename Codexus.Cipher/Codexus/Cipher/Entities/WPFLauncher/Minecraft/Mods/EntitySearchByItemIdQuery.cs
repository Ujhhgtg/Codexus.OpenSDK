using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.Minecraft.Mods;
// TODO: [RequiredMember]
public class EntitySearchByItemIdQuery
{
	// TODO: [RequiredMember]
	[JsonPropertyName("item_id")]
	public string ItemId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("length")]
	public int Length { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("offset")]
	public int Offset { get; set; }

}