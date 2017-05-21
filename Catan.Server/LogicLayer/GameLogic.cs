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

            catanClients[0].SpielfigurenContainer.Siedlungen.Add(new Siedlung(new HexagonPosition(0, 0), new HexagonPoint(1)));
            catanClients[0].SpielfigurenContainer.Strassen.Add(new Strasse(new HexagonPosition(0, 1), new HexagonEdge(new HexagonPoint(5),new HexagonPoint(4),4)));

            //catanClients[1].SpielfigurenContainer.Siedlungen.Add(new Siedlung(new HexagonPosition(1,0), new HexagonPoint(0)));

            currentClient = catanClients[0];

            currentClient.AllowedSiedlungen = getAllowedSiedlungenByClient(currentClient);
            currentClient.AllowedStaedte = getAllowedStaedteByClient(currentClient);
            currentClient.AllowedStrassen = getAllowedStrassenByClient(currentClient);

            GameStateMessage gameState = new GameStateMessage(catanClients, currentClient, HexagonGrid.Instance.Hexagones);

            iNetworkLayer.SendBroadcastMessage(gameState);
        }
        private bool[][][] getAllowedStrassenByClient(CatanClient currentClient)
        {
            bool[][][] allowedStrassen = initilize3DBoolArrayBasedOnHexfields();

            #region An den eigenen Siedlungen und Städten dürfen Straßen gebaut werden

            // Siedlungen
            foreach (var siedlung in currentClient.SpielfigurenContainer.Siedlungen)
            {
                var hexPosEdges=HexagonGrid.GetHexagonEdgesByGridPoint(HexagonGrid.Instance.HexagonesList, HexagonGrid.GetGridPointByHexagonPositionAndPoint(siedlung.HexagonPosition, siedlung.HexagonPoint));
                foreach (var hexPosEdge in hexPosEdges)
                {
                    allowedStrassen[hexPosEdge.HexagonPosition.RowIndex][hexPosEdge.HexagonPosition.ColumnIndex][hexPosEdge.HexagonEdge.Index]=
                    currentClient.SpielfigurenContainer.Strassen.Find(strasse => 
                    HexagonGrid.IsHexagonEdgeOnHexagonEdge(hexPosEdge,new HexagonPositionHexagonEdge(strasse.HexagonPosition,strasse.HexagonEdge)))==null;
                }
            }
            // Städte
            foreach (var stadt in currentClient.SpielfigurenContainer.Staedte)
            {
                var hexPosEdges = HexagonGrid.GetHexagonEdgesByGridPoint(HexagonGrid.Instance.HexagonesList, HexagonGrid.GetGridPointByHexagonPositionAndPoint(stadt.HexagonPosition, stadt.HexagonPoint));
                foreach (var hexPosEdge in hexPosEdges)
                {
                    allowedStrassen[hexPosEdge.HexagonPosition.RowIndex][hexPosEdge.HexagonPosition.ColumnIndex][hexPosEdge.HexagonEdge.Index] =
                    currentClient.SpielfigurenContainer.Strassen.Find(strasse =>
                    HexagonGrid.IsHexagonEdgeOnHexagonEdge(hexPosEdge, new HexagonPositionHexagonEdge(strasse.HexagonPosition, strasse.HexagonEdge))) == null;
                }
            }

            #endregion

            #region An den eigenen Straßen dürfen Straßen gebaut werden

            foreach (var strasse in currentClient.SpielfigurenContainer.Strassen)
            {
                var gridPointA = HexagonGrid.GetGridPointByHexagonPositionAndPoint(strasse.HexagonPosition, strasse.HexagonEdge.PointA);
                var gridPointB = HexagonGrid.GetGridPointByHexagonPositionAndPoint(strasse.HexagonPosition, strasse.HexagonEdge.PointB);

                var hexagonesA = HexagonGrid.GetHexagonesByGridPoint(gridPointA);
                var hexagonesB = HexagonGrid.GetHexagonesByGridPoint(gridPointB);

                var edgesA=HexagonGrid.GetHexagonEdgesByGridPoint(hexagonesA, gridPointA).Where(hexPosEdge=> 
                {

                    HexagonGrid.GetGridPointByHexagonPositionAndPoint(hexPosEdge.HexagonPosition, hexPosEdge.HexagonEdge.PointA).Equals(gridPointA);
                }


                var edgesB = HexagonGrid.GetHexagonEdgesByGridPoint(hexagonesB, gridPointB);




                var otherClients=catanClients.FindAll(client => client.ID != currentClient.ID);
                foreach (var otherClient in otherClients)
                {
                    otherClient.SpielfigurenContainer.
                }









                foreach (var hex in hexagones)
                {
                    hex.
                }

            }

            #endregion
            /*
            var otherClients = catanClients.FindAll(_client => _client.ID != currentClient.ID);
            foreach (var otherClient in otherClients)
            {
                foreach (var otherClientSiedlung in otherClient.SpielfigurenContainer.Siedlungen)
                {
                    foreach (var hexPosEdge in hexagonsPositionHexagonEdge)
                    {
                        allowedStrassen[hexPosEdge.HexagonPosition.RowIndex][hexPosEdge.HexagonPosition.ColumnIndex][hexPosEdge.HexagonEdge.Index]
                            = !HexagonGrid.IsGridPointOnHexagonEdge(hexPosEdge.HexagonPosition, hexPosEdge.HexagonEdge,
                        HexagonGrid.GetGridPointByHexagonPositionAndPoint(otherClientSiedlung.HexagonPosition, otherClientSiedlung.HexagonPoint));
                    }
                }

                foreach (var otherClientStadt in otherClient.SpielfigurenContainer.Staedte)
                {
                    foreach (var hexPosEdge in hexagonsPositionHexagonEdge)
                    {
                        if (allowedStrassen[hexPosEdge.HexagonPosition.RowIndex][hexPosEdge.HexagonPosition.ColumnIndex][hexPosEdge.HexagonEdge.Index])
                        {
                            allowedStrassen[hexPosEdge.HexagonPosition.RowIndex][hexPosEdge.HexagonPosition.ColumnIndex][hexPosEdge.HexagonEdge.Index]
                            = !HexagonGrid.IsGridPointOnHexagonEdge(hexPosEdge.HexagonPosition, hexPosEdge.HexagonEdge,
                               HexagonGrid.GetGridPointByHexagonPositionAndPoint(otherClientStadt.HexagonPosition, otherClientStadt.HexagonPoint));
                        }
                    }
                }
            }*/
            return allowedStrassen;
        }
        private bool[][][] getAllowedStaedteByClient(CatanClient currentClient)
        {
            var allowedStaedte = initilize3DBoolArrayBasedOnHexfields();

            foreach (var siedlung in currentClient.SpielfigurenContainer.Siedlungen)
            {
                var gridPoint = HexagonGrid.GetGridPointByHexagonPositionAndPoint(siedlung.HexagonPosition, siedlung.HexagonPoint);
                foreach (var hexPosEdge in HexagonGrid.GetHexagonEdgesByGridPoint(HexagonGrid.GetHexagonesByGridPoint(gridPoint),gridPoint))
                {
                    if (currentClient.SpielfigurenContainer.Strassen.Find(strasse =>
                        HexagonGrid.IsHexagonEdgeOnHexagonEdge(hexPosEdge, new HexagonPositionHexagonEdge(strasse.HexagonPosition, strasse.HexagonEdge))) != null)
                    {
                        allowedStaedte[siedlung.HexagonPosition.RowIndex][siedlung.HexagonPosition.ColumnIndex][siedlung.HexagonPoint.Index] = true;
                        break;
                    }
                }
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

                        var foundHexagones = HexagonGrid.GetHexagonesByGridPoint(currentGridPoint);
                        if (foundHexagones.Count >= 2)
                        {
                            #region Überprüfen, ob es hier eine Stadt oder Siedlung gibt

                            var stadtGefunden = catanClients.Exists(_client => _client.SpielfigurenContainer.Staedte.Find(
                                stadt => HexagonGrid.GetGridPointByHexagonPositionAndPoint(stadt.HexagonPosition, stadt.HexagonPoint).Equals(currentGridPoint))!=null);

                            var siedlungGefunden = catanClients.Exists(_client => _client.SpielfigurenContainer.Siedlungen.Find(
                                siedlung=> HexagonGrid.GetGridPointByHexagonPositionAndPoint(siedlung.HexagonPosition, siedlung.HexagonPoint).Equals(currentGridPoint)) != null);

                            #endregion

                            if (!stadtGefunden && !siedlungGefunden)
                            {
                                #region Überprüfen, ob die drei angrenzenden Kreuzungen von Siedlungen oder Städten besetzt sind

                                allowedSiedlungen[rowIndex][columnIndex][pointIndex]=catanClients.Find(_client =>

                                _client.SpielfigurenContainer.Siedlungen.Find(siedlung =>
                                HexagonGrid.GetHexagonEdgesByGridPoint(foundHexagones, currentGridPoint).Find(hex =>
                                HexagonGrid.IsGridPointOnHexagonEdge(hex.HexagonPosition, hex.HexagonEdge,
                                HexagonGrid.GetGridPointByHexagonPositionAndPoint(siedlung.HexagonPosition, siedlung.HexagonPoint)))!=null)!=null ||

                                 _client.SpielfigurenContainer.Staedte.Find(stadt =>
                                HexagonGrid.GetHexagonEdgesByGridPoint(foundHexagones, currentGridPoint).Find(hex =>
                                HexagonGrid.IsGridPointOnHexagonEdge(hex.HexagonPosition, hex.HexagonEdge,
                                HexagonGrid.GetGridPointByHexagonPositionAndPoint(stadt.HexagonPosition, stadt.HexagonPoint))) != null)!=null) ==null;

                                #endregion
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
