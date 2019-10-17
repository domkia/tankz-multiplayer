using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TankzClient.Framework
{
    public class NetworkManager
    {
        static string serverIP = "localhost";
        static int port = 420;
        private readonly Socket ClientSocket = new Socket
           (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        #region Singleton

        private static NetworkManager instance = null;
        public static NetworkManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NetworkManager();
                }
                return instance;
            }
        }
        #endregion

        public void ConnectToServer()
        {
            int attempts = 0;

            while (!ClientSocket.Connected)
            {
                try
                {
                    attempts++;
                    ClientSocket.Connect(serverIP, port);
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message + Environment.NewLine + "Connection attempt " + attempts);
                }
            }

            Console.WriteLine("connected");
        }

        public void SendRequest(string text)
        {
            string request = text;
            SendString(request);

            if (request.ToLower() == "exit")
            {
                Disconnect();
            }
        }

        public void ReceiveResponse()
        {
            var buffer = new byte[2048];
            try
            {
                if (buffer.ToString().Length == 2048) return;
                int received = ClientSocket.Receive(buffer, SocketFlags.None);
                if (received == 0) return;
                var data = new byte[received];
                Array.Copy(buffer, data, received);
                string text = Encoding.ASCII.GetString(data);
                Console.WriteLine(text);
                //MessageBox.Show(text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        private void SendString(string text)
        {
            try
            {
                byte[] buffer = Encoding.ASCII.GetBytes(text);
                ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Disconnect()
        {
            //SendString("exit");
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
            Environment.Exit(0);
        }
    }
}
