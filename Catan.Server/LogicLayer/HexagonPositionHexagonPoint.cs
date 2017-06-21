using Catan.Game;

namespace Catan.Server.LogicLayer
{
    public class HexagonPositionHexagonPoint
    {
        public HexagonPosition HexagonPosition { private set; get; }
        public HexagonPoint Point { private set; get; }

        public HexagonPositionHexagonPoint(HexagonPosition hexagonPosition, HexagonPoint hexagonPoint)
        {
            this.HexagonPosition = hexagonPosition;
            this.Point = hexagonPoint;
        }
    }
}