using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    [Serializable]
    public class Siedlung :SpielFigur
    {
        public HexagonPoint HexagonPoint { get; private set; }
        public Siedlung(HexagonPosition hexagonPosition, HexagonPoint hexagonpoint):base(hexagonPosition)
        {
            this.HexagonPoint = hexagonpoint;
        }
    }
}
