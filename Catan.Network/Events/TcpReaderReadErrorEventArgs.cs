using Catan.Network.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cartan.Network.Events
{
    public class TcpReaderReadErrorEventArgs:EventArgs
    {
        public TcpClient TcpClient { private set; get; }
        public Exception Exception { private set; get; }
        public TcpReaderReadErrorEventArgs(TcpClient tcpClient, Exception ex)
        {
            this.TcpClient = tcpClient;
            this.Exception = ex;
        }
    }
}
