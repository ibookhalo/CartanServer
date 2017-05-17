using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    [Serializable]
    public class HexagonPoint
    {
        public int HexagonGridRowIndex { private set; get; }
        public int HexagonGridColumnIndex { private set; get; }
        public HexagonPosition HexagonPosition { private set; get; }

        public HexagonPoint(int gridRowIndex, int gridColumnIndex, HexagonPosition hexagonPosition)
        {
            this.HexagonGridColumnIndex = gridColumnIndex;
            this.HexagonGridRowIndex = gridRowIndex;
            this.HexagonPosition = hexagonPosition;
        }
        public bool Equals(HexagonPoint pointB)
        {
            return (this.HexagonGridRowIndex == pointB.HexagonGridRowIndex) && (this.HexagonGridColumnIndex == pointB.HexagonGridColumnIndex);
        }
    }
}
