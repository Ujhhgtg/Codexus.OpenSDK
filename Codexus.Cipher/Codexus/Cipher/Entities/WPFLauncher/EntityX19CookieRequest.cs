using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher;
// TODO: [RequiredMember]
public class EntityX19CookieRequest
{
	// TODO: [RequiredMember]
	[JsonPropertyName("sauth_json")]
	public string Json { get; set; }

}