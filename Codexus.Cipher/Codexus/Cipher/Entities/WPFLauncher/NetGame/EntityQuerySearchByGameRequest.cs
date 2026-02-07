using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

// Token: 0x0200005E RID: 94
// TODO: [RequiredMember]
public class EntityQuerySearchByGameRequest
{
	// Token: 0x17000139 RID: 313
	// (get) Token: 0x060003A2 RID: 930 RVA: 0x00009338 File Offset: 0x00007538
	// (set) Token: 0x060003A3 RID: 931 RVA: 0x00009340 File Offset: 0x00007540
	// TODO: [RequiredMember]
	[JsonPropertyName("mc_version_id")]
	public int McVersionId { get; set; }

	// Token: 0x1700013A RID: 314
	// (get) Token: 0x060003A4 RID: 932 RVA: 0x00009349 File Offset: 0x00007549
	// (set) Token: 0x060003A5 RID: 933 RVA: 0x00009351 File Offset: 0x00007551
	// TODO: [RequiredMember]
	[JsonPropertyName("game_type")]
	public int GameType { get; set; }

	// Token: 0x060003A6 RID: 934 RVA: 0x0000935A File Offset: 0x0000755A
}