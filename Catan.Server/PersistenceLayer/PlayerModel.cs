using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Catan.Server.PersistenceLayer
{
    class PlayerModel : IPlayer
    {
        public bool ExistPlayer(string mail, string password)
        {
            using (IDbConnection db = new MySql.Data.MySqlClient.MySqlConnection("server=localhost;uid=root;database=catan;"))
            {
               return db.Query<Player>($"SELECT * FROM player WHERE email='{mail}' AND pass='{password}'").ToList().Count>0;
            }
        }
    }
}
