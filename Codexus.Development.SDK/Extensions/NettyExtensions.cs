using System;
using System.Collections.Generic;
using System.Text;
using Codexus.Development.SDK.Entities;
using Codexus.Development.SDK.Utils;
using DotNetty.Buffers;
using DotNetty.Common.Utilities;

namespace Codexus.Development.SDK.Extensions;

public static class NettyExtensions
{
	public static int ReadVarInt(this byte[] buffer)
	{
		var num = 0;
		var num2 = 0;
		foreach (var b in buffer)
		{
			var sb = (sbyte)b;
			num |= (sb & 0x7F) << num2++ * 7;
			if (num2 > 5)
			{
				throw new Exception("VarInt too big");
			}
			if ((sb & 0x80) != 128)
			{
				return num;
			}
		}
		throw new IndexOutOfRangeException();
	}

	extension(IByteBuffer buffer)
	{
		public Position ReadPosition()
		{
			var num = buffer.ReadLong();
			return new Position((int)(num >> 38), (int)(num << 52 >> 52), (int)(num << 26 >> 38));
		}

		public int ReadVarIntFromBuffer()
		{
			var num = 0;
			var num2 = 0;
			while (true)
			{
				var b = buffer.ReadByte();
				num |= (b & 0x7F) << num2;
				if ((b & 0x80) == 0)
				{
					break;
				}
				num2 += 7;
				if (num2 >= 32)
				{
					throw new Exception("VarInt is too big");
				}
			}
			return num;
		}

		public List<Property> ReadProperties()
		{
			var properties = new List<Property>();
			buffer.ReadWithCount(delegate
			{
				var item = buffer.ReadProperty();
				properties.Add(item);
			});
			return properties;
		}

		public Property ReadProperty()
		{
			var name = buffer.ReadStringFromBuffer(32767);
			var value = buffer.ReadStringFromBuffer(32767);
			var signature = buffer.ReadNullable(() => buffer.ReadStringFromBuffer(32767));
			return new Property
			{
				Name = name,
				Value = value,
				Signature = signature
			};
		}

		public T? ReadNullable<T>(Func<T> action)
		{
			return !buffer.ReadBoolean() ? default : action();
		}

		public void ReadWithCount(Action action)
		{
			var num = buffer.ReadVarIntFromBuffer();
			for (var i = 0; i < num; i++)
			{
				action();
			}
		}

		public string ReadStringFromBuffer(int maxLength)
		{
			var num = buffer.ReadVarIntFromBuffer();
			if (num > maxLength * 4)
			{
				throw new Exception(
					$"The received encoded string buffer length is longer than maximum allowed ({num} > {maxLength * 4})");
			}
			if (num < 0)
			{
				throw new Exception("The received encoded string buffer length is less than zero! Weird string!");
			}
			if (num > buffer.ReadableBytes)
			{
				num = buffer.ReadableBytes;
			}
			var array = new byte[num];
			buffer.ReadBytes(array);
			var text = Encoding.UTF8.GetString(array);
			return text.Length > maxLength
				? throw new Exception(
					$"The received string length is longer than maximum allowed ({num} > {maxLength})")
				: text;
		}

		public byte[] ReadByteArrayFromBuffer(int length)
		{
			var array = new byte[length];
			buffer.ReadBytes(array);
			return array;
		}

		public byte[] ReadByteArrayFromBuffer()
		{
			var num = buffer.ReadVarIntFromBuffer();
			if (num < 0)
			{
				throw new Exception("The received encoded string buffer length is less than zero! Weird string!");
			}
			var array = new byte[num];
			buffer.ReadBytes(array);
			return array;
		}

		public byte[] ReadByteArrayReadableBytes()
		{
			var array = new byte[buffer.ReadableBytes];
			buffer.ReadBytes(array);
			return array;
		}

		public IByteBuffer WriteStringToBuffer(string stringToWrite, int maxLength = 32767)
		{
			if (stringToWrite.Length > maxLength)
			{
				throw new Exception("String too big (was " + stringToWrite.Length + " bytes encoded, max " + maxLength + ")");
			}
			var bytes = Encoding.UTF8.GetBytes(stringToWrite);
			buffer.WriteVarInt(bytes.Length);
			buffer.WriteBytes(bytes);
			return buffer;
		}

		public IByteBuffer WriteByteArrayToBuffer(byte[] bytes)
		{
			return buffer.WriteVarInt(bytes.Length).WriteBytes(bytes);
		}

		public IByteBuffer WritePosition(int x, int y, int z)
		{
			return buffer.WritePosition(new Position(x, y, z));
		}

		public IByteBuffer WritePosition(Position position)
		{
			return buffer.WriteLong((long)((((ulong)position.X & 0x3FFFFFFuL) << 38) | (((ulong)position.Z & 0x3FFFFFFuL) << 12) | ((ulong)position.Y & 0xFFFuL)));
		}

		public IByteBuffer WriteProperties(List<Property>? properties)
		{
			if (properties == null)
			{
				buffer.WriteVarInt(0);
				return buffer;
			}

			buffer.WriteWithCount(properties.Count, delegate(int index)
			{
				buffer.WriteProperty(properties[index]);
			});
			return buffer;
		}

		public IByteBuffer WriteProperty(Property property)
		{
			buffer.WriteStringToBuffer(property.Name);
			buffer.WriteStringToBuffer(property.Value);
#pragma warning disable CS8604
			buffer.WriteNullable(property.Signature == null, () => { buffer.WriteStringToBuffer(property.Signature); });
#pragma warning restore CS8604
			return buffer;
		}

		public IByteBuffer WriteVarInt(int input)
		{
			while ((input & -128) != 0)
			{
				buffer.WriteByte((input & 0x7F) | 0x80);
				input >>>= 7;
			}
			buffer.WriteByte(input);
			return buffer;
		}

		public void WriteWithCount(int count, Action<int> action)
		{
			buffer.WriteVarInt(count);
			for (var i = 0; i < count; i++)
			{
				action(i);
			}
		}

		public void WriteNullable(bool isNull, Action action)
		{
			if (isNull)
			{
				buffer.WriteBoolean(false);
				return;
			}
			buffer.WriteBoolean(true);
			action();
		}

		public T WithReaderScope<T>(Func<IByteBuffer, T> action)
		{
			buffer.MarkReaderIndex();
			try
			{
				return action(buffer);
			}
			finally
			{
				buffer.ResetReaderIndex();
			}
		}

		public IByteBuffer WriteSignedByte(sbyte value)
		{
			buffer.WriteByte((byte)value);
			return buffer;
		}
	}

	public static int GetVarIntSize(this int input)
	{
		for (var i = 1; i < 5; i++)
		{
			if ((input & (-1 << i * 7)) == 0)
			{
				return i;
			}
		}
		return 5;
	}

	public static T GetOrDefault<T>(this IAttribute<T> attribute, Func<T> value)
	{
		if (attribute.Get() == null)
		{
			attribute.SetIfAbsent(value());
		}
		return attribute.Get();
	}
}
