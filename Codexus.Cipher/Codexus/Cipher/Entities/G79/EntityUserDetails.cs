using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79;
// TODO: [RequiredMember]
public class EntityUserDetails
{
	// TODO: [RequiredMember]
	[JsonPropertyName("name")]
	public string Name { get; set; }

}