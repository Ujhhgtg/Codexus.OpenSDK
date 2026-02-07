using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MPay.WhoAmi;

// Token: 0x02000084 RID: 132
public class EntityAimInfo
{
	// Token: 0x170001D2 RID: 466
	// (get) Token: 0x060004FE RID: 1278 RVA: 0x0000A26D File Offset: 0x0000846D
	// (set) Token: 0x060004FF RID: 1279 RVA: 0x0000A275 File Offset: 0x00008475
	[JsonPropertyName("code_1")]
	public Code1 Code1 { get; set; }

	// Token: 0x170001D3 RID: 467
	// (get) Token: 0x06000500 RID: 1280 RVA: 0x0000A27E File Offset: 0x0000847E
	// (set) Token: 0x06000501 RID: 1281 RVA: 0x0000A286 File Offset: 0x00008486
	[JsonPropertyName("code_2")]
	public Code2 Code2 { get; set; }

	// Token: 0x170001D4 RID: 468
	// (get) Token: 0x06000502 RID: 1282 RVA: 0x0000A28F File Offset: 0x0000848F
	// (set) Token: 0x06000503 RID: 1283 RVA: 0x0000A297 File Offset: 0x00008497
	[JsonPropertyName("code_3")]
	public Code3 Code3 { get; set; }

	// Token: 0x170001D5 RID: 469
	// (get) Token: 0x06000504 RID: 1284 RVA: 0x0000A2A0 File Offset: 0x000084A0
	// (set) Token: 0x06000505 RID: 1285 RVA: 0x0000A2A8 File Offset: 0x000084A8
	[JsonPropertyName("code_4")]
	public Code4 Code4 { get; set; }

	// Token: 0x170001D6 RID: 470
	// (get) Token: 0x06000506 RID: 1286 RVA: 0x0000A2B1 File Offset: 0x000084B1
	// (set) Token: 0x06000507 RID: 1287 RVA: 0x0000A2B9 File Offset: 0x000084B9
	[JsonPropertyName("isp")]
	public Isp Isp { get; set; }

	// Token: 0x170001D7 RID: 471
	// (get) Token: 0x06000508 RID: 1288 RVA: 0x0000A2C2 File Offset: 0x000084C2
	// (set) Token: 0x06000509 RID: 1289 RVA: 0x0000A2CA File Offset: 0x000084CA
	[JsonPropertyName("aim")]
	public string Aim { get; set; }

	// Token: 0x170001D8 RID: 472
	// (get) Token: 0x0600050A RID: 1290 RVA: 0x0000A2D3 File Offset: 0x000084D3
	// (set) Token: 0x0600050B RID: 1291 RVA: 0x0000A2DB File Offset: 0x000084DB
	[JsonPropertyName("country")]
	public string Country { get; set; } = "CN";

	// Token: 0x170001D9 RID: 473
	// (get) Token: 0x0600050C RID: 1292 RVA: 0x0000A2E4 File Offset: 0x000084E4
	// (set) Token: 0x0600050D RID: 1293 RVA: 0x0000A2EC File Offset: 0x000084EC
	[JsonPropertyName("tz")]
	public string Tz { get; set; } = "+0800";

	// Token: 0x170001DA RID: 474
	// (get) Token: 0x0600050E RID: 1294 RVA: 0x0000A2F5 File Offset: 0x000084F5
	// (set) Token: 0x0600050F RID: 1295 RVA: 0x0000A2FD File Offset: 0x000084FD
	[JsonPropertyName("tzid")]
	public string TzId { get; set; } = "";
}