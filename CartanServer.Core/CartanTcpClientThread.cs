using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cartan.Server
{
    class CartanTcpClientThread
    {
        internal TcpClient tcpClient;
        internal Thread thread;

        internal CartanTcpClientThread(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            this.thread = new Thread(new ThreadStart(runThread));
        }

        private void runThread()
        {
            while (tcpClient.Connected)
            {
                byte[] data = null;
                if ((data=ReadBytes(2))!= null)
                {
                    Console.Write(Encoding.UTF8.GetString(data));
                }
            }
        }
        private byte[] ReadBytes(int bufSize)
        {
            try
            {
                var netStream = tcpClient.GetStream();
                if (netStream.CanRead)
                {
                    tcpClient.ReceiveBufferSize = bufSize;
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        byte[] buffer = new byte[bufSize];
                        do
                        {
                            int read = netStream.Read(buffer, 0, buffer.Length);
                            if (read <= 0)
                            {
                                netStream.Close();
                                tcpClient.Close();
                                return null;
                            }
                            memoryStream.Write(buffer, 0, read);
                        } while (netStream.DataAvailable);

                        return memoryStream.ToArray();
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal void Start()
        {
            this.thread.Start();
        }
    }
}
