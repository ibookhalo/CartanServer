using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Catan.Game
{
    [Serializable]
    public class CatanClient
    {
        private static uint id;

        [NonSerialized]
        private TcpClient tcpClient;
        public TcpClient TcpClient { get { return tcpClient; }}

        public IPEndPoint IPAddress { get { return ((IPEndPoint)tcpClient?.Client.RemoteEndPoint); } }

        // Spielfiguren
        public SpielFigurenContainer SpielfigurenContainer { private set; get; }

        // Karten


        public Color Color { private set; get; }

        public string Name { private set; get; }
        public uint ID { private set; get; }
   
        public uint Siegpunkte { set; get; }
     

        public CatanClient(TcpClient tcpClient, string name)
        {
            this.tcpClient = tcpClient;
            this.Name = name;

            this.SpielfigurenContainer = new Game.SpielFigurenContainer();

            this.ID = ++id;
        }
    }
}
