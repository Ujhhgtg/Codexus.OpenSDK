using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.RentalGame;

public class EntityRentalGame
{
    [JsonPropertyName("entity_id")] public string EntityId { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("player_count")] public int PlayerCount { get; set; }
    [JsonPropertyName("server_name")] public string ServerName { get; set; }
}