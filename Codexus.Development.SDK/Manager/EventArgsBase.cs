using Codexus.Development.SDK.Connection;

namespace Codexus.Development.SDK.Manager;

// Token: 0x02000018 RID: 24
public abstract class EventArgsBase(GameConnection connection) : IEventArgs
{
	// Token: 0x17000027 RID: 39
	// (get) Token: 0x06000086 RID: 134 RVA: 0x00003D9E File Offset: 0x00001F9E
	// (set) Token: 0x06000087 RID: 135 RVA: 0x00003DA6 File Offset: 0x00001FA6
	public GameConnection Connection { get; set; } = connection;

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x06000088 RID: 136 RVA: 0x00003DAF File Offset: 0x00001FAF
	// (set) Token: 0x06000089 RID: 137 RVA: 0x00003DB7 File Offset: 0x00001FB7
	public bool IsCancelled { get; set; }
}