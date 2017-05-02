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

namespace Catan.Server
{
    public class CatanServer
    {
        private TcpListener tcpListener;
        private List<CatanClient> catanClients;
        private ushort maxPlayerCount;
        private string authPassword;
        private bool listening;
        private CatanClient currentClient;

        public CatanServer(ushort maxPlayerCount, IPEndPoint ipEndPoint, string authPassword)
        {
            this.tcpListener = new TcpListener(ipEndPoint);
            this.catanClients = new List<CatanClient>();
            this.maxPlayerCount = maxPlayerCount;
            this.authPassword = authPassword;
        }

        public void Start()
        {
            try
            {
                listening = true;
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
                if (socketEx.SocketErrorCode==SocketError.Interrupted)
                {
                    // Tcplistener wurde geschlossen. Durch Stop()
                    return;
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
                    CatanClientAuthenticationMessage authMessage = e.NetworkMessage as CatanClientAuthenticationMessage;
                    if (authMessage.Password.Equals(authPassword))
                    {
                        NetworkMessageWriter netMessageWriter = new NetworkMessageWriter(e.TcpClient);
                        netMessageWriter.WriteError += (o, ee) => { ee.TcpClient.Close(); };
                        netMessageWriter.WriteCompleted += NetMessageWriter_CatanClientAuth_Response_WriteCompleted;
                        netMessageWriter.WriteAsync(authMessage);
                    }
                    else
                    {
                        e.TcpClient.Close();
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

            // new catan-client
            CatanClient catanClient = new CatanClient(e.TcpClient, (e.NetMessage as CatanClientAuthenticationMessage).Playername);
            catanClients.Add(catanClient);
          
            Console.WriteLine($"Catan player joined: {catanClient.PlayerName}");
            if (catanClients.Count == maxPlayerCount)
            {
                tcpListener.Stop();
                letClientsPlayCatan();
            }
        }

        private bool isCatanClientConnected(CatanClient catanClient)
        {
            try
            {
                bool isDisconnected = (catanClient.TcpClient.Client.Poll(2, SelectMode.SelectRead) && catanClient.TcpClient.Client.Poll(2, SelectMode.SelectWrite) && !catanClient.TcpClient.Client.Poll(2, SelectMode.SelectError));

                if (isDisconnected)
                {
                    Console.WriteLine($"{catanClient.PlayerName} is disconnected !");
                }
                return !isDisconnected;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void letClientsPlayCatan()
        {
            // Der letzte Spieler ist als erster dran 
            catanClients.Reverse();

            currentClient = catanClients.First();
            GameStateMessage gameState = new GameStateMessage(catanClients, currentClient,GameStateMessage.GameState.Running);
            sendBroadcastMessage(gameState);

        }
        private void checkClientConnections(bool silently=false)
        {
            lock (catanClients)
            {
                if (catanClients.RemoveAll(catanClient => !isCatanClientConnected(catanClient)) > 0 && !silently)
                        throw new CatanClientDisconnectedException();
            }
        }

        public void sendBroadcastMessage(NetworkMessage netMessage)
        {
            try
            {
                checkClientConnections();
                foreach (CatanClient catanClient in catanClients)
                {
                    NetworkMessageWriter netMessageWriter = new NetworkMessageWriter(catanClient.TcpClient);
                    netMessageWriter.WriteCompleted += NetMessageWriter_BroadcastMessage_WriteCompleted;
                    netMessageWriter.WriteError += NetMessageWriter_BroadcastMessage_WriteError;
                    netMessageWriter.WriteAsync(netMessage);
                }
            }
            catch (CatanClientDisconnectedException ex)
            {

            }
            catch (Exception ex)
            {

            }
        }

        private void NetMessageWriter_BroadcastMessage_WriteError(object obj, NetworkMessageWriterWriteErrorEventArgs e)
        {

        }

        private void NetMessageWriter_BroadcastMessage_WriteCompleted(object obj, NetworkMessageWriterWriteCompletedEventArgs e)
        {
            Console.WriteLine($"Broadcast message sent");
        }
    }
}