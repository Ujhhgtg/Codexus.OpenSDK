using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.G79.RentalGame;

// Token: 0x02000094 RID: 148
// TODO: [RequiredMember]
public class EntityRentalGameServerAddress
{
	// Token: 0x1700020E RID: 526
	// (get) Token: 0x06000586 RID: 1414 RVA: 0x0000A791 File Offset: 0x00008991
	// (set) Token: 0x06000587 RID: 1415 RVA: 0x0000A799 File Offset: 0x00008999
	// TODO: [RequiredMember]
	[JsonPropertyName("mcserver_host")]
	public string Host { get; set; }

	// Token: 0x1700020F RID: 527
	// (get) Token: 0x06000588 RID: 1416 RVA: 0x0000A7A2 File Offset: 0x000089A2
	// (set) Token: 0x06000589 RID: 1417 RVA: 0x0000A7AA File Offset: 0x000089AA
	// TODO: [RequiredMember]
	[JsonPropertyName("mcserver_port")]
	public int Port { get; set; }

	// Token: 0x0600058A RID: 1418 RVA: 0x0000A7B3 File Offset: 0x000089B3
}