using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Catan.Network
{
    [Serializable]
    public class CatanClient
    {
        [NonSerialized]
        private TcpClient tcpClient;
        public TcpClient TcpClient { get { return tcpClient; }}
        public IPEndPoint IPAddress { private set; get; }
        public string PlayerName { private set; get; }

        public CatanClient(TcpClient tcpClient, string playerName)
        {
            this.tcpClient = tcpClient;
            this.IPAddress= ((IPEndPoint)tcpClient.Client.RemoteEndPoint);
            this.PlayerName = playerName;
        }
    }
}
