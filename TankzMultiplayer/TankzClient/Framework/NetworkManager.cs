using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TankzClient.Framework
{
    public class NetworkManager
    {
        string serverIP = "localhost";
        int port = 420;

        #region Singleton

        private static NetworkManager instance = null;
        public static NetworkManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new NetworkManager();
                return instance;
            }
        }
        #endregion
        public void SendPacket(string text)
        {
            TcpClient client = new TcpClient(serverIP, port);
            int bytecount = Encoding.ASCII.GetByteCount(text);

            byte[] sendData = new byte[bytecount];

            sendData = Encoding.ASCII.GetBytes(text);

            NetworkStream stream = client.GetStream();

            stream.Write(sendData, 0, sendData.Length);

            stream.Close();
            client.Close();
        }
    }
}
