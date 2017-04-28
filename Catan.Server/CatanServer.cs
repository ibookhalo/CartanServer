using Cartan.Network.Messaging;
using Catan.Network;
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
        private CatanTcpListener catanListener;
        private List<CatanClient> catanClients;
        private ushort maxPlayerCount;

        public CatanServer(ushort playerCount,IPEndPoint ipEndPoint,string authPassword)
        {
            this.catanListener = new CatanTcpListener(ipEndPoint, authPassword);
            this.catanClients = new List<CatanClient>();
            this.maxPlayerCount = playerCount;
        }

        public void Start()
        {
            catanListener.CatanClientConnected += CatanListener_CatanClientConnected; ;
            catanListener.Start();
        }

        private void CatanListener_CatanClientConnected(object tcpListener, Cartan.Network.Events.CatanClientConnectedEventArgs e)
        {
            catanClients.Add(e.CatanClient);
            Console.WriteLine($"Catan player joined: {e.CatanClient.PlayerName}");

            if (catanClients.Count + 1 == maxPlayerCount)
            {
                catanListener.Stop();
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