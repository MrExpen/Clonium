using System.Collections;

namespace GameLib.BitOperations;

public struct BitBoard : IEnumerable<BoardSquare>
{
    private ulong _board;
    
    #region SharpStyle

    public bool this[int row, int col]
    {
        get => Get(row, col);
        set
        {
            if (!value)
            {
                Reset(row, col);
            }
            else
            {
                Set(row, col);
            }
        }
    }
    public bool this[byte pos]
    {
        get => Get(pos);
        set
        {
            if (!value)
            {
                Reset(pos);
            }
            else
            {
                Set(pos);
            }
        }
    }
    public bool this[BoardSquare square]
    {
        get => Get(square);
        set
        {
            if (!value)
            {
                Reset(square);
            }
            else
            {
                Set(square);
            }
        }
    }

    public bool IsEmpty => _isEmpty();
    public ulong AsInt => _asInt();
    public int Count => _count();
    public int CountFew => _countFew();
    
    #endregion

    #region CppStyle

    private ulong _asInt() => _board;
    public void Clear() => _board = 0;
    private int _count()
    {
        var x = _board;
        x -= (x >> 1) & 0x5555555555555555;
        x = (x & 0x3333333333333333) + ((x >> 2) & 0x3333333333333333);
        x = (x + (x >> 4)) & 0x0F0F0F0F0F0F0F0F;
        return (int)((x * 0x0101010101010101) >> 56);
    }
    private int _countFew()
    {
        var x = _board;
        int count;
        for (count = 0; x != 0; ++count)
        {
            x &= x - 1;
        }

        return count;
    }
    
    private bool _isEmpty() => _board == 0;

    public bool Intersects(ref BitBoard other) => (_board & other._board) != 0;

    public void Mirror() => _board = BitUtils.ReverseBytesInBytes(_board);
    
    private void SetIf(BoardSquare square, bool cond) => SetIf(square.AsInt, cond);
    private void SetIf(byte pos, bool cond) => _board |= (cond ? 1UL : 0UL) << pos;
    private void SetIf(int row, int col, bool cond) => SetIf(new BoardSquare(row, col), cond);
    
    private bool Get(BoardSquare square) => Get(square.AsInt);
    private bool Get(byte pos) => (_board & (1UL << pos)) != 0;
    private bool Get(int row, int col) => Get(new BoardSquare(row, col));
    
    private void Set(BoardSquare square) => Set(square.AsInt);
    private void Set(byte pos) => _board |= 1UL << pos;
    private void Set(int row, int col) => Set(new BoardSquare(row, col));
    
    private void Reset(BoardSquare square) => Reset(square.AsInt);
    private void Reset(byte pos) => _board &= ~(1UL << pos);
    private void Reset(int row, int col) => Reset(new BoardSquare(row, col));
    
    #endregion

    #region Operators

    public static bool operator==(BitBoard left, BitBoard right) => left._board == right._board;
    public static bool operator!=(BitBoard left, BitBoard right) => left._board != right._board;
    
    public static BitBoard operator |(BitBoard a, BitBoard b) => new BitBoard(a._board | b._board);
    public static BitBoard operator &(BitBoard a, BitBoard b) => new BitBoard(a._board & b._board);
    public static ulong operator &(BitBoard a, ulong b) => a._board & b;
    public static ulong operator &(ulong b, BitBoard a) => a._board & b;
    public static BitBoard operator -(BitBoard a, BoardSquare b) => new BitBoard(a._board & ~b.AsBoard);
    public static BitBoard operator -(BitBoard a, BitBoard b) => new BitBoard(a._board & ~b._board);
    
    public static void Swap(ref BitBoard a, ref BitBoard b)
    {
        (a, b) = (b, a);
    }

    #endregion

    #region Override

    public override bool Equals(object? obj)
    {
        return obj is BitBoard other && Equals(other);
    }
    public override int GetHashCode()
    {
        return _board.GetHashCode();
    }
    public bool Equals(BitBoard other)
    {
        return _board == other._board;
    }
    
    #endregion

    #region IEnumerable

    public IEnumerator<BoardSquare> GetEnumerator() => new BoardSquareEnumerator(_board);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion
    
    #region Ctor

    public BitBoard(ulong board)
    {
        _board = board;
    }
    public BitBoard()
    {
        _board = default;
    }

    #endregion
}
