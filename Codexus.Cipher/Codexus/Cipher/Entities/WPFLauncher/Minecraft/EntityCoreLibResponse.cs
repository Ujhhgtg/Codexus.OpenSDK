using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.Minecraft;

public class EntityCoreLibResponse
{
    [JsonPropertyName("core_lib_md5")] public string CoreLibMd5 { get; set; }
    [JsonPropertyName("core_lib_name")] public string CoreLibName { get; set; }
    [JsonPropertyName("core_lib_size")] public int CoreLibSize { get; set; }
    [JsonPropertyName("core_lib_url")] public string CoreLibUrl { get; set; }
    [JsonPropertyName("mc_version")] public int McVersion { get; set; }
    [JsonPropertyName("md5")] public string Md5 { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("refresh_time")] public int RefreshTime { get; set; }
    [JsonPropertyName("size")] public int Size { get; set; }
    [JsonPropertyName("url")] public string Url { get; set; }
    [JsonPropertyName("version")] public string Version { get; set; }
}