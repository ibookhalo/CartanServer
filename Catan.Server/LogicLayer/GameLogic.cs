using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan.Game;
using Catan.Network.Messaging;
using System.Net;

namespace Catan.Server.LogicLayer
{
    public class GameLogic : Interfaces.ILogicLayer
    {
        private Interfaces.INetworkLayer networkLayer;
        private CatanClient currentClient;
        private List<CatanClient> catanClients;

        public GameLogic()
        { }
        private CatanClient getNextClient()
        {
            if (currentClient==null)
            {
                // Der letzte Spieler ist als erster dran 
                catanClients.Reverse();
                return catanClients.First();
            }
            else
            {
                return catanClients[(catanClients.IndexOf(currentClient) + 1) % catanClients.Count];
            }

        }
        public void ServerFinishedListening(List<CatanClient> catanClients)
        {
            // Let clients play catan !

            this.catanClients = catanClients;
            
            currentClient = getNextClient();

            GameStateMessage gameState = new GameStateMessage(catanClients, currentClient, LogicLayer.RandomHexagonFieldInfoGenerator.Generate());

            networkLayer.SendBroadcastMessage(gameState);
        }
        public void StartServer()
        {
            this.networkLayer = new NetworkLayer.CatanServer(1, new IPEndPoint(IPAddress.Parse("127.0.0.1"), 123), "ibo",this);
            this.networkLayer.StartTcpListener();
        }
        public void ClientGameStateChangeMessageReceived(CatanClientStateChangeMessage catanClientStateChangeMessage)
        {

        }
    }
}
