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
        private Interfaces.ILogicLayer logicLayer;

        private TcpListener tcpListener;
        private List<CatanClient> catanClients;
        private ushort maxClients;
        private string authPassword;

        public CatanServer(ushort maxClients, IPEndPoint ipEndPoint, string authPassword, Interfaces.ILogicLayer logicLayer)
        {
            this.logicLayer = logicLayer;

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
                    var netMessageReader = new NetworkMessageReader(tcpListener.AcceptTcpClient());

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
            }
            catch (Exception ex)
            {

            }
        }
        private void NetMessageReader_CatanClientAuth_Request_ReadCompleted(object obj, NetworkMessageReaderReadCompletedEventArgs e)
        {
            lock (catanClients)
            {
                try
                {
                    CatanClientAuthenticationRequestMessage authMessage = e.NetworkMessage as CatanClientAuthenticationRequestMessage;
                    if (authMessage.Password.Equals(authPassword))
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
                }
            }
        }

        private void NetMessageWriter_CatanClientAuth_Response_WriteCompleted(object obj, NetworkMessageWriterWriteCompletedEventArgs e)
        {
            checkClientConnections(true);

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

                logicLayer.ServerFinishedListening(catanClients);
            }
        }
        #endregion

        #region Authenticated clients messages handling
        private void NetMessageReader_CatanClientMessageReceived(object obj, NetworkMessageReaderReadCompletedEventArgs e)
        {
            if (e.NetworkMessage is CatanClientStateChangeMessage)
            {
                var gameState = e.NetworkMessage as CatanClientStateChangeMessage;
                if (!catanClients.Exists(client=>client.ID==gameState.ClientID && client.IPAddressPortNr.Equals(e.TcpClient.Client.RemoteEndPoint.ToString())))
                {

                }
                logicLayer.ClientGameStateChangeMessageReceived(e.NetworkMessage as CatanClientStateChangeMessage);
            }
        }
        #endregion

        #region Connection reliability
        private bool isCatanClientConnected(CatanClient catanClient)
        {
            try
            {
                bool isDisconnected = (catanClient.TcpClient.Client.Poll(2, SelectMode.SelectRead) && catanClient.TcpClient.Client.Poll(2, SelectMode.SelectWrite) && !catanClient.TcpClient.Client.Poll(2, SelectMode.SelectError));

                if (isDisconnected)
                {
                    Console.WriteLine($"{catanClient.Name} is disconnected !");
                }
                return !isDisconnected;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
     
        private void checkClientConnections(bool silently = false)
        {
            lock (catanClients)
            {
                if (catanClients.RemoveAll(catanClient => !isCatanClientConnected(catanClient)) > 0 && !silently)
                    throw new CatanClientDisconnectedException();
            }
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
                checkClientConnections();
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