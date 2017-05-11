using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    public class HexagonePosition
    {
        public uint RowIndex { private set; get; }
        public uint ColumnIndex { private set; get; }

        public HexagonePosition(uint rowIndex,uint columnIndex)
        {
            this.RowIndex = rowIndex;
            this.ColumnIndex = columnIndex;
        }
    }
}
