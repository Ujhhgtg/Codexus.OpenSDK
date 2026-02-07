using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;
// TODO: [RequiredMember]
public class EntityNetGameRequest
{
	// TODO: [RequiredMember]
	[JsonPropertyName("available_mc_versions")]
	public string[] AvailableMcVersions { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("item_type")]
	public int ItemType { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("length")]
	public int Length { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("offset")]
	public int Offset { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("master_type_id")]
	public string MasterTypeId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("secondary_type_id")]
	public string SecondaryTypeId { get; set; }

}