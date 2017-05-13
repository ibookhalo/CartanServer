using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    public class LandFeld
    {
       
        public HexagonePosition HexagonPosition { private set; get; }
        public Enums.Landfeld FeldType { private set; get; }
        public Enums.Rohstoff FeldErtrag { private set; get; }
        public LandFeld(Enums.Landfeld feldType,HexagonePosition hexagonePosition)
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
        public static Enums.Rohstoff GetErtragByLandFeldTyp(Enums.Landfeld typ)
        {
            switch (typ)
            {
                case Enums.Landfeld.Weideland:
                    return Enums.Rohstoff.Wolle;
                case Enums.Landfeld.Ackerland:
                    return Enums.Rohstoff.Getreide;
                case Enums.Landfeld.BergwerkGold:
                    return Enums.Rohstoff.Gold;
                case Enums.Landfeld.Eisenmine:
                    return Enums.Rohstoff.Eisen;
                case Enums.Landfeld.MeersFeld:
                    return Enums.Rohstoff.Wasser;
                case Enums.Landfeld.Wohnstaette:
                    return Enums.Rohstoff.Bewohner;
                default:
                    throw new NotImplementedException($"GetErtragByLandFeldTyp ({typ}) ");
            }
        }
    }
}
