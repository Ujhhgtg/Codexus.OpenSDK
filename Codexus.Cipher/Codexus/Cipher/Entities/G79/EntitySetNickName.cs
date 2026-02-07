using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79;
// TODO: [RequiredMember]
public class EntitySetNickName
{
	// TODO: [RequiredMember]
	[JsonPropertyName("name")]
	public required string Name { get; set; }

}