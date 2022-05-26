using GameLib;

namespace Bot;

public static class MoveSorter
{
    public static IEnumerable<Point> SortMoves(Board game, IEnumerable<Point> moves)
    {
        // return moves;
        var centers = new PointWithMass[game.PlayersCount];
        for (int i = 0; i < centers.Length; i++)
        {
            centers[i] = new PointWithMass(new DoublePoint(0, 0), 0);
        }
        for (int x = 0; x < game.LengthX; x++)
        {
            for (int y = 0; y < game.LengthY; y++)
            {
                if (game[x, y].HasOwner)
                {
                    centers[game[x, y].Player].Point = new DoublePoint(
                        centers[game[x, y].Player].Point.X + (x - centers[game[x, y].Player].Point.X) /
                        (centers[game[x, y].Player].Mass + (byte)game[x, y].State),
                        centers[game[x, y].Player].Point.Y + (y - centers[game[x, y].Player].Point.Y) /
                        (centers[game[x, y].Player].Mass + (byte)game[x, y].State)
                    );
                    centers[game[x, y].Player].Mass += (byte)game[x, y].State;
                }
            }
        }

        return moves.OrderByDescending(x =>
        {
            double minLength = double.MaxValue;
            for (int i = 0; i < centers.Length; i++)
            {
                if (i == game.PlayerTurn)
                {
                    continue;
                }

                minLength = Math.Min(minLength, Utils.GetLength(x, centers[i].Point));
            }

            return minLength;
        });
    }
}