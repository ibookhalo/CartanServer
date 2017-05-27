using Catan.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan.Network.Messaging;

namespace Catan.Server.Interfaces
{
    public interface ILogicLayer
    {
        void StartServer();
        void ServerFinishedListening(List<CatanClient> catanClients);
        void ClientGameStateChangeMessageReceived(CatanClientStateChangeMessage catanClientStateChangeMessage);
        void ThrowException(Exception ex);
    }
}
