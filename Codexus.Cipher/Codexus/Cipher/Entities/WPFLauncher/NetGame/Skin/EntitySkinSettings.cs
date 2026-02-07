using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Skin;
// TODO: [RequiredMember]
public class EntitySkinSettings
{
	// TODO: [RequiredMember]
	[JsonPropertyName("client_type")]
	public string ClientType { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("game_type")]
	public int GameType { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("skin_id")]
	public string SkinId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("skin_mode")]
	public int SkinMode { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("skin_type")]
	public int SkinType { get; set; }

}