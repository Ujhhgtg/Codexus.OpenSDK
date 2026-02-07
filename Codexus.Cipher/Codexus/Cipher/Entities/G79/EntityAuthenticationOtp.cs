using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79;

// Token: 0x0200008C RID: 140
// TODO: [RequiredMember]
public class EntityAuthenticationOtp
{
	// Token: 0x17000200 RID: 512
	// (get) Token: 0x06000562 RID: 1378 RVA: 0x0000A65B File Offset: 0x0000885B
	// (set) Token: 0x06000563 RID: 1379 RVA: 0x0000A663 File Offset: 0x00008863
	// TODO: [RequiredMember]
	[JsonPropertyName("entity_id")]
	public string EntityId { get; set; }

	// Token: 0x17000201 RID: 513
	// (get) Token: 0x06000564 RID: 1380 RVA: 0x0000A66C File Offset: 0x0000886C
	// (set) Token: 0x06000565 RID: 1381 RVA: 0x0000A674 File Offset: 0x00008874
	// TODO: [RequiredMember]
	[JsonPropertyName("token")]
	public string Token { get; set; }

	// Token: 0x06000566 RID: 1382 RVA: 0x0000A67D File Offset: 0x0000887D
}