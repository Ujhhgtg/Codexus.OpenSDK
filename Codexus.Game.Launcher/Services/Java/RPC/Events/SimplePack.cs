using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codexus.Game.Launcher.Services.Java.RPC.Events;

public static class SimplePack
{
    public static byte[]? Pack(params object[]? data)
    {
        byte[]? array;
        if (data == null)
        {
            array = null;
        }
        else
        {
            var array2 = Array.Empty<byte>();
            foreach (var obj in data)
            {
                var array3 = Array.Empty<byte>();
                var type = obj.GetType();
                switch (Type.GetTypeCode(type))
                {
                    case TypeCode.Object:
                    {
                        var flag2 = type == typeof(byte[]);
                        if (flag2)
                        {
                            array3 = (byte[])obj;
                        }
                        else
                        {
                            var flag3 = type == typeof(List<uint>);
                            if (flag3)
                            {
                                var list = new List<byte>();
                                var bytes = BitConverter.GetBytes((ushort)(((List<uint>)obj).Count * 4));
                                list.AddRange(array3);
                                list.AddRange(bytes);
                                foreach (var num in obj as List<uint>) list.AddRange(BitConverter.GetBytes(num));
                                array3 = list.ToArray();
                            }
                            else
                            {
                                var flag4 = type == typeof(List<ulong>);
                                if (flag4)
                                {
                                    var list2 = new List<byte>();
                                    var bytes2 = BitConverter.GetBytes((ushort)(((List<ulong>)obj).Count * 8));
                                    list2.AddRange(array3);
                                    list2.AddRange(bytes2);
                                    foreach (var num2 in obj as List<ulong>)
                                        list2.AddRange(BitConverter.GetBytes(num2));
                                    array3 = list2.ToArray();
                                }
                                else
                                {
                                    var flag5 = !(type == typeof(List<long>));
                                    if (!flag5)
                                    {
                                        var list3 = new List<byte>();
                                        var bytes3 = BitConverter.GetBytes((ushort)(((List<long>)obj).Count * 8));
                                        list3.AddRange(array3);
                                        list3.AddRange(bytes3);
                                        foreach (var num3 in obj as List<long>)
                                            list3.AddRange(BitConverter.GetBytes(num3));
                                        array3 = list3.ToArray();
                                    }
                                }
                            }
                        }

                        break;
                    }
                    case TypeCode.Boolean:
                        array3 = BitConverter.GetBytes((bool)obj);
                        break;
                    case TypeCode.Byte:
                        array3 = [(byte)obj];
                        break;
                    case TypeCode.Int16:
                        array3 = BitConverter.GetBytes((short)obj);
                        break;
                    case TypeCode.UInt16:
                        array3 = BitConverter.GetBytes((ushort)obj);
                        break;
                    case TypeCode.Int32:
                        array3 = BitConverter.GetBytes((int)obj);
                        break;
                    case TypeCode.UInt32:
                        array3 = BitConverter.GetBytes((uint)obj);
                        break;
                    case TypeCode.Int64:
                        array3 = BitConverter.GetBytes((long)obj);
                        break;
                    case TypeCode.Double:
                        array3 = BitConverter.GetBytes((double)obj);
                        break;
                    case TypeCode.String:
                        array3 = Encoding.UTF8.GetBytes((string)obj);
                        array3 = Pack((ushort)array3.Length, array3);
                        break;
                }

                var flag6 = array3 != null;
                if (flag6) array2 = array2.Concat(array3).ToArray();
            }

            array = array2;
        }

        return array;
    }
}