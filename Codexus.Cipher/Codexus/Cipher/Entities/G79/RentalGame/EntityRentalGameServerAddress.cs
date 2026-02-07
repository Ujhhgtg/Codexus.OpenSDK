using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.RentalGame;
// TODO: [RequiredMember]
public class EntityRentalGameServerAddress
{
	// TODO: [RequiredMember]
	[JsonPropertyName("mcserver_host")]
	public string Host { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("mcserver_port")]
	public int Port { get; set; }

}