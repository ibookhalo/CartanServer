using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    public class HexagonFieldHexagonEdge
    {
        public HexagonEdge Edge { private set; get; }
        public HexagonField Hexagon { private set; get; }

        public HexagonFieldHexagonEdge(HexagonField hexagonField, HexagonEdge hexagonEdge)
        {
            this.Hexagon = hexagonField;
            this.Edge = hexagonEdge;
        }
    }
}
