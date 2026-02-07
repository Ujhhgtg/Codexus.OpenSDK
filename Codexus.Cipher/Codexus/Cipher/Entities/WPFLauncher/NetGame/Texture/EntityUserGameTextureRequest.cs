using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Texture;

// Token: 0x02000063 RID: 99
public class EntityUserGameTextureRequest
{
	// Token: 0x17000145 RID: 325
	// (get) Token: 0x060003BE RID: 958 RVA: 0x0000943E File Offset: 0x0000763E
	// (set) Token: 0x060003BF RID: 959 RVA: 0x00009446 File Offset: 0x00007646
	[JsonPropertyName("user_id")]
	public string UserId { get; set; } = string.Empty;

	// Token: 0x17000146 RID: 326
	// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000944F File Offset: 0x0000764F
	// (set) Token: 0x060003C1 RID: 961 RVA: 0x00009457 File Offset: 0x00007657
	[JsonPropertyName("game_type")]
	public string GameType { get; set; } = string.Empty;

	// Token: 0x17000147 RID: 327
	// (get) Token: 0x060003C2 RID: 962 RVA: 0x00009460 File Offset: 0x00007660
	// (set) Token: 0x060003C3 RID: 963 RVA: 0x00009468 File Offset: 0x00007668
	[JsonPropertyName("client_type")]
	public EnumGameClientType ClientType { get; set; }
}