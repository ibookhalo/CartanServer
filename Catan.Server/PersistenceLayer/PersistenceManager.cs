using Catan.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Catan.Server.PersistenceLayer
{
   public class PersistenceManager : IPersistenceLayer
    {
        public bool ExistPlayer(string mail, string password)
        {
            return new PlayerModel().ExistPlayer(mail, password);
        }
    }
}
