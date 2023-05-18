using BattleshipGame.States;
using Spectre.Console;

namespace BattleshipGame.Renderers;

public class ConsoleGameRenderer : IGameRenderer
{
    private readonly bool _showShips;

    public ConsoleGameRenderer(bool showShips)
    {
        _showShips = showShips;
    }

    public void UpdateScreen(IGameState state, bool gameWon)
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[bold grey]BattleShip game[/]");

        UpdateBoard(state);

        UpdateScore(state);

        AnsiConsole.WriteLine();
        if (!gameWon)
        {
            AnsiConsole.WriteLine("Select field to fire (eg. A1, C5, I8)");
        }
    }

    private void UpdateBoard(IGameState state)
    {
        foreach (var position in state.Ships.SelectMany(s => s.Position))
        {
            state.Board.Rows.Update(
                (int)position.Coordinate.X,
                (int)position.Coordinate.Y,
                new Markup(
                    position.IsDamaged
                        ? "[red]X[/]"
                        : _showShips
                            ? "[green]O[/]"
                            : string.Empty
                )
            );
        }

        foreach (var missedFire in state.MissedFires)
        {
            state.Board.Rows.Update((int)missedFire.X, (int)missedFire.Y, new Text("."));
        }

        AnsiConsole.Write(state.Board);
    }

    private void UpdateScore(IGameState state)
    {
        var grid = new Grid();

        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();

        grid.AddRow(
            new Markup("[green]Hits:[/]"),
            new Markup("[red]Misses:[/]"),
            new Markup("Total shots:")
        );
        grid.AddRow(
            state.Ships.SelectMany(s => s.Position).Count(p => p.IsDamaged).ToString(),
            state.MissedFires.Count.ToString(),
            state.FiresCount.ToString()
        );

        AnsiConsole.Write(grid);
    }
}
