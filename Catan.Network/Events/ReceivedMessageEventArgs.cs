
using Catan.Network.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Network.Events
{
    public class ReceivedMessageEventArgs:EventArgs
    {
        public CatanClient CartanClient { private set; get; }
        public NetworkMessage Message { private set; get; }

        public ReceivedMessageEventArgs(CatanClient client, NetworkMessage message)
        {
            this.CartanClient = client;
            this.Message = message;
        }
    }
}
