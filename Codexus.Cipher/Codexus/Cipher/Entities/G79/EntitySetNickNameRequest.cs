using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79;

public class EntitySetNickNameRequest
{
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("entity_id")] public string EntityId { get; set; }
}