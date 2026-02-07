using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

// Token: 0x0200005F RID: 95
// TODO: [RequiredMember]
public class EntityQuerySearchByGameResponse
{
	// Token: 0x1700013B RID: 315
	// (get) Token: 0x060003A7 RID: 935 RVA: 0x00009363 File Offset: 0x00007563
	// (set) Token: 0x060003A8 RID: 936 RVA: 0x0000936B File Offset: 0x0000756B
	// TODO: [RequiredMember]
	[JsonPropertyName("mc_version_id")]
	public int McVersionId { get; set; }

	// Token: 0x1700013C RID: 316
	// (get) Token: 0x060003A9 RID: 937 RVA: 0x00009374 File Offset: 0x00007574
	// (set) Token: 0x060003AA RID: 938 RVA: 0x0000937C File Offset: 0x0000757C
	// TODO: [RequiredMember]
	[JsonPropertyName("game_type")]
	public int GameType { get; set; }

	// Token: 0x1700013D RID: 317
	// (get) Token: 0x060003AB RID: 939 RVA: 0x00009385 File Offset: 0x00007585
	// (set) Token: 0x060003AC RID: 940 RVA: 0x0000938D File Offset: 0x0000758D
	// TODO: [RequiredMember]
	[JsonPropertyName("iid_list")]
	public List<ulong> IidList { get; set; }

	// Token: 0x060003AD RID: 941 RVA: 0x00009396 File Offset: 0x00007596
	public EntityQuerySearchByGameResponse()
	{
	}
}