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
        public HexagonField[][] Hexagones;
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
        public static Point GetGridPointByHexagonPositionAndPoint(HexagonPosition hexagonPosition, HexagonPoint hexPoint)
        {
            Point pointIndexTest = new Point(); // point index 0
            switch (hexagonPosition.RowIndex)
            {
                case 0: pointIndexTest = new Point(3, hexagonPosition.RowIndex * 2); break;
                case 1: pointIndexTest = new Point(2, hexagonPosition.RowIndex * 2); break;
                case 2: pointIndexTest = new Point(1, hexagonPosition.RowIndex * 2); break;
                case 3: pointIndexTest = new Point(2, hexagonPosition.RowIndex * 2); break;
                case 4: pointIndexTest = new Point(3, hexagonPosition.RowIndex * 2); break;
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
            Hexagones = new HexagonField[5][];
            Hexagones[0] = new HexagonField[3]; // Row 0, Columns 3
            Hexagones[1] = new HexagonField[4];
            Hexagones[2] = new HexagonField[5];
            Hexagones[3] = new HexagonField[4];
            Hexagones[4] = new HexagonField[3];

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

                    Hexagones[rowIndex][columnIndex] = new HexagonField(hexPosition, randomHexagonFieldTypes[count], nrChips[count], points.ToList());
                    count++;
                }
            }
        }
        public static List<HexagonFieldHexagonEdge> GetHexagonEdgesByGridIndex(List<HexagonField> hexagones, int gridHexRow, int gridHexColumn)
        {
            List<HexagonFieldHexagonEdge> hexFieldEdge = new List<HexagonFieldHexagonEdge>();
            foreach (HexagonField currentHex in hexagones)
            {
                foreach (HexagonEdge currentEdge in currentHex.Edges)
                {
                    var gridPoint_A =GetGridPointByHexagonPositionAndPoint(currentHex.Position, currentEdge.PointA);
                    var gridPoint_B = GetGridPointByHexagonPositionAndPoint(currentHex.Position, currentEdge.PointB);
                    if ((gridPoint_A.X == gridHexColumn && gridPoint_A.Y == gridHexRow) || (gridPoint_B.X == gridHexColumn && gridPoint_B.Y == gridHexRow))
                        hexFieldEdge.Add(new HexagonFieldHexagonEdge(currentHex, currentEdge));
                }
            }
            return hexFieldEdge.ToList();
        }
        public static List<HexagonField> GetHexagonesByGridIndex(int gridRowIndex, int gridColumnIndex)
        {
            var hexfields = Instance.Hexagones;

            List<HexagonField> matchedHex = new List<HexagonField>();
            for (int rowIndex = 0; rowIndex < hexfields.Length; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < hexfields[rowIndex].GetLength(0); columnIndex++)
                {
                    var currentHexagon = hexfields[rowIndex][columnIndex];
                    for (int pointIndex = 0; pointIndex < currentHexagon.Points.Count; pointIndex++)
                    {
                        var _gridColumnIndex=GetGridPointByHexagonPositionAndPoint(hexfields[rowIndex][columnIndex].Position, new HexagonPoint(pointIndex)).X;
                        var _gridRowIndex = GetGridPointByHexagonPositionAndPoint(hexfields[rowIndex][columnIndex].Position, new HexagonPoint(pointIndex)).Y;

                        if (_gridColumnIndex == gridColumnIndex && _gridRowIndex == gridRowIndex)
                            matchedHex.Add(currentHexagon);
                    }
                }
            }
            return matchedHex;
        }

    }
}
