using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.Minecraft;

// Token: 0x0200006F RID: 111
// TODO: [RequiredMember]
public class EntityCoreLibResponse
{
	// Token: 0x1700017A RID: 378
	// (get) Token: 0x06000431 RID: 1073 RVA: 0x00009877 File Offset: 0x00007A77
	// (set) Token: 0x06000432 RID: 1074 RVA: 0x0000987F File Offset: 0x00007A7F
	// TODO: [RequiredMember]
	[JsonPropertyName("core_lib_md5")]
	public string CoreLibMd5 { get; set; }

	// Token: 0x1700017B RID: 379
	// (get) Token: 0x06000433 RID: 1075 RVA: 0x00009888 File Offset: 0x00007A88
	// (set) Token: 0x06000434 RID: 1076 RVA: 0x00009890 File Offset: 0x00007A90
	// TODO: [RequiredMember]
	[JsonPropertyName("core_lib_name")]
	public string CoreLibName { get; set; }

	// Token: 0x1700017C RID: 380
	// (get) Token: 0x06000435 RID: 1077 RVA: 0x00009899 File Offset: 0x00007A99
	// (set) Token: 0x06000436 RID: 1078 RVA: 0x000098A1 File Offset: 0x00007AA1
	// TODO: [RequiredMember]
	[JsonPropertyName("core_lib_size")]
	public int CoreLibSize { get; set; }

	// Token: 0x1700017D RID: 381
	// (get) Token: 0x06000437 RID: 1079 RVA: 0x000098AA File Offset: 0x00007AAA
	// (set) Token: 0x06000438 RID: 1080 RVA: 0x000098B2 File Offset: 0x00007AB2
	// TODO: [RequiredMember]
	[JsonPropertyName("core_lib_url")]
	public string CoreLibUrl { get; set; }

	// Token: 0x1700017E RID: 382
	// (get) Token: 0x06000439 RID: 1081 RVA: 0x000098BB File Offset: 0x00007ABB
	// (set) Token: 0x0600043A RID: 1082 RVA: 0x000098C3 File Offset: 0x00007AC3
	// TODO: [RequiredMember]
	[JsonPropertyName("mc_version")]
	public int McVersion { get; set; }

	// Token: 0x1700017F RID: 383
	// (get) Token: 0x0600043B RID: 1083 RVA: 0x000098CC File Offset: 0x00007ACC
	// (set) Token: 0x0600043C RID: 1084 RVA: 0x000098D4 File Offset: 0x00007AD4
	// TODO: [RequiredMember]
	[JsonPropertyName("md5")]
	public string Md5 { get; set; }

	// Token: 0x17000180 RID: 384
	// (get) Token: 0x0600043D RID: 1085 RVA: 0x000098DD File Offset: 0x00007ADD
	// (set) Token: 0x0600043E RID: 1086 RVA: 0x000098E5 File Offset: 0x00007AE5
	// TODO: [RequiredMember]
	[JsonPropertyName("name")]
	public string Name { get; set; }

	// Token: 0x17000181 RID: 385
	// (get) Token: 0x0600043F RID: 1087 RVA: 0x000098EE File Offset: 0x00007AEE
	// (set) Token: 0x06000440 RID: 1088 RVA: 0x000098F6 File Offset: 0x00007AF6
	// TODO: [RequiredMember]
	[JsonPropertyName("refresh_time")]
	public int RefreshTime { get; set; }

	// Token: 0x17000182 RID: 386
	// (get) Token: 0x06000441 RID: 1089 RVA: 0x000098FF File Offset: 0x00007AFF
	// (set) Token: 0x06000442 RID: 1090 RVA: 0x00009907 File Offset: 0x00007B07
	// TODO: [RequiredMember]
	[JsonPropertyName("size")]
	public int Size { get; set; }

	// Token: 0x17000183 RID: 387
	// (get) Token: 0x06000443 RID: 1091 RVA: 0x00009910 File Offset: 0x00007B10
	// (set) Token: 0x06000444 RID: 1092 RVA: 0x00009918 File Offset: 0x00007B18
	// TODO: [RequiredMember]
	[JsonPropertyName("url")]
	public string Url { get; set; }

	// Token: 0x17000184 RID: 388
	// (get) Token: 0x06000445 RID: 1093 RVA: 0x00009921 File Offset: 0x00007B21
	// (set) Token: 0x06000446 RID: 1094 RVA: 0x00009929 File Offset: 0x00007B29
	// TODO: [RequiredMember]
	[JsonPropertyName("version")]
	public string Version { get; set; }

	// Token: 0x06000447 RID: 1095 RVA: 0x00009932 File Offset: 0x00007B32
	public EntityCoreLibResponse()
	{
	}
}