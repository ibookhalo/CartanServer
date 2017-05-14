using Catan.Game;
using System;
using System.Collections.Generic;

namespace Catan.Network.Messaging
{
    [Serializable]
    public class CatanClientStateChangeMessage:NetworkMessage
    { 
        public List<SpielFigur> NewSpielFiguren { private set; get; }
        public List<Bankhandle> Bankhandles { private set; get; }


        public CatanClientStateChangeMessage(List<SpielFigur> newSpielFiguren, List<Bankhandle> bankhandles)
        {
            this.NewSpielFiguren = newSpielFiguren;
            this.Bankhandles = bankhandles;
        }
        
        [Serializable]
        public class Bankhandle
        {
            public KartenContainer.Rohstoffkarte BankAbgeben { private set; get; }
            public KartenContainer.Rohstoffkarte BankNehmen { private set; get; }
            public Bankhandle(KartenContainer.Rohstoffkarte bankAbgeben, KartenContainer.Rohstoffkarte bankNehmen)
            {
                this.BankAbgeben = bankAbgeben;
                this.BankNehmen = bankNehmen;
            }
        }
    }
}