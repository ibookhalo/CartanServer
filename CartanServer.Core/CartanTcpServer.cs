using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CartanServer.Core
{
    public class CartanTcpServer
    {
        private TcpListener tcpListner;
        public IPEndPoint IPEndPoint { private set; get; }
        private List<CartanTcpClientThread> tcpClientThreads;
        public CartanTcpServer(IPEndPoint ipEndPoint)
        {
            this.IPEndPoint = ipEndPoint;
            this.tcpClientThreads = new List<CartanTcpClientThread>();
        }

        public void Start()
        {
            try
            {
                tcpListner = new TcpListener(IPEndPoint);
                tcpListner.Start();
                while (true)
                {
                    while (tcpClientThreads.Count<4)
                    {
                        CartanTcpClientThread clientThread = new CartanTcpClientThread(tcpListner.AcceptTcpClient());
                        clientThread.Start();
                        tcpClientThreads.Add(clientThread);
                    }
                    //
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
