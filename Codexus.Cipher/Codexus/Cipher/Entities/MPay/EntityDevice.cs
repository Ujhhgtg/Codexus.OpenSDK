using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MPay;

// Token: 0x02000077 RID: 119
public class EntityDevice
{
	// Token: 0x1700019C RID: 412
	// (get) Token: 0x06000485 RID: 1157 RVA: 0x00009CF5 File Offset: 0x00007EF5
	// (set) Token: 0x06000486 RID: 1158 RVA: 0x00009CFD File Offset: 0x00007EFD
	[JsonPropertyName("id")]
	public string Id { get; set; } = string.Empty;

	// Token: 0x1700019D RID: 413
	// (get) Token: 0x06000487 RID: 1159 RVA: 0x00009D06 File Offset: 0x00007F06
	// (set) Token: 0x06000488 RID: 1160 RVA: 0x00009D0E File Offset: 0x00007F0E
	[JsonPropertyName("key")]
	public string Key { get; set; } = string.Empty;
}