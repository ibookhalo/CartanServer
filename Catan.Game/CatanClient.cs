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
        private static int id;

        [NonSerialized]
        private TcpClient tcpClient;
        public TcpClient TcpClient { get { return tcpClient; }}
        public string IPAddressPortNr { get; private set; }
        // Spielfiguren
        public SpielFigurenContainer SpielfigurenContainer { private set; get; }

        // Karten
        public KartenContainer KartenContainer { private set; get; }

        public Color Color { set; get; }

        public string Name { private set; get; }
        public int ID { private set; get; }
   
        public int Siegpunkte { set; get; }

        // 
        public bool[][][] AllowedStaedte { get; set; }
        public bool[][][] AllowedSiedlungen { get; set; }
        public bool[][][] AllowedStrassen { get; set; }
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
