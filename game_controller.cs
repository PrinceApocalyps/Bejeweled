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
        UserSelection();
    }
    
    public void UserSelection()
    {
        int r = 0, c = 0;
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

        currentSelection[0] = r;
        currentSelection[1] = c;
    }

    private bool TryReadInt(string prompt, out int value)
    {
        Console.WriteLine(prompt);
        return int.TryParse(Console.ReadLine(), out value); // ← passes value along so TryParse can write to it
    }
}