using Cartan.Network;
using Catan.Network.Events;
using Catan.Network.Messaging;
using Catan.Network.Messaging.ClientMessages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Network
{
    public class CatanTcpListener
    {
        /*########################################    Delegates    ####################################*/
        public delegate void CartanClientConnectedHandler(object tcpListener, ReceivedMessageEventArgs clientArgs);
        public delegate void ReceivedMessageHandler(object tcpListener, ReceivedMessageEventArgs receivedMessage);
        /*##############################################################################################*/
        public event CartanClientConnectedHandler CartanClientConnected;
        public event ReceivedMessageHandler ReceivedMessage;

        private string authPassword;
        public bool IsListening { private set; get; }
        private TcpListener tcpListener;

        public CatanTcpListener(string authPassword)
        {
            this.authPassword = authPassword;
        }
        public void Start()
        {
            this.tcpListener = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 123));
            this.IsListening = true;
           
            while (IsListening)
            {
                try
                {
                    tcpListener.Start();
                    var netStreamReader = new NetworkStreamReader(tcpListener.AcceptTcpClient());
                    netStreamReader.ReadCompleted += CatanTcpListener_ReadCompleted;
                    netStreamReader.ReadAsync();
                    
                }
                catch (Exception ex)
                {

                }
                    
            }
            tcpListener.Stop();
        }

        private void CatanTcpListener_ReadCompleted(object netStreamReader, Cartan.Network.Events.ReadCompletedEventArgs readCompletedEventArgs)
        {
            try
            {
                CatanClientAuthenticationMessage authMessage = new NetworkMessageFormatter<CatanClientAuthenticationMessage>().Deserialize(readCompletedEventArgs.Data);
                if (authMessage != null && authMessage.Password.Equals(authPassword))
                {
                    CatanClient cartanClient = new CatanClient(readCompletedEventArgs.TcpClient, authMessage.Playername);
                    CartanClientConnected?.Invoke(tcpListener, new ReceivedMessageEventArgs(cartanClient, authMessage));

                    // todo
                    byte[] buffer = new byte[NetworkMessage.MAX_DATA_SIZE_IN_BYTES];
                    result.Value.GetStream().BeginRead(buffer, 0, buffer.Length, clientReceivedMessageBeginReadCallback, new KeyValuePair<byte[], CatanClient>(buffer, cartanClient));
                }
                else
                {
                    result.Value.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void clienAuthenticationBeginReadCallback(IAsyncResult ar)
        {
            try
            {
                KeyValuePair<byte[], TcpClient> result = (KeyValuePair<byte[], TcpClient>)ar.AsyncState;
                result.Value.GetStream().EndRead(ar);

                CatanClientAuthenticationMessage authMessage = new NetworkMessageFormatter<CatanClientAuthenticationMessage>().Deserialize(result.Key as byte[]);
                if (authMessage != null && authMessage.Password.Equals(authPassword))
                {
                    CatanClient cartanClient = new CatanClient(result.Value, authMessage.Playername);
                    CartanClientConnected?.Invoke(tcpListener, new ReceivedMessageEventArgs(cartanClient, authMessage));


                    byte[] buffer = new byte[NetworkMessage.MAX_DATA_SIZE_IN_BYTES];
                    result.Value.GetStream().BeginRead(buffer, 0, buffer.Length, clientReceivedMessageBeginReadCallback, new KeyValuePair<byte[], CatanClient>(buffer, cartanClient));
                }
                else
                {
                    result.Value.Close();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void clientReceivedMessageBeginReadCallback(IAsyncResult ar)
        {
            try
            {
                KeyValuePair<byte[], CatanClient> result = (KeyValuePair<byte[], CatanClient>)ar.AsyncState;
                result.Value.TcpClient.GetStream().EndRead(ar);

                NetworkMessage message = new NetworkMessageFormatter<NetworkMessage>().Deserialize(result.Key as byte[]);
                if (message != null)
                {
                    ReceivedMessage?.Invoke(tcpListener, new ReceivedMessageEventArgs(result.Value, message));
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public void Stop()
        {
            this.IsListening = false;
        }
    }
}
