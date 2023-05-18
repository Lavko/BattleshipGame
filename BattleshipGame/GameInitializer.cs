using System.Numerics;
using BattleshipGame.Ships;
using Spectre.Console;

namespace BattleshipGame;

public class GameInitializer
{
    private const int BoardWidth = 10;
    private const int BoardHeight = 10;
    private readonly Random _random = new();

    public Table InitializeEmptyBoard()
    {
        var board = new Table();

        board.AddColumn(string.Empty);

        for (var i = 1; i <= BoardWidth; i++)
        {
            board.AddColumn(i.ToString());
        }

        for (var i = 0; i < BoardHeight; i++)
        {
            board.AddRow(Convert.ToChar('A' + i).ToString());
        }

        return board;
    }

    public IList<Ship> InitializeShips()
    {
        var ships = new List<Ship>();
        ships.Add(GetRandomShip(ShipType.Battleship, ships));
        ships.Add(GetRandomShip(ShipType.Destroyer, ships));
        ships.Add(GetRandomShip(ShipType.Destroyer, ships));

        return ships;
    }

    private Ship GetRandomShip(ShipType shipType, IReadOnlyCollection<Ship> ships)
    {
        while (true)
        {
            var ran = _random.Next(2);
            var orientation = (Orientation)ran;

            var maxXStartingPoint = orientation == Orientation.Vertical ? BoardHeight + 1 - Ship.GetShipSizeByType(shipType) : BoardHeight;
            var maxYStartingPoint = orientation == Orientation.Horizontal ? BoardWidth - Ship.GetShipSizeByType(shipType) : BoardWidth;
            var startXPoint = _random.Next(maxXStartingPoint);
            var startYPoint = _random.Next(maxYStartingPoint) + 1;

            var ship = new Ship(shipType, new Vector2(startXPoint, startYPoint), orientation);

            if (ships.SelectMany(s => s.Position).Intersect(ship.Position).Any())
            {
                continue;
            }

            return ship;
        }
    }
}