
using Catan.Game;
using Catan.Server;
using System;
using System.Collections.Generic;
using System.Net;

namespace Catan
{
    class Program
    {
    
        static void Main(string[] args)
        {
            Catan.Server.LogicLayer.GameLogic logic = new Server.LogicLayer.GameLogic();
            logic.StartServer();
            
            List<Catan.Game.CatanClient> clients = new List<Game.CatanClient>();

            clients.Add(new Game.CatanClient(null, "0.0.0.1", "Player 1"));
            clients.Add(new Game.CatanClient(null, "0.0.0.2", "Player 2"));
            clients.Add(new Game.CatanClient(null, "0.0.0.3", "Player 3"));
            clients.Add(new Game.CatanClient(null, "0.0.0.4", "Player 4"));

            logic.ServerFinishedListening(clients);

            var newSpielFiguren = new List<SpielFigur>();
            newSpielFiguren.Add(new Siedlung(new HexagonPosition(0, 0), new HexagonPoint(1)));

            logic.ClientGameStateChangeMessageReceived(new Network.Messaging.CatanClientStateChangeMessage(newSpielFiguren, null, null, clients[3].ID));
        }
    }
}
