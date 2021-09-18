using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, Common.Common.PORT);
            tcpListener.Start();
            Console.WriteLine($"Listening {tcpListener.LocalEndpoint}");
            using (TcpClient tcpClient = tcpListener.AcceptTcpClient())
            {
                Console.WriteLine($"Accepted {tcpClient.Client.RemoteEndPoint} -> {tcpClient.Client.LocalEndPoint}");
                NetworkStream networkStream = tcpClient.GetStream();
                long num = 666;
                Common.Common.WriteLong(networkStream,num);

                string message = Common.Common.ReadString(networkStream);
                
                Console.Write($"{message}");

            }
        }
    }
}
