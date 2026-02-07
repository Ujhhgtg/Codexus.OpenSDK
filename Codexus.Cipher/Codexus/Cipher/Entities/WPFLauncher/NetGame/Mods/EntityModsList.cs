using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame.Mods;

// Token: 0x0200006E RID: 110
public class EntityModsList
{
	// Token: 0x17000179 RID: 377
	// (get) Token: 0x0600042E RID: 1070 RVA: 0x00009852 File Offset: 0x00007A52
	// (set) Token: 0x0600042F RID: 1071 RVA: 0x0000985A File Offset: 0x00007A5A
	[JsonPropertyName("mods")]
	public List<EntityModsInfo> Mods { get; set; } = new();
}