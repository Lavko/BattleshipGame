using System.Numerics;
using BattleshipGame.Ships;
using Spectre.Console;

namespace BattleshipGame.States;

public interface IGameState
{
    Table Board { get; }
    IList<Ship> Ships { get; }
    IList<Vector2> MissedFires { get; }
    int FiresCount { get; set; }
}