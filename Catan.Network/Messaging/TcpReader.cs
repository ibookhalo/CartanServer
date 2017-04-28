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
    public class TcpReader
    {
        public TcpClient TcpClient { private set; get; }
        private NetworkStream netStream;

        public delegate void ReadCompletedHandler(object obj, TcpReaderReadCompletedEventArgs  e);
        public delegate void ReadErrorHandler(object obj, TcpReaderReadErrorEventArgs e);

        public event ReadCompletedHandler ReadCompleted;
        public event ReadErrorHandler ReadError;

        public TcpReader(TcpClient tcpClient)
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
                ReadCompleted?.Invoke(this, new TcpReaderReadCompletedEventArgs(ar.AsyncState as byte[], TcpClient));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
