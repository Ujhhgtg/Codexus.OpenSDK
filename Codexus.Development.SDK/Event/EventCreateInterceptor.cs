using Codexus.Development.SDK.Manager;

namespace Codexus.Development.SDK.Event;

// Token: 0x02000023 RID: 35
public class EventCreateInterceptor(int port) : IEventArgs
{
	// Token: 0x1700002E RID: 46
	// (get) Token: 0x060000D5 RID: 213 RVA: 0x00005D80 File Offset: 0x00003F80
	// (set) Token: 0x060000D6 RID: 214 RVA: 0x00005D88 File Offset: 0x00003F88
	public int Port { get; set; } = port;

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005D91 File Offset: 0x00003F91
	// (set) Token: 0x060000D8 RID: 216 RVA: 0x00005D99 File Offset: 0x00003F99
	public bool IsCancelled { get; set; }
}