using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    [Serializable]
    public class KartenContainer
    {
        public enum Rohstoffkarte { Wolle, Getreide, Gold, Eisen, Wasser, Bewohner };
        public enum Entwicklungskarte { Siegpunkt, Gold }

        // Rohstoffkarten
        private uint rohstoff_wolle;
        private uint rohstoff_getreide;
        private uint rohstoff_gold;
        private uint rohstoff_eisen;
        private uint rohstoff_wasser;
        private uint rohstoff_bewohner;

        // Entwicklungskarten
        private uint entwicklung_gold;
        private uint entwicklung_siegpunkt;


        public KartenContainer()
        { }
        public void AddEntwicklungskarte(Entwicklungskarte entwicklungskarte)
        {
            switch (entwicklungskarte)
            {
                case Entwicklungskarte.Gold:
                    entwicklung_gold++;
                    break;
                case Entwicklungskarte.Siegpunkt:
                    entwicklung_siegpunkt++;
                    break;
                default:
                    throw new NotImplementedException($"AddEntwicklungskarte ({entwicklungskarte}) ");
            }
        }
        public void RemoveEntwicklungskarte(Entwicklungskarte entwicklungskarte)
        {
            switch (entwicklungskarte)
            {
                case Entwicklungskarte.Gold:
                    entwicklung_gold--;
                    break;
                case Entwicklungskarte.Siegpunkt:
                    entwicklung_siegpunkt--;
                    break;
                default:
                    throw new NotImplementedException($"RemoveEntwicklungskarte ({entwicklungskarte}) ");
            }
        }
        public uint GetAnzahlByEntwicklungskarte(Entwicklungskarte entwicklungskarte)
        {
            switch (entwicklungskarte)
            {
                case Entwicklungskarte.Gold:
                    return entwicklung_gold;
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
                default:
                    throw new NotImplementedException($"RemoveRohstoffkarte ({rohstoffkarte}) ");
            }
        }

        public uint GetAnzahlByRohstoffkarte(Rohstoffkarte rohstoffkarte)
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
                default:
                    throw new NotImplementedException($"GetAnzahlByRohstoff ({rohstoffkarte}) ");
            }
        }
    }
}
