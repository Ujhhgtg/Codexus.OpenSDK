using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Skin;

// Token: 0x0200006B RID: 107
// TODO: [RequiredMember]
public class EntitySkinSettings
{
	// Token: 0x1700016E RID: 366
	// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000976F File Offset: 0x0000796F
	// (set) Token: 0x06000417 RID: 1047 RVA: 0x00009777 File Offset: 0x00007977
	// TODO: [RequiredMember]
	[JsonPropertyName("client_type")]
	public string ClientType { get; set; }

	// Token: 0x1700016F RID: 367
	// (get) Token: 0x06000418 RID: 1048 RVA: 0x00009780 File Offset: 0x00007980
	// (set) Token: 0x06000419 RID: 1049 RVA: 0x00009788 File Offset: 0x00007988
	// TODO: [RequiredMember]
	[JsonPropertyName("game_type")]
	public int GameType { get; set; }

	// Token: 0x17000170 RID: 368
	// (get) Token: 0x0600041A RID: 1050 RVA: 0x00009791 File Offset: 0x00007991
	// (set) Token: 0x0600041B RID: 1051 RVA: 0x00009799 File Offset: 0x00007999
	// TODO: [RequiredMember]
	[JsonPropertyName("skin_id")]
	public string SkinId { get; set; }

	// Token: 0x17000171 RID: 369
	// (get) Token: 0x0600041C RID: 1052 RVA: 0x000097A2 File Offset: 0x000079A2
	// (set) Token: 0x0600041D RID: 1053 RVA: 0x000097AA File Offset: 0x000079AA
	// TODO: [RequiredMember]
	[JsonPropertyName("skin_mode")]
	public int SkinMode { get; set; }

	// Token: 0x17000172 RID: 370
	// (get) Token: 0x0600041E RID: 1054 RVA: 0x000097B3 File Offset: 0x000079B3
	// (set) Token: 0x0600041F RID: 1055 RVA: 0x000097BB File Offset: 0x000079BB
	// TODO: [RequiredMember]
	[JsonPropertyName("skin_type")]
	public int SkinType { get; set; }

	// Token: 0x06000420 RID: 1056 RVA: 0x000097C4 File Offset: 0x000079C4
	public EntitySkinSettings()
	{
	}
}