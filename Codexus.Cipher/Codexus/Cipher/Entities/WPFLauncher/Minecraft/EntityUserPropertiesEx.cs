using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.Minecraft;

// Token: 0x02000071 RID: 113
// TODO: [RequiredMember]
public class EntityUserPropertiesEx
{
	// Token: 0x17000186 RID: 390
	// (get) Token: 0x0600044B RID: 1099 RVA: 0x00009955 File Offset: 0x00007B55
	// (set) Token: 0x0600044C RID: 1100 RVA: 0x0000995D File Offset: 0x00007B5D
	// TODO: [RequiredMember]
	[JsonPropertyName("GameType")]
	public int GameType { get; set; }

	// Token: 0x17000187 RID: 391
	// (get) Token: 0x0600044D RID: 1101 RVA: 0x00009966 File Offset: 0x00007B66
	// (set) Token: 0x0600044E RID: 1102 RVA: 0x0000996E File Offset: 0x00007B6E
	// TODO: [RequiredMember]
	[JsonPropertyName("isFilter")]
	public bool IsFilter { get; set; }

	// Token: 0x17000188 RID: 392
	// (get) Token: 0x0600044F RID: 1103 RVA: 0x00009977 File Offset: 0x00007B77
	// (set) Token: 0x06000450 RID: 1104 RVA: 0x0000997F File Offset: 0x00007B7F
	// TODO: [RequiredMember]
	[JsonPropertyName("channel")]
	public string Channel { get; set; }

	// Token: 0x17000189 RID: 393
	// (get) Token: 0x06000451 RID: 1105 RVA: 0x00009988 File Offset: 0x00007B88
	// (set) Token: 0x06000452 RID: 1106 RVA: 0x00009990 File Offset: 0x00007B90
	// TODO: [RequiredMember]
	[JsonPropertyName("timedelta")]
	public int TimeDelta { get; set; }

	// Token: 0x1700018A RID: 394
	// (get) Token: 0x06000453 RID: 1107 RVA: 0x00009999 File Offset: 0x00007B99
	// (set) Token: 0x06000454 RID: 1108 RVA: 0x000099A1 File Offset: 0x00007BA1
	// TODO: [RequiredMember]
	[JsonPropertyName("launcherVersion")]
	public string LauncherVersion { get; set; }

	// Token: 0x06000455 RID: 1109 RVA: 0x000099AA File Offset: 0x00007BAA
}