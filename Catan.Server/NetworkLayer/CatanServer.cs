using Catan.Network.EventArgs;
using Catan.Network.Messaging;
using Catan.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Catan.Game;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Catan.Server.Interfaces;

namespace Catan.Server.NetworkLayer
{
    public class CatanServer:Interfaces.INetworkLayer
    {
        private Interfaces.ILogicLayer iLogicLayer;

        private TcpListener tcpListener;
        private List<CatanClient> catanClients;
        private int maxClients;
        private string authPassword;

        public CatanServer(int maxClients, IPEndPoint ipEndPoint, string authPassword, Interfaces.ILogicLayer iLogicLayer)
        {
            this.iLogicLayer = iLogicLayer;

            this.tcpListener = new TcpListener(ipEndPoint);
            this.catanClients = new List<CatanClient>();
            this.maxClients = maxClients;
            this.authPassword = authPassword;
        }

        #region TcpListener and Clients authentication 
        public void StartTcpListener()
        {
            try
            {
                tcpListener.Start();
                while (true)
                {
                    NetworkMessageReader netMessageReader = new NetworkMessageReader(tcpListener.AcceptTcpClient());
                    netMessageReader.ReadCompleted += NetMessageReader_CatanClientAuth_Request_ReadCompleted;
                    netMessageReader.ReadError += (obj, e) => { e.TcpClient.Close(); };
                    netMessageReader.ReadAsync();
                }
            }
            catch (SocketException socketEx)
            {
                if (socketEx.SocketErrorCode == SocketError.Interrupted)
                {
                    // Tcplistener wurde geschlossen. Durch Stop()
                    while (true) Console.ReadLine();  // Server läuft weiter ...
                }
                else
                {
                    iLogicLayer.ThrowException(socketEx);
                }
            }
            catch (Exception ex)
            {
                iLogicLayer.ThrowException(ex);
            }
        }
        private void NetMessageReader_CatanClientAuth_Request_ReadCompleted(object obj, NetworkMessageReaderReadCompletedEventArgs e)
        {
            lock (catanClients)
            {
                try
                {
                    CatanClientAuthenticationRequestMessage authMessage = e.NetworkMessage as CatanClientAuthenticationRequestMessage;
                    // Checking auth
                    if (iLogicLayer.IsAuthenticationRequestMessageOk(authMessage))
                    {
                        NetworkMessageWriter netMessageWriter = new NetworkMessageWriter(e.TcpClient);
                        netMessageWriter.WriteError += (o, ee) => { ee.TcpClient.Close(); };
                        netMessageWriter.WriteCompleted += NetMessageWriter_CatanClientAuth_Response_WriteCompleted;
                        netMessageWriter.WriteAsync(new CatanClientAuthenticationResponseMessage(authMessage));
                    }
                    else
                    {
                        e.TcpClient.Close(); // Client wird rausgeworfen, wenn Pass falsch ist !!
                    }
                }
                catch (Exception ex)
                {
                    e.TcpClient.Close();
                    iLogicLayer.ThrowException(ex);
                }
            }
        }

        private void NetMessageWriter_CatanClientAuth_Response_WriteCompleted(object obj, NetworkMessageWriterWriteCompletedEventArgs e)
        {
            //getDisconnectedClientConnections(silently:true);

            // new catan client
            CatanClient catanClient = new CatanClient(e.TcpClient, e.TcpClient.Client.RemoteEndPoint.ToString(),(e.NetMessage as CatanClientAuthenticationResponseMessage).AuthRequestMessage.Playername);
            catanClients.Add(catanClient);

            Console.WriteLine($"Catan player joined: {catanClient.Name}");
            if (catanClients.Count == maxClients)
            {
                // Start game 
                tcpListener.Stop();
                Thread.Sleep(1000);

                catanClients.ForEach(client => 
                {
                    var netReader = new NetworkMessageReader(client.TcpClient);
                    netReader.ReadCompleted += NetMessageReader_CatanClientMessageReceived;
                    netReader.ReadAsync(readLoop:true);

                }); // error handling wird ignoriert !!!

                iLogicLayer.ServerFinishedListening(catanClients);
            }
        }
        #endregion

        #region Authenticated clients messages handling
        private void NetMessageReader_CatanClientMessageReceived(object obj, NetworkMessageReaderReadCompletedEventArgs e)
        {
            if (e.NetworkMessage is CatanClientStateChangeMessage)
            {
                var gameStateMessage = e.NetworkMessage as CatanClientStateChangeMessage;
                if (catanClients.Exists(client=>client.ID==gameStateMessage.ClientID && client.IPAddressPortNr.Equals(e.TcpClient.Client.RemoteEndPoint.ToString())))
                {
                    iLogicLayer.ClientGameStateChangeMessageReceived(gameStateMessage);
                }
            }
        }
        #endregion

        #region Connection reliability
        private bool isCatanClientConnected(CatanClient catanClient)
        {
            try
            {
                return !(catanClient.TcpClient.Client.Poll(500, SelectMode.SelectRead)      && 
                         catanClient.TcpClient.Client.Poll(500, SelectMode.SelectWrite)     && 
                        !catanClient.TcpClient.Client.Poll(500, SelectMode.SelectError));
            }
            catch (Exception ex)
            {
                iLogicLayer.ThrowException(ex); 
                return false;
            }
        }
     
        private List<CatanClient> getDisconnectedClientConnections(bool silently = false)
        {
            List<CatanClient> disconnectedClients = new List<CatanClient>();
            lock (catanClients)
            {
                foreach (var catanClient in this.catanClients)
                {
                    if (!isCatanClientConnected(catanClient))
                    {
                        disconnectedClients.Add(catanClient);
                        Console.WriteLine($"Client disconnected: {catanClient.IPAddressPortNr} ");
                    }
                }
                catanClients.RemoveAll(catanClient => 
                disconnectedClients.Exists(disconnectedClient=>disconnectedClient==catanClient));
            }
            return disconnectedClients;
        }
        #endregion

        #region Broadcastmessaging
        private void NetMessageWriter_BroadcastMessage_WriteError(object obj, NetworkMessageWriterWriteErrorEventArgs e)
        {

        }
        private void NetMessageWriter_BroadcastMessage_WriteCompleted(object obj, NetworkMessageWriterWriteCompletedEventArgs e)
        {
            Console.WriteLine($"Broadcast message sent {catanClients.Find(c=>c.TcpClient==e.TcpClient).IPAddressPortNr}");
        }

        public void SendBroadcastMessage(NetworkMessage networkMessage)
        {
            try
            {
                //getDisconnectedClientConnections();
                foreach (CatanClient catanClient in catanClients)
                {
                    NetworkMessageWriter netMessageWriter = new NetworkMessageWriter(catanClient.TcpClient);
                    netMessageWriter.WriteCompleted += NetMessageWriter_BroadcastMessage_WriteCompleted;
                    netMessageWriter.WriteError += NetMessageWriter_BroadcastMessage_WriteError;
                    netMessageWriter.WriteAsync(networkMessage);
                }
            }
            catch (CatanClientDisconnectedException ex)
            {
               
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}