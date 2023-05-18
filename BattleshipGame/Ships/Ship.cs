using System.Numerics;

namespace BattleshipGame.Ships;

public class Ship
{
    private const int BattleshipSize = 5;
    private const int DestroyerSize = 4;
    
    public ShipType Type { get; internal set; }
    public ShipPosition[] Position { get; internal set; }

    public Ship(ShipType type, Vector2 startPoint, Orientation orientation)
    {
        Type = type;
        SetPosition(startPoint, GetShipSizeByType(type), orientation);
    }

    public static int GetShipSizeByType(ShipType type) => type == ShipType.Battleship ? BattleshipSize : DestroyerSize;
    
    private void SetPosition(Vector2 startPoint, int size, Orientation orientation)
    {
        
        Position = new ShipPosition[size];
        Position[0] = new ShipPosition(startPoint, false);

        if (orientation == Orientation.Horizontal)
        {
            SetHorizontalPosition(size);
        }
        else
        {
            SetVerticalPosition(size);
        }

        
    }

    private void SetHorizontalPosition(int size)
    {
        for (var i = 1; i < size; i++)
        {
            Position[i] = new ShipPosition(new Vector2(Position[i - 1].Coordinate.X, Position[i - 1].Coordinate.Y + 1), false);
        }
    }
    
    private void SetVerticalPosition(int size)
    {
        for (var i = 1; i < size; i++)
        {
            Position[i] = new ShipPosition(new Vector2(Position[i - 1].Coordinate.X + 1, Position[i - 1].Coordinate.Y), false);
        }
    }
}