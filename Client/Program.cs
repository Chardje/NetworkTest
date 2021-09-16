using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint remoteEndpoint = new IPEndPoint(IPAddress.Parse("192.168.13.92"), Common.Common.PORT);
            IPEndPoint localEndpoint = new IPEndPoint(IPAddress.Parse("192.168.13.92"), 4321);
            using (TcpClient tcpClient = new TcpClient(localEndpoint))
            {
                Console.WriteLine($"Connecting {remoteEndpoint}");
                tcpClient.Connect(remoteEndpoint);
                Console.WriteLine($"Connected {tcpClient.Client.LocalEndPoint} -> {tcpClient.Client.RemoteEndPoint}");
                NetworkStream networkStream = tcpClient.GetStream();

                byte[] buffer = new byte[8];
                int p = 0;
                while (p < 8)
                {
                    int r = networkStream.Read(buffer, p, 8 - p);
                    if (r < 0) throw new Exception("end of stream");
                    p += r;
                }
                Console.Write($"{string.Join(',', buffer)}");
                long num = BitConverter.ToInt64(buffer);
                Console.WriteLine($"{num}");

                string str = "Epic!!!#$$%%^";
                byte[] vs = Encoding.UTF8.GetBytes(str);
                Console.WriteLine($"{vs.Length}: {str}");
                networkStream.Write(BitConverter.GetBytes(vs.Length));
                networkStream.Write(vs);
            }
        }
    }
}
