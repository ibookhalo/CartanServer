using Catan.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Server
{
    public class CatanServer
    {
        private CatanTcpListener cartanListener;
        private List<CatanClient> cartanClients;
        private ushort playerCount;

        public CatanServer(ushort playerCount, string authPassword)
        {
            this.cartanListener = new CatanTcpListener(authPassword);
            this.cartanClients = new List<CatanClient>();
            this.playerCount = playerCount;
        }

        public void Run()
        {
            cartanListener.CartanClientConnected += CartanListener_CartanClientConnected;
            cartanListener.ReceivedMessage += ReceivedMessage;
            cartanListener.Start();
        }

        private void ReceivedMessage(object tcpListener, Network.Events.ReceivedMessageEventArgs receivedMessage)
        {
            
        }

        private void CartanListener_CartanClientConnected(object tcpListener, Network.Events.ReceivedMessageEventArgs clientArgs)
        {
            cartanClients.Add(clientArgs.CartanClient);

            if (cartanClients.Count + 1 >= playerCount)
                cartanListener.Stop();

            Console.WriteLine($"Catan: {clientArgs.CartanClient.PlayerName} connected. Message = {clientArgs.Message}");
        }

    }
}