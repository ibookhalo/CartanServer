using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartan.Network.CartanClientMessages
{
    [Serializable]
    public class CartanClientAuthenticationMessage : NetworkClientMessage
    {
        public string Playername { private set; get; }
        public string Password { private set; get; }
        public CartanClientAuthenticationMessage() { }
        public CartanClientAuthenticationMessage(string password,string playername)
        {
            this.Password = password;
            this.Playername = playername;
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
