using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;
// TODO: [RequiredMember]
public class EntityCreateCharacter
{
	// TODO: [RequiredMember]
	[JsonPropertyName("game_id")]
	public string GameId { get; set; }
	[JsonPropertyName("game_type")]
	public int GameType { get; set; } = 2;
	// TODO: [RequiredMember]
	[JsonPropertyName("user_id")]
	public string UserId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("name")]
	public string Name { get; set; }
	[JsonPropertyName("create_time")]
	public int CreateTime { get; set; } = 555555;
	[JsonPropertyName("expire_time")]
	public int ExpireTime { get; set; }

}