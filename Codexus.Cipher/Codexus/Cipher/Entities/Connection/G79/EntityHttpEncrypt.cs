using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Connection.G79;

// Token: 0x020000A0 RID: 160
// TODO: [RequiredMember]
public class EntityHttpEncrypt
{
	// Token: 0x17000238 RID: 568
	// (get) Token: 0x060005EA RID: 1514 RVA: 0x0000AB72 File Offset: 0x00008D72
	// (set) Token: 0x060005EB RID: 1515 RVA: 0x0000AB7A File Offset: 0x00008D7A
	// TODO: [RequiredMember]
	[JsonPropertyName("body")]
	public required string Body { get; set; }

	// Token: 0x060005EC RID: 1516 RVA: 0x0000AB83 File Offset: 0x00008D83
	public EntityHttpEncrypt()
	{
	}
}