using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Com4399;

public class Entity4399Device
{
    [JsonPropertyName("device-id")] public string DeviceIdentifier { get; set; }
    [JsonPropertyName("device-id-sm")] public string DeviceIdentifierSm { get; set; }
    [JsonPropertyName("device-udid")] public string DeviceUdid { get; set; }

    [JsonPropertyName("device-state")] public string? DeviceState { get; set; }
}