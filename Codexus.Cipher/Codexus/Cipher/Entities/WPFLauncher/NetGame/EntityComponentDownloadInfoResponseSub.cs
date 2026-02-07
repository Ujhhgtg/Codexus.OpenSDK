using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

// Token: 0x02000050 RID: 80
// TODO: [RequiredMember]
public class EntityComponentDownloadInfoResponseSub
{
	// Token: 0x170000ED RID: 237
	// (get) Token: 0x060002FC RID: 764 RVA: 0x00008C33 File Offset: 0x00006E33
	// (set) Token: 0x060002FD RID: 765 RVA: 0x00008C3B File Offset: 0x00006E3B
	// TODO: [RequiredMember]
	[JsonPropertyName("java_version")]
	public int JavaVersion { get; set; }

	// Token: 0x170000EE RID: 238
	// (get) Token: 0x060002FE RID: 766 RVA: 0x00008C44 File Offset: 0x00006E44
	// (set) Token: 0x060002FF RID: 767 RVA: 0x00008C4C File Offset: 0x00006E4C
	// TODO: [RequiredMember]
	[JsonPropertyName("mc_version_name")]
	public string McVersionName { get; set; }

	// Token: 0x170000EF RID: 239
	// (get) Token: 0x06000300 RID: 768 RVA: 0x00008C55 File Offset: 0x00006E55
	// (set) Token: 0x06000301 RID: 769 RVA: 0x00008C5D File Offset: 0x00006E5D
	// TODO: [RequiredMember]
	[JsonPropertyName("res_url")]
	public string ResUrl { get; set; }

	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x06000302 RID: 770 RVA: 0x00008C66 File Offset: 0x00006E66
	// (set) Token: 0x06000303 RID: 771 RVA: 0x00008C6E File Offset: 0x00006E6E
	// TODO: [RequiredMember]
	[JsonPropertyName("res_size")]
	public long ResSize { get; set; }

	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x06000304 RID: 772 RVA: 0x00008C77 File Offset: 0x00006E77
	// (set) Token: 0x06000305 RID: 773 RVA: 0x00008C7F File Offset: 0x00006E7F
	// TODO: [RequiredMember]
	[JsonPropertyName("res_md5")]
	public string ResMd5 { get; set; }

	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x06000306 RID: 774 RVA: 0x00008C88 File Offset: 0x00006E88
	// (set) Token: 0x06000307 RID: 775 RVA: 0x00008C90 File Offset: 0x00006E90
	// TODO: [RequiredMember]
	[JsonPropertyName("jar_md5")]
	public string JarMd5 { get; set; }

	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x06000308 RID: 776 RVA: 0x00008C99 File Offset: 0x00006E99
	// (set) Token: 0x06000309 RID: 777 RVA: 0x00008CA1 File Offset: 0x00006EA1
	// TODO: [RequiredMember]
	[JsonPropertyName("res_name")]
	public string ResName { get; set; }

	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x0600030A RID: 778 RVA: 0x00008CAA File Offset: 0x00006EAA
	// (set) Token: 0x0600030B RID: 779 RVA: 0x00008CB2 File Offset: 0x00006EB2
	// TODO: [RequiredMember]
	[JsonPropertyName("res_version")]
	public int ResVersion { get; set; }

	// Token: 0x0600030C RID: 780 RVA: 0x00008CBB File Offset: 0x00006EBB
	public EntityComponentDownloadInfoResponseSub()
	{
	}
}