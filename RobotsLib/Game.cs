namespace RobotsLib;


public sealed class Game
{
    private readonly Random mRnd;

    private readonly GameField mField;


    public Game(int width, int height, int robots)
    {
        mField = new(width, height);
        mRnd = new Random();

        do
        {
            PlaceRobots(robots);
        }
        while (! SetPlayerLocationSafe());
    }


    #region API

    public CellState Cell(int x, int y) => mField.GetCell(x, y);

    #endregion


    private void PlaceRobots(int robots)
    {
        while (robots > 0)
        {
            var x = mRnd.Next(mField.Width);
            var y = mRnd.Next(mField.Height);

            if (mField.GetCell(x, y) != CellState.Empty) continue;

            mField.SetCell(x, y, CellState.Robot);
            robots--;
        }
    }


    /// <summary>
    /// Set player such that he is surrounded by empty cells.
    /// </summary>
    /// <returns><see langword="true"/> if such a place was found, false if not</returns>
    private bool SetPlayerLocationSafe()
    {
        var locations = Enumerable.Range(0, mField.Width * mField.Height).OrderBy(x => mRnd.Next()).ToArray();
        
        foreach (var loc in locations)
        {
            var (y, x) = Math.DivRem(loc, mField.Width);

            var isOccupied = false;
            for (var dx = -1; dx <= 1; dx++)
                for (var dy = -1; dy <= 1; dy++)
                    isOccupied = isOccupied || !mField.IsCellEmpty(x + dx, y + dy);

            if (isOccupied) continue;

            mField.SetCell(x, y, CellState.Player);
            return true;
        }

        return false;
    }
}
