using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Server.Interfaces
{
    interface INetworkLayer
    {
        void StartTcpListener();
        void SendBroadcastMessage(Network.Messaging.NetworkMessage networkMessage);
    }
}
