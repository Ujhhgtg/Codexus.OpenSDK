using System;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MPay;

// Token: 0x0200007C RID: 124
public class EntitySmsTicket
{
	// Token: 0x170001BC RID: 444
	// (get) Token: 0x060004CA RID: 1226 RVA: 0x0000A04C File Offset: 0x0000824C
	// (set) Token: 0x060004CB RID: 1227 RVA: 0x0000A054 File Offset: 0x00008254
	[JsonPropertyName("guide_text")]
	public string GuideText { get; set; } = string.Empty;

	// Token: 0x170001BD RID: 445
	// (get) Token: 0x060004CC RID: 1228 RVA: 0x0000A05D File Offset: 0x0000825D
	// (set) Token: 0x060004CD RID: 1229 RVA: 0x0000A065 File Offset: 0x00008265
	[JsonPropertyName("related_emails")]
	public string[] RelatedEmails { get; set; } = Array.Empty<string>();

	// Token: 0x170001BE RID: 446
	// (get) Token: 0x060004CE RID: 1230 RVA: 0x0000A06E File Offset: 0x0000826E
	// (set) Token: 0x060004CF RID: 1231 RVA: 0x0000A076 File Offset: 0x00008276
	[JsonPropertyName("ticket")]
	public string Ticket { get; set; } = string.Empty;

	// Token: 0x170001BF RID: 447
	// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0000A07F File Offset: 0x0000827F
	// (set) Token: 0x060004D1 RID: 1233 RVA: 0x0000A087 File Offset: 0x00008287
	[JsonPropertyName("related_accounts")]
	public string[] RelatedAccounts { get; set; } = Array.Empty<string>();
}