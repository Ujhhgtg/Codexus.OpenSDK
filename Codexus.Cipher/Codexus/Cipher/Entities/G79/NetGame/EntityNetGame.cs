using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.NetGame;
// TODO: [RequiredMember]
public class EntityNetGame
{
	// TODO: [RequiredMember]
	[JsonPropertyName("res")]
	public required List<EntityNetGameItem> Res { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("tag")]
	public required int Tag { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("campaign_id")]
	public required int CampaignId { get; set; }

}