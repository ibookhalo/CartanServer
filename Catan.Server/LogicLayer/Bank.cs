using Catan.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Server.LogicLayer
{
    public static class Bank
    {
        public static void ExchangeRohstoffkarte(KartenContainer rohstoffkarten, KartenContainer.Rohstoffkarte abgeben, KartenContainer.Rohstoffkarte nehmen)
        {
            if (rohstoffkarten.GetAnzahlByRohstoffkarte(abgeben)>=4)
            {
                // 4 Karten abgeben
                rohstoffkarten.RemoveRohstoffkarte(abgeben);
                rohstoffkarten.RemoveRohstoffkarte(abgeben);
                rohstoffkarten.RemoveRohstoffkarte(abgeben);
                rohstoffkarten.RemoveRohstoffkarte(abgeben);


                // eine Karte nehmen
                rohstoffkarten.AddRohstoffkarte(nehmen);
            }
            else
            {
                throw new ArgumentException($"GetRohstoffkarte Anzahl < 4");
            }
        }
    }
}
