using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Manager;

namespace Codexus.Development.SDK.Event;

// Token: 0x02000022 RID: 34
public class EventConnectionClosed : EventArgsBase
{
	// Token: 0x060000D3 RID: 211 RVA: 0x00005D65 File Offset: 0x00003F65
	public EventConnectionClosed(GameConnection connection)
		: base(connection)
	{
	}
}