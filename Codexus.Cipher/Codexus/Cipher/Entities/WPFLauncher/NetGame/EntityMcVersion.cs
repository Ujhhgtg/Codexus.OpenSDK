using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

// Token: 0x02000054 RID: 84
public class EntityMcVersion
{
	// Token: 0x17000104 RID: 260
	// (get) Token: 0x0600032E RID: 814 RVA: 0x00008E39 File Offset: 0x00007039
	// (set) Token: 0x0600032F RID: 815 RVA: 0x00008E41 File Offset: 0x00007041
	[JsonPropertyName("mcversionid")]
	public int McVersionId { get; set; }

	// Token: 0x17000105 RID: 261
	// (get) Token: 0x06000330 RID: 816 RVA: 0x00008E4A File Offset: 0x0000704A
	// (set) Token: 0x06000331 RID: 817 RVA: 0x00008E52 File Offset: 0x00007052
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;
}