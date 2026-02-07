using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;
// TODO: [RequiredMember]
public class EntityQueryGameCharacters
{
	[JsonPropertyName("offset")]
	public int Offset { get; set; }
	[JsonPropertyName("length")]
	public int Length { get; set; } = 10;
	// TODO: [RequiredMember]
	[JsonPropertyName("user_id")]
	public string UserId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("game_id")]
	public string GameId { get; set; }
	[JsonPropertyName("game_type")]
	public string GameType { get; set; } = "2";

}