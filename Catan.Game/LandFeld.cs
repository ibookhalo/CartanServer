using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    public class LandFeld
    {
        public enum LandfeldTyp { Weideland, Ackerland, BergwerkGold, Eisenmine, MeersFeld, Wohnstaette };
        public HexagonPosition HexagonPosition { private set; get; }
        public LandfeldTyp FeldType { private set; get; }
        public KartenContainer.Rohstoffkarte FeldErtrag { private set; get; }
        public LandFeld(LandfeldTyp feldType,HexagonPosition hexagonePosition)
        {
            this.FeldType = feldType;
            this.FeldErtrag = GetErtragByLandFeldTyp(feldType);
            this.HexagonPosition = hexagonePosition;
        }
        public static KartenContainer.Rohstoffkarte GetErtragByLandFeldTyp(LandfeldTyp typ)
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
                default:
                    throw new NotImplementedException($"GetErtragByLandFeldTyp ({typ}) ");
            }
        }
    }
}
