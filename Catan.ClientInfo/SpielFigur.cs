using Catan.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    public abstract class SpielFigur
    {
        public HexagonePosition HexagonePositionIndex { set; get; }
        public SpielFigur(HexagonePosition hexagonePositionIndex)
        {
            this.HexagonePositionIndex = hexagonePositionIndex;
        }
    }
}
