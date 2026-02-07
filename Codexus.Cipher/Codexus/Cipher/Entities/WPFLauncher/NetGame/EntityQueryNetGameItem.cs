using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

// Token: 0x0200005C RID: 92
public class EntityQueryNetGameItem
{
	// Token: 0x17000133 RID: 307
	// (get) Token: 0x06000394 RID: 916 RVA: 0x000092A3 File Offset: 0x000074A3
	// (set) Token: 0x06000395 RID: 917 RVA: 0x000092AB File Offset: 0x000074AB
	[JsonPropertyName("title_image_url")]
	public string TitleImageUrl { get; set; } = string.Empty;
}