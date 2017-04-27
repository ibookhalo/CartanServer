using Cartan.Network.Events;
using Cartan.Network.Messaging;
using Cartan.Network.Messaging.ClientMessages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Cartan.Network
{
    public class CartanTcpListener
    {
        /*########################################    Delegates    ####################################*/
        public delegate void CartanClientConnectedHandler(object tcpListener, CartanClientReceivedMessageEventArgs clientArgs);
        public delegate void ReceivedMessageHandler(object tcpListener, CartanClientReceivedMessageEventArgs receivedMessage);
        /*##############################################################################################*/
        public event CartanClientConnectedHandler CartanClientConnected;
        public event ReceivedMessageHandler ReceivedMessage;

        private string authPassword;
        public bool IsListening { private set; get; }
        private TcpListener tcpListener;

        public CartanTcpListener(string authPassword)
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
                    this.tcpListener.Start();
                    TcpClient tcpClient = this.tcpListener.AcceptTcpClient();
                    NetworkStream netStream = tcpClient.GetStream();
                    if (netStream.CanRead)
                    {
                        byte[] buffer = new byte[new CartanClientAuthenticationMessage().MaxDataSizeInBytes];
                        netStream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(clienAuthenticationBeginReadCallback), new KeyValuePair<byte[], TcpClient>(buffer, tcpClient));
                    }
                }
                catch (Exception ex)
                {

                }
                    
            }
            tcpListener.Stop();
           
        }

        private void clienAuthenticationBeginReadCallback(IAsyncResult ar)
        {
            try
            {
                KeyValuePair<byte[], TcpClient> result = (KeyValuePair<byte[], TcpClient>)ar.AsyncState;
                result.Value.GetStream().EndRead(ar);

                CartanClientAuthenticationMessage authMessage = new NetworkMessageFormatter<CartanClientAuthenticationMessage>().Deserialize(result.Key as byte[]);
                if (authMessage != null && authMessage.Password.Equals(authPassword))
                {
                    CartanClient cartanClient = new CartanClient(result.Value, authMessage.Playername);
                    CartanClientConnected?.Invoke(tcpListener, new CartanClientReceivedMessageEventArgs(cartanClient, authMessage));


                    byte[] buffer = new byte[NetworkMessage.MAX_DATA_SIZE_IN_BYTES];
                    result.Value.GetStream().BeginRead(buffer, 0, buffer.Length, clientReceivedMessageBeginReadCallback, new KeyValuePair<byte[], CartanClient>(buffer, cartanClient));
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
                KeyValuePair<byte[], CartanClient> result = (KeyValuePair<byte[], CartanClient>)ar.AsyncState;
                result.Value.TcpClient.GetStream().EndRead(ar);

                NetworkMessage message = new NetworkMessageFormatter<NetworkMessage>().Deserialize(result.Key as byte[]);
                if (message != null)
                {
                    ReceivedMessage?.Invoke(tcpListener, new CartanClientReceivedMessageEventArgs(result.Value, message));
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
