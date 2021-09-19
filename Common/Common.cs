using System;
using System.IO;
using System.Text;

namespace Common
{
    public class Common
    {
        public const int PORT = 1234;

        public static void WriteInt(Stream stream, int value)
        {
            stream.Write(BitConverter.GetBytes(value));
        }
        public static void WriteLong(Stream stream, long value)
        {
            stream.Write(BitConverter.GetBytes(value));
        }
        public static void WriteString(Stream stream, string value)
        {
            byte[] vs = Encoding.UTF8.GetBytes(value);            
            stream.Write(BitConverter.GetBytes(vs.Length));
            stream.Write(vs);
        }

        public static byte[] ReadBufferFully(Stream stream, byte[] buffer)
        {
            int length = buffer.Length;
            int position = 0;
            while (position < length)
            {
                int r = stream.Read(buffer, position, length - position);
                if (r < 0) throw new Exception("end of stream; read=" + position + "; expected=" + length);
                position += r;
            }
            return buffer;
        }

        public static int ReadInt(Stream stream)
        {
            return BitConverter.ToInt32(ReadBufferFully(stream, new byte[4]));
        }

        public static long ReadLong(Stream stream)
        {
            return BitConverter.ToInt64(ReadBufferFully(stream, new byte[8]));
        }

        public static string ReadString(Stream stream)
        {
            int length = ReadInt(stream);
            return Encoding.UTF8.GetString(ReadBufferFully(stream, new byte[length]));
        }
    }
}
