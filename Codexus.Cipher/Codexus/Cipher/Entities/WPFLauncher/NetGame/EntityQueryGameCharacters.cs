using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

// Token: 0x02000059 RID: 89
// TODO: [RequiredMember]
public class EntityQueryGameCharacters
{
	// Token: 0x17000124 RID: 292
	// (get) Token: 0x06000373 RID: 883 RVA: 0x0000911C File Offset: 0x0000731C
	// (set) Token: 0x06000374 RID: 884 RVA: 0x00009124 File Offset: 0x00007324
	[JsonPropertyName("offset")]
	public int Offset { get; set; }

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x06000375 RID: 885 RVA: 0x0000912D File Offset: 0x0000732D
	// (set) Token: 0x06000376 RID: 886 RVA: 0x00009135 File Offset: 0x00007335
	[JsonPropertyName("length")]
	public int Length { get; set; } = 10;

	// Token: 0x17000126 RID: 294
	// (get) Token: 0x06000377 RID: 887 RVA: 0x0000913E File Offset: 0x0000733E
	// (set) Token: 0x06000378 RID: 888 RVA: 0x00009146 File Offset: 0x00007346
	// TODO: [RequiredMember]
	[JsonPropertyName("user_id")]
	public string UserId { get; set; }

	// Token: 0x17000127 RID: 295
	// (get) Token: 0x06000379 RID: 889 RVA: 0x0000914F File Offset: 0x0000734F
	// (set) Token: 0x0600037A RID: 890 RVA: 0x00009157 File Offset: 0x00007357
	// TODO: [RequiredMember]
	[JsonPropertyName("game_id")]
	public string GameId { get; set; }

	// Token: 0x17000128 RID: 296
	// (get) Token: 0x0600037B RID: 891 RVA: 0x00009160 File Offset: 0x00007360
	// (set) Token: 0x0600037C RID: 892 RVA: 0x00009168 File Offset: 0x00007368
	[JsonPropertyName("game_type")]
	public string GameType { get; set; } = "2";

	// Token: 0x0600037D RID: 893 RVA: 0x00009171 File Offset: 0x00007371
}