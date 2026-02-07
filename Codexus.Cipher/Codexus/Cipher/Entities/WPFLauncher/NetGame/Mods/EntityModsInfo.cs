using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Mods;
// TODO: [RequiredMember]
public class EntityModsInfo
{
	// TODO: [RequiredMember]
	[JsonPropertyName("modPath")]
	public string ModPath { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("name")]
	public string Name { get; set; } = "";
	// TODO: [RequiredMember]
	[JsonPropertyName("id")]
	public string Id { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("iid")]
	public string Iid { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("md5")]
	public string Md5 { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("version")]
	public string Version { get; set; } = "";

}