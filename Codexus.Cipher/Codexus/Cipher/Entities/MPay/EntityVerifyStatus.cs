using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MPay;

// Token: 0x0200007F RID: 127
public class EntityVerifyStatus
{
	// Token: 0x170001C6 RID: 454
	// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0000A174 File Offset: 0x00008374
	// (set) Token: 0x060004E2 RID: 1250 RVA: 0x0000A17C File Offset: 0x0000837C
	[JsonPropertyName("need_sms")]
	public int NeedSms { get; set; }

	// Token: 0x170001C7 RID: 455
	// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0000A185 File Offset: 0x00008385
	// (set) Token: 0x060004E4 RID: 1252 RVA: 0x0000A18D File Offset: 0x0000838D
	[JsonPropertyName("need_email")]
	public int NeedEmail { get; set; }

	// Token: 0x170001C8 RID: 456
	// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0000A196 File Offset: 0x00008396
	// (set) Token: 0x060004E6 RID: 1254 RVA: 0x0000A19E File Offset: 0x0000839E
	[JsonPropertyName("need_passwd")]
	public int NeedPasswd { get; set; }

	// Token: 0x170001C9 RID: 457
	// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0000A1A7 File Offset: 0x000083A7
	// (set) Token: 0x060004E8 RID: 1256 RVA: 0x0000A1AF File Offset: 0x000083AF
	[JsonPropertyName("need_real_name")]
	public int NeedRealName { get; set; }
}