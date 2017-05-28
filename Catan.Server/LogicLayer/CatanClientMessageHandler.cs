using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan.Game;
using Catan.Network.Messaging;

namespace Catan.Server.LogicLayer
{
    class CatanClientMessageHandler
    {
        private CatanClient catanClient;

        public CatanClientMessageHandler()
        { }
        public void Handle(CatanClient catanClient, CatanClientStateChangeMessage catanClientStateChangeMessage)
        {
            this.catanClient = catanClient;
            if (catanClientStateChangeMessage.NewSpielFiguren !=null)
            {
                foreach (var newSpielFiguren in catanClientStateChangeMessage.NewSpielFiguren.Where(spielFigur => spielFigur is Siedlung).ToList())
                {
                    addNewSiedlung(newSpielFiguren as Siedlung);
                }

                foreach (var newSpielFiguren in catanClientStateChangeMessage.NewSpielFiguren.Where(spielFigur => spielFigur is Strasse).ToList())
                {
                    addNewStrasse(newSpielFiguren as Strasse);
                }

                foreach (var newSpielFiguren in catanClientStateChangeMessage.NewSpielFiguren.Where(spielFigur => spielFigur is Stadt).ToList())
                {
                    addNewStadt(newSpielFiguren as Stadt);
                }
            }

            if (catanClientStateChangeMessage.NewEntwicklungskarten!=null)
            {
                foreach (var newEntwicklungskarte in catanClientStateChangeMessage.NewEntwicklungskarten)
                {
                    addNewEntwicklungskarte(newEntwicklungskarte);
                }
            }
        }

        private void addNewEntwicklungskarte(KartenContainer.Entwicklungskarte newEntwicklungskarte)
        {
            if (catanClient.KartenContainer.GetAnzahlByRohstoffkarte(KartenContainer.Rohstoffkarte.Gold) >= 1 &&
                           catanClient.KartenContainer.GetAnzahlByRohstoffkarte(KartenContainer.Rohstoffkarte.Wasser) >= 1)
            {
                switch (newEntwicklungskarte)
                {
                    case KartenContainer.Entwicklungskarte.Siegpunkt:
                        catanClient.KartenContainer.RemoveRohstoffkarte(KartenContainer.Rohstoffkarte.Gold);
                        catanClient.KartenContainer.RemoveRohstoffkarte(KartenContainer.Rohstoffkarte.Wasser);

                        catanClient.KartenContainer.AddEntwicklungskarte(newEntwicklungskarte);
                        catanClient.Siegpunkte++;
                        break;
                    default:
                        throw new NotImplementedException($"addNewEntwicklungskarte {newEntwicklungskarte}");
                }
            }
        }

        private void addNewStadt(Stadt newStadt)
        {
            if (catanClient.KartenContainer.GetAnzahlByRohstoffkarte(KartenContainer.Rohstoffkarte.Eisen) >= 1 &&
                           catanClient.KartenContainer.GetAnzahlByRohstoffkarte(KartenContainer.Rohstoffkarte.Getreide) >= 1 &&
                           catanClient.KartenContainer.GetAnzahlByRohstoffkarte(KartenContainer.Rohstoffkarte.Bewohner) >= 1 &&

                           catanClient.AllowedSiedlungen[newStadt.HexagonPosition.RowIndex][newStadt.HexagonPosition.ColumnIndex][newStadt.HexagonPoint.Index])
            {
                catanClient.KartenContainer.RemoveRohstoffkarte(KartenContainer.Rohstoffkarte.Eisen);
                catanClient.KartenContainer.RemoveRohstoffkarte(KartenContainer.Rohstoffkarte.Getreide);
                catanClient.KartenContainer.RemoveRohstoffkarte(KartenContainer.Rohstoffkarte.Wolle);

                catanClient.Siegpunkte += 3;

                catanClient.SpielfigurenContainer.Staedte.Add(newStadt);
            }
        }

        private void addNewStrasse(Strasse newStrasse)
        {
            if (catanClient.KartenContainer.GetAnzahlByRohstoffkarte(KartenContainer.Rohstoffkarte.Eisen) >= 1 &&
                          catanClient.KartenContainer.GetAnzahlByRohstoffkarte(KartenContainer.Rohstoffkarte.Wasser) >= 1 &&
                          catanClient.AllowedStrassen[newStrasse.HexagonPosition.RowIndex][newStrasse.HexagonPosition.ColumnIndex][newStrasse.HexagonEdge.Index])
            {
                catanClient.KartenContainer.RemoveRohstoffkarte(KartenContainer.Rohstoffkarte.Eisen);
                catanClient.KartenContainer.RemoveRohstoffkarte(KartenContainer.Rohstoffkarte.Wasser);

                catanClient.SpielfigurenContainer.Strassen.Add(newStrasse);
            }
        }

        private void addNewSiedlung(Siedlung newSiedlung)
        {
            if (catanClient.KartenContainer.GetAnzahlByRohstoffkarte(KartenContainer.Rohstoffkarte.Eisen) >= 1 &&
                            catanClient.KartenContainer.GetAnzahlByRohstoffkarte(KartenContainer.Rohstoffkarte.Getreide) >= 1 &&
                            catanClient.KartenContainer.GetAnzahlByRohstoffkarte(KartenContainer.Rohstoffkarte.Wolle) >= 1 &&

                            catanClient.AllowedSiedlungen[newSiedlung.HexagonPosition.RowIndex][newSiedlung.HexagonPosition.ColumnIndex][newSiedlung.HexagonPoint.Index])
            {
                catanClient.KartenContainer.RemoveRohstoffkarte(KartenContainer.Rohstoffkarte.Eisen);
                catanClient.KartenContainer.RemoveRohstoffkarte(KartenContainer.Rohstoffkarte.Getreide);
                catanClient.KartenContainer.RemoveRohstoffkarte(KartenContainer.Rohstoffkarte.Wolle);

                catanClient.Siegpunkte += 2;

                catanClient.SpielfigurenContainer.Siedlungen.Add(newSiedlung);
            }

        }
    }
}
