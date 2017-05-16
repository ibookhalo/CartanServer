using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    [Serializable]
    public class Siedlung : SpielFigur
    {
        public int PointIndex { get; private set; }
        public Siedlung(HexagonPosition HexagonePositionIndex, int pointIndex) : base(HexagonePositionIndex)
        {
            this.PointIndex = pointIndex;
            
        }
    }
}
