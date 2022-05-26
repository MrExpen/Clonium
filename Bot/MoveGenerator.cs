using GameLib;

namespace Bot;

public static class MoveGenerator
{
    public static IEnumerable<Point> GetMoves(Board game)
    {
        for (int x = 0; x < game.LengthX; x++)
        {
            for (int y = 0; y < game.LengthY; y++)
            {
                if (game[x, y].HasOwner && game[x, y].Player == game.PlayerTurn)
                {
                    yield return new Point(x, y);
                }
            }
        }
    }
}