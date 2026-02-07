using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Mods;

public class EntityModsInfo
{
    [JsonPropertyName("modPath")] public string ModPath { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("iid")] public string Iid { get; set; }
    [JsonPropertyName("md5")] public string Md5 { get; set; }
    [JsonPropertyName("version")] public string Version { get; set; } = "";
}