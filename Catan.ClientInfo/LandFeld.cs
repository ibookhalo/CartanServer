using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    public class LandFeld
    {
        public enum LandFeldTyp {Weideland, Ackerland, BergwerkGold, Eisenmine, MeersFeld, Wohnstaette};
        public enum LandFeldErtrag { Wolle, Getreide, Gold, Eisen, Wasser, Bewohner };


        public HexagonePosition HexagonPosition { private set; get; }
        public LandFeldTyp FeldType { private set; get; }
        public LandFeldErtrag FeldErtrag { private set; get; }
        public LandFeld(LandFeldTyp feldType,HexagonePosition hexagonePosition)
        {
            this.FeldType = feldType;
            this.FeldErtrag = GetErtragByLandFeldTyp(feldType);
            this.HexagonPosition = hexagonePosition;
        }

        public uint AnzahlErtraege
        {
            get
            {
                switch (FeldErtrag)
                {
                    case LandFeldErtrag.Wolle:
                        return 1;
                    case LandFeldErtrag.Getreide:
                        return 1;
                    case LandFeldErtrag.Gold:
                        return 1;
                    case LandFeldErtrag.Eisen:
                        return 1;
                    case LandFeldErtrag.Wasser:
                        return 1;
                    case LandFeldErtrag.Bewohner:
                        return 2;
                    default:
                        throw new NotImplementedException($"Ertraege ({FeldErtrag}) ");
                }
            }
        }
        public static LandFeldErtrag GetErtragByLandFeldTyp(LandFeldTyp typ)
        {
            switch (typ)
            {
                case LandFeldTyp.Weideland:
                    return LandFeldErtrag.Wolle;
                case LandFeldTyp.Ackerland:
                    return LandFeldErtrag.Getreide;
                case LandFeldTyp.BergwerkGold:
                    return LandFeldErtrag.Gold;
                case LandFeldTyp.Eisenmine:
                    return LandFeldErtrag.Eisen;
                case LandFeldTyp.MeersFeld:
                    return LandFeldErtrag.Wasser;
                case LandFeldTyp.Wohnstaette:
                    return LandFeldErtrag.Bewohner;
                default:
                    throw new NotImplementedException($"GetErtragByLandFeldTyp ({typ}) ");
            }
        }
    }
}
