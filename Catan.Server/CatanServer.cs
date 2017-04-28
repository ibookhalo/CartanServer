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

            if (catanClients.Count + 1 >= maxPlayerCount)
            {
                catanListener.Stop();
            }

            Console.WriteLine($"Catan: {e.CatanClient.PlayerName}");
        }
       
    }
}