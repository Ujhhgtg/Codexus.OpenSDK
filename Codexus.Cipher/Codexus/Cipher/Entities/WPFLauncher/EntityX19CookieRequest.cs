using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher;

public class EntityX19CookieRequest
{
    [JsonPropertyName("sauth_json")] public string Json { get; set; }
}