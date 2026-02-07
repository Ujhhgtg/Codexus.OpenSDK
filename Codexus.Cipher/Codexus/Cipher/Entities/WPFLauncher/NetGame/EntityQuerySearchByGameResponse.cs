using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

public class EntityQuerySearchByGameResponse
{
    [JsonPropertyName("mc_version_id")] public int McVersionId { get; set; }
    [JsonPropertyName("game_type")] public int GameType { get; set; }
    [JsonPropertyName("iid_list")] public List<ulong> IidList { get; set; }
}