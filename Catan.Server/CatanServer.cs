using Cartan.Network.Events;
using Cartan.Network.Messaging;
using Catan.Network;
using Catan.Network.Messaging.ClientMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Server
{
    public class CatanServer
    {
        private TcpListener tcpListener;
        private List<CatanClient> catanClients;
        private ushort maxPlayerCount;

        public CatanServer(ushort maxPlayerCount,IPEndPoint ipEndPoint,string authPassword)
        {
            this.tcpListener = new TcpListener(ipEndPoint);
            this.catanClients = new List<CatanClient>();
            this.maxPlayerCount = maxPlayerCount;
        }

        public void Start()
        {
            try
            {
                tcpListener.Start();
                var tcpReader = new TcpReader(tcpListener.AcceptTcpClient());
                tcpReader.ReadCompleted += TcpReader_ReadCompleted;
                tcpReader.ReadAsync();
            }
            catch (Exception ex)
            {

            }
        }

        private void TcpReader_ReadCompleted(object obj, TcpReaderReadCompletedEventArgs  e)
        {
            
        }

        private void CatanListener_CatanClientConnected(object tcpListener, Cartan.Network.Events.CatanClientConnectedEventArgs e)
        {
            catanClients.Add(e.CatanClient);
            Console.WriteLine($"Catan player joined: {e.CatanClient.PlayerName}");

            if (catanClients.Count + 1 == maxPlayerCount)
            {
                this.tcpListener.Stop();
            }

            if (catanClients.Count==maxPlayerCount)
            {
                letClientsPlayCatan();
            }
        }

        private void letClientsPlayCatan()
        {
            // allen Clients die Liste der mit dem Server verbundenen Clients senden
            sendClientslistToClients();


            // Der letzte Spieler ist als erster dran 
            catanClients.Reverse();
        }

        private void sendClientslistToClients()
        {
            foreach (CatanClient catanClient in catanClients)
            {
                
            }
        }
    }
}