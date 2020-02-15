using System;

namespace Tinybuffers
{
    public interface IBufferable
    {
        int Size { get; }
        void BuildBuffer(byte[] buffer, int atOffset = 0);
        void Unbuffer(in byte[] buffer, int atOffset = 0);
    }

    public static class BufferBuilder
    {
        //Todo endianess

        //ToDo maybe use Bitconverter
        //Scalar SetPartialBuffer
        public static void SetPartialBuffer(this byte[] buffer, Byte toValue, int atOffset = 0)
        {
            buffer[atOffset] = (byte)toValue;
        }
        public static void SetPartialBuffer(this byte[] buffer, SByte toValue, int atOffset = 0)
        {
            buffer[atOffset] = (byte)toValue;
        }

        public static void SetPartialBuffer(this byte[] buffer, UInt16 toValue, int atOffset = 0)
        {
            buffer[atOffset + 0] = (byte)(toValue >> 8);
            buffer[atOffset + 1] = (byte)(toValue >> 0);
        }
        public static void SetPartialBuffer(this byte[] buffer, Int16 toValue, int atOffset = 0)
        {
            buffer[atOffset + 0] = (byte)(toValue >> 8);
            buffer[atOffset + 1] = (byte)(toValue >> 0);
        }

        public static void SetPartialBuffer(this byte[] buffer, UInt32 toValue, int atOffset = 0)
        {
            buffer[atOffset + 0] = (byte)(toValue >> 24);
            buffer[atOffset + 1] = (byte)(toValue >> 16);
            buffer[atOffset + 2] = (byte)(toValue >> 8);
            buffer[atOffset + 3] = (byte)(toValue >> 0);
        }
        public static void SetPartialBuffer(this byte[] buffer, Int32 toValue, int atOffset = 0)
        {
            buffer[atOffset + 0] = (byte)(toValue >> 24);
            buffer[atOffset + 1] = (byte)(toValue >> 16);
            buffer[atOffset + 2] = (byte)(toValue >> 8);
            buffer[atOffset + 3] = (byte)(toValue >> 0);
        }

        public static void SetPartialBuffer(this byte[] buffer, UInt64 toValue, int atOffset = 0)
        {
            buffer[atOffset + 0] = (byte)(toValue >> 56);
            buffer[atOffset + 1] = (byte)(toValue >> 48);
            buffer[atOffset + 2] = (byte)(toValue >> 40);
            buffer[atOffset + 3] = (byte)(toValue >> 32);
            buffer[atOffset + 4] = (byte)(toValue >> 24);
            buffer[atOffset + 5] = (byte)(toValue >> 16);
            buffer[atOffset + 6] = (byte)(toValue >> 8);
            buffer[atOffset + 7] = (byte)(toValue >> 0);
        }
        public static void SetPartialBuffer(this byte[] buffer, Int64 toValue, int atOffset = 0)
        {
            buffer[atOffset + 0] = (byte)(toValue >> 56);
            buffer[atOffset + 1] = (byte)(toValue >> 48);
            buffer[atOffset + 2] = (byte)(toValue >> 40);
            buffer[atOffset + 3] = (byte)(toValue >> 32);
            buffer[atOffset + 4] = (byte)(toValue >> 24);
            buffer[atOffset + 5] = (byte)(toValue >> 16);
            buffer[atOffset + 6] = (byte)(toValue >> 8);
            buffer[atOffset + 7] = (byte)(toValue >> 0);
        }

        public static void SetPartialBuffer<T>(this byte[] buffer, T toValue, int atOffset = 0) where T : IBufferable
        {
            toValue.BuildBuffer(buffer, atOffset);
        }

        //Array SetPartialBuffer
        public static void SetPartialBuffer(this byte[] buffer, Byte[] toValue, int atOffset = 0)
        {
            foreach (var element in toValue)
            {
                buffer.SetPartialBuffer(element, atOffset);
                atOffset += 8;
            }
        }
        public static void SetPartialBuffer(this byte[] buffer, SByte[] toValue, int atOffset = 0)
        {
            foreach (var element in toValue)
            {
                buffer.SetPartialBuffer(element, atOffset);
                atOffset += 8;
            }
        }

        public static void SetPartialBuffer(this byte[] buffer, UInt16[] toValue, int atOffset = 0)
        {
            foreach (var element in toValue)
            {
                buffer.SetPartialBuffer(element, atOffset);
                atOffset += 8;
            }
        }
        public static void SetPartialBuffer(this byte[] buffer, Int16[] toValue, int atOffset = 0)
        {
            foreach (var element in toValue)
            {
                buffer.SetPartialBuffer(element, atOffset);
                atOffset += 8;
            }
        }

