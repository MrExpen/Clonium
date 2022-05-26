namespace GameLib;

public struct Cell
{
    public CellState State { get; set; }
    public byte Player { get; set; }

    public bool HasOwner => State is not CellState.Wall and not CellState.Zero;

    public Cell(CellState state = CellState.Zero, byte player = 0)
    {
        Player = player;
        State = state;
    }
}