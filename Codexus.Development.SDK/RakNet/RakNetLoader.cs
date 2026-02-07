using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Codexus.Development.SDK.Attributes;
using Codexus.Development.SDK.Enums;

namespace Codexus.Development.SDK.RakNet;

// Token: 0x02000014 RID: 20
public static class RakNetLoader
{
	// Token: 0x06000072 RID: 114 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public static void FindLoader()
	{
		var types = Assembly.LoadFrom("Codexus.RakNet.ug").GetTypes();
		foreach (var type in types)
		{
			var customAttribute = type.GetCustomAttribute<ComponentLoader>(false);
			var flag = customAttribute is { Type: EnumLoaderType.RakNet } && typeof(IRakNetCreate).IsAssignableFrom(type);
			if (flag)
			{
				_loader = type;
				break;
			}
		}
		var flag2 = _loader == null;
		if (flag2)
		{
			throw new Exception("Could not initialize RakNet");
		}
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00003C74 File Offset: 0x00001E74
	public static IRakNetCreate ConstructLoader()
	{
		if (_loader == null)
		{
			throw new Exception("You must call FindLoader() before ConstructLoader()");
		}
		return (IRakNetCreate)Activator.CreateInstance(_loader);
	}

	// Token: 0x04000037 RID: 55
	private static Type? _loader;
}