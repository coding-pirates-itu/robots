using RobotsLib;


var width = 8;
var height = 8;
var game = new Game(width, height, 10);

WriteField(game);


void WriteField(Game game)
{
    Console.SetCursorPosition(0, 0);
    Console.WriteLine("ROBOTS");
    WriteHorizontalLine(width * 2);

    for (int y = 0; y < height; y++)
    {
        Console.Write('|');
        for (int x = 0; x < width; x++)
        {
            var sym = game.Cell(x, y) switch
            {
                CellState.Robot => "@@",
                CellState.Player => "><",
                CellState.Trash => "XX",
                _ => "  "
            };
            Console.Write(sym);
        }

        Console.WriteLine('|');
    }

    WriteHorizontalLine(width * 2);
}


void WriteHorizontalLine(int width)
{
    Console.Write("+");
    Console.Write(new string('-', width));
    Console.WriteLine("+");
}