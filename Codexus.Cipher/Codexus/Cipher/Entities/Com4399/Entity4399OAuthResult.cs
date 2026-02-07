using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Com4399;

// Token: 0x020000A6 RID: 166
public class Entity4399OAuthResult
{
	// Token: 0x17000256 RID: 598
	// (get) Token: 0x0600062C RID: 1580 RVA: 0x0000AE81 File Offset: 0x00009081
	// (set) Token: 0x0600062D RID: 1581 RVA: 0x0000AE89 File Offset: 0x00009089
	[JsonPropertyName("login_url")]
	public string LoginUrl { get; set; } = string.Empty;

	// Token: 0x17000257 RID: 599
	// (get) Token: 0x0600062E RID: 1582 RVA: 0x0000AE92 File Offset: 0x00009092
	// (set) Token: 0x0600062F RID: 1583 RVA: 0x0000AE9A File Offset: 0x0000909A
	[JsonPropertyName("login_url_backup")]
	public string LoginUrlBackup { get; set; } = string.Empty;

	// Token: 0x17000258 RID: 600
	// (get) Token: 0x06000630 RID: 1584 RVA: 0x0000AEA3 File Offset: 0x000090A3
	// (set) Token: 0x06000631 RID: 1585 RVA: 0x0000AEAB File Offset: 0x000090AB
	[JsonPropertyName("login_url_phone")]
	public string LoginUrlPhone { get; set; } = string.Empty;

	// Token: 0x17000259 RID: 601
	// (get) Token: 0x06000632 RID: 1586 RVA: 0x0000AEB4 File Offset: 0x000090B4
	// (set) Token: 0x06000633 RID: 1587 RVA: 0x0000AEBC File Offset: 0x000090BC
	[JsonPropertyName("login_url_backup_phone")]
	public string LoginUrlBackupPhone { get; set; } = string.Empty;
}