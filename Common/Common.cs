using System;
using System.Net.Sockets;

namespace Common
{
    public class Common
    {
        public const int PORT = 1234;

        public static void WriteInt(NetworkStream stream, int value)
        {
            //
        }
        public static void WriteLong(NetworkStream stream, long value)
        {
            //
        }
        public static void WriteString(NetworkStream stream, string value)
        {
            //
        }
        public static int ReadInt(NetworkStream stream)
        {
            return 0;
        }
        public static long ReadLong(NetworkStream stream)
        {
            return 0;
        }
        public static string ReadString(NetworkStream stream)
        {
            return "";
        }
    }
}
