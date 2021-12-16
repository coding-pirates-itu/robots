namespace RobotsLib;


public sealed class Game
{
    #region Fields

    private readonly Random mRnd;

    private readonly GameField mField;

    private int mPlayerX;

    private int mPlayerY;

    #endregion


    #region Properties

    public bool IsEnded { get; private set; }

    #endregion


    #region Init and clean-up

    /// <summary>
    /// Initialize the game state and field.
    /// </summary>
    /// <param name="width">Field width</param>
    /// <param name="height">Field height</param>
    /// <param name="robots">Number of robots</param>
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

    #endregion


    #region API

    public CellStates Cell(int x, int y) => mField.GetCell(x, y);


    public void Execute(Commands command)
    {
        switch (command)
        {
            case Commands.Die:
                IsEnded = true;
                break;
            case Commands.Up:
                if (MovePlayer(mPlayerX, mPlayerY - 1)) MoveRobots();
                break;
            case Commands.Down:
                if (MovePlayer(mPlayerX, mPlayerY + 1)) MoveRobots();
                break;
            case Commands.Left:
                if (MovePlayer(mPlayerX - 1, mPlayerY)) MoveRobots();
                break;
            case Commands.Right:
                if (MovePlayer(mPlayerX + 1, mPlayerY)) MoveRobots();
                break;
            case Commands.Stay:
                MoveRobots();
                break;
        }
    }

    #endregion


    #region Utility

    private void PlaceRobots(int robots)
    {
        while (robots > 0)
        {
            var x = mRnd.Next(mField.Width);
            var y = mRnd.Next(mField.Height);

            if (mField.GetCell(x, y) != CellStates.Empty) continue;

            mField.SetCell(x, y, CellStates.Robot);
            robots--;
        }
    }


    /// <summary>
    /// Set player location such that it is surrounded by empty cells.
    /// If returns <see langword="true"/>, <see cref="mPlayerX"/> and <see cref="mPlayerY"/> are set.
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

            mField.SetCell(x, y, CellStates.Player);
            mPlayerX = x;
            mPlayerY = y;

            return true;
        }

        return false;
    }


    private bool MovePlayer(int x, int y)
    {
        if (x < 0 || y < 0) return false;
        if (x >= mField.Width || y >= mField.Height) return false;
        if (mField.GetCell(x, y) != CellStates.Empty) return false;

        mField.SetCell(mPlayerX, mPlayerY, CellStates.Empty);
        mField.SetCell(x, y, CellStates.Player);
        mPlayerX = x;
        mPlayerY = y;

        return true;
    }


    private void MoveRobots()
    {
    }

    #endregion
}
