using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    [Serializable]
    public class HexagonPoint
    {
        public int Index { private set; get; }
        public HexagonPoint(int index)
        {
            this.Index = index;
        }
        public bool Equals(HexagonPoint hexagonPoint)
        {
            return this.Index == hexagonPoint.Index;
        }
        public override string ToString() => $"PointIndex: {Index}";
    }
}
