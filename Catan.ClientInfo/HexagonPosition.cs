using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    [Serializable]
    public class HexagonPosition
    {
        public uint RowIndex { private set; get; }
        public uint ColumnIndex { private set; get; }

        public HexagonPosition(uint rowIndex,uint columnIndex)
        {
            this.RowIndex = rowIndex;
            this.ColumnIndex = columnIndex;
        }
    }
}
