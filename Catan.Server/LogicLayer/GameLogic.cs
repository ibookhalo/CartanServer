using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan.Game;
using Catan.Network.Messaging;
using System.Net;
using System.Drawing;

namespace Catan.Server.LogicLayer
{
    public class GameLogic : Interfaces.ILogicLayer
    {
        private Interfaces.INetworkLayer iNetworkLayer;
        private CatanClient currentClient;

        private List<CatanClient> catanClients;
        private List<KeyValuePair<int, bool>> clientFinishedWithFirstTurn;

        public GameLogic()
        {
            ServerFinishedListening(new List<CatanClient>() { new CatanClient(null, null, null) });


            this.clientFinishedWithFirstTurn = new List<KeyValuePair<int, bool>>();
        }

        private CatanClient getNextClient()
        {
            if (currentClient == null)
            {
                // Der letzte Spieler ist als erster dran 
                catanClients.Reverse();
                return catanClients.First();
            }
            else
            {
                return catanClients[(catanClients.IndexOf(currentClient) + 1) % catanClients.Count];
            }

        }
        public void ServerFinishedListening(List<CatanClient> catanClients)
        {
            // Let clients play catan !

            this.catanClients = catanClients;


            //currentClient = getNextClient();

            catanClients[0].SpielfigurenContainer.Staedte.Add(new Stadt(new HexagonPosition(0, 0), 1));


            currentClient = catanClients[0];
            currentClient.AllowedStaedte = getAllowedStaedteByClient(currentClient);
            currentClient.AllowedSiedlungen = getAllowedSiedlungenByClient(currentClient);
            currentClient.AllowedStrassen = getAllowedStrassenByClient(currentClient);

            GameStateMessage gameState = new GameStateMessage(catanClients, currentClient, HexagonGrid.Instance.Hexagones);

            iNetworkLayer.SendBroadcastMessage(gameState);
        }

        private bool[][][] getAllowedStrassenByClient(CatanClient currentClient)
        {
            throw new NotImplementedException();
        }

        private bool[][][] getAllowedSiedlungenByClient(CatanClient currentClient)
        {
            throw new NotImplementedException();
        }

        private bool[][][] getAllowedStaedteByClient(CatanClient client)
        {
            var hexfields = HexagonGrid.Instance.Hexagones;

            bool[][][] ob = new bool[hexfields.Length][][];
            for (int rowIndex = 0; rowIndex < hexfields.Length; rowIndex++)
                ob[rowIndex] = new bool[hexfields[rowIndex].GetLength(0)][];

            for (int rowIndex = 0; rowIndex < ob.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < ob[rowIndex].GetLength(0); columnIndex++)
                {
                    ob[rowIndex][columnIndex] = new bool[6];
                    for (int pointIndex = 0; pointIndex < ob[rowIndex][columnIndex].GetLength(0); pointIndex++)
                    {
                        var gridColumnIndex = hexfields[rowIndex][columnIndex].Points[pointIndex].HexagonGridColumnIndex;
                        var gridRowIndex = hexfields[rowIndex][columnIndex].Points[pointIndex].HexagonGridRowIndex;

                        var foundHexagones = HexagonGrid.GetHexagonesByGridIndex(gridRowIndex, gridColumnIndex);
                        if (foundHexagones.Count >= 2)
                        {
                            // Stadt darf hier gebaut werden ...
                            // Überprüfen ob andere Spieler was hier haben
                            var stadtGefunden = catanClients.TrueForAll(_client => _client.SpielfigurenContainer.Staedte.Find
                              (stadt => stadt.HexagonePosition.ColumnIndex == columnIndex && stadt.HexagonePosition.RowIndex == rowIndex && stadt.PointIndex == pointIndex) != null);

                            if (!stadtGefunden)
                            {
                                // Strassen überprüfen
                                var foundEdges = HexagonGrid.GetHexagonEdgesByGridIndex(foundHexagones, gridRowIndex, gridColumnIndex);
                                var otherClients = catanClients.FindAll(_client => _client.ID != currentClient.ID);
                                
                                otherClients.Exists(c=>c.SpielfigurenContainer.Strassen.Exists(strasse=>strasse.HexagonEdge.))
                                ob[rowIndex][columnIndex][pointIndex] = true;
                            }
                            else
                            {
                                ob[rowIndex][columnIndex][pointIndex] = false;
                            }
                        }
                    }
                }
            }
            return ob;
        }
        public void StartServer()
        {
            this.iNetworkLayer = new NetworkLayer.CatanServer(1, new IPEndPoint(IPAddress.Parse("127.0.0.1"), 123), "ibo", this);
            this.iNetworkLayer.StartTcpListener();
        }
        public void ClientGameStateChangeMessageReceived(CatanClientStateChangeMessage catanClientStateChangeMessage)
        {
            /*if (clientFinishedWithFirstTurn.Exists(client=>client.Key==catanClientStateChangeMessage.)
            {
             
            }*/
        }
    }
}
