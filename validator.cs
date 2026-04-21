public class Validator
{
    private List<Gem> matchList;
    private readonly Gem?[,] _board;

    public Validator(Gem?[,] board)
    {
        _board = board;
    }
    public bool CheckAdjacent(int[] gem1, int[] gem2)
    {
        int rowDiff = Math.Abs(gem1[0] - gem2[0]);
        int colDiff = Math.Abs(gem1[1] - gem2[1]);
        return (rowDiff == 1 && colDiff == 0) 
            || (rowDiff == 0 && colDiff == 1);
    }

    public List<Gem> GetMatchList() => matchList;

    public bool isMatch()
    {
        matchList = new List<Gem>(); // ← reset each call so previous matches don't carry over
        int rows = _board.GetLength(0);
        int cols = _board.GetLength(1);

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                Gem.GemColor? color = _board[r, c]?.GetColor();
                if (color is null) continue;

                if (CheckMatch(r, c, color.Value))
                {
                    // Add the matching gem and the two behind it
                    matchList.Add(_board[r, c]!);
                    if (c >= 2 && _board[r, c-1]?.GetColor() == color)
                    {
                        matchList.Add(_board[r, c-1]!);
                        matchList.Add(_board[r, c-2]!);
                    }
                    if (r >= 2 && _board[r-1, c]?.GetColor() == color)
                    {
                        matchList.Add(_board[r-1, c]!);
                        matchList.Add(_board[r-2, c]!);
                    }
                }
            }
        }

        return matchList.Count > 0;
    }

    /// <summary>
    /// Checks whether placing a gem of the given color at (r, c) would
    /// complete a horizontal or vertical 3-in-a-row match.
    /// Only looks left and upward since the board is filled top-left to bottom-right.
    /// </summary>
    /// <param name="r">Row index of the candidate cell.</param>
    /// <param name="c">Column index of the candidate cell.</param>
    /// <param name="color">The color being considered for placement.</param>
    /// <returns>True if placing this color would create a match; otherwise false.</returns>
    public bool CheckMatch(int r, int c, Gem.GemColor color)
    {
        // Two to the left
        if (c >= 2
            && _board[r, c - 1]?.GetColor() == color
            && _board[r, c - 2]?.GetColor() == color)
            return true;

        // Two above
        if (r >= 2
            && _board[r - 1, c]?.GetColor() == color
            && _board[r - 2, c]?.GetColor() == color)
            return true;

        return false;
    }

    // public bool CheckPossibleMatch()
    // {
        
    // }
}