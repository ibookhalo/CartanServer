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

        public HexagonPoint(int gridRowIndex,int gridColumnIndex)
        {
            this.HexagonGridColumnIndex = gridColumnIndex;
            this.HexagonGridRowIndex = gridRowIndex;
        }
    }
}
