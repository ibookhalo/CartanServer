using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Network.Messaging.ClientMessages
{
    [Serializable]
    public class CatanClientAuthenticationMessage : NetworkMessage
    {

        public string Password { get; private set; }
        public string Playername { get; private set; }

        public CatanClientAuthenticationMessage(string password, string playerName)
        {
            this.Password = password;
            this.Playername = playerName;
        }

    }
}
