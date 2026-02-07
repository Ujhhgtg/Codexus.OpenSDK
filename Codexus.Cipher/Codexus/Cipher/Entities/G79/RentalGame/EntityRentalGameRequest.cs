using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.RentalGame;
// TODO: [RequiredMember]
public class EntityRentalGameRequest
{
	// TODO: [RequiredMember]
	[JsonPropertyName("sort_type")]
	public int SortType { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("order_type")]
	public int OrderType { get; set; }
	// TODO: [RequiredMember]
	[JsonPropertyName("offset")]
	public int Offset { get; set; }

}