using Cartan.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cartan.Server
{
    public class CartanServer
    {
        private CartanTcpListener cartanListener;
        private List<CartanClient> cartanClients;
        private ushort playerCount;

        public CartanServer(ushort playerCount, string authPassword)
        {
            this.cartanListener = new CartanTcpListener(authPassword);
            this.cartanClients = new List<CartanClient>();
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

            Console.WriteLine($"Cartan: {clientArgs.CartanClient.PlayerName} connected. Message = {clientArgs.Message}");
        }

    }
}