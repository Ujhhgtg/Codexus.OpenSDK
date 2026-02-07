using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Pc4399;

// Token: 0x02000076 RID: 118
public class Entity4399ResponseData
{
	// Token: 0x17000198 RID: 408
	// (get) Token: 0x0600047C RID: 1148 RVA: 0x00009C7C File Offset: 0x00007E7C
	// (set) Token: 0x0600047D RID: 1149 RVA: 0x00009C84 File Offset: 0x00007E84
	[JsonPropertyName("ops")]
	public List<Entity4399OpsItem> Ops { get; set; } = new();

	// Token: 0x17000199 RID: 409
	// (get) Token: 0x0600047E RID: 1150 RVA: 0x00009C8D File Offset: 0x00007E8D
	// (set) Token: 0x0600047F RID: 1151 RVA: 0x00009C95 File Offset: 0x00007E95
	[JsonPropertyName("username")]
	public string Username { get; set; } = string.Empty;

	// Token: 0x1700019A RID: 410
	// (get) Token: 0x06000480 RID: 1152 RVA: 0x00009C9E File Offset: 0x00007E9E
	// (set) Token: 0x06000481 RID: 1153 RVA: 0x00009CA6 File Offset: 0x00007EA6
	[JsonPropertyName("login_tip")]
	public string LoginTip { get; set; } = string.Empty;

	// Token: 0x1700019B RID: 411
	// (get) Token: 0x06000482 RID: 1154 RVA: 0x00009CAF File Offset: 0x00007EAF
	// (set) Token: 0x06000483 RID: 1155 RVA: 0x00009CB7 File Offset: 0x00007EB7
	[JsonPropertyName("sdk_login_data")]
	public string SdkLoginData { get; set; } = string.Empty;
}