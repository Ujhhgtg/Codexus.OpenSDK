using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.Minecraft;
// TODO: [RequiredMember]
public class EntityUserPropertiesEx
{
	// TODO: [RequiredMember]
	[JsonPropertyName("GameType")]
	public int GameType { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("isFilter")]
	public bool IsFilter { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("channel")]
	public string Channel { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("timedelta")]
	public int TimeDelta { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("launcherVersion")]
	public string LauncherVersion { get; set; }

}