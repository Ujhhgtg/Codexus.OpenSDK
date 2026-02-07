using System;

namespace Codexus.Development.SDK.RakNet;

// Token: 0x02000012 RID: 18
public interface IRakNet
{
	// Token: 0x0600006B RID: 107
	Guid GetId();

	// Token: 0x0600006C RID: 108
	string GetRemoteAddress();

	// Token: 0x0600006D RID: 109
	string GetLocalAddress();

	// Token: 0x0600006E RID: 110
	string GetRoleName();

	// Token: 0x0600006F RID: 111
	string GetGameName();

	// Token: 0x06000070 RID: 112
	void Shutdown();
}