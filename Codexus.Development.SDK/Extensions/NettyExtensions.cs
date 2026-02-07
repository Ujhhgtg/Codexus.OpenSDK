using System;
using System.Collections.Generic;
using System.Text;
using Codexus.Development.SDK.Entities;
using Codexus.Development.SDK.Utils;
using DotNetty.Buffers;
using DotNetty.Common.Utilities;

namespace Codexus.Development.SDK.Extensions;

// Token: 0x02000021 RID: 33
public static class NettyExtensions
{
	public static int ReadVarInt(this byte[] buffer)
	{
		var num = 0;
		var num2 = 0;
		foreach (sbyte b in buffer)
		{
			num |= (b & sbyte.MaxValue) << num2++ * 7;
			var flag = num2 > 5;
			if (flag)
			{
				throw new Exception("VarInt too big");
			}
			var flag2 = (b & 128) != 128;
			if (flag2)
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
			var num2 = (int)(num >> 38);
			var num3 = (int)(num << 52 >> 52);
			var num4 = (int)(num << 26 >> 38);
			return new Position(num2, num3, num4);
		}

		public int ReadVarIntFromBuffer()
		{
			var num = 0;
			var num2 = 0;
			for (;;)
			{
				var b = buffer.ReadByte();
				num |= (b & 127) << num2;
				var flag = (b & 128) == 0;
				if (flag)
				{
					break;
				}
				num2 += 7;
				var flag2 = num2 >= 32;
				if (flag2)
				{
					goto Block_2;
				}
			}
			return num;
			Block_2:
			throw new Exception("VarInt is too big");
		}

		public List<Property> ReadProperties()
		{
			var properties = new List<Property>();
			buffer.ReadWithCount(delegate
			{
				var property = buffer.ReadProperty();
				properties.Add(property);
			});
			return properties;
		}

		public Property ReadProperty()
		{
			var text = buffer.ReadStringFromBuffer(32767);
			var text2 = buffer.ReadStringFromBuffer(32767);
			var text3 = buffer.ReadNullable(() => buffer.ReadStringFromBuffer(32767));
			return new Property
			{
				Name = text,
				Value = text2,
				Signature = text3
			};
		}

		public T? ReadNullable<T>(Func<T> action)
		{
			var flag = !buffer.ReadBoolean();
			T t;
			if (flag)
			{
				t = default;
			}
			else
			{
				t = action();
			}
			return t;
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
			var flag = num > maxLength * 4;
			if (flag)
			{
				throw new Exception(string.Concat(new[]
				{
					"The received encoded string buffer length is longer than maximum allowed (",
					num.ToString(),
					" > ",
					(maxLength * 4).ToString(),
					")"
				}));
			}
			var flag2 = num < 0;
			if (flag2)
			{
				throw new Exception("The received encoded string buffer length is less than zero! Weird string!");
			}
			var flag3 = num > buffer.ReadableBytes;
			if (flag3)
			{
				num = buffer.ReadableBytes;
			}
			var array = new byte[num];
			buffer.ReadBytes(array);
			var @string = Encoding.UTF8.GetString(array);
			var flag4 = @string.Length > maxLength;
			if (flag4)
			{
				throw new Exception(string.Concat(new[]
				{
					"The received string length is longer than maximum allowed (",
					num.ToString(),
					" > ",
					maxLength.ToString(),
					")"
				}));
			}
			return @string;
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
			var flag = num < 0;
			if (flag)
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
			var flag = stringToWrite.Length > maxLength;
			if (flag)
			{
				throw new Exception(string.Concat(new[]
				{
					"String too big (was ",
					stringToWrite.Length.ToString(),
					" bytes encoded, max ",
					maxLength.ToString(),
					")"
				}));
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
			var num = ((position.X & 67108863L) << 38) | ((position.Z & 67108863L) << 12) | (position.Y & 4095L);
			return buffer.WriteLong(num);
		}

		public IByteBuffer WriteProperties(List<Property> properties)
		{
			var flag = properties == null;
			IByteBuffer byteBuffer;
			if (flag)
			{
				buffer.WriteVarInt(0);
				byteBuffer = buffer;
			}
			else
			{
				buffer.WriteWithCount(properties.Count, delegate(int index)
				{
					buffer.WriteProperty(properties[index]);
				});
				byteBuffer = buffer;
			}
			return byteBuffer;
		}

		public IByteBuffer WriteProperty(Property property)
		{
			buffer.WriteStringToBuffer(property.Name);
			buffer.WriteStringToBuffer(property.Value);
			buffer.WriteNullable(property.Signature == null, delegate
			{
				buffer.WriteStringToBuffer(property.Signature);
			});
			return buffer;
		}

		public IByteBuffer WriteVarInt(int input)
		{
			while ((input & -128) != 0)
			{
				buffer.WriteByte((input & 127) | 128);
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

		public void WriteNullable(bool nullable, Action action)
		{
			if (nullable)
			{
				buffer.WriteBoolean(false);
			}
			else
			{
				buffer.WriteBoolean(true);
				action();
			}
		}

		public T WithReaderScope<T>(Func<IByteBuffer, T> action)
		{
			buffer.MarkReaderIndex();
			T t;
			try
			{
				t = action(buffer);
			}
			finally
			{
				buffer.ResetReaderIndex();
			}
			return t;
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
			var flag = (input & (-1 << i * 7)) == 0;
			if (flag)
			{
				return i;
			}
		}
		return 5;
	}

	public static T GetOrDefault<T>(this IAttribute<T> attribute, Func<T> value)
	{
		var flag = attribute.Get() == null;
		if (flag)
		{
			attribute.SetIfAbsent(value());
		}
		return attribute.Get();
	}
}