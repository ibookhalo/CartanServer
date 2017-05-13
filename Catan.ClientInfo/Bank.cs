﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    public static class Bank
    {
        public static void GetRohstoffkarte(RohstoffKartenContainer rohstoffkarten,LandFeld.LandFeldErtrag abgeben,LandFeld.LandFeldErtrag nehmen)
        {
            if (rohstoffkarten.GetAnzahlByRohstoff(abgeben)>=4)
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
