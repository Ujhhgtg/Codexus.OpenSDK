using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MPay;
// TODO: [RequiredMember]
public class EntityMPayUserResponse
{
	[JsonPropertyName("force_pwd")]
	public bool ForcePwd { get; set; }
		
	[JsonPropertyName("verify_status")]
	public EntityVerifyStatus VerifyStatus
	{
			
		get;
			
		set;
	}
	// TODO: [RequiredMember]
	[JsonPropertyName("user")]
	public EntityMPayUser User { get; set; } = new();

}