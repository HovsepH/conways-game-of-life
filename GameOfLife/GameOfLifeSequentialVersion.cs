namespace GameOfLife;

/// <summary>
/// Represents Conway's Game of Life in a sequential version.
/// The class provides methods to simulate the game's evolution based on simple rules.
/// </summary>
public sealed class GameOfLifeSequentialVersion
{
    private readonly bool[,] initialGrid;
    private bool[,] grid;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameOfLifeSequentialVersion"/> class with the specified number of rows and columns. The initial state of the grid is randomly set with alive or dead cells.
    /// </summary>
    /// <param name="rows">The number of rows in the grid.</param>
    /// <param name="columns">The number of columns in the grid.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the number of rows or columns is less than or equal to 0.</exception>
    public GameOfLifeSequentialVersion(int rows, int columns)
    {
        if (rows <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(rows), "Rows count must be greater than 0.");
        }

        if (columns <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(columns), "Columns count must be greater than 0.");
        }

        this.grid = new bool[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                this.grid[i, j] = GetRandomBool();
            }
        }

        this.initialGrid = (bool[,])this.grid.Clone();
        this.Generation = 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GameOfLifeSequentialVersion"/> class with the given grid.
    /// </summary>
    /// <param name="grid">The 2D array representing the initial state of the grid.</param>
    /// <exception cref="ArgumentNullException">Thrown when the input grid is null.</exception>
    public GameOfLifeSequentialVersion(bool[,] grid)
    {
        if (grid == null)
        {
            throw new ArgumentNullException(nameof(grid), "provided grid is null");
        }

        this.grid = grid;
        this.initialGrid = (bool[,])this.grid.Clone();
        this.Generation = 0;
    }

    /// <summary>
    /// Gets the current generation grid as a separate copy.
    /// </summary>
    public bool[,] CurrentGeneration
    {
        get
        {
            var copyGrid = (bool[,])this.grid.Clone();
            return copyGrid;
        }
    }

    /// <summary>
    /// Gets the current generation number.
    /// </summary>
    public int Generation { get; private set; }

    public static bool GetRandomBool()
    {
        Random random = new Random();
        return random.Next(2) == 0;
    }

    /// <summary>
    /// Restarts the game by resetting the current grid to the initial state.
    /// </summary>
    public void Restart()
    {
        this.Generation = 0;
        this.grid = this.initialGrid;
    }

    /// <summary>
    /// Advances the game to the next generation based on the rules of Conway's Game of Life.
    /// </summary>
    public void NextGeneration()
    {
        var nextGrid = (bool[,])this.grid.Clone();

        for (int i = 0; i < this.grid.GetLength(0); i++)
        {
            for (int j = 0; j < this.grid.GetLength(1); j++)
            {
                var aliveNeighbours = this.CountAliveNeighbors(i, j);

                if (this.grid[i, j])
                {
                    nextGrid[i, j] = aliveNeighbours == 2 || aliveNeighbours == 3;
                }
                else
                {
                    nextGrid[i, j] = aliveNeighbours == 3;
                }
            }
        }

        this.grid = nextGrid;
        this.Generation++;
    }

    /// <summary>
    /// Counts the number of alive neighbors for a given cell in the grid.
    /// </summary>
    /// <param name="row">The row index of the cell.</param>
    /// <param name="column">The column index of the cell.</param>
    /// <returns>The number of alive neighbors for the specified cell.</returns>
    private int CountAliveNeighbors(int row, int column)
    {
        int aliveNeighbours = 0;

        for (int i = row - 1; i <= row + 1; i++)
        {
            for (int j = column - 1; j <= column + 1; j++)
            {
                if (i == row && j == column)
                {
                    continue;
                }

                if (this.IsWithinArrayBoundsRow(i) && this.IsWithinArrayBoundsColumn(j) && this.grid[i, j])
                {
                    aliveNeighbours++;
                }
            }
        }

        return aliveNeighbours;
    }

    private bool IsWithinArrayBoundsRow(int rowNumber)
    {
        if (rowNumber >= 0 && rowNumber < this.grid.GetLength(0))
        {
            return true;
        }

        return false;
    }

    private bool IsWithinArrayBoundsColumn(int columnNumber)
    {
        if (columnNumber >= 0 && columnNumber < this.grid.GetLength(1))
        {
            return true;
        }

        return false;
    }
}
