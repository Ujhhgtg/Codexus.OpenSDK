using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79;

public class EntityAuthenticationOtp
{
    [JsonPropertyName("entity_id")] public string EntityId { get; set; }
    [JsonPropertyName("token")] public string Token { get; set; }
}