using System.Text.Json.Serialization;

namespace Codexus.Development.SDK.Entities;

public class BodyIn
{
    [JsonPropertyName("body")] public string Body { get; set; }
}