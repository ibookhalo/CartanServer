using System;
using System.Collections.Generic;

namespace Catan.Game
{
    [Serializable]
    public class HexagonEdge
    {
        public HexagonPoint PointA { private set; get; }
        public HexagonPoint PointB { private set; get; }
        public int Index { private set; get; }

        public HexagonEdge(HexagonPoint pointA, HexagonPoint pointB,int index)
        {
            this.PointA = pointA;
            this.PointB = pointB;

            this.Index = index;
        }

        public override string ToString() => $"Index: {Index}";
    }
}