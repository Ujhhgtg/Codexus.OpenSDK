using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79;
// TODO: [RequiredMember]
public class EntityAuthenticationOtp
{
	// TODO: [RequiredMember]
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("token")]
	public string Token { get; set; }

}