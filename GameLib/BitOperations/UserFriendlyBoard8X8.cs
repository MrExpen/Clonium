namespace GameLib.BitOperations;

public struct UserFriendlyBoard8X8 : IBoard8X8
{
    private Board8X8 _board;
    
    #region Game

    public void Move(int row, int col) => Move(BitUtils.ToPosition(row, col));

    public void Move(byte pos)
    {
        if (Ours[pos] && Turn)
        {
            
        }
    }
    public void Move(BoardSquare square) => Move(square.Row, square.Col);

    public bool Turn { get; private set; }
    
    #endregion

    #region SharpStyle

    public BitBoard Ours => _board.Ours;
    public BitBoard Theirs => _board.Theirs;
    public BitBoard One => _board.One;
    public BitBoard Two => _board.Two;
    public BitBoard Three => _board.Three;

    #endregion
    
    #region Ctor

    public UserFriendlyBoard8X8(bool first)
    {
        _board = new Board8X8();
        Turn = first;
        if (!first)
        {
            _board.Mirror();
        }
    }

    #endregion
}