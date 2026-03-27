using System;
public class Board
{
    private const int Rows = 8;
    private const int Cols = 8;
    private Gem?[,] _board;
    private readonly Random _rng = new();
    public Board()
    {
        _board = new Gem?[Rows, Cols];
    }

    public void fillboard()
    {
        var colors = Enum.GetValues<Gem.GemColor>();
        for(int r= 0; r< Rows; r++)
        {
            for(int c=0; c<Cols; c++)
            {
                Gem.GemColor chosen;
                int attempts = 0;
                do
                {
                    chosen = colors[_rng.Next(colors.Length)];
                    attempts++;
                } while(attempts<20&& WouldMatch(r,c,chosen));
            }
        }
    }

    public bool WouldMatch(int r, int c, Gem.GemColor color)
    {
        //Two to the left
        if(c > 2 
            && _board[r, c-1]?.GetColor() == color
            && _board[r, c-2]?.GetColor() == color)
            return true;

        //Two above
        if(r>=2
            && _board[r-1, c]?.GetColor() == color
            && _board[r-2, c]?.GetColor() == color)
            return true;

        return false;
    }

    public void swapGems(int[] a, int[] b)
    {
        (int r1, int c1) = (a[0], a[1]);
        (int r2, int c2) = (b[0], b[1]);

        //change the position of the gems on the board
        (_board[r1,c1], _board[r2, c2]) = (_board[r2,c2], _board[r1,c1]);


        //change the internal position of the gems after swap
        _board[r1, c1]?.SetPos(r1,c1);
        _board[r2, c2]?.SetPos(r2,c2);
    }

    public void removeGems(List<Gem> matchList)
    {
        foreach (var gem in matchList)
        {
            int[] pos = gem.get_pos();
            (int row, int col) = (pos[0], pos[1]);

            _board[row, col] = null;
        }
    }

    //drop gems---gravity 
    // It looks from the bottom and moves upward
    // when it finds an empty cell it
    // grabs the closest gem above and pulls it down
    // then stops and moves to the next gem

    public void dropGems()
    {
        for (int c = 0; c < Cols; c++)
        {
            for (int r = Rows - 1; r >= 0; r--)
            {
                if (_board[r, c] == null)
                {
                    for (int k = r - 1; k >= 0; k--)
                    {
                        if (_board[k, c] != null)
                        {
                            _board[r, c] = _board[k, c];
                            _board[k, c] = null;

                            _board[r, c]?.SetPos(r, c);
                            break;
                        }
                    }
                }
            }
        }
    }
}
