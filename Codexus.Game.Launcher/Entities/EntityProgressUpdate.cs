using System;
using System.Text.Json.Serialization;

namespace Codexus.Game.Launcher.Entities;
public class EntityProgressUpdate
{
	[JsonPropertyName("id")] public Guid Id { get; set; }
	[JsonPropertyName("percent")] public int Percent { get; set; }
	[JsonPropertyName("message")] public string Message { get; set; }

}