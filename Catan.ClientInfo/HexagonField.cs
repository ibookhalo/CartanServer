using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    [Serializable]
    public class HexagonField
    {
        public LandFeld.LandfeldTyp LandfeldTyp { private set; get; }
        public uint Nr { private set; get; }
        public HexagonField(LandFeld.LandfeldTyp landfeldTyp,uint nr)
        {
            this.LandfeldTyp = landfeldTyp;
            this.Nr = nr;
        }
    }
}
