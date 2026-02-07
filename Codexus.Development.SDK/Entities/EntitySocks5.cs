using System.Text.Json.Serialization;

namespace Codexus.Development.SDK.Entities;

// Token: 0x0200002D RID: 45
public class EntitySocks5
{
	// Token: 0x17000037 RID: 55
	// (get) Token: 0x060000EB RID: 235 RVA: 0x00005E5A File Offset: 0x0000405A
	// (set) Token: 0x060000EC RID: 236 RVA: 0x00005E62 File Offset: 0x00004062
	[JsonPropertyName("enabled")]
	public bool Enabled { get; set; }

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x060000ED RID: 237 RVA: 0x00005E6B File Offset: 0x0000406B
	// (set) Token: 0x060000EE RID: 238 RVA: 0x00005E73 File Offset: 0x00004073
	[JsonPropertyName("address")]
	public string Address
	{
		get;
		set;
	} = "127.0.0.1";

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x060000EF RID: 239 RVA: 0x00005E7C File Offset: 0x0000407C
	// (set) Token: 0x060000F0 RID: 240 RVA: 0x00005E84 File Offset: 0x00004084
	[JsonPropertyName("port")]
	public int Port { get; set; } = 1080;

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x060000F1 RID: 241 RVA: 0x00005E8D File Offset: 0x0000408D
	// (set) Token: 0x060000F2 RID: 242 RVA: 0x00005E95 File Offset: 0x00004095
	[JsonPropertyName("username")]
	public string Username { get; set; }

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x060000F3 RID: 243 RVA: 0x00005E9E File Offset: 0x0000409E
	// (set) Token: 0x060000F4 RID: 244 RVA: 0x00005EA6 File Offset: 0x000040A6
	[JsonPropertyName("password")]
	public string Password { get; set; }
}