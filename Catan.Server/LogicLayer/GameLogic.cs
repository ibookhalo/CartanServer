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
            ServerFinishedListening(new List<CatanClient>() { new CatanClient(null, null, null), new CatanClient(null, null, null) });


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

            catanClients[0].SpielfigurenContainer.Staedte.Add(new Stadt(new HexagonPosition(0, 0), new HexagonPoint(1)));
            catanClients[1].SpielfigurenContainer.Strassen.Add(new Strasse(new HexagonPosition(1, 1), HexagonGrid.Instance.Hexagones[1][1].Edges[0]));


            currentClient = catanClients[0];

            currentClient.AllowedSiedlungen = getAllowedStaedteByClient(currentClient);
            currentClient.AllowedStaedte = getAllowedStaedteByClient(currentClient);
            currentClient.AllowedStrassen = getAllowedStrassenByClient(currentClient);

            GameStateMessage gameState = new GameStateMessage(catanClients, currentClient, HexagonGrid.Instance.Hexagones);

            iNetworkLayer.SendBroadcastMessage(gameState);
        }
        private bool[][][] getAllowedStrassenByClient(CatanClient currentClient)
        {
            bool[][][] allowedStaedte = initilize3DBoolArrayBasedOnHexfields();

            foreach (var stadt in currentClient.SpielfigurenContainer.Staedte)
            {
                // neben angrenzende Städte zu bauen, ist erlaubt
                var gridPoint=HexagonGrid.GetGridPointByHexagonPositionAndPoint(stadt.HexagonPosition, stadt.HexagonPoint);
                var hexagones = new List<Hexagon>();

                for (int rowIndex = 0; rowIndex < HexagonGrid.Instance.Hexagones.GetLength(0); rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < HexagonGrid.Instance.Hexagones[rowIndex].GetLength(0); columnIndex++)
                    {
                        hexagones.Add(HexagonGrid.Instance.Hexagones[rowIndex][columnIndex]);
                    }
                }

                var hexagonEdges=HexagonGrid.GetHexagonEdgesByGridIndex(hexagones, gridPoint.Y, gridPoint.X);

            }
            return allowedStaedte;
        }
        private bool[][][] getAllowedStaedteByClient(CatanClient currentClient)
        {
            var allowedStaedte = initilize3DBoolArrayBasedOnHexfields();

            foreach (var stadt in currentClient.SpielfigurenContainer.Siedlungen)
            {
                allowedStaedte[stadt.HexagonPosition.RowIndex][stadt.HexagonPosition.ColumnIndex][stadt.HexagonPoint.Index] = true;
            }

            return allowedStaedte;
        }
        private bool[][][] initilize3DBoolArrayBasedOnHexfields()
        {
            var hexfields = HexagonGrid.Instance.Hexagones;

            bool[][][] ob = new bool[hexfields.Length][][];
            for (int rowIndex = 0; rowIndex < hexfields.Length; rowIndex++)
            {
                ob[rowIndex] = new bool[hexfields[rowIndex].GetLength(0)][];
                for (int columnIndex = 0; columnIndex < ob[rowIndex].GetLength(0); columnIndex++)
                {
                    ob[rowIndex][columnIndex] = new bool[6];
                }
            }

            return ob;
        }
        private bool[][][] getAllowedSiedlungenByClient(CatanClient client)
        {
            bool[][][] allowedSiedlungen = initilize3DBoolArrayBasedOnHexfields();

            for (int rowIndex = 0; rowIndex < allowedSiedlungen.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < allowedSiedlungen[rowIndex].GetLength(0); columnIndex++)
                {
                    for (int pointIndex = 0; pointIndex < allowedSiedlungen[rowIndex][columnIndex].GetLength(0); pointIndex++)
                    {
                        var currentGridPoint = HexagonGrid.GetGridPointByHexagonPositionAndPoint(new HexagonPosition(rowIndex, columnIndex), new HexagonPoint(pointIndex));

                        var foundHexagones = HexagonGrid.GetHexagonesByGridPoint(currentGridPoint.RowIndex, currentGridPoint.ColumnIndex);
                        if (foundHexagones.Count >= 2)
                        {
                            // Stadt darf hier gebaut werden ...
                            // Überprüfen ob andere Spieler was hier haben
                            var stadtGefunden = catanClients.TrueForAll(_client => _client.SpielfigurenContainer.Staedte.Find(
                                stadt => HexagonGrid.GetGridPointByHexagonPositionAndPoint(stadt.HexagonPosition, stadt.HexagonPoint).Equals(currentGridPoint))!=null);

                            if (!stadtGefunden)
                            {
                                // Strassen überprüfen
                                var otherClients = catanClients.FindAll(_client => _client.ID != currentClient.ID);

                                foreach (var otherClient in otherClients)
                                {
                                    foreach (var strasse in otherClient.SpielfigurenContainer.Strassen)
                                    {
                                        allowedSiedlungen[rowIndex][columnIndex][pointIndex] = !HexagonGrid.IsGridPointOnStrasse(strasse, currentGridPoint);
                                    }
                                }
                            }
                            else
                            {
                                allowedSiedlungen[rowIndex][columnIndex][pointIndex] = false;
                            }
                        }
                    }
                }
            }
            return allowedSiedlungen;
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
