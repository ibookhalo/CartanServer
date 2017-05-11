using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    public class Stadt : Siedlung
    { 
        public Stadt(HexagonePosition HexagonePositionIndex, uint pointIndex) : base(HexagonePositionIndex,pointIndex)
        {
        }
    }
}
