using System;

namespace Codexus.Development.SDK.Attributes;

// Token: 0x02000033 RID: 51
[AttributeUsage(AttributeTargets.Class)]
public class Plugin(
	string id,
	string name,
	string description,
	string author,
	string version,
	string[] dependencies = null)
	: Attribute
{
	// Token: 0x1700004E RID: 78
	// (get) Token: 0x06000125 RID: 293 RVA: 0x000064D6 File Offset: 0x000046D6
	public string Id { get; } = id;

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x06000126 RID: 294 RVA: 0x000064DE File Offset: 0x000046DE
	public string Name { get; } = name;

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x06000127 RID: 295 RVA: 0x000064E6 File Offset: 0x000046E6
	public string Author { get; } = author;

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x06000128 RID: 296 RVA: 0x000064EE File Offset: 0x000046EE
	public string Description { get; } = description;

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x06000129 RID: 297 RVA: 0x000064F6 File Offset: 0x000046F6
	public string Version { get; } = version;

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x0600012A RID: 298 RVA: 0x000064FE File Offset: 0x000046FE
		
	public string[] Dependencies
	{
		get;
	} = dependencies;
}