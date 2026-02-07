using System.Collections.Generic;
using System.Linq;
using Codexus.Cipher.Entities.WPFLauncher.NetGame;

namespace Codexus.Cipher.Utils;

// Token: 0x02000010 RID: 16
public class GameVersionConverter
{
	// Token: 0x06000064 RID: 100 RVA: 0x00002E18 File Offset: 0x00001018
	public static EnumGameVersion Convert(int versionId)
	{
		return VersionMap.GetValueOrDefault(versionId.ToString(), EnumGameVersion.NONE);
	}

	// Token: 0x06000065 RID: 101
	public static int Convert(EnumGameVersion version)
	{
		var entry = VersionMap.FirstOrDefault(pair => pair.Value == version);
		if (string.IsNullOrEmpty(entry.Key))
		{
			return 0;
		}
		int result;
		if (int.TryParse(entry.Key, out result))
		{
			return result;
		}
		return -1;
	}

	// Token: 0x0400002B RID: 43
	private static readonly Dictionary<string, EnumGameVersion> VersionMap = new()
	{
		{
			"",
			EnumGameVersion.NONE
		},
		{
			"1",
			EnumGameVersion.V_1_7_10
		},
		{
			"2",
			EnumGameVersion.V_1_8
		},
		{
			"3",
			EnumGameVersion.V_1_9_4
		},
		{
			"5",
			EnumGameVersion.V_1_11_2
		},
		{
			"6",
			EnumGameVersion.V_1_8_8
		},
		{
			"7",
			EnumGameVersion.V_1_10_2
		},
		{
			"8",
			EnumGameVersion.V_1_6_4
		},
		{
			"9",
			EnumGameVersion.V_1_7_2
		},
		{
			"10",
			EnumGameVersion.V_1_12_2
		},
		{
			"11",
			EnumGameVersion.NONE
		},
		{
			"12",
			EnumGameVersion.V_1_8_9
		},
		{
			"13",
			EnumGameVersion.V_CPP
		},
		{
			"14",
			EnumGameVersion.V_1_13_2
		},
		{
			"15",
			EnumGameVersion.V_1_14_3
		},
		{
			"16",
			EnumGameVersion.V_1_15
		},
		{
			"17",
			EnumGameVersion.V_1_16
		},
		{
			"18",
			EnumGameVersion.V_RTX
		},
		{
			"19",
			EnumGameVersion.V_1_18
		},
		{
			"23",
			EnumGameVersion.V_1_19_2
		},
		{
			"21",
			EnumGameVersion.V_1_20
		},
		{
			"22",
			EnumGameVersion.V_1_20_6
		},
		{
			"24",
			EnumGameVersion.V_1_21
		}
	};
}