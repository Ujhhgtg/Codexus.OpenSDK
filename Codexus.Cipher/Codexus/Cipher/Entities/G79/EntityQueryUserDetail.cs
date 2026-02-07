using System;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79;
// TODO: [RequiredMember]
public class EntityQueryUserDetail
{
	// TODO: [RequiredMember]
	[JsonPropertyName("version")]
	public Version Version { get; set; }

}