using Catan.Network.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cartan.Network.Events
{
    public class ReadCompletedEventArgs:EventArgs
    {
        public byte[] Data { private set; get; }
        public TcpClient TcpClient { private set; get; }
        public ReadCompletedEventArgs(byte[] data, TcpClient tcpClient)
        {
            this.Data = data;
            this.TcpClient = tcpClient;
        }
    }
}
