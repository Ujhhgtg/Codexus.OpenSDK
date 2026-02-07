using System.Xml.Serialization;

namespace Codexus.Cipher.Entities.WPFLauncher.NetGame;
public enum EnumGameVersion : uint
{
	[XmlEnum(Name = "")]
	NONE,
	[XmlEnum(Name = "100.0.0")]
	V_CPP = 100000000U,
	[XmlEnum(Name = "300.0.0")]
	V_X64_CPP = 300000000U,
	[XmlEnum(Name = "200.0.0")]
	V_RTX = 200000000U,
	[XmlEnum(Name = "1.7.10")]
	V_1_7_10 = 1007010U,
	[XmlEnum(Name = "1.10.2")]
	V_1_10_2 = 1010002U,
	[XmlEnum(Name = "1.8")]
	V_1_8 = 1008000U,
	[XmlEnum(Name = "1.11.2")]
	V_1_11_2 = 1011002U,
	[XmlEnum(Name = "1.12.2")]
	V_1_12_2 = 1012002U,
	[XmlEnum(Name = "1.8.8")]
	V_1_8_8 = 1008008U,
	[XmlEnum(Name = "1.8.9")]
	V_1_8_9,
	[XmlEnum(Name = "1.9.4")]
	V_1_9_4 = 1009004U,
	[XmlEnum(Name = "1.6.4")]
	V_1_6_4 = 1006004U,
	[XmlEnum(Name = "1.7.2")]
	V_1_7_2 = 1007002U,
	[XmlEnum(Name = "1.12")]
	V_1_12 = 1012000U,
	[XmlEnum(Name = "1.13.2")]
	V_1_13_2 = 1013002U,
	[XmlEnum(Name = "1.14.3")]
	V_1_14_3 = 1014003U,
	[XmlEnum(Name = "1.15")]
	V_1_15 = 1015000U,
	[XmlEnum(Name = "1.16")]
	V_1_16 = 1016000U,
	[XmlEnum(Name = "1.18")]
	V_1_18 = 1018000U,
	[XmlEnum(Name = "1.19.2")]
	V_1_19_2 = 1019002U,
	[XmlEnum(Name = "1.20")]
	V_1_20 = 1020000U,
	[XmlEnum(Name = "1.20.6")]
	V_1_20_6 = 1020006U,
	[XmlEnum(Name = "1.21")]
	V_1_21 = 1021000U
}