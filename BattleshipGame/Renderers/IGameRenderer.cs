using BattleshipGame.States;

namespace BattleshipGame.Renderers;

public interface IGameRenderer
{
    void UpdateScreen(IGameState state, bool gameWon);
}
