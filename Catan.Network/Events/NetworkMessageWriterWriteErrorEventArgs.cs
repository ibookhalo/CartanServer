using Catan.Network.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Network.Events
{
    public class NetworkMessageWriterWriteErrorEventArgs
    {
        public TcpClient TcpClient { private set; get; }
        public Exception Exception { private set; get; }
        public NetworkMessage NetworkMessage { private set; get; }
        public NetworkMessageWriterWriteErrorEventArgs(NetworkMessage netMessage, TcpClient tcpClient, Exception ex)
        {
            this.TcpClient = tcpClient;
            this.NetworkMessage = netMessage;
            this.Exception = ex;
        }
    }
}
