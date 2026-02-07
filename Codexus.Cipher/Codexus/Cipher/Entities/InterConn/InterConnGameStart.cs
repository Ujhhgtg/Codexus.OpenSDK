using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.InterConn;
// TODO: [RequiredMember]
public class InterConnGameStart
{
	// TODO: [RequiredMember]
	[JsonPropertyName("game_id")]
	public string GameId { get; set; }
	[JsonPropertyName("game_type")]
	public string GameType { get; set; } = "2";
	[JsonPropertyName("strict_mode")]
	public bool StrictMode { get; set; } = true;
	// TODO: [RequiredMember]
	[JsonPropertyName("item_list")]
	public string[] ItemList { get; set; }

}