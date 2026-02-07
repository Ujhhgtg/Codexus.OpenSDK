using DotNetty.Buffers;

namespace Codexus.Development.SDK.Connection;

// Token: 0x02000031 RID: 49
public interface IConnection
{
	// Token: 0x0600011F RID: 287
	void Prepare();

	// Token: 0x06000120 RID: 288
	void OnServerReceived(IByteBuffer buffer);

	// Token: 0x06000121 RID: 289
	void OnClientReceived(IByteBuffer buffer);

	// Token: 0x06000122 RID: 290
	void Shutdown();
}