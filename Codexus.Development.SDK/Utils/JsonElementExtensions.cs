using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Codexus.Development.SDK.Utils;
public static class JsonElementExtensions
{

	extension(JsonElement element)
	{
		public object? GetObject()
		{
			var valueKind = element.ValueKind;
			var obj = valueKind switch
			{
				JsonValueKind.Object => element.DeserializeToDictionary(),
				JsonValueKind.Array => element.DeserializeToList(),
				JsonValueKind.String => element.GetString(),
				JsonValueKind.Number => element.GetNumber(),
				JsonValueKind.True => true,
				JsonValueKind.False => false,
				JsonValueKind.Null => null,
				_ => element
			};
			return obj;
		}

		private object GetNumber()
		{
			var flag = element.TryGetInt32(out var num);
			object obj;
			if (flag)
			{
				obj = num;
			}
			else
			{
				var flag2 = element.TryGetInt64(out var num2);
				if (flag2)
				{
					obj = num2;
				}
				else
				{
					var flag3 = element.TryGetDouble(out var num3);
					if (flag3)
					{
						obj = num3;
					}
					else
					{
						var flag4 = element.TryGetDecimal(out var num4);
						if (flag4)
						{
							obj = num4;
						}
						else
						{
							obj = element.ToString();
						}
					}
				}
			}
			return obj;
		}

		private List<object?> DeserializeToList()
		{
			return element.EnumerateArray().Select(jsonElement => jsonElement.GetObject()).ToList();
		}

		private Dictionary<string, object> DeserializeToDictionary()
		{
			var dictionary = new Dictionary<string, object>();
			foreach (var jsonProperty in element.EnumerateObject())
			{
				dictionary[jsonProperty.Name] = jsonProperty.Value.GetObject();
			}
			return dictionary;
		}
	}
}