namespace GameLib.BitOperations;

public struct BoardSquare
{
    private byte _square;

    #region SharpStyle

    public byte AsInt => _asInt();
    public ulong AsBoard => _asBoard();
    public int Row => _row();
    public int Col => _col();
    
    #endregion

    #region CppStyle

    private byte _asInt() => _square;
    private ulong _asBoard()=> 1UL << _square;
    public void Set(int row, int col) => _square = (byte)(row * 8 + col);
    private int _row() => _square / 8;
    private int _col() => _square % 8;
    public void Mirror() => _square ^= 0b111000;

    #endregion

    #region Override

    public override string ToString() => $"Row={Row}, Col={Col}";
    public bool Equals(BoardSquare other)
    {
        return _square == other._square;
    }

    public override bool Equals(object? obj)
    {
        return obj is BoardSquare other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _square.GetHashCode();
    }

    #endregion
    
    #region Operators

    public static bool operator ==(BoardSquare left, BoardSquare right) => left._square == right._square;
    public static bool operator !=(BoardSquare left, BoardSquare right) => left._square != right._square;

    #endregion

    #region Static

    public static bool IsValidCoord(int x) => x is >= 0 and < 8;

    public static bool IsValid(int row, int col) => IsValidCoord(row) && IsValidCoord(col);
    
    #endregion
    
    #region Ctor

    public BoardSquare()
    {
        _square = default;
    }
    public BoardSquare(byte num)
    {
        _square = num;
    }
    public BoardSquare(int row, int col) : this((byte)(row * 8 + col)) {}
    public BoardSquare(string str, bool black = false) : this(black ? '8' - str[1] : str[1] - '1', str[0] - 'a')
    {
    }

    #endregion
}