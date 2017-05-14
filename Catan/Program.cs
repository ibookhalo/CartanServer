
using Catan.Server;
using System;
using System.Net;

namespace Catan
{
    class Program
    {
    
        static void Main(string[] args)
        {
            Catan.Server.LogicLayer.GameLogic lo = new Server.LogicLayer.GameLogic();
            lo.StartServer();
        }
    }
}
