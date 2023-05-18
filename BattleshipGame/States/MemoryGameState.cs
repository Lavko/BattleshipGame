using System.Numerics;
using BattleshipGame.Ships;
using Spectre.Console;

namespace BattleshipGame.States;

public class MemoryGameState : IGameState
{
    public MemoryGameState(Table board, IList<Ship> ships, IList<Vector2> missedFires)
    {
        Board = board;
        Ships = ships;
        MissedFires = missedFires;
    }

    public Table Board { get; }
    public IList<Ship> Ships { get; }
    public IList<Vector2> MissedFires { get; }
    public int FiresCount { get; set; }
}
