using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Mods;

// Token: 0x0200006D RID: 109
// TODO: [RequiredMember]
public class EntityModsInfo
{
	// Token: 0x17000173 RID: 371
	// (get) Token: 0x06000421 RID: 1057 RVA: 0x000097CD File Offset: 0x000079CD
	// (set) Token: 0x06000422 RID: 1058 RVA: 0x000097D5 File Offset: 0x000079D5
	// TODO: [RequiredMember]
	[JsonPropertyName("modPath")]
	public string ModPath { get; set; }

	// Token: 0x17000174 RID: 372
	// (get) Token: 0x06000423 RID: 1059 RVA: 0x000097DE File Offset: 0x000079DE
	// (set) Token: 0x06000424 RID: 1060 RVA: 0x000097E6 File Offset: 0x000079E6
	// TODO: [RequiredMember]
	[JsonPropertyName("name")]
	public string Name { get; set; } = "";

	// Token: 0x17000175 RID: 373
	// (get) Token: 0x06000425 RID: 1061 RVA: 0x000097EF File Offset: 0x000079EF
	// (set) Token: 0x06000426 RID: 1062 RVA: 0x000097F7 File Offset: 0x000079F7
	// TODO: [RequiredMember]
	[JsonPropertyName("id")]
	public string Id { get; set; }

	// Token: 0x17000176 RID: 374
	// (get) Token: 0x06000427 RID: 1063 RVA: 0x00009800 File Offset: 0x00007A00
	// (set) Token: 0x06000428 RID: 1064 RVA: 0x00009808 File Offset: 0x00007A08
	// TODO: [RequiredMember]
	[JsonPropertyName("iid")]
	public string Iid { get; set; }

	// Token: 0x17000177 RID: 375
	// (get) Token: 0x06000429 RID: 1065 RVA: 0x00009811 File Offset: 0x00007A11
	// (set) Token: 0x0600042A RID: 1066 RVA: 0x00009819 File Offset: 0x00007A19
	// TODO: [RequiredMember]
	[JsonPropertyName("md5")]
	public string Md5 { get; set; }

	// Token: 0x17000178 RID: 376
	// (get) Token: 0x0600042B RID: 1067 RVA: 0x00009822 File Offset: 0x00007A22
	// (set) Token: 0x0600042C RID: 1068 RVA: 0x0000982A File Offset: 0x00007A2A
	// TODO: [RequiredMember]
	[JsonPropertyName("version")]
	public string Version { get; set; } = "";

	// Token: 0x0600042D RID: 1069 RVA: 0x00009833 File Offset: 0x00007A33
	public EntityModsInfo()
	{
	}
}