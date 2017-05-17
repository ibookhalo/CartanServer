using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    [Serializable]
    public class Strasse : SpielFigur
    {
        public HexagonEdge HexagonEdge { get; set; }

        public Strasse(HexagonPosition hexagonePosition, HexagonEdge hexagonEdge) : base(hexagonePosition)
        {
            this.HexagonEdge = hexagonEdge;
        }
    }
}
