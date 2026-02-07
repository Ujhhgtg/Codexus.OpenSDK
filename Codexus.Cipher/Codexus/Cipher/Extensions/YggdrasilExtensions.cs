using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Codexus.Cipher.Extensions;

public static class YggdrasilExtensions
{
    public static byte[] EncodeSha256(this byte[] input)
    {
        return SHA256.HashData(input);
    }

    extension(int value)
    {
        public byte[] ToByteArray(bool littleEndian = true)
        {
            var bytes = BitConverter.GetBytes(value);
            var flag = BitConverter.IsLittleEndian != littleEndian;
            if (flag) Array.Reverse(bytes);
            return bytes;
        }

        public byte[] ToShortByteArray(bool littleEndian = true)
        {
            var bytes = BitConverter.GetBytes((short)value);
            var flag = BitConverter.IsLittleEndian != littleEndian;
            if (flag) Array.Reverse(bytes);
            return bytes;
        }
    }

    extension(long value)
    {
        public byte[] ToByteArray(bool littleEndian = true)
        {
            var bytes = BitConverter.GetBytes(value);
            var flag = BitConverter.IsLittleEndian != littleEndian;
            if (flag) Array.Reverse(bytes);
            return bytes;
        }

        public byte[] ToShortByteArray(bool littleEndian = true)
        {
            var bytes = BitConverter.GetBytes((short)value);
            var flag = BitConverter.IsLittleEndian != littleEndian;
            if (flag) Array.Reverse(bytes);
            return bytes;
        }
    }

    extension(MemoryStream stream)
    {
        public void WriteInt(int value, bool littleEndian = true)
        {
            var array = value.ToByteArray(littleEndian);
            stream.Write(array);
        }

        public void WriteShort(int value, bool littleEndian = true)
        {
            var array = value.ToShortByteArray(littleEndian);
            stream.Write(array);
        }

        public void WriteLong(long value, bool littleEndian = true)
        {
            var array = value.ToByteArray(littleEndian);
            stream.Write(array);
        }

        public void WriteBytes(byte[] data)
        {
            stream.Write(data);
        }

        public void WriteString(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            stream.WriteByte((byte)bytes.Length);
            stream.Write(bytes);
        }

        public void WriteByteLengthString(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            stream.WriteByte((byte)bytes.Length);
            stream.Write(bytes);
        }

        public void WriteShortString(string value, bool littleEndian = true)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            stream.WriteShort(bytes.Length, littleEndian);
            stream.Write(bytes);
        }

        public void WriteShortBytes(byte[] data, bool littleEndian = true)
        {
            stream.WriteShort(data.Length, littleEndian);
            stream.Write(data);
        }
    }
}