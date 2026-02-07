using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Connection.G79;
// TODO: [RequiredMember]
public class EntityHttpDecrypt
{
	// TODO: [RequiredMember]
	[JsonPropertyName("body")]
	public string Body { get; set; }

}