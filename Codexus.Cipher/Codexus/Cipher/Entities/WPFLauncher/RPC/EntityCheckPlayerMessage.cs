using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.RPC;

// Token: 0x0200003D RID: 61
// TODO: [RequiredMember]
public class EntityCheckPlayerMessage
{
	// Token: 0x17000094 RID: 148
	// (get) Token: 0x0600023C RID: 572 RVA: 0x0000841C File Offset: 0x0000661C
	// (set) Token: 0x0600023D RID: 573 RVA: 0x00008424 File Offset: 0x00006624
	// TODO: [RequiredMember]
	[JsonPropertyName("a")]
	public int Length { get; set; }

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x0600023E RID: 574 RVA: 0x0000842D File Offset: 0x0000662D
	// (set) Token: 0x0600023F RID: 575 RVA: 0x00008435 File Offset: 0x00006635
	// TODO: [RequiredMember]
	[JsonPropertyName("b")]
	public string Message { get; set; }

	// Token: 0x06000240 RID: 576 RVA: 0x0000843E File Offset: 0x0000663E
	public EntityCheckPlayerMessage()
	{
	}
}