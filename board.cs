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
}
