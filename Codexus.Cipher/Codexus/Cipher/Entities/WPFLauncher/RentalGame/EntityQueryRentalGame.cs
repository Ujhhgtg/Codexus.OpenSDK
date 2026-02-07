using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.RentalGame;

// Token: 0x02000041 RID: 65
public class EntityQueryRentalGame
{
	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x0600025C RID: 604 RVA: 0x00008570 File Offset: 0x00006770
	// (set) Token: 0x0600025D RID: 605 RVA: 0x00008578 File Offset: 0x00006778
	[JsonPropertyName("offset")]
	public int Offset { get; set; }

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x0600025E RID: 606 RVA: 0x00008581 File Offset: 0x00006781
	// (set) Token: 0x0600025F RID: 607 RVA: 0x00008589 File Offset: 0x00006789
	[JsonPropertyName("sort_type")]
	public int SortType { get; set; }
}