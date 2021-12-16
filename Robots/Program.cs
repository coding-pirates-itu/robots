using RobotsLib;


var width = 8;
var height = 8;
var game = new Game(width, height, 10);

while (!game.IsEnded)
{
    WriteField(game);
    var command = GetInput();
    game.Execute(command);
}


void WriteField(Game game)
{
    Console.SetCursorPosition(0, 0);
    Console.WriteLine("ROBOTS");
    WriteHorizontalLine(width);

    for (int y = 0; y < height; y++)
    {
        Console.Write('|');

        for (int x = 0; x < width; x++)
        {
            var sym = game.Cell(x, y) switch
            {
                CellStates.Robot => "oo",
                CellStates.Player => "><",
                CellStates.Trash => "WW",
                CellStates.Rip => "xx",
                _ => "  "
            };
            Console.Write(sym);
        }

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
        ConsoleKey.Spacebar => Commands.Stay,
        ConsoleKey.Escape => Commands.Die,
        _ => Commands.None
    };
}
