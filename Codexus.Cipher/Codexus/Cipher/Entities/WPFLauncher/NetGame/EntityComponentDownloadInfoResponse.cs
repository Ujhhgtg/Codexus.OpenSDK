using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

public class EntityComponentDownloadInfoResponse
{
    [JsonPropertyName("entity_id")] public string EntityId { get; set; }
    [JsonPropertyName("item_id")] public string ItemId { get; set; }
    [JsonPropertyName("user_id")] public string UserId { get; set; }
    [JsonPropertyName("itype")] public int IType { get; set; }
    [JsonPropertyName("mtypeid")] public int MTypeId { get; set; }
    [JsonPropertyName("stypeid")] public int STypeId { get; set; }
    [JsonPropertyName("sub_entities")] public List<EntityComponentDownloadInfoResponseSub> SubEntities { get; set; }
    [JsonPropertyName("sub_mod_list")] public List<ulong> SubModList { get; set; }
}