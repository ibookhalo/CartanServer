using Cartan.Server;
using System.Net;

namespace Cartan
{
    class Program
    {
        static void Main(string[] args)
        {
            CartanTcpServer s = new CartanTcpServer(new IPEndPoint( IPAddress.Parse("127.0.0.1"),123));
            s.Start();
        }
    }
}
