using Catan.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Server.LogicLayer
{
    static class RandomHexagonFieldGenerator
    {
        public static HexagonField[][] Generate()
        {

            LandFeld.LandfeldTyp[] randomHexagonFieldTypes = generateRandomHexagonFieldTyps();
            int[] nrChips = { 5, 2, 6, 3, 8, 10, 9, 12, 11, 4, 8, 10, 9, 4, 5, 6, 3, 11, 1 };


            // 7 Rows
            HexagonField[][] hexGrid = new HexagonField[5][];
            hexGrid[0] = new HexagonField[3]; // Row 0, Columns 3
            hexGrid[1] = new HexagonField[4];
            hexGrid[2] = new HexagonField[5];
            hexGrid[3] = new HexagonField[4];
            hexGrid[4] = new HexagonField[3];

            int count = 0;
            for (int rowIndex = 0; rowIndex < hexGrid.GetLength(0); rowIndex++)
            {
                var points = new HexagonPoint[6];

                for (int columnIndex = 0; columnIndex < hexGrid[rowIndex].GetLength(0); columnIndex++)
                {
                    var hexPosition = new HexagonPosition(rowIndex, columnIndex);
                    for (int pointIndex = 0; pointIndex < points.Length; pointIndex++)
                    {
                        var point = getGridPointByHexagonPosition(hexPosition, pointIndex);
                        points[pointIndex] = new HexagonPoint(point.Y, point.X);
                    }

                    hexGrid[rowIndex][columnIndex] = new HexagonField(hexPosition, randomHexagonFieldTypes[count], nrChips[count], points.ToList());
                    count++;
                }
            }
            return hexGrid;
        }
        private static Point getGridPointByHexagonPosition(HexagonPosition hexagonPosition, int hexPointIndex)
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
            if (hexPointIndex == 0)
                ;
            else if (hexPointIndex == 1)
                pointIndexTest.Offset(1, 1);
            else if (hexPointIndex == 2)
                pointIndexTest.Offset(1, 2);
            else if (hexPointIndex == 3)
                pointIndexTest.Offset(0, 3);
            else if (hexPointIndex == 4)
                pointIndexTest.Offset(-1, 2);
            else if (hexPointIndex == 5)
                pointIndexTest.Offset(-1, 1);
            else
                throw new NotImplementedException($"getGridPointByHexagonRowIndexColumnIndex ");

            if (hexagonPosition.ColumnIndex > 0)
                pointIndexTest.Offset(2 * hexagonPosition.ColumnIndex, 0);


            return pointIndexTest;
        }

        private static LandFeld.LandfeldTyp[] generateRandomHexagonFieldTyps()
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

    }
}
