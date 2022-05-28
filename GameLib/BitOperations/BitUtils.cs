namespace GameLib.BitOperations;

public static class BitUtils
{
    public static ulong ReverseBytesInBytes(ulong v) {
        v = (v & 0x00000000FFFFFFFF) << 32 | (v & 0xFFFFFFFF00000000) >> 32;
        v = (v & 0x0000FFFF0000FFFF) << 16 | (v & 0xFFFF0000FFFF0000) >> 16;
        v = (v & 0x00FF00FF00FF00FF) << 8 | (v & 0xFF00FF00FF00FF00) >> 8;
        return v;
    }

    public static byte ToPosition(int row, int col) => BoardSquare.IsValid(row, col) ? (byte)(row * 8 + col) : (byte)(10 * 8 + 10);
    public static byte GetLowestBit(ulong v)
    {
        if ((v & 0xFFFFFFFF) != 0)
        {
            return _getLowestBit(v);
        }
        return (byte)(_getLowestBit(v >> 32) + 32);
    }
    private static byte _getLowestBit(ulong v)
    {
        byte result = 0;
        while ((v & (1U << result)) == 0)
        {
            result++;
        }

        return result;
    }
}