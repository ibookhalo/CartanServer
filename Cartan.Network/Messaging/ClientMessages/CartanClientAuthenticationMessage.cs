using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartan.Network.Messaging.ClientMessages
{
    class CartanClientAuthenticationMessage : NetworkMessage
    {

        public string Password { get; private set; }
        public string Playername { get; private set; }

        public CartanClientAuthenticationMessage(string password, string playerName)
        {
            this.Password = Password;
            this.Playername = playerName;
        }

        public CartanClientAuthenticationMessage()
        {
        }

        public override ushort MaxDataSizeInBytes
        {
            get
            {
                return 1024;
            }
        }
    }
}
