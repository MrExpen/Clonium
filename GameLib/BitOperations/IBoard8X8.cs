namespace GameLib.BitOperations;

public interface IBoard8X8
{
    void Move(int row, int col);
    void Move(byte pos);
    void Move(BoardSquare square);

    BitBoard Ours { get; }
    BitBoard Theirs { get; }
    BitBoard One { get; }
    BitBoard Two { get; }
    BitBoard Three { get; }
}