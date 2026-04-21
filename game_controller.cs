public class Controller
{

    private int[] currentSelection;
    private readonly Board game_board;
    private readonly Validator validator;
    public Controller()
    {
        game_board = new Board();
        validator = new Validator(game_board.GetBoard());
        GameLoop();
    }

    public void GameLoop()
    {
        while (true)
        {
            game_board.PrintBoard();
            Console.WriteLine("choose gem");
            int[] pos_gem1 = UserSelection();
            Console.WriteLine("swap gem with");
            int[] pos_gem2 = UserSelection();
            if(validator.CheckAdjacent(pos_gem1, pos_gem2))
            {
                game_board.swapGems(pos_gem1, pos_gem2);
                if (!validator.isMatch())
                {
                    game_board.swapGems(pos_gem1, pos_gem2); // swap back if no match
                }
                else
                {
                    while (validator.isMatch())
                    {
                        game_board.removeGems(validator.GetMatchList());
                        game_board.dropGems();
                        game_board.fillboard();
                        game_board.PrintBoard();
                        Console.WriteLine();
                    }
                }



            }


        }
    }

    public int[] UserSelection()
    {
        int r = 0, c = 0;
        int[] selection = new int[2];
        bool valid = false;

        while (!valid)
        {
            if (!TryReadInt("Enter Row (0-7)", out r) || 
                !TryReadInt("Enter Column (0-7)", out c))
            {
                Console.WriteLine("Invalid input. Please enter numbers only.");
            }
            else if (r < 0 || r > 7 || c < 0 || c > 7)
            {
                Console.WriteLine("Out of range. Row and Column must be between 0 and 7.");
            }
            else
            {
                valid = true; // ← only exits the loop when input is fully valid
            }
        }

        selection[0] = r;
        selection[1] = c;
        return selection;
    }

    private bool TryReadInt(string prompt, out int value)
    {
        Console.WriteLine(prompt);
        return int.TryParse(Console.ReadLine(), out value); // ← passes value along so TryParse can write to it
    }
}