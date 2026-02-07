using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Com4399;

// Token: 0x020000A5 RID: 165
public class Entity4399OAuthResponse
{
	// Token: 0x17000253 RID: 595
	// (get) Token: 0x06000625 RID: 1573 RVA: 0x0000AE2F File Offset: 0x0000902F
	// (set) Token: 0x06000626 RID: 1574 RVA: 0x0000AE37 File Offset: 0x00009037
	[JsonPropertyName("code")]
	public int Code { get; set; }

	// Token: 0x17000254 RID: 596
	// (get) Token: 0x06000627 RID: 1575 RVA: 0x0000AE40 File Offset: 0x00009040
	// (set) Token: 0x06000628 RID: 1576 RVA: 0x0000AE48 File Offset: 0x00009048
	[JsonPropertyName("message")]
	public string Message { get; set; } = string.Empty;

	// Token: 0x17000255 RID: 597
	// (get) Token: 0x06000629 RID: 1577 RVA: 0x0000AE51 File Offset: 0x00009051
	// (set) Token: 0x0600062A RID: 1578 RVA: 0x0000AE59 File Offset: 0x00009059
	[JsonPropertyName("result")]
	public Entity4399OAuthResult Result { get; set; } = new();
}