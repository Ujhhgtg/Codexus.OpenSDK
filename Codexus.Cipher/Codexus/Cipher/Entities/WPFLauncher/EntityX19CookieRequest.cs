using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher;

// Token: 0x0200003C RID: 60
// TODO: [RequiredMember]
public class EntityX19CookieRequest
{
	// Token: 0x17000093 RID: 147
	// (get) Token: 0x06000239 RID: 569 RVA: 0x00008402 File Offset: 0x00006602
	// (set) Token: 0x0600023A RID: 570 RVA: 0x0000840A File Offset: 0x0000660A
	// TODO: [RequiredMember]
	[JsonPropertyName("sauth_json")]
	public string Json { get; set; }

	// Token: 0x0600023B RID: 571 RVA: 0x00008413 File Offset: 0x00006613
}