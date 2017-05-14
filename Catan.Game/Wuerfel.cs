using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    public static class Wuerfel
    {
        private static Random random = new Random();

        public static int Wuerfeln()
        {
            return random.Next(2, 13);
        }
    }
}
