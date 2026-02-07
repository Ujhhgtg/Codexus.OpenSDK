using System.Xml.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;

// Token: 0x02000061 RID: 97
public enum EnumGameVersion : uint
{
	// Token: 0x040001B6 RID: 438
	[XmlEnum(Name = "")]
	NONE,
	// Token: 0x040001B7 RID: 439
	[XmlEnum(Name = "100.0.0")]
	V_CPP = 100000000U,
	// Token: 0x040001B8 RID: 440
	[XmlEnum(Name = "300.0.0")]
	V_X64_CPP = 300000000U,
	// Token: 0x040001B9 RID: 441
	[XmlEnum(Name = "200.0.0")]
	V_RTX = 200000000U,
	// Token: 0x040001BA RID: 442
	[XmlEnum(Name = "1.7.10")]
	V_1_7_10 = 1007010U,
	// Token: 0x040001BB RID: 443
	[XmlEnum(Name = "1.10.2")]
	V_1_10_2 = 1010002U,
	// Token: 0x040001BC RID: 444
	[XmlEnum(Name = "1.8")]
	V_1_8 = 1008000U,
	// Token: 0x040001BD RID: 445
	[XmlEnum(Name = "1.11.2")]
	V_1_11_2 = 1011002U,
	// Token: 0x040001BE RID: 446
	[XmlEnum(Name = "1.12.2")]
	V_1_12_2 = 1012002U,
	// Token: 0x040001BF RID: 447
	[XmlEnum(Name = "1.8.8")]
	V_1_8_8 = 1008008U,
	// Token: 0x040001C0 RID: 448
	[XmlEnum(Name = "1.8.9")]
	V_1_8_9,
	// Token: 0x040001C1 RID: 449
	[XmlEnum(Name = "1.9.4")]
	V_1_9_4 = 1009004U,
	// Token: 0x040001C2 RID: 450
	[XmlEnum(Name = "1.6.4")]
	V_1_6_4 = 1006004U,
	// Token: 0x040001C3 RID: 451
	[XmlEnum(Name = "1.7.2")]
	V_1_7_2 = 1007002U,
	// Token: 0x040001C4 RID: 452
	[XmlEnum(Name = "1.12")]
	V_1_12 = 1012000U,
	// Token: 0x040001C5 RID: 453
	[XmlEnum(Name = "1.13.2")]
	V_1_13_2 = 1013002U,
	// Token: 0x040001C6 RID: 454
	[XmlEnum(Name = "1.14.3")]
	V_1_14_3 = 1014003U,
	// Token: 0x040001C7 RID: 455
	[XmlEnum(Name = "1.15")]
	V_1_15 = 1015000U,
	// Token: 0x040001C8 RID: 456
	[XmlEnum(Name = "1.16")]
	V_1_16 = 1016000U,
	// Token: 0x040001C9 RID: 457
	[XmlEnum(Name = "1.18")]
	V_1_18 = 1018000U,
	// Token: 0x040001CA RID: 458
	[XmlEnum(Name = "1.19.2")]
	V_1_19_2 = 1019002U,
	// Token: 0x040001CB RID: 459
	[XmlEnum(Name = "1.20")]
	V_1_20 = 1020000U,
	// Token: 0x040001CC RID: 460
	[XmlEnum(Name = "1.20.6")]
	V_1_20_6 = 1020006U,
	// Token: 0x040001CD RID: 461
	[XmlEnum(Name = "1.21")]
	V_1_21 = 1021000U
}