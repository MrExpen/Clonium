using Microsoft.VisualBasic.CompilerServices;

namespace GameLib.BitOperations;

public struct Board8X8 : IBoard8X8
{
    private BitBoard _ourPieces;
    private BitBoard _theirPieces;
    private BitBoard _one;
    private BitBoard _two;

    #region Game

    public void Move(int row, int col) => Move(new BoardSquare(row, col));

    public void Move(byte pos)
    {
        var positions = new Queue<BoardSquare>();
        positions.Enqueue(new BoardSquare(pos));
        
        while (positions.Count != 0)
        {
            var square = positions.Dequeue();
            if (!BoardSquare.IsValid(square.Row, square.Col)) continue;
            
            if (One[square])
            {
                _one[square] = false;
                _two[square] = true;
                _theirPieces[square] = false;
                _ourPieces[square] = true;
            }
            else if (Two[square])
            {
                _two[square] = false;
                _theirPieces[square] = false;
                _ourPieces[square] = true;
            }
            else if (Three[square])
            {
                _theirPieces[square] = false;
                _ourPieces[square] = false;
                
                positions.Enqueue(new BoardSquare(BitUtils.ToPosition(square.Row - 1, square.Col)));
                positions.Enqueue(new BoardSquare(BitUtils.ToPosition(square.Row + 1, square.Col)));
                positions.Enqueue(new BoardSquare(BitUtils.ToPosition(square.Row, square.Col - 1)));
                positions.Enqueue(new BoardSquare(BitUtils.ToPosition(square.Row, square.Col + 1)));
            }
            else
            {
                _ourPieces[square] = true;
                _one[square] = true;
            }
        }
        
    }

    public void Move(BoardSquare square) => Move(square.AsInt);

    #endregion

    #region SharpStyle

    public BitBoard Ours => _ourPieces;
    public BitBoard Theirs => _theirPieces;
    public BitBoard One => _one;
    public BitBoard Two => _two;
    public BitBoard Three => (_ourPieces | _theirPieces) - _one - _two;

    #endregion

    #region CppStyle

    public void Mirror()
    {
        BitBoard.Swap(ref _ourPieces, ref _theirPieces);
    }

    #endregion

    #region Override

    public override int GetHashCode()
    {
        return HashCode.Combine(_one, _theirPieces, _one, _two);
    }

    #endregion

    #region Ctor

    public Board8X8()
    {
        _ourPieces = new BitBoard(0x200);
        _theirPieces = new BitBoard(0x40000000000000);
        _one = new BitBoard(0);
        _two = new BitBoard(0);
    }

    #endregion
}