using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    [Serializable]
    public class Strasse : SpielFigur
    {
        public int EdgeIndex { get; set; }

        public Strasse(HexagonPosition hexagonePosition, int edgeIndex) : base(hexagonePosition)
        {
            this.EdgeIndex = edgeIndex;
        }

    }
}
