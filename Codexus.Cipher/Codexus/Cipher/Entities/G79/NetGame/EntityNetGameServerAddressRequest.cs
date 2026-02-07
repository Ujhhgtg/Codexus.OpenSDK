using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.NetGame;

public class EntityNetGameServerAddressRequest
{
    [JsonPropertyName("item_id")] public string ItemId { get; set; }
}