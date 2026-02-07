using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;
// TODO: [RequiredMember]
public class EntitySearchByIdsQuery
{
	// TODO: [RequiredMember]
	[JsonPropertyName("item_id_list")]
	public List<ulong> ItemIdList { get; set; }

}