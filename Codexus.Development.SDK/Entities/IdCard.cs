using System.Text.Json.Serialization;

namespace Codexus.Development.SDK.Entities;

// Token: 0x0200002E RID: 46
public class IdCard
{
	// Token: 0x1700003C RID: 60
	// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005ECE File Offset: 0x000040CE
	// (set) Token: 0x060000F7 RID: 247 RVA: 0x00005ED6 File Offset: 0x000040D6
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005EDF File Offset: 0x000040DF
	// (set) Token: 0x060000F9 RID: 249 RVA: 0x00005EE7 File Offset: 0x000040E7
	[JsonPropertyName("idNumber")]
	public string IdNumber { get; set; } = string.Empty;
}