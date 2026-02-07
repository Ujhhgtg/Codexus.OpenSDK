using System.Text.Json.Serialization;
using Codexus.Cipher.Entities.Converter;

namespace Codexus.Cipher.Entities.G79;

// Token: 0x0200008B RID: 139
public class Entities<T> : EntityResponse
{
	// Token: 0x170001FD RID: 509
	// (get) Token: 0x0600055B RID: 1371 RVA: 0x0000A614 File Offset: 0x00008814
	// (set) Token: 0x0600055C RID: 1372 RVA: 0x0000A61C File Offset: 0x0000881C
	[JsonPropertyName("details")]
	public string Details { get; set; } = string.Empty;

	// Token: 0x170001FE RID: 510
	// (get) Token: 0x0600055D RID: 1373 RVA: 0x0000A625 File Offset: 0x00008825
	// (set) Token: 0x0600055E RID: 1374 RVA: 0x0000A62D File Offset: 0x0000882D
		
	[JsonPropertyName("entity")]
	public T Data
	{
			
		get;
			
		set;
	}

	// Token: 0x170001FF RID: 511
	// (get) Token: 0x0600055F RID: 1375 RVA: 0x0000A636 File Offset: 0x00008836
	// (set) Token: 0x06000560 RID: 1376 RVA: 0x0000A63E File Offset: 0x0000883E
	[JsonPropertyName("total")]
	[JsonConverter(typeof(NetEaseStringConverter))]
	public int Total { get; set; }
}