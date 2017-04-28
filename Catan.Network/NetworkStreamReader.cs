using Cartan.Network.Events;
using Catan.Network.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cartan.Network
{
    public class NetworkStreamReader
    {
        public TcpClient TcpClient { private set; get; }
        private NetworkStream netStream;
        public delegate void ReadCompletedHandler(object netStreamReader, ReadCompletedEventArgs readCompletedEventArgs);
        public event ReadCompletedHandler ReadCompleted;

        public NetworkStreamReader(TcpClient tcpClient)
        {
            this.TcpClient = tcpClient;
        }

        public void ReadAsync()
        {
            try
            {
                byte[] buffer = new byte[NetworkMessage.MAX_DATA_SIZE_IN_BYTES];
                netStream = TcpClient.GetStream();
                netStream.BeginRead(buffer, 0, buffer.Length, readCallback, new KeyValuePair<byte[], TcpClient>(buffer, TcpClient));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void readCallback(IAsyncResult ar)
        {
            KeyValuePair<byte[], TcpClient> result = (KeyValuePair<byte[], TcpClient>)ar.AsyncState;
            try
            {
                netStream.EndRead(ar);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            ReadCompleted?.Invoke(this, new ReadCompletedEventArgs(result.Key, result.Value));
        }
    }
}
