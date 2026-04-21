using System;
using System.Security.Cryptography.X509Certificates;

/// <summary>
/// Represents the 8x8 game board, managing gem placement, swapping, matching, and gravity.
/// </summary>
public class Board
{
    private const int Rows = 8;
    private const int Cols = 8;
    private Gem?[,] _board;
    private Validator validator;
    private readonly Random _rng = new();

    /// <summary>
    /// Initializes a new board and fills it with randomly colored gems,
    /// ensuring no matches exist at the start.
    /// </summary>
    public Board()
    {
        _board = new Gem?[Rows, Cols];
        validator = new Validator(_board);
        fillboard();
    }

    /// <summary>
    /// Fills all empty cells on the board with randomly colored gems.
    /// Attempts to avoid creating immediate 3-in-a-row matches during placement.
    /// </summary>
    public void fillboard()
    {
        var colors = Enum.GetValues<Gem.GemColor>();
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Cols; c++)
            {
                if (_board[r, c] is null)
                {
                    Gem.GemColor chosen;
                    int attempts = 0;

                    // Retry up to 20 times to find a color that doesn't form a match.
                    // Falls through with whatever color was last picked if no valid one is found.
                    do
                    {
                        chosen = colors[_rng.Next(colors.Length)];
                        attempts++;
                    } while (attempts < 20 && validator.CheckMatch(r, c, chosen));

                    _board[r, c] = new Gem(r, c, chosen);
                }
            }
        }
    }

    

    /// <summary>
    /// Swaps two gems on the board and updates each gem's internal position to match.
    /// </summary>
    /// <param name="a">Board coordinates [row, col] of the first gem.</param>
    /// <param name="b">Board coordinates [row, col] of the second gem.</param>
    public void swapGems(int[] a, int[] b)
    {
        (int r1, int c1) = (a[0], a[1]);
        (int r2, int c2) = (b[0], b[1]);

        // Swap the references in the board grid
        (_board[r1, c1], _board[r2, c2]) = (_board[r2, c2], _board[r1, c1]);

        // Sync each gem's stored position after the swap
        _board[r1, c1]?.SetPos(r1, c1);
        _board[r2, c2]?.SetPos(r2, c2);
    }

    /// <summary>
    /// Removes all gems in the match list from the board, leaving those cells empty (null).
    /// </summary>
    /// <param name="matchList">The list of matched gems to remove.</param>
    public void removeGems(List<Gem> matchList)
    {
        foreach (var gem in matchList)
        {
            int[] pos = gem.get_pos();
            (int row, int col) = (pos[0], pos[1]);
            _board[row, col] = null;
        }
    }

    /// <summary>
    /// Applies gravity by sliding gems downward into empty cells column by column.
    /// Scans each column from the bottom up; when an empty cell is found, the closest
    /// gem above it is pulled down to fill the gap.
    /// </summary>
    public void dropGems()
    {
        for (int c = 0; c < Cols; c++)
        {
            for (int r = Rows - 1; r >= 0; r--)
            {
                if (_board[r, c] == null)
                {
                    // Search upward for the nearest gem to fall into this empty cell
                    for (int k = r - 1; k >= 0; k--)
                    {
                        if (_board[k, c] != null)
                        {
                            _board[r, c] = _board[k, c];
                            _board[k, c] = null;
                            _board[r, c]?.SetPos(r, c);
                            break; // Move on to the next empty cell
                        }
                    }
                }
            }
        }
    }

    /// <summary>Returns the underlying 2D gem grid.</summary>
    public Gem?[,] GetBoard() => _board;

    /// <summary>Returns the number of rows on the board.</summary>
    public int GetRows() => Rows;

    /// <summary>Returns the number of columns on the board.</summary>
    public int GetCols() => Cols;
}