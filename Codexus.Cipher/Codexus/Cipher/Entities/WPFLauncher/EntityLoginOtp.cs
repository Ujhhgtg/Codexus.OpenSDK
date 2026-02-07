using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher;

// Token: 0x02000039 RID: 57
public class EntityLoginOtp
{
	// Token: 0x17000081 RID: 129
	// (get) Token: 0x06000212 RID: 530 RVA: 0x00008245 File Offset: 0x00006445
	// (set) Token: 0x06000213 RID: 531 RVA: 0x0000824D File Offset: 0x0000644D
	[JsonPropertyName("otp")]
	private int Otp { get; set; }

	// Token: 0x17000082 RID: 130
	// (get) Token: 0x06000214 RID: 532 RVA: 0x00008256 File Offset: 0x00006456
	// (set) Token: 0x06000215 RID: 533 RVA: 0x0000825E File Offset: 0x0000645E
	[JsonPropertyName("otp_token")]
	public string OtpToken { get; set; } = string.Empty;

	// Token: 0x17000083 RID: 131
	// (get) Token: 0x06000216 RID: 534 RVA: 0x00008267 File Offset: 0x00006467
	// (set) Token: 0x06000217 RID: 535 RVA: 0x0000826F File Offset: 0x0000646F
	[JsonPropertyName("aid")]
	public int Aid { get; set; }

	// Token: 0x17000084 RID: 132
	// (get) Token: 0x06000218 RID: 536 RVA: 0x00008278 File Offset: 0x00006478
	// (set) Token: 0x06000219 RID: 537 RVA: 0x00008280 File Offset: 0x00006480
	[JsonPropertyName("lock_time")]
	public int LockTime { get; set; }

	// Token: 0x17000085 RID: 133
	// (get) Token: 0x0600021A RID: 538 RVA: 0x00008289 File Offset: 0x00006489
	// (set) Token: 0x0600021B RID: 539 RVA: 0x00008291 File Offset: 0x00006491
	[JsonPropertyName("open_otp")]
	public int OpenOtp { get; set; }
}