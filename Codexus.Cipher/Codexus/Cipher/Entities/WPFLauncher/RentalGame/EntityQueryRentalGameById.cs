using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.RentalGame;

// Token: 0x02000042 RID: 66
public class EntityQueryRentalGameById
{
	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x06000261 RID: 609 RVA: 0x0000859B File Offset: 0x0000679B
	// (set) Token: 0x06000262 RID: 610 RVA: 0x000085A3 File Offset: 0x000067A3
	[JsonPropertyName("offset")]
	public ulong Offset { get; set; }

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x06000263 RID: 611 RVA: 0x000085AC File Offset: 0x000067AC
	// (set) Token: 0x06000264 RID: 612 RVA: 0x000085B4 File Offset: 0x000067B4
	[JsonPropertyName("sort_type")]
	public EnumSortType SortType { get; set; }

	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x06000265 RID: 613 RVA: 0x000085BD File Offset: 0x000067BD
	// (set) Token: 0x06000266 RID: 614 RVA: 0x000085C5 File Offset: 0x000067C5
	[JsonPropertyName("world_name_key")]
	public List<string> WorldNameKey { get; set; } = new();
}