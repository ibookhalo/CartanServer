using Catan.Game;
using System;
using System.Collections.Generic;

namespace Catan.Network.Messaging
{
    [Serializable]
    public class CatanClientStateChangeMessage:NetworkMessage
    { 
        public List<SpielFigur> NewSpielFiguren { private set; get; }
        public List<Bankhandle> NewBankhandles { private set; get; }
        public List<KartenContainer.Entwicklungskarte> NewEntwicklungskarten { private set; get; }
        public int ClientID { get;private set; }
        public bool IsTurnDone { get; private set; }

        public CatanClientStateChangeMessage(List<SpielFigur> newSpielFiguren,List<KartenContainer.Entwicklungskarte> newEntwicklungskarten, List<Bankhandle> bankhandles,int clientID,bool turnDone)
        {
            this.NewSpielFiguren = newSpielFiguren;
            this.NewBankhandles = bankhandles;
            this.NewEntwicklungskarten = newEntwicklungskarten;
            this.ClientID = clientID;
            this.IsTurnDone = turnDone;
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