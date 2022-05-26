using GameLib;

namespace Bot;

public static class PositionEstimator
{
    public static int GetEvaluation(Board game)
    {
        if (game.PlayersCount > 2)
        {
            throw new NotSupportedException();
        }
    
        if (game.Winner.HasValue)
        {
            return int.MaxValue;
        }
        
        var result = 0;
        for (int x = 0; x < game.LengthX; x++)
        {
            for (int y = 0; y < game.LengthY; y++)
            {
                if (game[x, y].HasOwner)
                {
                    result += GetPointWeight(ref game, x, y);
                }
            }
        }

        return result;
    }

    private static int GetPointWeight(ref Board game, Point point)
        => GetPointWeight(ref game, point.X, point.Y);

    private static int GetPointWeight(ref Board game, int x, int y)
    {
        int result = 0;
        if (game[x, y].HasOwner)
        {
            if (game[x, y].Player == game.PlayerTurn)
            {
                result++;
                var fine = 0;
                
                fine = Math.Max(fine, GetFine(game[x, y], game[x - 1, y]));
                fine = Math.Max(fine, GetFine(game[x, y], game[x + 1, y]));
                fine = Math.Max(fine, GetFine(game[x, y], game[x, y - 1]));
                fine = Math.Max(fine, GetFine(game[x, y], game[x, y + 1]));

                result -= fine;
            }
            else
            {
                result--;
            }
        }

        return result;
    }

    private static int GetFine(Cell my, Cell notMy)
    {
        if (notMy.HasOwner && notMy.Player != my.Player && notMy.State >= my.State)
        {
            return ((byte)my.State + (byte)notMy.State) * ((byte)my.State + (byte)notMy.State);
        }

        return 0;
    }
}