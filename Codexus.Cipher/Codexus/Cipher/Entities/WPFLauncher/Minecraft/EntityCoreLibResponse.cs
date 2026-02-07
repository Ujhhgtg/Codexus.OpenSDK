using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.Minecraft;
// TODO: [RequiredMember]
public class EntityCoreLibResponse
{
	// TODO: [RequiredMember]
	[JsonPropertyName("core_lib_md5")]
	public string CoreLibMd5 { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("core_lib_name")]
	public string CoreLibName { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("core_lib_size")]
	public int CoreLibSize { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("core_lib_url")]
	public string CoreLibUrl { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("mc_version")]
	public int McVersion { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("md5")]
	public string Md5 { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("name")]
	public string Name { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("refresh_time")]
	public int RefreshTime { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("size")]
	public int Size { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("url")]
	public string Url { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("version")]
	public string Version { get; set; }

}