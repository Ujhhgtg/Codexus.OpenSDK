using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Skin;

public class EntitySkinSettings
{
    [JsonPropertyName("client_type")] public string ClientType { get; set; }
    [JsonPropertyName("game_type")] public int GameType { get; set; }
    [JsonPropertyName("skin_id")] public string SkinId { get; set; }
    [JsonPropertyName("skin_mode")] public int SkinMode { get; set; }
    [JsonPropertyName("skin_type")] public int SkinType { get; set; }
}