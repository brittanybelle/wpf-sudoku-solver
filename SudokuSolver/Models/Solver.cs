using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SudokuSolver.Models
{
    public class Solver
    {
        public Board ActiveBoard;
        private List<Cell> emptyCellsRemaining;

        public Solver()
        {
            ActiveBoard = new Board();
            emptyCellsRemaining = new List<Cell>();
        }

        public void Solve(List<int> inputArray)
        {
            // Initialize
            ActiveBoard.UpdateBoard(inputArray);
            emptyCellsRemaining = new List<Cell>();
            UpdateEmptyCellsList();

            // Run Loop
            while (emptyCellsRemaining.Count > 0)
            {
                bool updatesWereMade = false;

                updatesWereMade = RunCandidateCheckingMethod();

                if (! updatesWereMade)
                {
                    MessageBox.Show("Puzzle is unsolvable.");
                    break;
                }
            }
        }

        private void UpdateEmptyCellsList()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (ActiveBoard.Cells[i][j].Content == 0)
                    {
                        emptyCellsRemaining.Add(ActiveBoard.Cells[i][j]);
                    }
                }
            }

            /*
            for (int i = 0; i < emptyCellsRemaining.Count; i++)
            {
                Console.WriteLine("empty cell: " + emptyCellsRemaining[i].Row + ",  " + emptyCellsRemaining[i].Column);
            }*/
        }

        private bool RunCandidateCheckingMethod()
        {
            for (int i = 0; i < emptyCellsRemaining.Count; i++)
            {
                Cell cellToCheck = emptyCellsRemaining[i];
                int row = cellToCheck.Row;
                int col = cellToCheck.Column;

                List<int> listOfCandidates = new List<int>();
                for (int testDigit = 1; testDigit < 10; testDigit++)
                {
                    if (ActiveBoard.IsPotentialCandidate(testDigit, row, col))
                    {
                        listOfCandidates.Add(testDigit);
                    }
                }

                if (listOfCandidates.Count == 1)
                {
                    ActiveBoard.Cells[row][col].Content = listOfCandidates[0];
                    int indexToRemove = emptyCellsRemaining.FindIndex(cell => (cell.Row == row && cell.Column == col));
                    emptyCellsRemaining.RemoveAt(indexToRemove);
                    return true;
                }
            }
            return false;
        }

    }
}
