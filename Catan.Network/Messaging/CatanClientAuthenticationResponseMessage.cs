using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Network.Messaging
{
    [Serializable]
    public class CatanClientAuthenticationResponseMessage:NetworkMessage
    {
        public CatanClientAuthenticationRequestMessage AuthRequestMessage { private set; get; }

        public CatanClientAuthenticationResponseMessage(CatanClientAuthenticationRequestMessage authMessage)
        {
            this.AuthRequestMessage = authMessage;
        }
    }
}
