using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Network.Messaging.ServerMessages
{
    [Serializable]
    public class GameStateMessage:NetworkMessage
    {
        public List<CatanClient> Clients { private set; get; }
        public CatanClient CurrentClient { private set; get; }
        public GameStateMessage(List<CatanClient> clients,CatanClient currentClient)
        {
            this.Clients = clients;
            this.CurrentClient = currentClient;
        }
    }
}
