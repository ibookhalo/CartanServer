using Catan.Network.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Network.Events
{
    public class NetworkMessageReaderReadErrorEventArgs:EventArgs
    {
        public TcpClient TcpClient { private set; get; }
        public Exception Exception { private set; get; }
        public NetworkMessageReaderReadErrorEventArgs(TcpClient tcpClient, Exception ex)
        {
            this.TcpClient = tcpClient;
            this.Exception = ex;
        }
    }
}
