using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.Minecraft;

// Token: 0x02000070 RID: 112
// TODO: [RequiredMember]
public class EntityMcDownloadVersion
{
	// Token: 0x17000185 RID: 389
	// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000993B File Offset: 0x00007B3B
	// (set) Token: 0x06000449 RID: 1097 RVA: 0x00009943 File Offset: 0x00007B43
	// TODO: [RequiredMember]
	[JsonPropertyName("mc_version")]
	public int McVersion { get; set; }

	// Token: 0x0600044A RID: 1098 RVA: 0x0000994C File Offset: 0x00007B4C
	public EntityMcDownloadVersion()
	{
	}
}