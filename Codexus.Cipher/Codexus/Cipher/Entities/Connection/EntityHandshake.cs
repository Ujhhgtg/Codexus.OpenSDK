using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Connection;
// TODO: [RequiredMember]
public class EntityHandshake
{
	// TODO: [RequiredMember]
	[JsonPropertyName("handshakeBody")]
	public required string HandshakeBody { get; set; }

}