using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Com4399;

// Token: 0x020000A7 RID: 167
public class Entity4399UserInfoResponse
{
	// Token: 0x1700025A RID: 602
	// (get) Token: 0x06000635 RID: 1589 RVA: 0x0000AEFA File Offset: 0x000090FA
	// (set) Token: 0x06000636 RID: 1590 RVA: 0x0000AF02 File Offset: 0x00009102
	[JsonPropertyName("code")]
	public string Code { get; set; } = string.Empty;

	// Token: 0x1700025B RID: 603
	// (get) Token: 0x06000637 RID: 1591 RVA: 0x0000AF0B File Offset: 0x0000910B
	// (set) Token: 0x06000638 RID: 1592 RVA: 0x0000AF13 File Offset: 0x00009113
	[JsonPropertyName("message")]
	public string Message { get; set; } = string.Empty;

	// Token: 0x1700025C RID: 604
	// (get) Token: 0x06000639 RID: 1593 RVA: 0x0000AF1C File Offset: 0x0000911C
	// (set) Token: 0x0600063A RID: 1594 RVA: 0x0000AF24 File Offset: 0x00009124
		
	[JsonPropertyName("result")]
	public Entity4399UserInfoResult Result
	{
			
		get;
			
		set;
	} = new();
}