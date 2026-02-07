using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.RentalGame;

// Token: 0x02000093 RID: 147
// TODO: [RequiredMember]
public class EntityRentalGameRequest
{
	// Token: 0x1700020B RID: 523
	// (get) Token: 0x0600057F RID: 1407 RVA: 0x0000A755 File Offset: 0x00008955
	// (set) Token: 0x06000580 RID: 1408 RVA: 0x0000A75D File Offset: 0x0000895D
	// TODO: [RequiredMember]
	[JsonPropertyName("sort_type")]
	public int SortType { get; set; }

	// Token: 0x1700020C RID: 524
	// (get) Token: 0x06000581 RID: 1409 RVA: 0x0000A766 File Offset: 0x00008966
	// (set) Token: 0x06000582 RID: 1410 RVA: 0x0000A76E File Offset: 0x0000896E
	// TODO: [RequiredMember]
	[JsonPropertyName("order_type")]
	public int OrderType { get; set; }

	// Token: 0x1700020D RID: 525
	// (get) Token: 0x06000583 RID: 1411 RVA: 0x0000A777 File Offset: 0x00008977
	// (set) Token: 0x06000584 RID: 1412 RVA: 0x0000A77F File Offset: 0x0000897F
	// TODO: [RequiredMember]
	[JsonPropertyName("offset")]
	public int Offset { get; set; }

	// Token: 0x06000585 RID: 1413 RVA: 0x0000A788 File Offset: 0x00008988
	public EntityRentalGameRequest()
	{
	}
}