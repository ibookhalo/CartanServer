using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Server.LogicLayer
{
    class GridPoint
    {
        public int ColumnIndex { private set; get; }
        public int RowIndex { private set; get; }

        public GridPoint(int columnIndex, int rowIndex)
        {
            this.ColumnIndex = columnIndex;
            this.RowIndex = rowIndex;
        }
        public void Offset(int columns, int rows)
        {
            this.ColumnIndex += columns;
            this.RowIndex += rows;
        }

        public bool Equals(GridPoint gridPoint)
        {
            return (this.ColumnIndex==gridPoint.ColumnIndex) && (this.RowIndex==gridPoint.RowIndex);
        }
    }
}
