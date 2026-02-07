using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

public class EntityQueryNetGameDetailRequest
{
    [JsonPropertyName("item_id")] public string ItemId { get; set; }
}