using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

public class EntitySearchByIdsQuery
{
    [JsonPropertyName("item_id_list")] public List<ulong> ItemIdList { get; set; }
}