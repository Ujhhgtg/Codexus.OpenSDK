using System.Runtime.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.RentalGame;

// Token: 0x0200004C RID: 76
public enum EnumServerType
{
	// Token: 0x0400014F RID: 335
	[EnumMember(Value = "docker")]
	Docker,
	// Token: 0x04000150 RID: 336
	[EnumMember(Value = "vmware")]
	Vmware
}