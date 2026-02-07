using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Connection.G79;
// TODO: [RequiredMember]
public class EntityZkpGetCheckNum
{
	// TODO: [RequiredMember]
	[JsonPropertyName("body")]
	public required string Body { get; set; }

}