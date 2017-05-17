using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    [Serializable]
    public class Stadt : Siedlung
    { 
        public Stadt(HexagonPosition hexagonePosition, HexagonPoint hexagonPoint) : base(hexagonePosition,hexagonPoint)
        {
        }
    }
}
