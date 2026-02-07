using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Manager;

namespace Codexus.Development.SDK.Event;

// Token: 0x02000025 RID: 37
public class EventLoginSuccess : EventArgsBase
{
	// Token: 0x060000DB RID: 219 RVA: 0x00005DBC File Offset: 0x00003FBC
	public EventLoginSuccess(GameConnection connection)
		: base(connection)
	{
	}
}