using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Network.Messaging
{
    [Serializable]
    public class GameStateMessage:NetworkMessage
    {
        public enum GameState { Running, WaitingForClients }


        public List<CatanClient> Clients { private set; get; }
        public CatanClient CurrentClient { private set; get; }
        public GameState CurrentGameState { set; get; }

        public GameStateMessage(List<CatanClient> clients,CatanClient currentClient,GameState currentGameState)
        {
            this.Clients = clients;
            this.CurrentClient = currentClient;
            this.CurrentGameState = currentGameState;
        }
    }
}
