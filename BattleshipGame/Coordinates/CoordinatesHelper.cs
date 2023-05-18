using System.Numerics;
using Spectre.Console;

namespace BattleshipGame.Coordinates;

public static class CoordinatesHelper
{
    public static Vector2 ReadAsCoordinates(string? input)
    {
        while (true)
        {
            if (string.IsNullOrEmpty(input))
            {
                AnsiConsole.WriteLine(
                    "Coordinates are empty, please select correct one (eg. A1, C5, I8):"
                );
                input = Console.ReadLine();
                continue;
            }

            if (input.Length < 2 && input.Length > 3)
            {
                AnsiConsole.WriteLine(
                    "Coordinates can have only 2 or 3 characters, please select correct one (eg. A1, C5, I8):"
                );
                input = Console.ReadLine();
                continue;
            }

            var xLetterCoordinate = input[0].ToString().ToUpper();
            var coordinates = new Vector2(xLetterCoordinate[0] - 65, int.Parse(input.Substring(1)));

            if (coordinates.X > GameController.BoardHeight - 1)
            {
                AnsiConsole.WriteLine(
                    $"X coordinate must be between 'A' and '{'A' + GameController.BoardHeight}', please select correct one (eg. A1, C5, I8):"
                );
                input = Console.ReadLine();
                continue;
            }

            if (coordinates.X > GameController.BoardWidth)
            {
                AnsiConsole.WriteLine(
                    $"X coordinate must be between '1' and '{GameController.BoardWidth}', please select correct one (eg. A1, C5, I8):"
                );
                input = Console.ReadLine();
                continue;
            }

            return coordinates;
        }
    }
}
