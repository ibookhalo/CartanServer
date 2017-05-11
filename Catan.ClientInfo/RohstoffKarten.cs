using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    public class RohstoffKarten
    {
        public uint Wolle { private set; get; }
        public uint Getreide { private set; get; }
        public uint Gold { private set; get; }
        public uint Eisen { private set; get; }
        public uint Wasser { private set; get; }
        public uint Bewohner { private set; get; }

        public RohstoffKarten()
        {}

        public void AddRohstoffkarte(LandFeld.LandFeldErtrag ertrag)
        {
            switch (ertrag)
            {
                case LandFeld.LandFeldErtrag.Wolle:
                    Wolle++;
                    break;
                case LandFeld.LandFeldErtrag.Getreide:
                    Getreide++;
                    break;
                case LandFeld.LandFeldErtrag.Gold:
                    Gold++;
                    break;
                case LandFeld.LandFeldErtrag.Eisen:
                    Eisen++;
                    break;
                case LandFeld.LandFeldErtrag.Wasser:
                    Wasser++;
                    break;
                case LandFeld.LandFeldErtrag.Bewohner:
                    Bewohner++;
                    break;
                default:
                    throw new NotImplementedException($"AddRohstoffkarte ({ertrag}) ");
            }
        }
        public void RemoveRohstoffkarte(LandFeld.LandFeldErtrag ertrag)
        {
            switch (ertrag)
            {
                case LandFeld.LandFeldErtrag.Wolle:
                    Wolle--;
                    break;
                case LandFeld.LandFeldErtrag.Getreide:
                    Getreide--;
                    break;
                case LandFeld.LandFeldErtrag.Gold:
                    Gold--;
                    break;
                case LandFeld.LandFeldErtrag.Eisen:
                    Eisen--;
                    break;
                case LandFeld.LandFeldErtrag.Wasser:
                    Wasser--;
                    break;
                case LandFeld.LandFeldErtrag.Bewohner:
                    Bewohner--;
                    break;
                default:
                    throw new NotImplementedException($"RemoveRohstoffkarte ({ertrag}) ");
            }
        }

        public uint GetAnzahlByRohstoff(LandFeld.LandFeldErtrag ertrag)
        {
            switch (ertrag)
            {
                case LandFeld.LandFeldErtrag.Wolle:
                    return Wolle;
                case LandFeld.LandFeldErtrag.Getreide:
                    return Getreide;
                case LandFeld.LandFeldErtrag.Gold:
                    return Gold;
                case LandFeld.LandFeldErtrag.Eisen:
                    return Eisen;
                case LandFeld.LandFeldErtrag.Wasser:
                    return Wasser;
                case LandFeld.LandFeldErtrag.Bewohner:
                    return Bewohner;
                default:
                    throw new NotImplementedException($"GetAnzahlByRohstoff ({ertrag}) ");
            }
        }
    }
}
