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
        public Stadt(HexagonPosition HexagonePositionIndex, uint pointIndex) : base(HexagonePositionIndex,pointIndex)
        {
        }
    }
}
