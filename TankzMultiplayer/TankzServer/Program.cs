using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TankzServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
            TcpListener server = new TcpListener(ip, 420);
            TcpClient client = default(TcpClient);

            try
            {
                server.Start();
                Console.WriteLine("Server is running...");
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.Read();
            }

            while (true)
            {
                client = server.AcceptTcpClient();
                byte[] receivedbuffer = new byte[100];
                NetworkStream stream = client.GetStream();

                stream.Read(receivedbuffer, 0, receivedbuffer.Length);

                string msg = Encoding.ASCII.GetString(receivedbuffer, 0, receivedbuffer.Length);
                Console.WriteLine(msg);
            }
        }
    }
}
