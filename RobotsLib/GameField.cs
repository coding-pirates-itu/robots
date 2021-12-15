namespace RobotsLib;


internal class GameField
{
    private readonly CellState[,] mCells;

    public int Width { get; }

    public int Height { get; }


    public GameField(int width, int height)
    {
        Width = width;
        Height = height;
        mCells = new CellState[Width, Height];
    }


    /// <summary>
    /// Get the cell state if it's inside the field, otherwise <see cref="CellState.Empty"/>.
    /// </summary>
    internal CellState GetCell(int x, int y) =>
        (x < 0 || y < 0) ? CellState.Empty :
        (x >= Width || y >= Height) ? CellState.Empty :
        mCells[x, y];


    internal void SetCell(int x, int y, CellState state) => mCells[x, y] = state;


    /// <summary>
    /// Whether the cell is empty or out-of-field.
    /// </summary>
    /// <returns><see langword="false"/> if the cell is occupied</returns>
    internal bool IsCellEmpty(int x, int y) => GetCell(x, y) == CellState.Empty;
}
