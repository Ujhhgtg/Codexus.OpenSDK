using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.NetGame;

public class EntityNetGameRequest
{
    [JsonPropertyName("version")] public string Version { get; set; }
    [JsonPropertyName("channel_id")] public int ChannelId { get; set; }
}