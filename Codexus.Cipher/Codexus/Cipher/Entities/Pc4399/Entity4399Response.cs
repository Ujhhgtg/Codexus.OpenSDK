using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Pc4399;

// Token: 0x02000075 RID: 117
public class Entity4399Response
{
	// Token: 0x17000195 RID: 405
	// (get) Token: 0x06000475 RID: 1141 RVA: 0x00009C2A File Offset: 0x00007E2A
	// (set) Token: 0x06000476 RID: 1142 RVA: 0x00009C32 File Offset: 0x00007E32
	[JsonPropertyName("code")]
	public int Code { get; set; }

	// Token: 0x17000196 RID: 406
	// (get) Token: 0x06000477 RID: 1143 RVA: 0x00009C3B File Offset: 0x00007E3B
	// (set) Token: 0x06000478 RID: 1144 RVA: 0x00009C43 File Offset: 0x00007E43
	[JsonPropertyName("msg")]
	public string Msg { get; set; } = string.Empty;

	// Token: 0x17000197 RID: 407
	// (get) Token: 0x06000479 RID: 1145 RVA: 0x00009C4C File Offset: 0x00007E4C
	// (set) Token: 0x0600047A RID: 1146 RVA: 0x00009C54 File Offset: 0x00007E54
	[JsonPropertyName("data")]
	public Entity4399ResponseData Data { get; set; } = new();
}