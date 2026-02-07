using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MPay.WhoAmi;

// Token: 0x02000086 RID: 134
public class GeoLocationData
{
	// Token: 0x170001DF RID: 479
	// (get) Token: 0x0600051A RID: 1306 RVA: 0x0000A37D File Offset: 0x0000857D
	// (set) Token: 0x0600051B RID: 1307 RVA: 0x0000A385 File Offset: 0x00008585
	[JsonPropertyName("code_1")]
	public Code1 Code1 { get; set; }

	// Token: 0x170001E0 RID: 480
	// (get) Token: 0x0600051C RID: 1308 RVA: 0x0000A38E File Offset: 0x0000858E
	// (set) Token: 0x0600051D RID: 1309 RVA: 0x0000A396 File Offset: 0x00008596
	[JsonPropertyName("code_2")]
	public Code2 Code2 { get; set; }

	// Token: 0x170001E1 RID: 481
	// (get) Token: 0x0600051E RID: 1310 RVA: 0x0000A39F File Offset: 0x0000859F
	// (set) Token: 0x0600051F RID: 1311 RVA: 0x0000A3A7 File Offset: 0x000085A7
	[JsonPropertyName("code_3")]
	public Code3 Code3 { get; set; }

	// Token: 0x170001E2 RID: 482
	// (get) Token: 0x06000520 RID: 1312 RVA: 0x0000A3B0 File Offset: 0x000085B0
	// (set) Token: 0x06000521 RID: 1313 RVA: 0x0000A3B8 File Offset: 0x000085B8
	[JsonPropertyName("code_4")]
	public Code4 Code4 { get; set; }

	// Token: 0x170001E3 RID: 483
	// (get) Token: 0x06000522 RID: 1314 RVA: 0x0000A3C1 File Offset: 0x000085C1
	// (set) Token: 0x06000523 RID: 1315 RVA: 0x0000A3C9 File Offset: 0x000085C9
	[JsonPropertyName("isp")]
	public Isp Isp { get; set; }

	// Token: 0x170001E4 RID: 484
	// (get) Token: 0x06000524 RID: 1316 RVA: 0x0000A3D2 File Offset: 0x000085D2
	// (set) Token: 0x06000525 RID: 1317 RVA: 0x0000A3DA File Offset: 0x000085DA
	[JsonPropertyName("ip")]
	public string Ip { get; set; }
}