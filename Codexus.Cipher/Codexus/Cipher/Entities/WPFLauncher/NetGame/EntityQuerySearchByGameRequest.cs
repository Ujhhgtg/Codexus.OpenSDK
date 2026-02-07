using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;
// TODO: [RequiredMember]
public class EntityQuerySearchByGameRequest
{
	// TODO: [RequiredMember]
	[JsonPropertyName("mc_version_id")]
	public int McVersionId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("game_type")]
	public int GameType { get; set; }

}