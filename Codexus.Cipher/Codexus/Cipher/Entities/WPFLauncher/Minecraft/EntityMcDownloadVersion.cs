using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.Minecraft;
// TODO: [RequiredMember]
public class EntityMcDownloadVersion
{
	// TODO: [RequiredMember]
	[JsonPropertyName("mc_version")]
	public int McVersion { get; set; }

}