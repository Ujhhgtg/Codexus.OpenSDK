using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher;
// TODO: [RequiredMember]
public class EntityAuthenticationVersion
{
	// TODO: [RequiredMember]
	[JsonPropertyName("version")]
	public string Version { get; set; }
	[JsonPropertyName("launcher_md5")]
	public string LauncherMd5 { get; set; } = string.Empty;
	[JsonPropertyName("updater_md5")]
	public string UpdaterMd5 { get; set; } = string.Empty;

}