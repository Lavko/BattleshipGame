using System.Numerics;
using BattleshipGame.Ships;
using Spectre.Console;

namespace BattleshipGame;

public class GameController
{
    private const bool ShowShips = false;
    private const int BoardWidth = 10;
    private const int BoardHeight = 10;

    private int _firesCount = 0;

    private readonly GameRenderer _renderer;
    private readonly Table _board;
    private readonly IEnumerable<Ship> _ships;
    private readonly IList<Vector2> _missedFires;

    public GameController()
    {
        var initializer = new GameInitializer();

        _renderer = new GameRenderer(ShowShips);
        _board = initializer.InitializeEmptyBoard();
        _ships = initializer.InitializeShips();
        _missedFires = new List<Vector2>();
    }

    public void Start()
    {
        var areDestroyed = false;
        
        while (!areDestroyed)
        {
            _renderer.UpdateScreen(_board, _ships, _missedFires, _firesCount);
            
            var fireCoordinates = ReadAsField(Console.ReadLine());
            _firesCount++;

            CheckCoordinateAsDamaged(fireCoordinates);

            areDestroyed = _ships.SelectMany(s => s.Position).All(p => p.IsDamaged);
        }
        
        AnsiConsole.Clear();
        _renderer.UpdateScreen(_board, _ships, _missedFires, _firesCount);
        AnsiConsole.WriteLine("You won!");
    }

    private Vector2 ReadAsField(string? input)
    {
        while (true)
        {
            if (string.IsNullOrEmpty(input))
            {
                AnsiConsole.WriteLine("Coordinates are empty, please select correct one (eg. A1, C5, I8):");
                input = Console.ReadLine();
                continue;
            }

            if (input.Length < 2 && input.Length > 3)
            {
                AnsiConsole.WriteLine("Coordinates can have only 2 or 3 characters, please select correct one (eg. A1, C5, I8):");
                input = Console.ReadLine();
                continue;
            }

            var coordinates = new Vector2(input[0] - 65, int.Parse(input.Substring(1)));

            if (coordinates.X > BoardHeight - 1)
            {
                AnsiConsole.WriteLine($"X coordinate must be between 'A' and '{'A' + BoardHeight}', please select correct one (eg. A1, C5, I8):");
                input = Console.ReadLine();
                continue;
            }

            if (coordinates.X > BoardWidth)
            {
                AnsiConsole.WriteLine($"X coordinate must be between '1' and '{BoardWidth}', please select correct one (eg. A1, C5, I8):");
                input = Console.ReadLine();
                continue;
            }

            return coordinates;
        }
    }

    private void CheckCoordinateAsDamaged(Vector2 fireCoordinates)
    {
        var shipCoordinate = _ships.SelectMany(s => s.Position).FirstOrDefault(p => p.Coordinate == fireCoordinates);
        
        if (shipCoordinate is not null)
        {
            shipCoordinate.IsDamaged = true;
        }
        else
        {
            _missedFires.Add(fireCoordinates);
        }
    }
}