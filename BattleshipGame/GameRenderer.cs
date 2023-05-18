using System.Numerics;
using BattleshipGame.Ships;
using Spectre.Console;

namespace BattleshipGame;

public class GameRenderer
{
    private readonly bool _showShips;

    public GameRenderer(bool showShips)
    {
        _showShips = showShips;
    }

    public void UpdateScreen(Table board, IEnumerable<Ship> ships, IList<Vector2> missedFires, int firesCount)
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[bold grey]BattleShip game[/]");

        UpdateBoard(board, ships, missedFires);
        
        UpdateScore(ships, missedFires, firesCount);
        
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine("Select field to fire (eg. A1, C5, I8)");
    }

    private void UpdateBoard(Table board, IEnumerable<Ship> ships, IList<Vector2> missedFires)
    {
        foreach (var position in ships.SelectMany(s => s.Position))
        {
            board.Rows.Update(
                (int)position.Coordinate.X, 
                (int)position.Coordinate.Y, 
                new Markup(position.IsDamaged ? "[red]X[/]" : _showShips ? "[green]O[/]" : string.Empty));
        }
        
        foreach (var missedFire in missedFires)
        {
            board.Rows.Update((int)missedFire.X, (int)missedFire.Y, new Text("."));
        }
        
        AnsiConsole.Write(board);
    }

    private void UpdateScore(IEnumerable<Ship> ships, IList<Vector2> missedFires, int firesCount)
    {
        var grid = new Grid();
        
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();

        grid.AddRow(new Markup[]{new ("[green]Hits:[/]"), new ("[red]Misses:[/]"), new ("Total shots:")});
        grid.AddRow(new[]
        {
            ships.SelectMany(s => s.Position).Count(p => p.IsDamaged).ToString(), 
            missedFires.Count.ToString(),
            firesCount.ToString()
        });
        
        AnsiConsole.Write(grid);
    }
}