using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher;

// Token: 0x0200003A RID: 58
public class EntityPatchVersion
{
	// Token: 0x17000086 RID: 134
	// (get) Token: 0x0600021D RID: 541 RVA: 0x000082AE File Offset: 0x000064AE
	// (set) Token: 0x0600021E RID: 542 RVA: 0x000082B6 File Offset: 0x000064B6
	[JsonPropertyName("size")]
	public long Size { get; set; }

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x0600021F RID: 543 RVA: 0x000082BF File Offset: 0x000064BF
	// (set) Token: 0x06000220 RID: 544 RVA: 0x000082C7 File Offset: 0x000064C7
	[JsonPropertyName("url")]
	public string Url { get; set; } = "";

	// Token: 0x17000088 RID: 136
	// (get) Token: 0x06000221 RID: 545 RVA: 0x000082D0 File Offset: 0x000064D0
	// (set) Token: 0x06000222 RID: 546 RVA: 0x000082D8 File Offset: 0x000064D8
	[JsonPropertyName("md5")]
	public string Md5 { get; set; } = "";
}