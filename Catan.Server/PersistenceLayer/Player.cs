using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Server.PersistenceLayer
{
    [Serializable]
    public class Player
    {
        public string Pass;
        public string EMail;
        public string NickName;
        public int ID;

        public Player() { }
        public Player(string mail,string password,string nickname)
        {
            this.EMail = mail;
            this.Pass = password;
            this.NickName = nickname;
        }        
    }
}
