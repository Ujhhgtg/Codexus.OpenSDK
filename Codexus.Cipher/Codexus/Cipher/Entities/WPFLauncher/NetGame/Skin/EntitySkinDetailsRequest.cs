using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Skin;
// TODO: [RequiredMember]
public class EntitySkinDetailsRequest
{
	// TODO: [RequiredMember]
	[JsonPropertyName("channel_id")]
	public int ChannelId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("entity_ids")]
	public List<string> EntityIds { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("is_has")]
	public bool IsHas { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("with_price")]
	public bool WithPrice { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("with_title_image")]
	public bool WithTitleImage { get; set; }

}