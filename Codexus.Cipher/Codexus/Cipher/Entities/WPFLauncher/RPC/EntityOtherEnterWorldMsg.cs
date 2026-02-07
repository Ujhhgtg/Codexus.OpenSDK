using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.RPC;

// Token: 0x0200003E RID: 62
public class EntityOtherEnterWorldMsg
{
	// Token: 0x17000096 RID: 150
	// (get) Token: 0x06000241 RID: 577 RVA: 0x00008447 File Offset: 0x00006647
	// (set) Token: 0x06000242 RID: 578 RVA: 0x0000844F File Offset: 0x0000664F
	[JsonPropertyName("id")]
	public short Id { get; set; }

	// Token: 0x17000097 RID: 151
	// (get) Token: 0x06000243 RID: 579 RVA: 0x00008458 File Offset: 0x00006658
	// (set) Token: 0x06000244 RID: 580 RVA: 0x00008460 File Offset: 0x00006660
	[JsonPropertyName("len")]
	public ushort Length { get; set; }

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x06000245 RID: 581 RVA: 0x00008469 File Offset: 0x00006669
	// (set) Token: 0x06000246 RID: 582 RVA: 0x00008471 File Offset: 0x00006671
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x06000247 RID: 583 RVA: 0x0000847A File Offset: 0x0000667A
	// (set) Token: 0x06000248 RID: 584 RVA: 0x00008482 File Offset: 0x00006682
	[JsonPropertyName("len1")]
	public ushort UuidLength { get; set; }

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x06000249 RID: 585 RVA: 0x0000848B File Offset: 0x0000668B
	// (set) Token: 0x0600024A RID: 586 RVA: 0x00008493 File Offset: 0x00006693
	[JsonPropertyName("uuid")]
	public string Uuid { get; set; } = string.Empty;
}