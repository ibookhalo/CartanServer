using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Network.Messaging.ClientMessages
{
    class CatanClientAuthenticationMessage : NetworkMessage
    {

        public string Password { get; private set; }
        public string Playername { get; private set; }

        public CatanClientAuthenticationMessage(string password, string playerName)
        {
            this.Password = Password;
            this.Playername = playerName;
        }

        public CatanClientAuthenticationMessage()
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
