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

                long num = Common.Common.ReadLong(networkStream);
                Console.WriteLine($"{num}");

                string str = "Epic!!!#$$%%^";
                Common.Common.WriteString(networkStream,str);
            }
        }
    }
}
