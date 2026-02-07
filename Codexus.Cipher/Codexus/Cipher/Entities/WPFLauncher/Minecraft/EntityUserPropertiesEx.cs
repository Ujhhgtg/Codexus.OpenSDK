using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.Minecraft;

public class EntityUserPropertiesEx
{
    [JsonPropertyName("GameType")] public int GameType { get; set; }
    [JsonPropertyName("isFilter")] public bool IsFilter { get; set; }
    [JsonPropertyName("channel")] public string Channel { get; set; }
    [JsonPropertyName("timedelta")] public int TimeDelta { get; set; }
    [JsonPropertyName("launcherVersion")] public string LauncherVersion { get; set; }
}