using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

// Token: 0x0200005B RID: 91
// TODO: [RequiredMember]
public class EntityQueryNetGameDetailRequest
{
	// Token: 0x17000132 RID: 306
	// (get) Token: 0x06000391 RID: 913 RVA: 0x00009289 File Offset: 0x00007489
	// (set) Token: 0x06000392 RID: 914 RVA: 0x00009291 File Offset: 0x00007491
	// TODO: [RequiredMember]
	[JsonPropertyName("item_id")]
	public string ItemId { get; set; }

	// Token: 0x06000393 RID: 915 RVA: 0x0000929A File Offset: 0x0000749A
}