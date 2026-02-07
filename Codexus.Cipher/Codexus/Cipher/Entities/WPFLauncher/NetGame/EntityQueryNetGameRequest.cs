using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

// Token: 0x0200005D RID: 93
// TODO: [RequiredMember]
public class EntityQueryNetGameRequest
{
	// Token: 0x17000134 RID: 308
	// (get) Token: 0x06000397 RID: 919 RVA: 0x000092C8 File Offset: 0x000074C8
	// (set) Token: 0x06000398 RID: 920 RVA: 0x000092D0 File Offset: 0x000074D0
	[JsonPropertyName("channel_id")]
	public string ChannelId { get; set; } = "21";

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x06000399 RID: 921 RVA: 0x000092D9 File Offset: 0x000074D9
	// (set) Token: 0x0600039A RID: 922 RVA: 0x000092E1 File Offset: 0x000074E1
	// TODO: [RequiredMember]
	[JsonPropertyName("entity_ids")]
	public string[] EntityIds { get; set; }

	// Token: 0x17000136 RID: 310
	// (get) Token: 0x0600039B RID: 923 RVA: 0x000092EA File Offset: 0x000074EA
	// (set) Token: 0x0600039C RID: 924 RVA: 0x000092F2 File Offset: 0x000074F2
	[JsonPropertyName("is_has")]
	public bool IsHas { get; set; }

	// Token: 0x17000137 RID: 311
	// (get) Token: 0x0600039D RID: 925 RVA: 0x000092FB File Offset: 0x000074FB
	// (set) Token: 0x0600039E RID: 926 RVA: 0x00009303 File Offset: 0x00007503
	[JsonPropertyName("with_price")]
	public int WithPrice { get; set; }

	// Token: 0x17000138 RID: 312
	// (get) Token: 0x0600039F RID: 927 RVA: 0x0000930C File Offset: 0x0000750C
	// (set) Token: 0x060003A0 RID: 928 RVA: 0x00009314 File Offset: 0x00007514
	[JsonPropertyName("with_title_image")]
	public int WithTitleImage { get; set; } = 1;

	// Token: 0x060003A1 RID: 929 RVA: 0x0000931D File Offset: 0x0000751D
	public EntityQueryNetGameRequest()
	{
	}
}