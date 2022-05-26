using GameLib;

namespace Bot;

public static class MinMax
{
    public static Point GetBestMove(Board game, int deep)
    {
        if (game.PlayersCount > 2)
        {
            throw new NotImplementedException();
        }

        var bestMove = -int.MaxValue;
        var bestMoveFound = new Point(0, 0);
        
        foreach (var move in MoveSorter.SortMoves(game, MoveGenerator.GetMoves(game)))
        {
            var board = new Board(game);
            board.MakeTurn(move);
            var value = Analyze(ref board, deep - 1, game.PlayerTurn);
            if (value >= bestMove)
            {
                bestMove = value;
                bestMoveFound = move;
            }
        }

        return bestMoveFound;
    }

    private static int Analyze(ref Board game, int deep, int? maximisingPlayer)
    {
        var isMaximisingPlayer = game.PlayerTurn == maximisingPlayer;
        if (deep <= 0 || game.Winner.HasValue)
        {
            return PositionEstimator.GetEvaluation(game) * (isMaximisingPlayer ? 1 : -1);
        }

        var bestMove = isMaximisingPlayer ? -int.MaxValue : int.MaxValue;

        foreach (var move in MoveSorter.SortMoves(game, MoveGenerator.GetMoves(game)))
        {
            var board = new Board(game);
            board.MakeTurn(move);
            if (isMaximisingPlayer)
            {
                bestMove = Math.Max(bestMove, Analyze(ref board, deep - 1, maximisingPlayer));
            }
            else
            {
                bestMove = Math.Min(bestMove, Analyze(ref board, deep - 1, maximisingPlayer));
            }
        }

        return bestMove;
    }
}