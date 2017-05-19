using Catan.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Server.LogicLayer
{
    class HexagonPositionHexagonEdge
    {
        public HexagonEdge HexagonEdge { private set; get; }
        public HexagonPosition HexagonPosition { private set; get; }

        public HexagonPositionHexagonEdge(HexagonPosition hexagonPosition, HexagonEdge hexagonEdge)
        {
            this.HexagonPosition = hexagonPosition;
            this.HexagonEdge = hexagonEdge;
        }
    }
}
