using Catan.Network.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Network.Events
{
    public class NetworkMessageWriterWriteCompletedEventArgs:EventArgs
    {
        public TcpClient TcpClient { private set; get; }
        public NetworkMessage NetMessage { private set; get; }
        public NetworkMessageWriterWriteCompletedEventArgs(NetworkMessage netMessage, TcpClient tcpClient)
        {
            this.TcpClient = tcpClient;
            this.NetMessage = netMessage;
        }
    }
}
