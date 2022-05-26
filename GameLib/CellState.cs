namespace GameLib;

public enum CellState : byte
{
    Zero,
    One,
    Two,
    Three,
    Four,
    Wall = byte.MaxValue
}