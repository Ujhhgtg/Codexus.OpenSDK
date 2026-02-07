using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities;

// Token: 0x02000032 RID: 50
public class EntityResponse
{
	// Token: 0x17000033 RID: 51
	// (get) Token: 0x06000170 RID: 368 RVA: 0x00007B5C File Offset: 0x00005D5C
	// (set) Token: 0x06000171 RID: 369 RVA: 0x00007B64 File Offset: 0x00005D64
	[JsonPropertyName("code")]
	public int Code { get; set; }

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x06000172 RID: 370 RVA: 0x00007B6D File Offset: 0x00005D6D
	// (set) Token: 0x06000173 RID: 371 RVA: 0x00007B75 File Offset: 0x00005D75
	[JsonPropertyName("message")]
	public string Message { get; set; } = string.Empty;
}