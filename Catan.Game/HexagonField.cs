using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    [Serializable]
    public class HexagonField
    {
        public LandFeld.LandfeldTyp LandfeldTyp { private set; get; }
        public int Nr { private set; get; }
        public HexagonPosition Position { private set; get; }
        public List<HexagonPoint> Points { private set; get; }
        public List<HexagonEdge> Edges { private set; get; }

        public HexagonField(HexagonPosition position, LandFeld.LandfeldTyp landfeldTyp,int nr,List<HexagonPoint> points)
        {
            this.Position = position;

            this.LandfeldTyp = landfeldTyp;
            this.Nr = nr;
            this.Points = points;


            this.Edges = new List<Game.HexagonEdge>();
            Edges.Add(new Game.HexagonEdge(points[0], points[1], 0));
            Edges.Add(new Game.HexagonEdge(points[1], points[2], 1));
            Edges.Add(new Game.HexagonEdge(points[2], points[3], 2));
            Edges.Add(new Game.HexagonEdge(points[3], points[4], 3));
            Edges.Add(new Game.HexagonEdge(points[4], points[5], 4));
            Edges.Add(new Game.HexagonEdge(points[5], points[0], 5));
        }

        public override string ToString() => Position.ToString();
    }
}
