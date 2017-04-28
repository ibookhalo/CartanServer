using Catan.Network.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cartan.Network.Events
{
    public class NetworkMessageReadCompletedEventArgs: EventArgs
    {
        public NetworkMessage NetworkMessage { private set; get; }
        public TcpClient TcpClient { private set; get; }
        public NetworkMessageReadCompletedEventArgs(NetworkMessage catanNetworkMessage, TcpClient tcpClient)
        {
            this.NetworkMessage = catanNetworkMessage;
            this.TcpClient = tcpClient;
        }
    }
}
