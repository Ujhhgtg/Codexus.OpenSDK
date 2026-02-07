using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.Minecraft.Mods;

public class EntitySearchByItemIdQuery
{
    [JsonPropertyName("item_id")] public string ItemId { get; set; }
    [JsonPropertyName("length")] public int Length { get; set; }
    [JsonPropertyName("offset")] public int Offset { get; set; }
}