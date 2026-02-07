using System.Text.Json.Serialization;

namespace Codexus.Development.SDK.Entities;

// Token: 0x0200002B RID: 43
public class BodyIn
{
	// Token: 0x17000033 RID: 51
	// (get) Token: 0x060000E1 RID: 225 RVA: 0x00005E04 File Offset: 0x00004004
	// (set) Token: 0x060000E2 RID: 226 RVA: 0x00005E0C File Offset: 0x0000400C
	[JsonPropertyName("body")]
	public string Body { get; set; }
}