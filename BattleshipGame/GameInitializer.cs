using System.Numerics;
using BattleshipGame.Ships;
using Spectre.Console;

namespace BattleshipGame;

public class GameInitializer
{
    private readonly Random _random = new();

    public Table InitializeEmptyBoard()
    {
        var board = new Table();

        board.AddColumn(string.Empty);

        for (var i = 1; i <= GameController.BoardWidth; i++)
        {
            board.AddColumn(i.ToString());
        }

        for (var i = 0; i < GameController.BoardHeight; i++)
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
            var orientation = (Orientation)_random.Next(2);

            var randomPosition = GetRandomShipStartPosition(orientation, shipType);

            var ship = new Ship(shipType, randomPosition, orientation);

            if (
                ships
                    .SelectMany(s => s.Position)
                    .Select(p => p.Coordinate)
                    .Intersect(ship.Position.Select(p => p.Coordinate))
                    .Any()
            )
            {
                continue;
            }

            return ship;
        }
    }

    private Vector2 GetRandomShipStartPosition(Orientation orientation, ShipType shipType)
    {
        var maxXStartingPoint =
            orientation == Orientation.Vertical
                ? GameController.BoardHeight + 1 - Ship.GetShipSizeByType(shipType)
                : GameController.BoardHeight;

        var maxYStartingPoint =
            orientation == Orientation.Horizontal
                ? GameController.BoardWidth - Ship.GetShipSizeByType(shipType)
                : GameController.BoardWidth;

        return new Vector2(_random.Next(maxXStartingPoint), _random.Next(maxYStartingPoint) + 1);
    }
}
