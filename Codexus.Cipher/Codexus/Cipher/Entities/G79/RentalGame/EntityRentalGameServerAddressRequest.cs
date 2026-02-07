using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.RentalGame;

public class EntityRentalGameServerAddressRequest
{
    [JsonPropertyName("server_id")] public string ServerId { get; set; }
    [JsonPropertyName("pwd")] public string Password { get; set; }
}