using Bot;
using GameLib;
using GameLib.BitOperations;
using GameLib.Exceptions;

var game = new Board8X8();

int selectedX = 0, selectedY = 0;

while (!game.Ours.IsEmpty && !game.Three.IsEmpty)
{
    Console.BackgroundColor = ConsoleColor.Black;
    Console.Clear();

    for (int y = 0; y < 8; y++)
    {
        for (int x = 0; x < 8; x++)
        {
            if (selectedX == x && selectedY == y)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }

            if ((game.Ours | game.Theirs)[x, y])
            {
                Console.BackgroundColor = game.Ours[x, y] ? ConsoleColor.Blue : ConsoleColor.Red;
                if (game.One[x, y])
                {
                    Console.Write(1);
                }
                else if (game.Two[x, y])
                {
                    Console.Write(2);
                }
                else
                {
                    Console.Write(3);
                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write('.');
            }
        }
        Console.WriteLine();
    }

    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.White;
    // game.MakeTurn(MinMax.GetBestMove(game, 4));
    
    var key = Console.ReadKey();
    switch (key.Key)
    {
        case ConsoleKey.LeftArrow:
            selectedX = (selectedX - 1 + 8) % 8;
            break;
        case ConsoleKey.RightArrow:
            selectedX = (selectedX + 1) % 8;
            break;
        case ConsoleKey.UpArrow:
            selectedY = (selectedY - 1 + 8) % 8;
            break;
        case ConsoleKey.DownArrow:
            selectedY = (selectedY + 1) % 8;
            break;
        case ConsoleKey.Enter:
            try
            {
                game.Move(selectedX, selectedY);
            }
            catch (InvalidTurnException)
            {
                // ignored
            }
            break;
    }
}

