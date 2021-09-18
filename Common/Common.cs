using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Common
{
    public class Common
    {
        public const int PORT = 1234;

        public static void WriteInt(NetworkStream stream, int value)
        {
            stream.Write(BitConverter.GetBytes(value));
        }
        public static void WriteLong(NetworkStream stream, long value)
        {
            stream.Write(BitConverter.GetBytes(value));
        }
        public static void WriteString(NetworkStream stream, string value)
        {
            byte[] vs = Encoding.UTF8.GetBytes(value);            
            stream.Write(BitConverter.GetBytes(vs.Length));
            stream.Write(vs);
        }
        public static int ReadInt(NetworkStream stream)
        {
            byte[] buffer = new byte[4];
            int p = 0;
            while (p < 8)
            {
                int r = stream.Read(buffer, p, 8 - p);
                if (r < 0) throw new Exception("end of stream");
                p += r;
            }
            int num = BitConverter.ToInt32(buffer);
            return num;
        }
        public static long ReadLong(NetworkStream stream)
        {
            byte[] buffer = new byte[8];
            int p = 0;
            while (p < 8)
            {
                int r = stream.Read(buffer, p, 8 - p);
                if (r < 0) throw new Exception("end of stream");
                p += r;
            }
            long num = BitConverter.ToInt64(buffer);
            return num;
        }
        public static string ReadString(NetworkStream stream)
        {
            int length;
            {
                byte[] buffer = new byte[4];
                int p = 0;
                while (p < 4)
                {
                    int r = stream.Read(buffer, p, 4 - p);
                    if (r < 0) throw new Exception("end of stream");
                    p += r;
                }
                length = BitConverter.ToInt32(buffer);
            }
            {
                byte[] buffer = new byte[length];
                int p = 0;
                while (p < length)
                {
                    int r = stream.Read(buffer, p, length - p);
                    if (r < 0) throw new Exception("end of stream");
                    p += r;
                }
                string message = Encoding.UTF8.GetString(buffer);
                return message;
            }
        }
    }
}
