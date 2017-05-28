using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    [Serializable]
    public class KartenContainer
    {
        public enum Rohstoffkarte { Wolle, Getreide, Gold, Eisen, Wasser, Bewohner, Holz };
        public enum Entwicklungskarte { Siegpunkt }

        // Rohstoffkarten
        private int rohstoff_wolle;
        private int rohstoff_getreide;
        private int rohstoff_gold;
        private int rohstoff_eisen;
        private int rohstoff_wasser;
        private int rohstoff_bewohner;
        private int rohstoff_holz;

        // Entwicklungskarten
        private int entwicklung_siegpunkt;


        public KartenContainer()
        { }
        public void AddEntwicklungskarte(Entwicklungskarte entwicklungskarte)
        {
            switch (entwicklungskarte)
            {
                case Entwicklungskarte.Siegpunkt:
                    entwicklung_siegpunkt++;
                    break;
                default:
                    throw new NotImplementedException($"AddEntwicklungskarte ({entwicklungskarte}) ");
            }
        }
        public int GetAnzahlByEntwicklungskarte(Entwicklungskarte entwicklungskarte)
        {
            switch (entwicklungskarte)
            {
                case Entwicklungskarte.Siegpunkt:
                    return entwicklung_siegpunkt;
                default:
                    throw new NotImplementedException($"GetAnzahlByEntwicklungskarte ({entwicklungskarte}) ");
            }
        }
        public void AddRohstoffkarte(Rohstoffkarte rohstoffkarte)
        {
            switch (rohstoffkarte)
            {
                case Rohstoffkarte.Wolle:
                    rohstoff_wolle++;
                    break;
                case Rohstoffkarte.Getreide:
                    rohstoff_getreide++;
                    break;
                case Rohstoffkarte.Gold:
                    rohstoff_gold++;
                    break;
                case Rohstoffkarte.Eisen:
                    rohstoff_eisen++;
                    break;
                case Rohstoffkarte.Wasser:
                    rohstoff_wasser++;
                    break;
                case Rohstoffkarte.Bewohner:
                    rohstoff_bewohner++;
                    break;
                case Rohstoffkarte.Holz:
                    rohstoff_holz++;
                    break;
                default:
                    throw new NotImplementedException($"AddRohstoffkarte ({rohstoffkarte}) ");
            }
        }
        public void RemoveRohstoffkarte(Rohstoffkarte rohstoffkarte)
        {
            switch (rohstoffkarte)
            {
                case Rohstoffkarte.Wolle:
                    rohstoff_wolle--;
                    break;
                case Rohstoffkarte.Getreide:
                    rohstoff_getreide--;
                    break;
                case Rohstoffkarte.Gold:
                    rohstoff_gold--;
                    break;
                case Rohstoffkarte.Eisen:
                    rohstoff_eisen--;
                    break;
                case Rohstoffkarte.Wasser:
                    rohstoff_wasser--;
                    break;
                case Rohstoffkarte.Bewohner:
                    rohstoff_bewohner--;
                    break;
                case Rohstoffkarte.Holz:
                    rohstoff_holz--;
                    break;
                default:
                    throw new NotImplementedException($"RemoveRohstoffkarte ({rohstoffkarte}) ");
            }
        }

        public int GetAnzahlByRohstoffkarte(Rohstoffkarte rohstoffkarte)
        {
            switch (rohstoffkarte)
            {
                case Rohstoffkarte.Wolle:
                    return rohstoff_wolle;
                case Rohstoffkarte.Getreide:
                    return rohstoff_getreide;
                case Rohstoffkarte.Gold:
                    return rohstoff_gold;
                case Rohstoffkarte.Eisen:
                    return rohstoff_eisen;
                case Rohstoffkarte.Wasser:
                    return rohstoff_wasser;
                case Rohstoffkarte.Bewohner:
                    return rohstoff_bewohner;
                case Rohstoffkarte.Holz:
                    return rohstoff_holz;
                default:
                    throw new NotImplementedException($"GetAnzahlByRohstoff ({rohstoffkarte}) ");
            }
        }
    }
}
