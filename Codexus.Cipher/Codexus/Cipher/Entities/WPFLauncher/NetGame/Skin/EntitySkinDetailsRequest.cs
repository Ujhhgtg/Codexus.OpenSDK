using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Skin;

// Token: 0x02000069 RID: 105
// TODO: [RequiredMember]
public class EntitySkinDetailsRequest
{
	// Token: 0x1700015F RID: 351
	// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000965E File Offset: 0x0000785E
	// (set) Token: 0x060003F7 RID: 1015 RVA: 0x00009666 File Offset: 0x00007866
	// TODO: [RequiredMember]
	[JsonPropertyName("channel_id")]
	public int ChannelId { get; set; }

	// Token: 0x17000160 RID: 352
	// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000966F File Offset: 0x0000786F
	// (set) Token: 0x060003F9 RID: 1017 RVA: 0x00009677 File Offset: 0x00007877
	// TODO: [RequiredMember]
	[JsonPropertyName("entity_ids")]
	public List<string> EntityIds { get; set; }

	// Token: 0x17000161 RID: 353
	// (get) Token: 0x060003FA RID: 1018 RVA: 0x00009680 File Offset: 0x00007880
	// (set) Token: 0x060003FB RID: 1019 RVA: 0x00009688 File Offset: 0x00007888
	// TODO: [RequiredMember]
	[JsonPropertyName("is_has")]
	public bool IsHas { get; set; }

	// Token: 0x17000162 RID: 354
	// (get) Token: 0x060003FC RID: 1020 RVA: 0x00009691 File Offset: 0x00007891
	// (set) Token: 0x060003FD RID: 1021 RVA: 0x00009699 File Offset: 0x00007899
	// TODO: [RequiredMember]
	[JsonPropertyName("with_price")]
	public bool WithPrice { get; set; }

	// Token: 0x17000163 RID: 355
	// (get) Token: 0x060003FE RID: 1022 RVA: 0x000096A2 File Offset: 0x000078A2
	// (set) Token: 0x060003FF RID: 1023 RVA: 0x000096AA File Offset: 0x000078AA
	// TODO: [RequiredMember]
	[JsonPropertyName("with_title_image")]
	public bool WithTitleImage { get; set; }

	// Token: 0x06000400 RID: 1024 RVA: 0x000096B3 File Offset: 0x000078B3
	public EntitySkinDetailsRequest()
	{
	}
}