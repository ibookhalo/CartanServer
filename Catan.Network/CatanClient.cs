using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Network
{
    public class CatanClient
    {
        public TcpClient TcpClient { private set; get; }
        public IPEndPoint IPAddress { private set; get; }
        public string PlayerName { private set; get; }

        public CatanClient(TcpClient tcpClient, string playerName)
        {
            this.TcpClient = tcpClient;
            this.IPAddress= ((IPEndPoint)tcpClient.Client.RemoteEndPoint);
            this.PlayerName = playerName;
        }
    }
}
