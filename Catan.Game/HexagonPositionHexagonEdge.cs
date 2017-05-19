using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    public class HexagonPositionHexagonEdge
    {
        public HexagonEdge Edge { private set; get; }
        public Hexagon Hexagon { private set; get; }

        public HexagonPositionHexagonEdge(Hexagon hexagonField, HexagonEdge hexagonEdge)
        {
            this.Hexagon = hexagonField;
            this.Edge = hexagonEdge;
        }
    }
}
