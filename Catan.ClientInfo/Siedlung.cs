using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    [Serializable]
    public class Siedlung : SpielFigur
    {
        public uint PointIndex { get; private set; }
        public Siedlung(HexagonPosition HexagonePositionIndex, uint pointIndex) : base(HexagonePositionIndex)
        {
            this.PointIndex = pointIndex;
            
        }
    }
}
