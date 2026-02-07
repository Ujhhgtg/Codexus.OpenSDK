using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher;

// Token: 0x02000038 RID: 56
// TODO: [RequiredMember]
public class EntityAuthenticationVersion
{
	// Token: 0x1700007E RID: 126
	// (get) Token: 0x0600020B RID: 523 RVA: 0x000081F3 File Offset: 0x000063F3
	// (set) Token: 0x0600020C RID: 524 RVA: 0x000081FB File Offset: 0x000063FB
	// TODO: [RequiredMember]
	[JsonPropertyName("version")]
	public string Version { get; set; }

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x0600020D RID: 525 RVA: 0x00008204 File Offset: 0x00006404
	// (set) Token: 0x0600020E RID: 526 RVA: 0x0000820C File Offset: 0x0000640C
	[JsonPropertyName("launcher_md5")]
	public string LauncherMd5 { get; set; } = string.Empty;

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x0600020F RID: 527 RVA: 0x00008215 File Offset: 0x00006415
	// (set) Token: 0x06000210 RID: 528 RVA: 0x0000821D File Offset: 0x0000641D
	[JsonPropertyName("updater_md5")]
	public string UpdaterMd5 { get; set; } = string.Empty;

	// Token: 0x06000211 RID: 529 RVA: 0x00008226 File Offset: 0x00006426
	public EntityAuthenticationVersion()
	{
	}
}