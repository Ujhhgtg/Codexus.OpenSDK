using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Com4399;

// Token: 0x020000A3 RID: 163
// TODO: [RequiredMember]
public class Entity4399Device
{
	// Token: 0x1700023B RID: 571
	// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0000ABC0 File Offset: 0x00008DC0
	// (set) Token: 0x060005F4 RID: 1524 RVA: 0x0000ABC8 File Offset: 0x00008DC8
	// TODO: [RequiredMember]
	[JsonPropertyName("device-id")]
	public required string DeviceIdentifier { get; set; }

	// Token: 0x1700023C RID: 572
	// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0000ABD1 File Offset: 0x00008DD1
	// (set) Token: 0x060005F6 RID: 1526 RVA: 0x0000ABD9 File Offset: 0x00008DD9
	// TODO: [RequiredMember]
	[JsonPropertyName("device-id-sm")]
	public required string DeviceIdentifierSm { get; set; }

	// Token: 0x1700023D RID: 573
	// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0000ABE2 File Offset: 0x00008DE2
	// (set) Token: 0x060005F8 RID: 1528 RVA: 0x0000ABEA File Offset: 0x00008DEA
	// TODO: [RequiredMember]
	[JsonPropertyName("device-udid")]
	public required string DeviceUdid { get; set; }

	// Token: 0x1700023E RID: 574
	// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0000ABF3 File Offset: 0x00008DF3
	// (set) Token: 0x060005FA RID: 1530 RVA: 0x0000ABFB File Offset: 0x00008DFB
		
	[JsonPropertyName("device-state")]
	public string DeviceState
	{
			
		get;
			
		set;
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x0000AC04 File Offset: 0x00008E04
}