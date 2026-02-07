using System.Text.Json.Serialization;
using Codexus.Cipher.Utils;

namespace Codexus.Cipher.Entities.WPFLauncher.RentalGame;

// Token: 0x02000049 RID: 73
public class EntityRentalGameServerAddress
{
	// Token: 0x170000DA RID: 218
	// (get) Token: 0x060002D4 RID: 724 RVA: 0x00008AA7 File Offset: 0x00006CA7
	// (set) Token: 0x060002D5 RID: 725 RVA: 0x00008AAF File Offset: 0x00006CAF
	[JsonPropertyName("entity_id")]
	[JsonConverter(typeof(JsonConventer.StringFromNumberOrStringConverter))]
	public string EntityId { get; set; } = string.Empty;

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x060002D6 RID: 726 RVA: 0x00008AB8 File Offset: 0x00006CB8
	// (set) Token: 0x060002D7 RID: 727 RVA: 0x00008AC0 File Offset: 0x00006CC0
	[JsonPropertyName("mcserver_host")]
	public string McServerHost { get; set; } = string.Empty;

	// Token: 0x170000DC RID: 220
	// (get) Token: 0x060002D8 RID: 728 RVA: 0x00008AC9 File Offset: 0x00006CC9
	// (set) Token: 0x060002D9 RID: 729 RVA: 0x00008AD1 File Offset: 0x00006CD1
	[JsonPropertyName("mcserver_port")]
	public ushort McServerPort { get; set; }

	// Token: 0x170000DD RID: 221
	// (get) Token: 0x060002DA RID: 730 RVA: 0x00008ADA File Offset: 0x00006CDA
	// (set) Token: 0x060002DB RID: 731 RVA: 0x00008AE2 File Offset: 0x00006CE2
	[JsonPropertyName("state")]
	public EnumServerStatus State { get; set; }

	// Token: 0x170000DE RID: 222
	// (get) Token: 0x060002DC RID: 732 RVA: 0x00008AEB File Offset: 0x00006CEB
	// (set) Token: 0x060002DD RID: 733 RVA: 0x00008AF3 File Offset: 0x00006CF3
	[JsonPropertyName("cmcc_mcserver_host")]
	public string CmccMcServerHost { get; set; } = string.Empty;

	// Token: 0x170000DF RID: 223
	// (get) Token: 0x060002DE RID: 734 RVA: 0x00008AFC File Offset: 0x00006CFC
	// (set) Token: 0x060002DF RID: 735 RVA: 0x00008B04 File Offset: 0x00006D04
	[JsonPropertyName("cmcc_mcserver_port")]
	public int CmccMcServerPort { get; set; }

	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x060002E0 RID: 736 RVA: 0x00008B0D File Offset: 0x00006D0D
	// (set) Token: 0x060002E1 RID: 737 RVA: 0x00008B15 File Offset: 0x00006D15
	[JsonPropertyName("ctcc_mcserver_host")]
	public string CtccMcServerHost { get; set; } = string.Empty;

	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x060002E2 RID: 738 RVA: 0x00008B1E File Offset: 0x00006D1E
	// (set) Token: 0x060002E3 RID: 739 RVA: 0x00008B26 File Offset: 0x00006D26
	[JsonPropertyName("ctcc_mcserver_port")]
	public int CtccMcServerPort { get; set; }

	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x060002E4 RID: 740 RVA: 0x00008B2F File Offset: 0x00006D2F
	// (set) Token: 0x060002E5 RID: 741 RVA: 0x00008B37 File Offset: 0x00006D37
	[JsonPropertyName("cucc_mcserver_host")]
	public string CuccMcServerHost { get; set; } = string.Empty;

	// Token: 0x170000E3 RID: 227
	// (get) Token: 0x060002E6 RID: 742 RVA: 0x00008B40 File Offset: 0x00006D40
	// (set) Token: 0x060002E7 RID: 743 RVA: 0x00008B48 File Offset: 0x00006D48
	[JsonPropertyName("cucc_mcserver_port")]
	public int CussMcServerPort { get; set; }

	// Token: 0x170000E4 RID: 228
	// (get) Token: 0x060002E8 RID: 744 RVA: 0x00008B51 File Offset: 0x00006D51
	// (set) Token: 0x060002E9 RID: 745 RVA: 0x00008B59 File Offset: 0x00006D59
	[JsonPropertyName("isp_enable")]
	public bool IspEnable { get; set; }
}