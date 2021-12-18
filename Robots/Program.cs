using RobotsLib;


var width = 8;
var height = 8;
var game = new Game(width, height, 10);

while (!game.IsEnded)
{
    Robots.Renderer.WriteField(game);
    var command = GetInput();
    game.Execute(command);
}

Robots.Renderer.WriteField(game);


static Commands GetInput()
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
