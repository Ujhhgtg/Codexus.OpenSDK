using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Connection.G79;

// Token: 0x020000A2 RID: 162
// TODO: [RequiredMember]
public class EntityZkpGetStartType
{
	// Token: 0x1700023A RID: 570
	// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0000ABA6 File Offset: 0x00008DA6
	// (set) Token: 0x060005F1 RID: 1521 RVA: 0x0000ABAE File Offset: 0x00008DAE
	// TODO: [RequiredMember]
	[JsonPropertyName("body")]
	public required string Body { get; set; }

	// Token: 0x060005F2 RID: 1522 RVA: 0x0000ABB7 File Offset: 0x00008DB7
	public EntityZkpGetStartType()
	{
	}
}