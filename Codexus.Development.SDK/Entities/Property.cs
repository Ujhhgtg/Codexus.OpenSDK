namespace Codexus.Development.SDK.Entities;

// Token: 0x0200002F RID: 47
public class Property
{
	// Token: 0x1700003E RID: 62
	// (get) Token: 0x060000FB RID: 251 RVA: 0x00005F0F File Offset: 0x0000410F
	// (set) Token: 0x060000FC RID: 252 RVA: 0x00005F17 File Offset: 0x00004117
	public string Name { get; set; }

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x060000FD RID: 253 RVA: 0x00005F20 File Offset: 0x00004120
	// (set) Token: 0x060000FE RID: 254 RVA: 0x00005F28 File Offset: 0x00004128
	public string Value { get; set; }

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x060000FF RID: 255 RVA: 0x00005F31 File Offset: 0x00004131
	// (set) Token: 0x06000100 RID: 256 RVA: 0x00005F39 File Offset: 0x00004139
	public string? Signature
	{
		get;
		set;
	}
}