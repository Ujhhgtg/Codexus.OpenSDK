using System.Text.Json.Serialization;

namespace Codexus.Development.SDK.Entities;

public class EntityAvailableUser
{
    [JsonPropertyName("id")] public string UserId { get; set; }
    [JsonPropertyName("token")] public string AccessToken { get; set; }
    [JsonPropertyName("last_login_time")] public long LastLoginTime { get; set; }
}