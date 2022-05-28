using System.Collections;
using Microsoft.VisualBasic.CompilerServices;

namespace GameLib.BitOperations;

public class BoardSquareEnumerator : IEnumerator<BoardSquare>
{
    private ulong _bitBoard;
    public BoardSquareEnumerator(ulong bitBoard)
    {
        _bitBoard = bitBoard;
    }
    
    public bool MoveNext()
    {
        if (_bitBoard == 0)
        {
            return false;
        }
        Current = new BoardSquare(BitUtils.GetLowestBit(_bitBoard));
        _bitBoard &= _bitBoard - 1;
        return true;
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public BoardSquare Current { get; private set; }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}