
using Catan.Server;
using System;
using System.Net;

namespace Cartan
{
    class Program
    {
        static void Main(string[] args)
        {
            CatanServer ser = new CatanServer(2, authPassword:"ibo");
            ser.Run();
            Console.Read();
        }
    }
}
