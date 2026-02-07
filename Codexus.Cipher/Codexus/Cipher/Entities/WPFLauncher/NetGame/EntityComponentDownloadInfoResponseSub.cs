using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

public class EntityComponentDownloadInfoResponseSub
{
    [JsonPropertyName("java_version")] public int JavaVersion { get; set; }
    [JsonPropertyName("mc_version_name")] public string McVersionName { get; set; }
    [JsonPropertyName("res_url")] public string ResUrl { get; set; }
    [JsonPropertyName("res_size")] public long ResSize { get; set; }
    [JsonPropertyName("res_md5")] public string ResMd5 { get; set; }
    [JsonPropertyName("jar_md5")] public string JarMd5 { get; set; }
    [JsonPropertyName("res_name")] public string ResName { get; set; }
    [JsonPropertyName("res_version")] public int ResVersion { get; set; }
}