using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Server.LogicLayer
{
    static class BuildChecker
    {
        public static bool CanBuildStadt(Game.KartenContainer kartenContainer)
        {
            return (kartenContainer.GetAnzahlByRohstoffkarte(Game.KartenContainer.Rohstoffkarte.Eisen) >= 1 &&
                           kartenContainer.GetAnzahlByRohstoffkarte(Game.KartenContainer.Rohstoffkarte.Getreide) >= 1 &&
                           kartenContainer.GetAnzahlByRohstoffkarte(Game.KartenContainer.Rohstoffkarte.Bewohner) >= 1);
        }

        public static bool CanBuildStrasse(Game.KartenContainer kartenContainer)
        {
            return kartenContainer.GetAnzahlByRohstoffkarte(Game.KartenContainer.Rohstoffkarte.Eisen) >= 1 &&
                          kartenContainer.GetAnzahlByRohstoffkarte(Game.KartenContainer.Rohstoffkarte.Wasser) >= 1;
        }

        public static bool CanBuildSiedlung(Game.KartenContainer kartenContainer)
        {
            return kartenContainer.GetAnzahlByRohstoffkarte(Game.KartenContainer.Rohstoffkarte.Eisen) >= 1 &&
                            kartenContainer.GetAnzahlByRohstoffkarte(Game.KartenContainer.Rohstoffkarte.Getreide) >= 1 &&
                            kartenContainer.GetAnzahlByRohstoffkarte(Game.KartenContainer.Rohstoffkarte.Wasser) >= 1;
        }
    }
}
