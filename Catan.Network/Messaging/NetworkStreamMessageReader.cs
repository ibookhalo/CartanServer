using Cartan.Network.Events;
using Catan.Network.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cartan.Network.Messaging
{
    public class NetworkMessageStreamReader
    {
        public TcpClient TcpClient { private set; get; }
        private NetworkStream netStream;

        public delegate void ReadCompletedHandler(object catanNetworkMessageStreamReader, NetworkMessageReadCompletedEventArgs e);
        public delegate void ReadErrorHandler(object catanNetworkMessageStreamReader, NetworkMessageReadErrorEventArgs e);

        public event ReadCompletedHandler ReadCompleted;
        public event ReadErrorHandler ReadError;

        public NetworkMessageStreamReader(TcpClient tcpClient)
        {
            this.TcpClient = tcpClient;
        }

        public void ReadAsync()
        {
            try
            {
                byte[] buffer = new byte[TcpClient.ReceiveBufferSize = NetworkMessage.MAX_DATA_SIZE_IN_BYTES];
                netStream = TcpClient.GetStream();
                netStream.BeginRead(buffer, 0, buffer.Length, readCallback,buffer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void readCallback(IAsyncResult ar)
        {
            try
            {
                netStream.EndRead(ar);
                NetworkMessage networkMessage = new NetworkMessageFormatter().Deserialize(ar.AsyncState as byte[]);
                if (networkMessage !=null)
                {
                    ReadCompleted?.Invoke(this, new NetworkMessageReadCompletedEventArgs(networkMessage,TcpClient));
                }
                else
                {
                    ReadError?.Invoke(this, new NetworkMessageReadErrorEventArgs(TcpClient));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
