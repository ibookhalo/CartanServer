
using Catan.Game;
using Catan.Server;
using System;
using System.Collections.Generic;
using System.Net;

namespace Catan
{
    class Program
    {
    
        static void Main(string[] args)
        {
            Catan.Server.LogicLayer.GameLogic logic = new Server.LogicLayer.GameLogic();
            logic.StartServer();
        }
    }
}
