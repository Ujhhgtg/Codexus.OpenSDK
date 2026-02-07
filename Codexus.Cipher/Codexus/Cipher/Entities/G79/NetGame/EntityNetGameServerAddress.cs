using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.NetGame;

public class EntityNetGameServerAddress
{
    [JsonPropertyName("host")] public string Host { get; set; }
    [JsonPropertyName("port")] public int Port { get; set; }
}