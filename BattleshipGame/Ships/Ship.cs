using System.Numerics;

namespace BattleshipGame.Ships;

public class Ship
{
    private const int BattleshipSize = 5;
    private const int DestroyerSize = 4;

    public ShipType Type { get; internal set; }
    public ShipPosition[] Position { get; private set; } = null!;

    public Ship(ShipType type, Vector2 startPoint, Orientation orientation)
    {
        Type = type;
        SetPosition(startPoint, GetShipSizeByType(type), orientation);
    }

    public bool IsSunk() => Position.All(p => p.IsDamaged);

    public static int GetShipSizeByType(ShipType type) =>
        type == ShipType.Battleship ? BattleshipSize : DestroyerSize;

    private void SetPosition(Vector2 startPoint, int size, Orientation orientation)
    {
        Position = new ShipPosition[size];
        Position[0] = new ShipPosition(startPoint, false);

        SetPosition(size, orientation);
    }

    private void SetPosition(int size, Orientation orientation)
    {
        for (var i = 1; i < size; i++)
        {
            var x = Position[i - 1].Coordinate.X;
            var y = Position[i - 1].Coordinate.Y;

            Position[i] = new ShipPosition(
                new Vector2(
                    orientation == Orientation.Horizontal ? x : x + 1,
                    orientation == Orientation.Horizontal ? y + 1 : y
                ),
                false
            );
        }
    }
}