        public static void SetPartialBuffer(this byte[] buffer, UInt32[] toValue, int atOffset = 0)
        {
            foreach (var element in toValue)
            {
                buffer.SetPartialBuffer(element, atOffset);
                atOffset += 8;
            }
        }
        public static void SetPartialBuffer(this byte[] buffer, Int32[] toValue, int atOffset = 0)
        {
            foreach (var element in toValue)
            {
                buffer.SetPartialBuffer(element, atOffset);
                atOffset += 8;
            }
        }
        public static void SetPartialBuffer(this byte[] buffer, UInt64[] toValue, int atOffset = 0)
        {
            foreach (var element in toValue)
            {
                buffer.SetPartialBuffer(element, atOffset);
                atOffset += 8;
            }
        }
        public static void SetPartialBuffer(this byte[] buffer, Int64[] toValue, int atOffset = 0)
        {
            foreach (var element in toValue)
            {
                buffer.SetPartialBuffer(element, atOffset);
                atOffset += 8;
            }
        }
        public static void SetPartialBuffer<T>(this byte[] buffer, T[] toValue, int atOffset = 0) where T : IBufferable
        {
            foreach (var element in toValue)
            {
                buffer.SetPartialBuffer(element, atOffset);
                atOffset += element.Size;
            }
        }

        //Unbuffer Scalar
        public static void ReadInto(this byte[] buffer, ref Byte value, int atOffset = 0)
        {
            value = buffer[atOffset];
        }
        public static void ReadInto(this byte[] buffer, ref SByte value, int atOffset = 0)
        {
            value = (SByte)buffer[atOffset];
        }
        public static void ReadInto(this byte[] buffer, ref UInt16 value, int atOffset = 0)
        {
            value = BitConverter.ToUInt16(buffer, atOffset);
        }
        public static void ReadInto(this byte[] buffer, ref Int16 value, int atOffset = 0)
        {
            value = BitConverter.ToInt16(buffer, atOffset);
        }
        public static void ReadInto(this byte[] buffer, ref UInt32 value, int atOffset = 0)
        {
            value = BitConverter.ToUInt32(buffer, atOffset);
        }
        public static void ReadInto(this byte[] buffer, ref Int32 value, int atOffset = 0)
        {
            value = BitConverter.ToInt32(buffer, atOffset);
        }
        public static void ReadInto(this byte[] buffer, ref UInt64 value, int atOffset = 0)
        {
            value = BitConverter.ToUInt64(buffer, atOffset);
        }
        public static void ReadInto(this byte[] buffer, ref Int64 value, int atOffset = 0)
        {
            value = BitConverter.ToInt64(buffer, atOffset);
        }
        public static void ReadInto<T>(this byte[] buffer, ref T value, int atOffset = 0) where T : IBufferable
        {
            value.Unbuffer(buffer, atOffset);
        }

        //Unbuffer Array
        public static void ReadInto(this byte[] buffer, ref Byte[] value, int atOffset = 0)
        {
            for (int i = 0; i < value.Length; i++)
            {
                ReadInto(buffer, ref value[i], atOffset);
                atOffset += 1;
            }
        }
        public static void ReadInto(this byte[] buffer, ref SByte[] value, int atOffset = 0)
        {
            for (int i = 0; i < value.Length; i++)
            {
                ReadInto(buffer, ref value[i], atOffset);
                atOffset += 1;
            }
        }
        public static void ReadInto(this byte[] buffer, ref UInt16[] value, int atOffset = 0)
        {
            for (int i = 0; i < value.Length; i++)
            {
                ReadInto(buffer, ref value[i], atOffset);
                atOffset += 2;
            }
        }
        public static void ReadInto(this byte[] buffer, ref Int16[] value, int atOffset = 0)
        {
            for (int i = 0; i < value.Length; i++)
            {
                ReadInto(buffer, ref value[i], atOffset);
                atOffset += 2;
            }
        }
        public static void ReadInto(this byte[] buffer, ref UInt32[] value, int atOffset = 0)
        {
            for (int i = 0; i < value.Length; i++)
            {
                ReadInto(buffer, ref value[i], atOffset);
                atOffset += 4;
            }
        }
        public static void ReadInto(this byte[] buffer, ref Int32[] value, int atOffset = 0)
        {
            for (int i = 0; i < value.Length; i++)
            {
                ReadInto(buffer, ref value[i], atOffset);
                atOffset += 4;
            }
        }
        public static void ReadInto(this byte[] buffer, ref UInt64[] value, int atOffset = 0)
        {
            for (int i = 0; i < value.Length; i++)
            {
                ReadInto(buffer, ref value[i], atOffset);
                atOffset += 8;
            }
        }
        public static void ReadInto(this byte[] buffer, ref Int64[] value, int atOffset = 0)
        {
            for (int i = 0; i < value.Length; i++)
            {
                ReadInto(buffer, ref value[i], atOffset);
                atOffset += 8;
            }
        }

    }
}