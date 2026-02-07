using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Com4399;
// TODO: [RequiredMember]
public class Entity4399Device
{
	// TODO: [RequiredMember]
	[JsonPropertyName("device-id")]
	public required string DeviceIdentifier { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("device-id-sm")]
	public required string DeviceIdentifierSm { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("device-udid")]
	public required string DeviceUdid { get; set; }
		
	[JsonPropertyName("device-state")]
	public string DeviceState
	{
			
		get;
			
		set;
	}

}