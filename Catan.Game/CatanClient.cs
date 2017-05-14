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
        [NonSerialized]
        private static uint id;

        [NonSerialized]
        private TcpClient tcpClient;
        public TcpClient TcpClient { get { return tcpClient; }}
        public string IPAddressPortNr { get; private set; }
        public uint Port { get; private set; }

        // Spielfiguren
        public SpielFigurenContainer SpielfigurenContainer { private set; get; }

        // Karten
        public KartenContainer KartenContainer { private set; get; }

        public Color Color { private set; get; }

        public string Name { private set; get; }
        public uint ID { private set; get; }
   
        public uint Siegpunkte { set; get; }


        public CatanClient(TcpClient tcpClient,string IpAddressPortNr, string name)
        {
            this.tcpClient = tcpClient;
            this.Name = name;
            this.IPAddressPortNr = IpAddressPortNr;

            this.SpielfigurenContainer = new Game.SpielFigurenContainer();
            this.KartenContainer = new Game.KartenContainer();

            this.ID = ++id;
        }
    }
}
