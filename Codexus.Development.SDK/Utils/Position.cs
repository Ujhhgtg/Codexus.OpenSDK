namespace Codexus.Development.SDK.Utils;

// Token: 0x0200000F RID: 15
public class Position(int x, int y, int z)
{
	// Token: 0x1700001E RID: 30
	// (get) Token: 0x0600004B RID: 75 RVA: 0x000032F1 File Offset: 0x000014F1
	// (set) Token: 0x0600004C RID: 76 RVA: 0x000032F9 File Offset: 0x000014F9
	public int X { get; set; } = x;

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x0600004D RID: 77 RVA: 0x00003302 File Offset: 0x00001502
	// (set) Token: 0x0600004E RID: 78 RVA: 0x0000330A File Offset: 0x0000150A
	public int Y { get; set; } = y;

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x0600004F RID: 79 RVA: 0x00003313 File Offset: 0x00001513
	// (set) Token: 0x06000050 RID: 80 RVA: 0x0000331B File Offset: 0x0000151B
	public int Z { get; set; } = z;
}