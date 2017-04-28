using Catan.Network.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cartan.Network.Events
{
    public class NetworkMessageReadErrorEventArgs:EventArgs
    {
        public TcpClient TcpClient { private set; get; }
        public NetworkMessageReadErrorEventArgs(TcpClient tcpClient)
        {
            this.TcpClient = tcpClient;
        }
    }
}
