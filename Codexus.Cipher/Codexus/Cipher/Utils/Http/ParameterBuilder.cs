using System;
using System.Collections.Generic;
using System.Linq;

namespace Codexus.Cipher.Utils.Http;

public class ParameterBuilder
{
    private readonly Dictionary<string, string> _parameters = new();

    public string? Url { get; set; }

    public ParameterBuilder()
    {
    }

    public ParameterBuilder(string parameter)
    {
        if (parameter.Contains('?'))
        {
            Url = parameter[..parameter.IndexOf('?')];
            var text = parameter;
            var num = parameter.IndexOf('?') + 1;
            parameter = text[num..];
        }
        var array = parameter.Split('&');
        foreach (var s in array)
        {
            var array2 = s.Split('=');
            if (array2.Length == 2)
            {
                _parameters.Add(array2[0], array2[1]);
            }
        }
    }

    public string Get(string parameter)
    {
        return !_parameters.TryGetValue(parameter, out var value) ? string.Empty : value;
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
        return string.Join("&", _parameters.Select((KeyValuePair<string, string> p) => Uri.EscapeDataString(p.Key) + "=" + Uri.EscapeDataString(p.Value)));
    }

    public string ToQueryUrl()
    {
        return Url + "?" + FormUrlEncode();
    }

    public override string ToString()
    {
        return _parameters.Aggregate(string.Empty, (current, kv) => (current == string.Empty ? current : current + "&") + kv.Key + "=" + kv.Value);
    }
}
