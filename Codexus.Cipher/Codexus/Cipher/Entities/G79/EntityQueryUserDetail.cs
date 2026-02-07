using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79;

// Token: 0x0200008E RID: 142
// TODO: [RequiredMember]
public class EntityQueryUserDetail
{
	// Token: 0x17000202 RID: 514
	// (get) Token: 0x06000568 RID: 1384 RVA: 0x0000A68F File Offset: 0x0000888F
	// (set) Token: 0x06000569 RID: 1385 RVA: 0x0000A697 File Offset: 0x00008897
	// TODO: [RequiredMember]
	[JsonPropertyName("version")]
	public Version Version { get; set; }

	// Token: 0x0600056A RID: 1386 RVA: 0x0000A6A0 File Offset: 0x000088A0
	public EntityQueryUserDetail()
	{
	}
}