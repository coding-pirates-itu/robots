using Robots.Lib;

namespace Robots;


internal class Renderer
{
    public static void WriteField(Game game)
    {
        Console.SetCursorPosition(0, 0);
        var frame = ConsoleColor.Gray;
        Console.ForegroundColor = frame;
        Console.WriteLine("ROBOTS");
        WriteTopHorizontalLine(game.Width);

        for (int y = 0; y < game.Height; y++)
        {
            Console.ForegroundColor = frame;
            Console.Write('│'); // 179

            for (int x = 0; x < game.Width; x++)
            {
                var sym = game.Cell(x, y) switch
                {
                    CellStates.Robot => "╣╠", // 185, 204
                    CellStates.Player => "◄►", // 17, 16
                    CellStates.Trash => "▓▓", // 178 x 2
                    CellStates.Rip => "▄▄", // 220 x 2
                    CellStates.Won => "☺☺", // 1 x 2
                    _ => "  "
                };
                var color = game.Cell(x, y) switch
                {
                    CellStates.Robot => ConsoleColor.Blue,
                    CellStates.Player => ConsoleColor.White,
                    CellStates.Trash => ConsoleColor.DarkGray,
                    CellStates.Rip => ConsoleColor.Gray,
                    CellStates.Won => ConsoleColor.Yellow,
                    _ => ConsoleColor.White
                };
                Console.ForegroundColor = color;
                Console.Write(sym);
            }

            Console.ForegroundColor = frame;
            Console.WriteLine('│'); // 179
        }

        WriteBottomHorizontalLine(game.Width);
    }


    private static void WriteTopHorizontalLine(int width)
    {
        Console.WriteLine("┌" + new string('─', width * 2) + "┐"); // 218, 196, 191
    }


    private static void WriteBottomHorizontalLine(int width)
    {
        Console.WriteLine("└" + new string('─', width * 2) + "┘"); // 192, 196, 217
    }
}
