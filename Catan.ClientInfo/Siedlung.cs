using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    public class Siedlung : SpielFigur
    {
        public uint PointIndex { get; private set; }
        public Siedlung(HexagonePosition HexagonePositionIndex, uint pointIndex) : base(HexagonePositionIndex)
        {
            this.PointIndex = pointIndex;
        }
    }
}
