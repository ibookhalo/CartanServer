﻿using System;
using System.Collections.Generic;

namespace Catan.Game
{
    [Serializable]
    public class HexagonEdge
    {
        public List<HexagonPoint> Points { private set; get; }

        public HexagonEdge(List<HexagonPoint> points)
        {
            this.Points = points;
        }
    }
}