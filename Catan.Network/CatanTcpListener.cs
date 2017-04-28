using Cartan.Network.Events;
using Cartan.Network.Messaging;
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
        public delegate void CatanClientConnectedHandler(object tcpListener, CatanClientConnectedEventArgs e);
        /*##############################################################################################*/
        public event CatanClientConnectedHandler CatanClientConnected;

        private string authPassword;
        public bool IsListening { private set; get; }
        private TcpListener tcpListener;
        private IPEndPoint ipEndPoint;

        public CatanTcpListener(IPEndPoint ipEndPoint,string authPassword)
        {
            this.authPassword = authPassword;
            this.ipEndPoint = ipEndPoint;
        }
        public void Start()
        {
            this.tcpListener = new TcpListener(ipEndPoint);
            this.IsListening = true;

            while (IsListening)
            {
                try
                {
                    tcpListener.Start();
                    var netMessageStreamReader = new NetworkMessageStreamReader(tcpListener.AcceptTcpClient());
                    netMessageStreamReader.ReadCompleted += authMessageRead;
                    netMessageStreamReader.ReadError += authMessageReadError;
                    netMessageStreamReader.ReadAsync();
                }
                catch (Exception ex)
                {

                }
                    
            }
            tcpListener.Stop();
        }

        private void authMessageReadError(object catanNetworkMessageStreamReader, NetworkMessageReadErrorEventArgs e)
        {
            e.TcpClient.Close();
        }

        private void authMessageRead(object catanNetworkMessageStreamReader, NetworkMessageReadCompletedEventArgs readCompletedEventArgs)
        {
            try
            {
                CatanClientAuthenticationMessage message = null;
                if ((readCompletedEventArgs.NetworkMessage is CatanClientAuthenticationMessage) &&
                    (message = readCompletedEventArgs.NetworkMessage as CatanClientAuthenticationMessage).Password.Equals(authPassword))
                {
                    CatanClient catanClient = new CatanClient(readCompletedEventArgs.TcpClient, message.Playername);
                    CatanClientConnected?.Invoke(this, new CatanClientConnectedEventArgs(catanClient));
                }
                else
                {
                    readCompletedEventArgs.TcpClient.Close();
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
