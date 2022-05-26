using Bot;
using GameLib;
using GameLib.Exceptions;

var game = new Board();

int selectedX = 0, selectedY = 0;

while (!game.Winner.HasValue)
{
    if (game.PlayerTurn == 1)
    {
        game.MakeTurn(MinMax.GetBestMove(game, 4));
    }
    
    
    Console.BackgroundColor = ConsoleColor.Black;
    Console.Clear();

    for (int y = 0; y < game.LengthY; y++)
    {
        for (int x = 0; x < game.LengthX; x++)
        {
            if (selectedX == x && selectedY == y)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            switch (game[x, y].State)
            {
                case CellState.Wall:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write('#');
                    break;
                case CellState.Zero:
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write('.');
                    break;
                default:
                    Console.BackgroundColor = game[x, y].Player switch
                    {
                        0 => ConsoleColor.Blue,
                        1 => ConsoleColor.Red,
                        2 => ConsoleColor.Green,
                        3 => ConsoleColor.Yellow,
                        _ => throw new Exception()
                    };
                    Console.Write((byte)game[x, y].State);
                    break;
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
            selectedX = (selectedX - 1 + game.LengthX) % game.LengthX;
            break;
        case ConsoleKey.RightArrow:
            selectedX = (selectedX + 1) % game.LengthX;
            break;
        case ConsoleKey.UpArrow:
            selectedY = (selectedY - 1 + game.LengthY) % game.LengthY;
            break;
        case ConsoleKey.DownArrow:
            selectedY = (selectedY + 1) % game.LengthY;
            break;
        case ConsoleKey.Enter:
            try
            {
                game.MakeTurn(selectedX, selectedY);
            }
            catch (InvalidTurnException)
            {
                // ignored
            }
            break;
    }
}