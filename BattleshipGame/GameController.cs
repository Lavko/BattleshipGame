using System.Numerics;
using BattleshipGame.Coordinates;
using BattleshipGame.Renderers;
using BattleshipGame.States;
using Spectre.Console;

namespace BattleshipGame;

public class GameController
{
    public const int BoardWidth = 10;
    public const int BoardHeight = 10;

    private readonly IGameRenderer _renderer;
    private readonly IGameState _gameState;

    public GameController(IGameState gameState, IGameRenderer renderer)
    {
        _gameState = gameState;
        _renderer = renderer;
    }

    public void Start()
    {
        var isGameWon = false;

        while (!isGameWon)
        {
            _renderer.UpdateScreen(_gameState, false);

            CheckCoordinateAsDamaged(CoordinatesHelper.ReadAsCoordinates(Console.ReadLine()));
            _gameState.FiresCount++;

            isGameWon = CheckIfAllShipsDestroyed();
        }

        _renderer.UpdateScreen(_gameState, true);
    }

    private void CheckCoordinateAsDamaged(Vector2 fireCoordinates)
    {
        var shipCoordinate = _gameState.Ships
            .SelectMany(s => s.Position)
            .FirstOrDefault(p => p.Coordinate == fireCoordinates);

        if (shipCoordinate is not null)
        {
            shipCoordinate.IsDamaged = true;
        }
        else
        {
            _gameState.MissedFires.Add(fireCoordinates);
        }
    }

    private bool CheckIfAllShipsDestroyed() =>
        _gameState.Ships.SelectMany(s => s.Position).All(p => p.IsDamaged);
}
