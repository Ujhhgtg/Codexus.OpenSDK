using System.Text.Json.Serialization;

namespace Codexus.Development.SDK.Entities;

// Token: 0x0200002C RID: 44
public class EntityAvailableUser
{
	// Token: 0x17000034 RID: 52
	// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005E1E File Offset: 0x0000401E
	// (set) Token: 0x060000E5 RID: 229 RVA: 0x00005E26 File Offset: 0x00004026
	[JsonPropertyName("id")]
	public string UserId { get; set; }

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005E2F File Offset: 0x0000402F
	// (set) Token: 0x060000E7 RID: 231 RVA: 0x00005E37 File Offset: 0x00004037
	[JsonPropertyName("token")]
	public string AccessToken { get; set; }

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005E40 File Offset: 0x00004040
	// (set) Token: 0x060000E9 RID: 233 RVA: 0x00005E48 File Offset: 0x00004048
	[JsonPropertyName("last_login_time")]
	public long LastLoginTime { get; set; }
}