using RobotsLib;
using System.Drawing;


var width = 8;
var height = 8;
var game = new Game(width, height, 10);

while (!game.IsEnded)
{
    WriteField(game);
    var command = GetInput();
    game.Execute(command);
}

WriteField(game);


void WriteField(Game game)
{
    Console.SetCursorPosition(0, 0);
    var frame = ConsoleColor.Gray;
    Console.ForegroundColor = frame;
    Console.WriteLine("ROBOTS");
    WriteHorizontalLine(width);

    for (int y = 0; y < height; y++)
    {
        Console.ForegroundColor = frame;
        Console.Write('|');

        for (int x = 0; x < width; x++)
        {
            var sym = game.Cell(x, y) switch
            {
                CellStates.Robot => "}{",
                CellStates.Player => "<>",
                CellStates.Trash => "/\\",
                CellStates.Rip => "xx",
                CellStates.Won => "!!",
                _ => "  "
            };
            var color = game.Cell(x, y) switch
            {
                CellStates.Robot => ConsoleColor.Red,
                CellStates.Player => ConsoleColor.White,
                CellStates.Trash => ConsoleColor.Blue,
                CellStates.Rip => ConsoleColor.Gray,
                CellStates.Won => ConsoleColor.Yellow,
                _ => ConsoleColor.White
            };
            Console.ForegroundColor = color;
            Console.Write(sym);
        }

        Console.ForegroundColor = frame;
        Console.WriteLine('|');
    }

    WriteHorizontalLine(width);
}


void WriteHorizontalLine(int width)
{
    Console.WriteLine("+" + new string('-', width * 2) + "+");
}


Commands GetInput()
{
    var ch = Console.ReadKey();

    return ch.Key switch
    {
        ConsoleKey.UpArrow => Commands.Up,
        ConsoleKey.DownArrow => Commands.Down,
        ConsoleKey.LeftArrow => Commands.Left,
        ConsoleKey.RightArrow => Commands.Right,

        ConsoleKey.W => Commands.Stay,
        ConsoleKey.Spacebar => Commands.Teleport,
        ConsoleKey.Escape => Commands.Die,

        ConsoleKey.NumPad1 => Commands.DownLeft,
        ConsoleKey.NumPad2 => Commands.Down,
        ConsoleKey.NumPad3 => Commands.DownRight,
        ConsoleKey.NumPad4 => Commands.Left,
        ConsoleKey.NumPad5 => Commands.Teleport,
        ConsoleKey.NumPad6 => Commands.Right,
        ConsoleKey.NumPad7 => Commands.UpLeft,
        ConsoleKey.NumPad8 => Commands.Up,
        ConsoleKey.NumPad9 => Commands.UpRight,

        _ => Commands.None
    };
}
