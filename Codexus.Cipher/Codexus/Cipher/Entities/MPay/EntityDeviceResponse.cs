using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MPay;

// Token: 0x02000078 RID: 120
public class EntityDeviceResponse
{
	// Token: 0x1700019E RID: 414
	// (get) Token: 0x0600048A RID: 1162 RVA: 0x00009D36 File Offset: 0x00007F36
	// (set) Token: 0x0600048B RID: 1163 RVA: 0x00009D3E File Offset: 0x00007F3E
	[JsonPropertyName("device")]
	public EntityDevice EntityDevice { get; set; } = new();
}