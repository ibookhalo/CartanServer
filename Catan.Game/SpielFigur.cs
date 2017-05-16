using Catan.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    [Serializable]
    public abstract class SpielFigur
    {
        public HexagonPosition HexagonePosition { set; get; }
        public SpielFigur(HexagonPosition hexagonePositionIndex)
        {
            this.HexagonePosition = hexagonePositionIndex;
        }
    }
}
