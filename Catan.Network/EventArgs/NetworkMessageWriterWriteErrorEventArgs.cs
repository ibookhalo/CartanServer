using Catan.Network.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Network.EventArgs
{
    public class NetworkMessageWriterWriteErrorEventArgs:NetworkMessageErrorEventArgs
    {
        public NetworkMessage NetworkMessage { private set; get; }
        public NetworkMessageWriterWriteErrorEventArgs(NetworkMessage netMessage, TcpClient tcpClient, Exception ex):base(tcpClient,ex)
        {
            this.NetworkMessage = netMessage;
        }
    }
}
