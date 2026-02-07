using System;
using System.Collections.Generic;
using System.Linq;

namespace Codexus.Cipher.Utils.Http;
public class ParameterBuilder
{
		
	public string Url
	{
			
		get;
			
		set;
	}

	public ParameterBuilder()
	{
	}

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

	public string Get(string parameter)
	{
		var flag = !_parameters.TryGetValue(parameter, out var text);
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

	public ParameterBuilder Append(string key, string value)
	{
		_parameters[key] = value;
		return this;
	}

	public ParameterBuilder Remove(string key)
	{
		_parameters.Remove(key);
		return this;
	}

	public string FormUrlEncode()
	{
		var enumerable = _parameters.Select(p => Uri.EscapeDataString(p.Key) + "=" + Uri.EscapeDataString(p.Value));
		return string.Join("&", enumerable);
	}

	public string ToQueryUrl()
	{
		return Url + "?" + FormUrlEncode();
	}

	public override string ToString()
	{
		return _parameters.Aggregate(string.Empty, (current, kv) => (current == string.Empty ? current : current + "&") + kv.Key + "=" + kv.Value);
	}
	private readonly Dictionary<string, string> _parameters = new();
}