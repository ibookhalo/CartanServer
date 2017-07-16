using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Server.Interfaces
{
    interface IPersistenceLayer
    {
        bool ExistPlayer(string mail, string password);
    }
}
