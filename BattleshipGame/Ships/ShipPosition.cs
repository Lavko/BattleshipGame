using System.Numerics;

namespace BattleshipGame.Ships;

public class ShipPosition
{
    public Vector2 Coordinate { get; internal set; }
    public bool IsDamaged { get; set; }

    public ShipPosition(Vector2 coordinate, bool isDamaged)
    {
        Coordinate = coordinate;
        IsDamaged = isDamaged;
    }
}