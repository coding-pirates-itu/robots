namespace Robots.Lib;


internal class GameField
{
    #region Fields

    private readonly CellStates[,] mCells;

    #endregion


    #region Properties

    public int Width { get; }

    public int Height { get; }

    #endregion


    public GameField(int width, int height)
    {
        Width = width;
        Height = height;
        mCells = new CellStates[Width, Height];
    }


    /// <summary>
    /// Get the cell state if it's inside the field, otherwise <see cref="CellStates.Empty"/>.
    /// </summary>
    internal CellStates GetCell(int x, int y) =>
        (x < 0 || y < 0) ? CellStates.Empty :
        (x >= Width || y >= Height) ? CellStates.Empty :
        mCells[x, y];


    internal void SetCell(int x, int y, CellStates state) => mCells[x, y] = state;


    /// <summary>
    /// Whether the cell is empty or out-of-field.
    /// </summary>
    /// <returns><see langword="false"/> if the cell is occupied</returns>
    internal bool IsCellEmpty(int x, int y) => GetCell(x, y) == CellStates.Empty;
}
