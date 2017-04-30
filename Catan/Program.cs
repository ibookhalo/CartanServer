
using Catan.Server;
using System;
using System.Net;

namespace Catan
{
    class Program
    {
        static void Main(string[] args)
        {
            CatanServer ser = new CatanServer(2, new IPEndPoint(IPAddress.Parse("127.0.0.1"), 123), "ibo");
            ser.Start();
            Console.Read();
        }
    }
}
