namespace GameOfLife;

/// <summary>
/// Provides extension methods for simulating Conway's Game of Life on different versions.
/// </summary>
public static class GameOfLifeExtension
{
    /// <summary>
    /// Simulates the evolution of Conway's Game of Life for a specified number of generations using the sequential version.
    /// The result is written to the provided <see cref="TextWriter"/> using the specified characters for alive and dead cells.
    /// </summary>
    /// <param name="game">The sequential version of the Game of Life.</param>
    /// <param name="generations">The number of generations to simulate.</param>
    /// <param name="writer">The <see cref="TextWriter"/> used to output the simulation result.</param>
    /// <param name="aliveCell">The character representing an alive cell.</param>
    /// <param name="deadCell">The character representing a dead cell.</param>
    /// <exception cref="ArgumentNullException">Thrown when <param name="game"/> parameters is null.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <param name="writer"/> parameters is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <param name="generations"/> is less than or equal to 0.</exception>
    public static void Simulate(this GameOfLifeSequentialVersion? game, int generations, TextWriter? writer, char aliveCell, char deadCell)
    {
        ArgumentNullException.ThrowIfNull(game);

        ArgumentNullException.ThrowIfNull(writer);

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(generations);

        while (game.Generation < generations)
        {
            var grid = game.CurrentGeneration;

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    writer.Write(grid[i, j] ? aliveCell : deadCell);
                }

                writer.WriteLine();
            }

            game.NextGeneration();
        }
    }

    /// <summary>
    /// Asynchronously simulates the evolution of Conway's Game of Life for a specified number of generations using the parallel version.
    /// The result is written to the provided TextWriter using the specified characters for alive and dead cells.
    /// </summary>
    /// <param name="game">The parallel version of the Game of Life.</param>
    /// <param name="generations">The number of generations to simulate.</param>
    /// <param name="writer">The <see cref="TextWriter"/> used to output the simulation result.</param>
    /// <param name="aliveCell">The character representing an alive cell.</param>
    /// <param name="deadCell">The character representing a dead cell.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <param name="game"/> parameters is null.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <param name="writer"/> parameters is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <param name="generations"/> is less than or equal to 0.</exception>
    public static async Task SimulateAsync(this GameOfLifeParallelVersion? game, int generations, TextWriter? writer, char aliveCell, char deadCell)
    {
        ThrowError(game, writer, generations);
#pragma warning disable
        while (game.Generation < generations)
        {
            var grid = game.CurrentGeneration;

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    await writer.WriteAsync(grid[i, j] ? aliveCell : deadCell);
                }

                await writer.WriteLineAsync();
            }

            game.NextGeneration();
        }
#pragma warning enable
    }

    private static void ThrowError(GameOfLifeParallelVersion? game, TextWriter? writer, int generations)
    {
        ArgumentNullException.ThrowIfNull(game);

        ArgumentNullException.ThrowIfNull(writer);

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(generations);
    }
}
