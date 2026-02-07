using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Pc4399;

// Token: 0x02000074 RID: 116
public class Entity4399OpsItem
{
	// Token: 0x17000191 RID: 401
	// (get) Token: 0x0600046C RID: 1132 RVA: 0x00009BBC File Offset: 0x00007DBC
	// (set) Token: 0x0600046D RID: 1133 RVA: 0x00009BC4 File Offset: 0x00007DC4
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	// Token: 0x17000192 RID: 402
	// (get) Token: 0x0600046E RID: 1134 RVA: 0x00009BCD File Offset: 0x00007DCD
	// (set) Token: 0x0600046F RID: 1135 RVA: 0x00009BD5 File Offset: 0x00007DD5
	[JsonPropertyName("link")]
	public string Link { get; set; } = string.Empty;

	// Token: 0x17000193 RID: 403
	// (get) Token: 0x06000470 RID: 1136 RVA: 0x00009BDE File Offset: 0x00007DDE
	// (set) Token: 0x06000471 RID: 1137 RVA: 0x00009BE6 File Offset: 0x00007DE6
	[JsonPropertyName("banner")]
	public string Banner { get; set; } = string.Empty;

	// Token: 0x17000194 RID: 404
	// (get) Token: 0x06000472 RID: 1138 RVA: 0x00009BEF File Offset: 0x00007DEF
	// (set) Token: 0x06000473 RID: 1139 RVA: 0x00009BF7 File Offset: 0x00007DF7
	[JsonPropertyName("type")]
	public int Type { get; set; }
}