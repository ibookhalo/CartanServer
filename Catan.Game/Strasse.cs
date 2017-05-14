using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    [Serializable]
    public class Strasse : SpielFigur
    {
        public uint EdgeIndex { get; set; }

        public Strasse(HexagonPosition HexagonePositionIndex, uint edgeIndex) : base(HexagonePositionIndex)
        {
            this.EdgeIndex = edgeIndex;
        }

    }
}
