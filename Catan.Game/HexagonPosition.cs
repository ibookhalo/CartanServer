using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    [Serializable]
    public class HexagonPosition
    {
        public int RowIndex { private set; get; }
        public int ColumnIndex { private set; get; }

        public HexagonPosition(int rowIndex,int columnIndex)
        {
            this.RowIndex = rowIndex;
            this.ColumnIndex = columnIndex;
        }

        public bool Equals(HexagonPosition hexPosition)
        {
            return this.RowIndex == hexPosition.RowIndex && this.ColumnIndex == hexPosition.ColumnIndex;
        }
        public override string ToString() => $"RowIndex: {RowIndex}, ColumnIndex: {ColumnIndex}";
    }
}
