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
                byte[] array = BitConverter.GetBytes(num);
                Console.WriteLine($"{num} -> {string.Join(',', array)}");
                networkStream.Write(array);

                int length;
                {
                    byte[] buffer = new byte[4];
                    int p = 0;
                    while (p < 4)
                    {
                        int r = networkStream.Read(buffer, p, 4 - p);
                        if (r < 0) throw new Exception("end of stream");
                        p += r;
                    }
                    Console.Write($"{string.Join(',', buffer)}");
                    length = BitConverter.ToInt32(buffer);
                }
                Console.WriteLine($"{length}");
                {
                    byte[] buffer = new byte[length];
                    int p = 0;
                    while (p < length)
                    {
                        int r = networkStream.Read(buffer, p, length - p);
                        if (r < 0) throw new Exception("end of stream");
                        p += r;
                    }
                    Console.Write($"{string.Join(',', buffer)}");
                    string message = Encoding.UTF8.GetString(buffer);
                    Console.Write($"{message}");
                }

            }
        }
    }
}
