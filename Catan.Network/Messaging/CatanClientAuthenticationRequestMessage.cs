using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Network.Messaging
{
    [Serializable]
    public class CatanClientAuthenticationRequestMessage : NetworkMessage
    {

        public string Password { get; private set; }
        public string Playername { get; private set; }
        public string MailAddress { get; set; }

        public CatanClientAuthenticationRequestMessage(string mailAdress, string password, string playerName)
        {
            this.Password = password;
            this.Playername = playerName;
            this.MailAddress = mailAdress;
        }

    }
}
