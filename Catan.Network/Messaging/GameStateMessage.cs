using Catan.Game;
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
        public List<CatanClient> Clients { private set; get; }
        public CatanClient CurrentClient { private set; get; }
        public Hexagon[][] HexagoneFields { private set; get; }

        public GameStateMessage(List<CatanClient> clients,CatanClient currentClient, Hexagon[][] hexagoneFields)
        {
            this.Clients = clients;
            this.CurrentClient = currentClient;
            this.HexagoneFields = hexagoneFields;
        }
    }
}
