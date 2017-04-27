
using Cartan.Network.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartan.Network.Events
{
    public class CartanClientReceivedMessageEventArgs:EventArgs
    {
        public CartanClient CartanClient { private set; get; }
        public NetworkMessage Message { private set; get; }

        public CartanClientReceivedMessageEventArgs(CartanClient client, NetworkMessage message)
        {
            this.CartanClient = client;
            this.Message = message;
        }
    }
}
