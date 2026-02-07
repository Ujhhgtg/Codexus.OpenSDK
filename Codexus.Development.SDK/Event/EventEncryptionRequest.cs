using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Manager;

namespace Codexus.Development.SDK.Event;

// Token: 0x02000024 RID: 36
public class EventEncryptionRequest : EventArgsBase
{
	// Token: 0x17000030 RID: 48
	// (get) Token: 0x060000D9 RID: 217 RVA: 0x00005DA2 File Offset: 0x00003FA2
	public string ServerId { get; }

	// Token: 0x060000DA RID: 218 RVA: 0x00005DAA File Offset: 0x00003FAA
	public EventEncryptionRequest(GameConnection connection, string serverId)
		: base(connection)
	{
		ServerId = serverId;
	}
}