using Catan.Network.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Network.EventArgs
{
    public class NetworkMessageReaderReadCompletedEventArgs : System.EventArgs
    {
        public NetworkMessage NetworkMessage { private set; get; }
        public TcpClient TcpClient { private set; get; }
        public NetworkMessageReaderReadCompletedEventArgs (NetworkMessage netMessage, TcpClient tcpClient)
        {
            this.NetworkMessage = netMessage;
            this.TcpClient = tcpClient;
        }
    }
 }
