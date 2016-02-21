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
                bool test1Passed = false;
                bool test2Passed = false;

                test1Passed = RunCandidateCheckingMethod();
                test2Passed = RunPlaceFindingMethod();

                if (! (test1Passed || test2Passed))
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

        private List<Cell> GetEmptyCellsFromList(List<Cell> listOfCells)
        {
            List<Cell> listOfEmptyCells = new List<Cell>();
            for (int i = 0; i < 9; i++)
            {
                if (listOfCells[i].Content == 0)
                {
                    listOfEmptyCells.Add(listOfCells[i]);
                }
            }
            return listOfCells;
        }

        private void RemoveCellFromEmptyCellsList(int rowIndex, int columnIndex)
        {
            int indexToRemove = emptyCellsRemaining.FindIndex(cell => (cell.Row == rowIndex && cell.Column == columnIndex));
            emptyCellsRemaining.RemoveAt(indexToRemove);
        }

        private bool RunCandidateCheckingMethod()
        {
            // Loop through empty cells
            for (int i = 0; i < emptyCellsRemaining.Count; i++)
            {
                Cell cellToCheck = emptyCellsRemaining[i];
                int row = cellToCheck.Row;
                int col = cellToCheck.Column;

                List<int> listOfCandidates = GetListOfCandidatesAtCell(row, col);

                if (listOfCandidates.Count == 1)
                {
                    ActiveBoard.Cells[row][col].Content = listOfCandidates[0];
                    RemoveCellFromEmptyCellsList(row, col);
                    return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// "Place Finding Method" - this method loops through the digits 1-9,
        /// checking in turn that each row, cell, and 3x3 square contains the
        /// digit in question. For each empty cell, if that is the *only* empty 
        /// cell that can possibly contain that number (within the row, or
        /// column, or 3x3 square) then fill that cell.
        /// </summary>
        /// <returns></returns>
        private bool RunPlaceFindingMethod()
        {
            // Loop through digits
            for (int n = 1; n < 10; n++)
            {
                // Loop through rows
                for (int i = 0; i < 9; i++)
                {
                    List<Cell> row = ActiveBoard.GetRow(i);

                    // Check whether N is present - if not, check row's empty cells
                    if (!ActiveBoard.NumberIsInList(n, row))
                    {
                        List<Cell> listOfCellsAsCandidates = new List<Cell>();
                        for (int j = 0; j < 9; j++)
                        {
                            if (row[j].Content == 0 && ActiveBoard.IsPotentialCandidate(n, i, j))
                            {
                                listOfCellsAsCandidates.Add(ActiveBoard.Cells[i][j]);
                            }
                        }
                        // If there is exactly one in the list, add it
                        if (listOfCellsAsCandidates.Count == 1)
                        {
                            Cell cellToAdd = listOfCellsAsCandidates[0];
                            ActiveBoard.Cells[cellToAdd.Row][cellToAdd.Column].Content = n;
                            RemoveCellFromEmptyCellsList(cellToAdd.Row, cellToAdd.Column);
                            return true;
                        }
                    }
                }
                
                // RunPlaceFindingMethod => Loop through columns
                for (int j = 0; j < 9; j++)
                {
                    List<Cell> col = ActiveBoard.GetColumn(j);

                    // Check whether N is present - if not, check row's empty cells
                    if (!ActiveBoard.NumberIsInList(n, col))
                    {
                        List<Cell> listOfCellsAsCandidates = new List<Cell>();
                        for (int i = 0; i < 9; i++)
                        {
                            if (col[i].Content == 0 && ActiveBoard.IsPotentialCandidate(n, i, j))
                            {
                                listOfCellsAsCandidates.Add(ActiveBoard.Cells[i][j]);
                            }
                        }
                        // If there is exactly one in the list, add it
                        if (listOfCellsAsCandidates.Count == 1)
                        {
                            Cell cellToAdd = listOfCellsAsCandidates[0];
                            ActiveBoard.Cells[cellToAdd.Row][cellToAdd.Column].Content = n;
                            RemoveCellFromEmptyCellsList(cellToAdd.Row, cellToAdd.Column);
                            return true;
                        }
                    }
                }

                // RunPlaceFindingMethod => Loop through squares
                for (int k = 0; k < 9; k++)
                {
                    List<Cell> square = ActiveBoard.GetSquare(k);

                    // Check whether N is present - if not, check row's empty cells
                    if (!ActiveBoard.NumberIsInList(n, square))
                    {
                        List<Cell> listOfCellsAsCandidates = new List<Cell>();
                        for (int i = 0; i < 9; i++)
                        {
                            Cell testCell = square[i];
                            if (testCell.Content == 0 && ActiveBoard.IsPotentialCandidate(n, testCell.Row, testCell.Column))
                            {
                                listOfCellsAsCandidates.Add(ActiveBoard.Cells[testCell.Row][testCell.Column]);
                            }
                        }
                        // If there is exactly one in the list, add it
                        if (listOfCellsAsCandidates.Count == 1)
                        {
                            Cell cellToAdd = listOfCellsAsCandidates[0];
                            ActiveBoard.Cells[cellToAdd.Row][cellToAdd.Column].Content = n;
                            RemoveCellFromEmptyCellsList(cellToAdd.Row, cellToAdd.Column);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private List<int> GetListOfCandidatesAtCell(int rowIndex, int columnIndex)
        {
            List<int> listOfCandidates = new List<int>();
            for (int testDigit = 1; testDigit < 10; testDigit++)
            {
                if (ActiveBoard.IsPotentialCandidate(testDigit, rowIndex, columnIndex))
                {
                    listOfCandidates.Add(testDigit);
                }
            }
            return listOfCandidates;
        }

    }
}
