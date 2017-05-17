using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    [Serializable]
    public class Strasse
    {
        public int EdgeIndex { get; set; }

        public Strasse(HexagonPosition hexagonePosition, int edgeIndex)
        {
            this.EdgeIndex = edgeIndex;
        }

    }
}
