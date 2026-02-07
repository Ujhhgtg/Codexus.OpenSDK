using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79;
// TODO: [RequiredMember]
public class EntitySetNickNameRequest
{
	// TODO: [RequiredMember]
	[JsonPropertyName("name")]
	public string Name { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; }

}