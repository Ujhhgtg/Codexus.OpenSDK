using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities;

// Token: 0x02000031 RID: 49
public class Entity<T> : EntityResponse
{
	// Token: 0x17000031 RID: 49
	// (get) Token: 0x0600016B RID: 363 RVA: 0x00007B26 File Offset: 0x00005D26
	// (set) Token: 0x0600016C RID: 364 RVA: 0x00007B2E File Offset: 0x00005D2E
	[JsonPropertyName("details")]
	public string Details { get; set; } = string.Empty;

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x0600016D RID: 365 RVA: 0x00007B37 File Offset: 0x00005D37
	// (set) Token: 0x0600016E RID: 366 RVA: 0x00007B3F File Offset: 0x00005D3F
		
	[JsonPropertyName("entity")]
	public T Data
	{
			
		get;
			
		set;
	}
}