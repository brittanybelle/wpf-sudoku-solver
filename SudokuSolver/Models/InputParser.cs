using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SudokuSolver.Models
{
    public static class InputParser
    {
        public static List<int> GetBoard(string inputFileText)
        {
            List<int> board = new List<int>();

            // Parse each character of the input.
            foreach (char c in inputFileText)
            {
                //If character is a digit, add it to the boardString.
                if (char.IsDigit(c))
                {
                    board.Add(Convert.ToInt32(c.ToString()));
                }

                // If character is a period, add to boardString as a '0'.
                else if (c.ToString().Equals("."))
                {
                    board.Add(0);
                }
            }

            // Check that the boardString has the right number of symbols.
            if (board.Count != 81)
            {
                MessageBox.Show("Input error: the data file must contain exactly 81 \ncharacters from the set {. 0 1 2 3 4 5 6 7 8 9}.");
                return null;
            }

            // Check that the initial board is legal.
            Board testBoard = new Board();
            testBoard.UpdateBoard(board);
            if (! testBoard.IsValid())
            {
                MessageBox.Show("Input error: the input board is not legal!");
                return null;
            }

            // If all input is good, return the proper boardString.
            return board;
        }

    }
}
