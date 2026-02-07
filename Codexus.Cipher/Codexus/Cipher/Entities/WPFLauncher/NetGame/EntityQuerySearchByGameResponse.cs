using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;
// TODO: [RequiredMember]
public class EntityQuerySearchByGameResponse
{
	// TODO: [RequiredMember]
	[JsonPropertyName("mc_version_id")]
	public int McVersionId { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("game_type")]
	public int GameType { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("iid_list")]
	public List<ulong> IidList { get; set; }

}