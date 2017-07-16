using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Catan.Game.Hexagon;

namespace Catan.Game
{
    public static class LandFeld
    {
        public static KartenContainer.Rohstoffkarte GetErtragByLandFeldTyp(Hexagon.LandfeldTyp typ)
        {
            switch (typ)
            {
                case LandfeldTyp.Weideland:
                    return KartenContainer.Rohstoffkarte.Wolle;
                case LandfeldTyp.Ackerland:
                    return KartenContainer.Rohstoffkarte.Getreide;
                case LandfeldTyp.BergwerkGold:
                    return KartenContainer.Rohstoffkarte.Gold;
                case LandfeldTyp.Eisenmine:
                    return KartenContainer.Rohstoffkarte.Eisen;
                case LandfeldTyp.MeersFeld:
                    return KartenContainer.Rohstoffkarte.Wasser;
                case LandfeldTyp.Wohnstaette:
                    return KartenContainer.Rohstoffkarte.Bewohner;
                case LandfeldTyp.Wald:
                    return KartenContainer.Rohstoffkarte.Holz;
                default:
                    throw new NotImplementedException($"GetErtragByLandFeldTyp ({typ}) ");
            }
        }
    }
}
