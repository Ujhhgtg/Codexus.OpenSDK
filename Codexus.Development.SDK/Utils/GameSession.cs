using System.Runtime.CompilerServices;

namespace Codexus.Development.SDK.Utils;

// Token: 0x0200000A RID: 10
public class GameSession(string nickName, string userId, string userToken)
{
	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000039 RID: 57 RVA: 0x00002D51 File Offset: 0x00000F51
	// (set) Token: 0x0600003A RID: 58 RVA: 0x00002D59 File Offset: 0x00000F59
	public string NickName { get; set; } = nickName;

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x0600003B RID: 59 RVA: 0x00002D62 File Offset: 0x00000F62
	// (set) Token: 0x0600003C RID: 60 RVA: 0x00002D6A File Offset: 0x00000F6A
	public string UserId { get; set; } = userId;

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x0600003D RID: 61 RVA: 0x00002D73 File Offset: 0x00000F73
	// (set) Token: 0x0600003E RID: 62 RVA: 0x00002D7B File Offset: 0x00000F7B
	public string UserToken { get; set; } = userToken;
}