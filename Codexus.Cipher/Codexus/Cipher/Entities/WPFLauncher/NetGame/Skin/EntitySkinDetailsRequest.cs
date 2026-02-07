using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Skin;

public class EntitySkinDetailsRequest
{
    [JsonPropertyName("channel_id")] public int ChannelId { get; set; }
    [JsonPropertyName("entity_ids")] public List<string> EntityIds { get; set; }
    [JsonPropertyName("is_has")] public bool IsHas { get; set; }
    [JsonPropertyName("with_price")] public bool WithPrice { get; set; }
    [JsonPropertyName("with_title_image")] public bool WithTitleImage { get; set; }
}