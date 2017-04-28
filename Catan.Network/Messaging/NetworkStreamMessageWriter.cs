using Catan.Network.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cartan.Network.Messaging
{
    public class NetworkStreamMessageWriter
    {
        public TcpClient TcpClient { private set; get; }
        private NetworkStream netStream;
        public NetworkStreamMessageWriter(TcpClient tcpClient)
        {
            this.TcpClient = tcpClient;
        }

        public void WriteAsync()
        {
            try
            {
                byte[] buffer = new byte[TcpClient.SendBufferSize = NetworkMessage.MAX_DATA_SIZE_IN_BYTES];
                netStream = TcpClient.GetStream();
                netStream.BeginWrite(buffer, 0, buffer.Length, writeCallback, TcpClient);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void writeCallback(IAsyncResult ar)
        {

        }
    }
}
