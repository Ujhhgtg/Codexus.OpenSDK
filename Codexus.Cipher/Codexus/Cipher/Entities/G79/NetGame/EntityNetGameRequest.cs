using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.NetGame;
// TODO: [RequiredMember]
public class EntityNetGameRequest
{
	// TODO: [RequiredMember]
	[JsonPropertyName("version")]
	public string Version { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("channel_id")]
	public int ChannelId { get; set; }

}