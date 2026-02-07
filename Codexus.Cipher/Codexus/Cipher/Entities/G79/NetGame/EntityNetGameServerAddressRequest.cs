using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.NetGame;
// TODO: [RequiredMember]
public class EntityNetGameServerAddressRequest
{
	// TODO: [RequiredMember]
	[JsonPropertyName("item_id")]
	public string ItemId { get; set; }

}