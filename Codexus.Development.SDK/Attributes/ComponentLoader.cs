using System;
using Codexus.Development.SDK.Enums;

namespace Codexus.Development.SDK.Attributes;

// Token: 0x02000032 RID: 50
[AttributeUsage(AttributeTargets.Class)]
public class ComponentLoader(EnumLoaderType type) : Attribute
{
	// Token: 0x1700004D RID: 77
	// (get) Token: 0x06000124 RID: 292 RVA: 0x000064CE File Offset: 0x000046CE
	public EnumLoaderType Type { get; } = type;
}