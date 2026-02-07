using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.RPC;

public class EntityCheckPlayerMessage
{
    [JsonPropertyName("a")] public int Length { get; set; }
    [JsonPropertyName("b")] public string Message { get; set; }
}