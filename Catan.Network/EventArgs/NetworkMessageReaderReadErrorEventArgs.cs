using Catan.Network.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Network.EventArgs
{
    public class NetworkMessageReaderReadErrorEventArgs:NetworkMessageErrorEventArgs
    {
        public NetworkMessageReaderReadErrorEventArgs(TcpClient tcpClient, Exception ex):base(tcpClient,ex) {}
    }
}
