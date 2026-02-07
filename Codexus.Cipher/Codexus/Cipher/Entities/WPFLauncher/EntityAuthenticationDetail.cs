using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Codexus.Cipher.Utils;

namespace Codexus.Cipher.Entities.WPFLauncher;

// Token: 0x02000035 RID: 53
// TODO: [RequiredMember]
public class EntityAuthenticationDetail
{
	// Token: 0x17000047 RID: 71
	// (get) Token: 0x0600019A RID: 410 RVA: 0x00007CD4 File Offset: 0x00005ED4
	// (set) Token: 0x0600019B RID: 411 RVA: 0x00007CDC File Offset: 0x00005EDC
	[JsonPropertyName("os_name")]
	public string OsName { get; set; } = "windows";

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x0600019C RID: 412 RVA: 0x00007CE5 File Offset: 0x00005EE5
	// (set) Token: 0x0600019D RID: 413 RVA: 0x00007CED File Offset: 0x00005EED
	[JsonPropertyName("os_ver")]
	public string OsVer { get; set; } = "Microsoft Windows 11 专业版";

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x0600019E RID: 414 RVA: 0x00007CF6 File Offset: 0x00005EF6
	// (set) Token: 0x0600019F RID: 415 RVA: 0x00007CFE File Offset: 0x00005EFE
	[JsonPropertyName("mac_addr")]
	public string MacAddr { get; set; } = StringGenerator.GenerateRandomMacAddress("");

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x060001A0 RID: 416 RVA: 0x00007D07 File Offset: 0x00005F07
	// (set) Token: 0x060001A1 RID: 417 RVA: 0x00007D0F File Offset: 0x00005F0F
	// TODO: [RequiredMember]
	[JsonPropertyName("udid")]
	public string Udid { get; set; }

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x060001A2 RID: 418 RVA: 0x00007D18 File Offset: 0x00005F18
	// (set) Token: 0x060001A3 RID: 419 RVA: 0x00007D20 File Offset: 0x00005F20
	// TODO: [RequiredMember]
	[JsonPropertyName("app_ver")]
	public string AppVersion { get; set; }

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x060001A4 RID: 420 RVA: 0x00007D29 File Offset: 0x00005F29
	// (set) Token: 0x060001A5 RID: 421 RVA: 0x00007D31 File Offset: 0x00005F31
	[JsonPropertyName("sdk_ver")]
	public string SdkVersion { get; set; } = "";

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x060001A6 RID: 422 RVA: 0x00007D3A File Offset: 0x00005F3A
	// (set) Token: 0x060001A7 RID: 423 RVA: 0x00007D42 File Offset: 0x00005F42
	[JsonPropertyName("network")]
	public string Network { get; set; } = "";

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x060001A8 RID: 424 RVA: 0x00007D4B File Offset: 0x00005F4B
	// (set) Token: 0x060001A9 RID: 425 RVA: 0x00007D53 File Offset: 0x00005F53
	// TODO: [RequiredMember]
	[JsonPropertyName("disk")]
	public string Disk { get; set; }

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x060001AA RID: 426 RVA: 0x00007D5C File Offset: 0x00005F5C
	// (set) Token: 0x060001AB RID: 427 RVA: 0x00007D64 File Offset: 0x00005F64
	[JsonPropertyName("is64bit")]
	public string Is64Bit { get; set; } = "1";

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x060001AC RID: 428 RVA: 0x00007D6D File Offset: 0x00005F6D
	// (set) Token: 0x060001AD RID: 429 RVA: 0x00007D75 File Offset: 0x00005F75
	[JsonPropertyName("video_card1")]
	public string VideoCard1 { get; set; } = "Microsoft Hyper-V 视频";

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x060001AE RID: 430 RVA: 0x00007D7E File Offset: 0x00005F7E
	// (set) Token: 0x060001AF RID: 431 RVA: 0x00007D86 File Offset: 0x00005F86
	[JsonPropertyName("video_card2")]
	public string VideoCard2 { get; set; } = "Microsoft Remote Display Adapter";

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x060001B0 RID: 432 RVA: 0x00007D8F File Offset: 0x00005F8F
	// (set) Token: 0x060001B1 RID: 433 RVA: 0x00007D97 File Offset: 0x00005F97
	[JsonPropertyName("video_card3")]
	public string VideoCard3 { get; set; } = "";

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x060001B2 RID: 434 RVA: 0x00007DA0 File Offset: 0x00005FA0
	// (set) Token: 0x060001B3 RID: 435 RVA: 0x00007DA8 File Offset: 0x00005FA8
	[JsonPropertyName("video_card4")]
	public string VideoCard4 { get; set; } = "";

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x060001B4 RID: 436 RVA: 0x00007DB1 File Offset: 0x00005FB1
	// (set) Token: 0x060001B5 RID: 437 RVA: 0x00007DB9 File Offset: 0x00005FB9
	[JsonPropertyName("launcher_type")]
	public string LauncherType { get; set; } = "PC_java";

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x060001B6 RID: 438 RVA: 0x00007DC2 File Offset: 0x00005FC2
	// (set) Token: 0x060001B7 RID: 439 RVA: 0x00007DCA File Offset: 0x00005FCA
	// TODO: [RequiredMember]
	[JsonPropertyName("pay_channel")]
	public string PayChannel { get; set; }

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x060001B8 RID: 440 RVA: 0x00007DD3 File Offset: 0x00005FD3
	// (set) Token: 0x060001B9 RID: 441 RVA: 0x00007DDB File Offset: 0x00005FDB
	[JsonPropertyName("dotnet_ver")]
	public string DotnetVersion { get; set; } = "4.8.0";

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x060001BA RID: 442 RVA: 0x00007DE4 File Offset: 0x00005FE4
	// (set) Token: 0x060001BB RID: 443 RVA: 0x00007DEC File Offset: 0x00005FEC
	[JsonPropertyName("cpu_type")]
	public string CpuType { get; set; } = "Intel(R) Core(TM) i9-14900KF";

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x060001BC RID: 444 RVA: 0x00007DF5 File Offset: 0x00005FF5
	// (set) Token: 0x060001BD RID: 445 RVA: 0x00007DFD File Offset: 0x00005FFD
	[JsonPropertyName("ram_size")]
	public string RamSize { get; set; } = "8589934592";

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x060001BE RID: 446 RVA: 0x00007E06 File Offset: 0x00006006
	// (set) Token: 0x060001BF RID: 447 RVA: 0x00007E0E File Offset: 0x0000600E
	[JsonPropertyName("device_width")]
	public string DeviceWidth { get; set; } = "1920";

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x060001C0 RID: 448 RVA: 0x00007E17 File Offset: 0x00006017
	// (set) Token: 0x060001C1 RID: 449 RVA: 0x00007E1F File Offset: 0x0000601F
	[JsonPropertyName("device_height")]
	public string DeviceHeight { get; set; } = "1080";

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x060001C2 RID: 450 RVA: 0x00007E28 File Offset: 0x00006028
	// (set) Token: 0x060001C3 RID: 451 RVA: 0x00007E30 File Offset: 0x00006030
	[JsonPropertyName("os_detail")]
	public string OsDetail { get; set; } = "10.0.26100";

	// Token: 0x060001C4 RID: 452 RVA: 0x00007E3C File Offset: 0x0000603C
	public EntityAuthenticationDetail()
	{
	}
}