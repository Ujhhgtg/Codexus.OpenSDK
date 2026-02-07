using System;
using System.Collections.Generic;
using System.Linq;

namespace Codexus.Cipher.Utils.Http;

// Token: 0x02000016 RID: 22
public class ParameterBuilder
{
	// Token: 0x17000020 RID: 32
	// (get) Token: 0x06000077 RID: 119 RVA: 0x0000351A File Offset: 0x0000171A
	// (set) Token: 0x06000078 RID: 120 RVA: 0x00003522 File Offset: 0x00001722
		
	public string Url
	{
			
		get;
			
		set;
	}

	// Token: 0x06000079 RID: 121 RVA: 0x0000352B File Offset: 0x0000172B
	public ParameterBuilder()
	{
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00003540 File Offset: 0x00001740
	public ParameterBuilder(string parameter)
	{
		var flag = parameter.Contains('?');
		if (flag)
		{
			Url = parameter.Substring(0, parameter.IndexOf('?'));
			var text = parameter;
			var num = parameter.IndexOf('?') + 1;
			parameter = text.Substring(num, text.Length - num);
		}
		var array = parameter.Split('&');
		for (var i = 0; i < array.Length; i++)
		{
			var array2 = array[i].Split('=');
			var flag2 = array2.Length == 2;
			if (flag2)
			{
				_parameters.Add(array2[0], array2[1]);
			}
		}
	}

	// Token: 0x0600007B RID: 123 RVA: 0x000035F8 File Offset: 0x000017F8
	public string Get(string parameter)
	{
		string text;
		var flag = !_parameters.TryGetValue(parameter, out text);
		string text2;
		if (flag)
		{
			text2 = string.Empty;
		}
		else
		{
			text2 = text;
		}
		return text2;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x0000362C File Offset: 0x0000182C
	public ParameterBuilder Append(string key, string value)
	{
		_parameters[key] = value;
		return this;
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00003650 File Offset: 0x00001850
	public ParameterBuilder Remove(string key)
	{
		_parameters.Remove(key);
		return this;
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00003670 File Offset: 0x00001870
	public string FormUrlEncode()
	{
		var enumerable = _parameters.Select(p => Uri.EscapeDataString(p.Key) + "=" + Uri.EscapeDataString(p.Value));
		return string.Join("&", enumerable);
	}

	// Token: 0x0600007F RID: 127 RVA: 0x000036B8 File Offset: 0x000018B8
	public string ToQueryUrl()
	{
		return Url + "?" + FormUrlEncode();
	}

	// Token: 0x06000080 RID: 128 RVA: 0x000036E0 File Offset: 0x000018E0
	public override string ToString()
	{
		return _parameters.Aggregate(string.Empty, (current, kv) => (current == string.Empty ? current : current + "&") + kv.Key + "=" + kv.Value);
	}

	// Token: 0x04000042 RID: 66
	private readonly Dictionary<string, string> _parameters = new();
}