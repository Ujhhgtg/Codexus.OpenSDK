using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.RPC;
// TODO: [RequiredMember]
public class EntityCheckPlayerMessage
{
	// TODO: [RequiredMember]
	[JsonPropertyName("a")]
	public int Length { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("b")]
	public string Message { get; set; }

}