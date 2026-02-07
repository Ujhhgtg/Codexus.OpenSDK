using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

public class EntityNetGameRequest
{
    [JsonPropertyName("available_mc_versions")]
    public string[] AvailableMcVersions { get; set; }

    [JsonPropertyName("item_type")] public int ItemType { get; set; }
    [JsonPropertyName("length")] public int Length { get; set; }
    [JsonPropertyName("offset")] public int Offset { get; set; }
    [JsonPropertyName("master_type_id")] public string MasterTypeId { get; set; }

    [JsonPropertyName("secondary_type_id")]
    public string SecondaryTypeId { get; set; }
}