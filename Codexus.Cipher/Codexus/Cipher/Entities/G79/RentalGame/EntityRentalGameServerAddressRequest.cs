using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.RentalGame;
// TODO: [RequiredMember]
public class EntityRentalGameServerAddressRequest
{
	// TODO: [RequiredMember]
	[JsonPropertyName("server_id")]
	public string ServerId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("pwd")]
	public string Password { get; set; }

}