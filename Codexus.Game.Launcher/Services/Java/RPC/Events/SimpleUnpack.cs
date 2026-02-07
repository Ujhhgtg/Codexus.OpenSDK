using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codexus.Game.Launcher.Services.Java.RPC.Events;

public class SimpleUnpack
{
    public SimpleUnpack(byte[] bytes)
    {
        _data = bytes;
        _index = 0;
        _lastLength = 0;
    }

    public void Unpack<T>(ref T content)
    {
        var fields = typeof(T).GetFields();
        foreach (var fieldInfo in fields)
        {
            var value = fieldInfo.GetValue(content);
            var fieldType = fieldInfo.FieldType;
            var flag = value != null;
            if (flag)
            {
                InnerUnpack(ref value, fieldType);
                fieldInfo.SetValue(content, value);
            }
        }

        var properties = typeof(T).GetProperties();
        foreach (var propertyInfo in properties)
        {
            var canWrite = propertyInfo.CanWrite;
            if (canWrite)
            {
                var value2 = propertyInfo.GetValue(content);
                var propertyType = propertyInfo.PropertyType;
                var flag2 = value2 != null;
                if (flag2)
                {
                    InnerUnpack(ref value2, propertyType);
                    propertyInfo.SetValue(content, value2);
                }
            }
        }

        var flag3 = content != null;
        if (flag3) content = ConvertValue<T>(content);
    }

    private static T ConvertValue<T>(object value)
    {
        return (T)(object)Convert.ChangeType(value, typeof(T));
    }

    private void InnerUnpack(ref object value, Type type)
    {
        var typeCode = Type.GetTypeCode(type);
        switch (typeCode)
        {
            case TypeCode.Object:
            {
                var flag = type == typeof(byte[]);
                if (flag)
                {
                    value = _data.Skip(_index).Take(_lastLength).ToArray();
                    _index += _lastLength;
                }
                else
                {
                    var flag2 = type == typeof(List<uint>);
                    if (flag2)
                    {
                        var num = _lastLength;
                        var list = new List<uint>();
                        while (num > 0)
                        {
                            list.Add(BitConverter.ToUInt32(_data, _index));
                            _index += 4;
                            num -= 4;
                        }

                        value = list;
                    }
                }

                break;
            }
            case TypeCode.DBNull:
            case TypeCode.Boolean:
            case TypeCode.Char:
            case TypeCode.SByte:
                break;
            case TypeCode.Byte:
            {
                var data = _data;
                var index = _index;
                _index = index + 1;
                value = data[index];
                break;
            }
            case TypeCode.Int16:
                value = BitConverter.ToInt16(_data, _index);
                _index += 2;
                break;
            case TypeCode.UInt16:
                value = BitConverter.ToUInt16(_data, _index);
                _index += 2;
                _lastLength = (ushort)value;
                break;
            case TypeCode.Int32:
                value = BitConverter.ToInt32(_data, _index);
                _index += 4;
                break;
            case TypeCode.UInt32:
                value = BitConverter.ToUInt32(_data, _index);
                _index += 4;
                break;
            default:
                if (typeCode == TypeCode.String)
                {
                    value = Encoding.UTF8.GetString(_data, _index, _lastLength);
                    _index += _lastLength;
                }

                break;
        }
    }

    private readonly byte[] _data;
    private int _index;
    private ushort _lastLength;
}