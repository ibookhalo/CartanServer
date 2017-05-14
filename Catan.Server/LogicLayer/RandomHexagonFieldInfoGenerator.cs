using Catan.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Server.LogicLayer
{
    static class RandomHexagonFieldInfoGenerator
    {
        public static HexagonField[][] Generate()
        {

            LandFeld.LandfeldTyp[] randomHexagonFieldTypes = generateRandomHexagonFieldTyps();
            uint[] nrChips = {5, 2, 6, 3, 8, 10,9, 12, 11, 4, 8, 10, 9, 4, 5, 6, 3 , 11,1 };


            // 7 Rows
            HexagonField[][] hexGrid = new HexagonField[5][];
            hexGrid[0] = new HexagonField[3]; // Row 0, Columns 3
            hexGrid[1] = new HexagonField[4];
            hexGrid[2] = new HexagonField[5];
            hexGrid[3] = new HexagonField[4];
            hexGrid[4] = new HexagonField[3];

            uint count = 0;
            for (int rowIndex = 0; rowIndex < hexGrid.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < hexGrid[rowIndex].GetLength(0); columnIndex++)
                {
                    hexGrid[rowIndex][columnIndex] = new HexagonField(randomHexagonFieldTypes[count], nrChips[count]);
                    count++;
                }
            }
            return hexGrid;
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
