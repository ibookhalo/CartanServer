using Cartan.Server;
using System;
using System.Net;

namespace Cartan
{
    class Program
    {
        static void Main(string[] args)
        {
            CartanServer ser = new CartanServer(2, authPassword:"ibo");
            ser.Run();
            Console.Read();
        }
    }
}
