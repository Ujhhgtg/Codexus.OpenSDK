using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;
// TODO: [RequiredMember]
public class EntityComponentDownloadInfoResponseSub
{
	// TODO: [RequiredMember]
	[JsonPropertyName("java_version")]
	public int JavaVersion { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("mc_version_name")]
	public string McVersionName { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("res_url")]
	public string ResUrl { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("res_size")]
	public long ResSize { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("res_md5")]
	public string ResMd5 { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("jar_md5")]
	public string JarMd5 { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("res_name")]
	public string ResName { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("res_version")]
	public int ResVersion { get; set; }

}