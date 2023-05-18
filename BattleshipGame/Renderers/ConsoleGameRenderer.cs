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
        AnsiConsole.MarkupLine(
            gameWon ? "[bold green]You won![/]" : "Select field to fire (eg. A1, C5, I8)"
        );
    }

    private void UpdateBoard(IGameState state)
    {
        foreach (var ship in state.Ships)
        {
            foreach (var position in ship.Position)
            {
                state.Board.Rows.Update(
                    (int)position.Coordinate.X,
                    (int)position.Coordinate.Y,
                    new Markup(
                        position.IsDamaged
                            ? ship.IsSunk()
                                ? "[bold darkred]X[/]"
                                : "[red]x[/]"
                            : _showShips
                                ? "[green]O[/]"
                                : string.Empty
                    )
                );
            }
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

        grid.AddColumns(4);

        grid.AddRow(
            new Markup("[red]Hits:[/]"),
            new Markup("[green]Misses:[/]"),
            new Markup("Total shots:"),
            new Markup("[darkred]Sunk ships:[/]")
        );
        grid.AddRow(
            state.Ships.SelectMany(s => s.Position).Count(p => p.IsDamaged).ToString(),
            state.MissedFires.Count.ToString(),
            state.FiresCount.ToString(),
            state.Ships.Count(s => s.IsSunk()).ToString()
        );

        AnsiConsole.Write(grid);
    }
}
