using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Manager;

namespace Codexus.Development.SDK.Event;

// Token: 0x02000026 RID: 38
public class EventParseAddress : EventArgsBase
{
	// Token: 0x17000031 RID: 49
	// (get) Token: 0x060000DC RID: 220 RVA: 0x00005DC7 File Offset: 0x00003FC7
	// (set) Token: 0x060000DD RID: 221 RVA: 0x00005DCF File Offset: 0x00003FCF
	public string Address { get; set; }

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x060000DE RID: 222 RVA: 0x00005DD8 File Offset: 0x00003FD8
	// (set) Token: 0x060000DF RID: 223 RVA: 0x00005DE0 File Offset: 0x00003FE0
	public int Port { get; set; }

	// Token: 0x060000E0 RID: 224 RVA: 0x00005DE9 File Offset: 0x00003FE9
	public EventParseAddress(GameConnection connection, string address, int port)
		: base(connection)
	{
		Address = address;
		Port = port;
	}
}