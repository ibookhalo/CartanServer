using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    [Serializable]
    public class Siedlung 
    {
        public HexagonPosition HexagonePosition { set; get; }
        public int PointIndex { get; private set; }
        public Siedlung(HexagonPosition HexagonePositionIndex, int pointIndex)
        {
            KeyValuePair
            this.PointIndex = pointIndex;
            this.HexagonePosition = HexagonePosition;
        }
    }
}
