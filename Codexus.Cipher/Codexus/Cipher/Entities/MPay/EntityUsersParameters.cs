using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.MPay;

// Token: 0x0200007D RID: 125
public class EntityUsersParameters
{
	// Token: 0x170001C0 RID: 448
	// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0000A0C5 File Offset: 0x000082C5
	// (set) Token: 0x060004D4 RID: 1236 RVA: 0x0000A0CD File Offset: 0x000082CD
	[JsonPropertyName("password")]
	public string Password { get; set; } = string.Empty;

	// Token: 0x170001C1 RID: 449
	// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0000A0D6 File Offset: 0x000082D6
	// (set) Token: 0x060004D6 RID: 1238 RVA: 0x0000A0DE File Offset: 0x000082DE
	[JsonPropertyName("unique_id")]
	public string Unique { get; set; } = string.Empty;

	// Token: 0x170001C2 RID: 450
	// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0000A0E7 File Offset: 0x000082E7
	// (set) Token: 0x060004D8 RID: 1240 RVA: 0x0000A0EF File Offset: 0x000082EF
	[JsonPropertyName("username")]
	public string Username { get; set; } = string.Empty;
}