using System;
using System.Runtime.InteropServices;

Board GameBoard = new Board();

var board = GameBoard.GetBoard();

for (int r = 0; r < GameBoard.GetRows(); r++)
    {
        for (int c = 0; c < GameBoard.GetCols(); c++)
        {
            var gem = board[r, c];

            string output = gem == null
                ? "Empty"
                : gem.GetColor().ToString();

            Console.Write($"{output,-8} "); // fixed width alignment
        }

        Console.WriteLine();
    }