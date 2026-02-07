using System;
using System.Text.Json.Serialization;
using Codexus.Cipher.Entities.Converter;

namespace Codexus.Cipher.Entities;

// Token: 0x02000030 RID: 48
public class Entities<T> : EntityResponse
{
	// Token: 0x1700002E RID: 46
	// (get) Token: 0x06000164 RID: 356 RVA: 0x00007AD4 File Offset: 0x00005CD4
	// (set) Token: 0x06000165 RID: 357 RVA: 0x00007ADC File Offset: 0x00005CDC
	[JsonPropertyName("details")]
	public string Details { get; set; } = string.Empty;

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x06000166 RID: 358 RVA: 0x00007AE5 File Offset: 0x00005CE5
	// (set) Token: 0x06000167 RID: 359 RVA: 0x00007AED File Offset: 0x00005CED
	[JsonPropertyName("entities")]
	public T[] Data { get; set; } = Array.Empty<T>();

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x06000168 RID: 360 RVA: 0x00007AF6 File Offset: 0x00005CF6
	// (set) Token: 0x06000169 RID: 361 RVA: 0x00007AFE File Offset: 0x00005CFE
	[JsonPropertyName("total")]
	[JsonConverter(typeof(NetEaseStringConverter))]
	public int Total { get; set; }
}