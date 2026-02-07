using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Texture;

// Token: 0x02000062 RID: 98
public class EntityUserGameTexture
{
	// Token: 0x1700013F RID: 319
	// (get) Token: 0x060003B1 RID: 945 RVA: 0x000093B9 File Offset: 0x000075B9
	// (set) Token: 0x060003B2 RID: 946 RVA: 0x000093C1 File Offset: 0x000075C1
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; } = string.Empty;

	// Token: 0x17000140 RID: 320
	// (get) Token: 0x060003B3 RID: 947 RVA: 0x000093CA File Offset: 0x000075CA
	// (set) Token: 0x060003B4 RID: 948 RVA: 0x000093D2 File Offset: 0x000075D2
	[JsonPropertyName("game_type")]
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public EnumGType GameType { get; set; }

	// Token: 0x17000141 RID: 321
	// (get) Token: 0x060003B5 RID: 949 RVA: 0x000093DB File Offset: 0x000075DB
	// (set) Token: 0x060003B6 RID: 950 RVA: 0x000093E3 File Offset: 0x000075E3
	[JsonPropertyName("skin_type")]
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public EnumTextureType SkinType { get; set; }

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x060003B7 RID: 951 RVA: 0x000093EC File Offset: 0x000075EC
	// (set) Token: 0x060003B8 RID: 952 RVA: 0x000093F4 File Offset: 0x000075F4
	[JsonPropertyName("skin_id")]
	public string SkinId { get; set; } = string.Empty;

	// Token: 0x17000143 RID: 323
	// (get) Token: 0x060003B9 RID: 953 RVA: 0x000093FD File Offset: 0x000075FD
	// (set) Token: 0x060003BA RID: 954 RVA: 0x00009405 File Offset: 0x00007605
	[JsonPropertyName("skin_mode")]
	public int SkinMode { get; set; }

	// Token: 0x17000144 RID: 324
	// (get) Token: 0x060003BB RID: 955 RVA: 0x0000940E File Offset: 0x0000760E
	// (set) Token: 0x060003BC RID: 956 RVA: 0x00009416 File Offset: 0x00007616
	[JsonPropertyName("client_type")]
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public EnumGameClientType ClientType { get; set; }
}