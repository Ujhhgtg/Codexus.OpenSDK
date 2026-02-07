using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.RentalGame;
// TODO: [RequiredMember]
public class EntityRentalGame
{
	// TODO: [RequiredMember]
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("name")]
	public string Name { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("player_count")]
	public int PlayerCount { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("server_name")]
	public string ServerName { get; set; }

}