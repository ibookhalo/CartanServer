using Catan.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Server.LogicLayer
{
    class HexagonGrid
    {
        private static HexagonGrid instance;
        public Hexagon[][] Hexagones { private set; get; }
        public List<Hexagon> HexagonesList
        {
            get
            {
                var hexagones = new List<Hexagon>();

                for (int rowIndex = 0; rowIndex < HexagonGrid.Instance.Hexagones.GetLength(0); rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < HexagonGrid.Instance.Hexagones[rowIndex].GetLength(0); columnIndex++)
                    {
                        hexagones.Add(HexagonGrid.Instance.Hexagones[rowIndex][columnIndex]);
                    }
                }
                return hexagones;
            }
        }
        private HexagonGrid()
        {
            generateHexagonGrid();
        }
        public static HexagonGrid Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HexagonGrid();
                }
                return instance;
            }
        }
        private LandFeld.LandfeldTyp[] generateRandomHexagonFieldTyps()
        {
            LandFeld.LandfeldTyp[] landFields =
         {
                LandFeld.LandfeldTyp.Weideland,
                LandFeld.LandfeldTyp.Ackerland,
                LandFeld.LandfeldTyp.BergwerkGold,
                LandFeld.LandfeldTyp.Eisenmine,
                LandFeld.LandfeldTyp.MeersFeld,
                LandFeld.LandfeldTyp.Wohnstaette,

                LandFeld.LandfeldTyp.Weideland,
                LandFeld.LandfeldTyp.Ackerland,
                LandFeld.LandfeldTyp.BergwerkGold,
                LandFeld.LandfeldTyp.Eisenmine,
                LandFeld.LandfeldTyp.MeersFeld,
                LandFeld.LandfeldTyp.Wohnstaette,

                LandFeld.LandfeldTyp.Weideland,
                LandFeld.LandfeldTyp.Ackerland,
                LandFeld.LandfeldTyp.Eisenmine,
                LandFeld.LandfeldTyp.MeersFeld,
                LandFeld.LandfeldTyp.Wohnstaette,

                LandFeld.LandfeldTyp.Weideland,
                LandFeld.LandfeldTyp.Ackerland,
            };
            Random rnd = new Random();
            List<int> landFieldsIndexes = new List<int>();
            List<LandFeld.LandfeldTyp> randomLandFieldTypes = new List<LandFeld.LandfeldTyp>();

            int newRandomFieldTypIndex;
            while (true)
            {
                newRandomFieldTypIndex = rnd.Next(0, landFields.Length);
                if (!landFieldsIndexes.Contains(newRandomFieldTypIndex))
                {
                    landFieldsIndexes.Add(newRandomFieldTypIndex);
                    randomLandFieldTypes.Add(landFields[newRandomFieldTypIndex]);

                    if (landFieldsIndexes.Count == landFields.Length)
                        break;
                }
            }
            return randomLandFieldTypes.ToArray();
        }
        public static bool IsHexagonEdgeOnHexagonEdge(HexagonPositionHexagonEdge hexagonPositionHexagonEdge1, HexagonPositionHexagonEdge hexagonPositionHexagonEdge2)
        {
            var gridPoint1_A = GetGridPointByHexagonPositionAndPoint(hexagonPositionHexagonEdge1.HexagonPosition, hexagonPositionHexagonEdge1.HexagonEdge.PointA);
            var gridPoint1_B = GetGridPointByHexagonPositionAndPoint(hexagonPositionHexagonEdge1.HexagonPosition, hexagonPositionHexagonEdge1.HexagonEdge.PointB);

            var gridPoint2_A = GetGridPointByHexagonPositionAndPoint(hexagonPositionHexagonEdge2.HexagonPosition, hexagonPositionHexagonEdge2.HexagonEdge.PointA);
            var gridPoint2_B = GetGridPointByHexagonPositionAndPoint(hexagonPositionHexagonEdge2.HexagonPosition, hexagonPositionHexagonEdge2.HexagonEdge.PointB);

            return (gridPoint1_A.Equals(gridPoint2_A) && gridPoint1_B.Equals(gridPoint2_B))  || (gridPoint1_A.Equals(gridPoint2_B) && gridPoint1_B.Equals(gridPoint2_A));
        }
        public static GridPoint GetGridPointByHexagonPositionAndPoint(HexagonPosition hexagonPosition, HexagonPoint hexPoint)
        {
            GridPoint pointIndexTest = null; // point index 0
            switch (hexagonPosition.RowIndex)
            {
                case 0: pointIndexTest = new GridPoint(3, hexagonPosition.RowIndex * 2); break;
                case 1: pointIndexTest = new GridPoint(2, hexagonPosition.RowIndex * 2); break;
                case 2: pointIndexTest = new GridPoint(1, hexagonPosition.RowIndex * 2); break;
                case 3: pointIndexTest = new GridPoint(2, hexagonPosition.RowIndex * 2); break;
                case 4: pointIndexTest = new GridPoint(3, hexagonPosition.RowIndex * 2); break;
                default:
                    throw new NotImplementedException($"getGridPointByHexagonRowIndexColumnIndex ");
            }
            if (hexPoint.Index == 0)
                ;
            else if (hexPoint.Index == 1)
                pointIndexTest.Offset(1, 1);
            else if (hexPoint.Index == 2)
                pointIndexTest.Offset(1, 2);
            else if (hexPoint.Index == 3)
                pointIndexTest.Offset(0, 3);
            else if (hexPoint.Index == 4)
                pointIndexTest.Offset(-1, 2);
            else if (hexPoint.Index == 5)
                pointIndexTest.Offset(-1, 1);
            else
                throw new NotImplementedException($"getGridPointByHexagonRowIndexColumnIndex ");

            if (hexagonPosition.ColumnIndex > 0)
                pointIndexTest.Offset(2 * hexagonPosition.ColumnIndex, 0);


            return pointIndexTest;
        }
        private void generateHexagonGrid()
        {
            LandFeld.LandfeldTyp[] randomHexagonFieldTypes = generateRandomHexagonFieldTyps();
            int[] nrChips = { 5, 2, 6, 3, 8, 10, 9, 12, 11, 4, 8, 10, 9, 4, 5, 6, 3, 11, 1 };


            // 7 Rows
            Hexagones = new Hexagon[5][];
            Hexagones[0] = new Hexagon[3]; // Row 0, Columns 3
            Hexagones[1] = new Hexagon[4];
            Hexagones[2] = new Hexagon[5];
            Hexagones[3] = new Hexagon[4];
            Hexagones[4] = new Hexagon[3];

            int count = 0;
            for (int rowIndex = 0; rowIndex < Hexagones.GetLength(0); rowIndex++)
            {
                var points = new HexagonPoint[6];

                for (int columnIndex = 0; columnIndex < Hexagones[rowIndex].GetLength(0); columnIndex++)
                {
                    var hexPosition = new HexagonPosition(rowIndex, columnIndex);
                    for (int pointIndex = 0; pointIndex < points.Length; pointIndex++)
                    {
                        points[pointIndex] = new HexagonPoint(pointIndex);
                    }

                    Hexagones[rowIndex][columnIndex] = new Hexagon(hexPosition, randomHexagonFieldTypes[count], nrChips[count], points.ToList());
                    count++;
                }
            }
        }
        public static List<HexagonPositionHexagonEdge> GetHexagonEdgesByGridPoint(List<Hexagon> hexagones, GridPoint gridPoint)
        {
            List<HexagonPositionHexagonEdge> hexFieldEdge = new List<HexagonPositionHexagonEdge>();
            foreach (Hexagon currentHex in hexagones)
            {
                foreach (HexagonEdge currentEdge in currentHex.Edges)
                {
                    if (IsGridPointOnHexagonEdge(currentHex.Position, currentEdge, gridPoint))
                        hexFieldEdge.Add(new HexagonPositionHexagonEdge(currentHex.Position, currentEdge));
                }
            }
            return hexFieldEdge.ToList();
        }
        public static List<Hexagon> GetHexagonesByGridPoint(GridPoint gridPoint)
        {
            var hexfields = Instance.Hexagones;

            List<Hexagon> matchedHex = new List<Hexagon>();
            for (int rowIndex = 0; rowIndex < hexfields.Length; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < hexfields[rowIndex].GetLength(0); columnIndex++)
                {
                    var currentHexagon = hexfields[rowIndex][columnIndex];
                    for (int pointIndex = 0; pointIndex < currentHexagon.Points.Count; pointIndex++)
                    {
                        if (GetGridPointByHexagonPositionAndPoint(hexfields[rowIndex][columnIndex].Position, new HexagonPoint(pointIndex)).Equals(gridPoint))
                            matchedHex.Add(currentHexagon);
                    }
                }
            }
            return matchedHex;
        }
        public static bool IsGridPointOnStrasse(Strasse strasse, GridPoint gridPoint)
        {
            return IsGridPointOnHexagonEdge(strasse.HexagonPosition, strasse.HexagonEdge, gridPoint);
        }
        public static bool IsGridPointOnHexagonEdge(HexagonPosition hexagonPosition, HexagonEdge hexagonEdge, GridPoint gridPoint)
        {
            return HexagonGrid.GetGridPointByHexagonPositionAndPoint(hexagonPosition, hexagonEdge.PointA).Equals(gridPoint) ||
                   HexagonGrid.GetGridPointByHexagonPositionAndPoint(hexagonPosition, hexagonEdge.PointB).Equals(gridPoint);

        }
    }
}
