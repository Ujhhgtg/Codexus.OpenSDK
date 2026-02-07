using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.InterConn;

// Token: 0x0200008A RID: 138
// TODO: [RequiredMember]
public class InterConnGameStart
{
	// Token: 0x170001F9 RID: 505
	// (get) Token: 0x06000552 RID: 1362 RVA: 0x0000A5B5 File Offset: 0x000087B5
	// (set) Token: 0x06000553 RID: 1363 RVA: 0x0000A5BD File Offset: 0x000087BD
	// TODO: [RequiredMember]
	[JsonPropertyName("game_id")]
	public string GameId { get; set; }

	// Token: 0x170001FA RID: 506
	// (get) Token: 0x06000554 RID: 1364 RVA: 0x0000A5C6 File Offset: 0x000087C6
	// (set) Token: 0x06000555 RID: 1365 RVA: 0x0000A5CE File Offset: 0x000087CE
	[JsonPropertyName("game_type")]
	public string GameType { get; set; } = "2";

	// Token: 0x170001FB RID: 507
	// (get) Token: 0x06000556 RID: 1366 RVA: 0x0000A5D7 File Offset: 0x000087D7
	// (set) Token: 0x06000557 RID: 1367 RVA: 0x0000A5DF File Offset: 0x000087DF
	[JsonPropertyName("strict_mode")]
	public bool StrictMode { get; set; } = true;

	// Token: 0x170001FC RID: 508
	// (get) Token: 0x06000558 RID: 1368 RVA: 0x0000A5E8 File Offset: 0x000087E8
	// (set) Token: 0x06000559 RID: 1369 RVA: 0x0000A5F0 File Offset: 0x000087F0
	// TODO: [RequiredMember]
	[JsonPropertyName("item_list")]
	public string[] ItemList { get; set; }

	// Token: 0x0600055A RID: 1370 RVA: 0x0000A5F9 File Offset: 0x000087F9
	public InterConnGameStart()
	{
	}
}