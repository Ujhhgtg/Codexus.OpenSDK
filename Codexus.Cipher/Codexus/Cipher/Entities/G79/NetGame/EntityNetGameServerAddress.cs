using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.NetGame;
// TODO: [RequiredMember]
public class EntityNetGameServerAddress
{
	// TODO: [RequiredMember]
	[JsonPropertyName("host")]
	public string Host { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("port")]
	public int Port { get; set; }

}