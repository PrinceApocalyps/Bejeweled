using System;

class Program
{
    static void Main()
    {
        Board GameBoard = new Board();
        var board = GameBoard.GetBoard();

        for (int r = 0; r < GameBoard.GetRows(); r++)
        {
            for (int c = 0; c < GameBoard.GetCols(); c++)
            {
                var gem = board[r, c];

                string output = gem == null
                    ? "Empty"
                    : GetEmoji(gem.GetColor());

                Console.Write($"{output,-2} ");
            }

            Console.WriteLine();
        }
    }

    public static string GetEmoji(Gem.GemColor color)
    {
        return color switch
        {
            Gem.GemColor.Red => "🟥",
            Gem.GemColor.Blue => "🟦",
            Gem.GemColor.Green => "🟩",
            Gem.GemColor.Yellow => "🟨",
            Gem.GemColor.Purple => "🟪",
            _ => "?"
        };
    }
}