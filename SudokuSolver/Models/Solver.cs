using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
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
            emptyCellsRemaining = GetEmptyCellsFromBoard(ActiveBoard);

            // Run Loop
            while (emptyCellsRemaining.Count > 0)
            {
                bool test1Passed = false;
                bool test2Passed = false;
                bool test3Passed = false;

                test1Passed = RunCandidateCheckingMethod();
                test2Passed = RunPlaceFindingMethod();

                if (!(test1Passed || test2Passed))
                {
                    test3Passed = RunBruteForceMethod();
                }

                if (! (test1Passed || test2Passed || test3Passed))
                {
                    MessageBox.Show("Puzzle is unsolvable.");
                    break;
                }
            }

            if (ActiveBoard.IsValid() && ActiveBoard.IsFull())
            {
                MessageBox.Show("Puzzle solved!");
            }
        }

        private List<Cell> GetEmptyCellsFromBoard(Board inputBoard)
        {
            List<Cell> listOfEmptyCells = new List<Cell>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (inputBoard.Cells[i][j].Content == 0)
                    {
                        listOfEmptyCells.Add(inputBoard.Cells[i][j]);
                    }
                }
            }

            return listOfEmptyCells;
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

        /// <summary>
        /// "Candidate Checking Method" - for each empty cell, if there is only
        /// one possible value that the cell can take, then update the cell
        /// with that value.
        /// </summary>
        /// <returns>True if a correct number is found; else false.</returns>
        private bool RunCandidateCheckingMethod()
        {
            // Loop through empty cells
            for (int i = 0; i < emptyCellsRemaining.Count; i++)
            {
                Cell cellToCheck = emptyCellsRemaining[i];
                int row = cellToCheck.Row;
                int col = cellToCheck.Column;

                List<int> listOfCandidates = GetListOfCandidatesAtCell(ref ActiveBoard, row, col);

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
        /// <returns>True if a correct number is found; else false.</returns>
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

        /// <summary>
        /// Brute force: run hypothetical boards using a dumb brute force
        /// method. The recursive RunHypotheticalBoard function is used here.
        /// </summary>
        /// <returns>Hopefully a completed board.</returns>
        private bool RunBruteForceMethod()
        {
            Board returnBoard = new Board();
            returnBoard = RunHypotheticalBoard(ActiveBoard);

            // Triple checking our recursive function's output...
            if (returnBoard != null)
            {
                if (returnBoard.IsValid() && returnBoard.IsFull())
                {
                    ActiveBoard.UpdateBoard(returnBoard.GetBoardStateAsIntArray());
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // recursive function to do depth-first search.
        private Board RunHypotheticalBoard(Board startBoard)
        {
            Console.WriteLine("ÈIN AGAINÈ");

            // Make a new board for testing purposes
            Board hypotheticalBoard = new Board();
            hypotheticalBoard.UpdateBoard(startBoard.GetBoardStateAsIntArray());

            List<Cell> listOfEmptyCells = GetEmptyCellsFromBoard(hypotheticalBoard);

            // Check that there are still empty cells left. If not, return the board.
            if (listOfEmptyCells.Count > 0)
            {
                // Start by choosing a particular cell with a small number of possible candidates, to limit the branching options.
                Cell cellToFill = ChooseEmptyCellWithLeastNumberOfCandidates(ref hypotheticalBoard, listOfEmptyCells);
                List<int> listOfCandidates = GetListOfCandidatesAtCell(ref hypotheticalBoard, cellToFill.Row, cellToFill.Column);

                // Check that list of candidates is nonzero. If not, this is a dead end, so return null.
                if (listOfCandidates.Count > 0)
                {
                    // Now we iterate through each possible value.
                    for (int i = 0; i < listOfCandidates.Count; i++)
                    {
                        // Set up a new "dummy" board.
                        Board newHypotheticalBoard = new Board();
                        newHypotheticalBoard.UpdateBoard(hypotheticalBoard.GetBoardStateAsIntArray());

                        // In the dummy board, add the potential cell value (current value)
                        newHypotheticalBoard.Cells[cellToFill.Row][cellToFill.Column].Content = listOfCandidates[i];

                        // Confirm board validity (not really necessary, but just for paranoid double-checking)
                        if (!newHypotheticalBoard.IsValid())
                        {
                            MessageBox.Show("Grievous error has occurred!!");
                            return null;
                        }

                        // Check whether board is full. If it's full, we're done.
                        if (newHypotheticalBoard.IsFull())
                        {
                            return newHypotheticalBoard;
                        }

                        // If the board's not full, we need to make another branch.
                        else
                        {
                            newHypotheticalBoard = RunHypotheticalBoard(newHypotheticalBoard);
                            if (newHypotheticalBoard != null)
                            {
                                break;
                            }
                        }
                        /*
                        // Confirm board validity (not really necessary, but just for paranoid double-checking)
                        if (!newHypotheticalBoard.IsValid())
                        {
                            MessageBox.Show("Grievous error has occurred!!");
                            return null;
                        }
                        // Check whether board is full. If it's full, we're done.
                        else if (newHypotheticalBoard.IsFull())
                        {
                            return newHypotheticalBoard;
                        }
                        // If the board's not full, we need to test another value.
                        else
                        {
                            newHypotheticalBoard = RunHypotheticalBoard(newHypotheticalBoard);
                            if newHypotheticalBoard !=
                            return newHypotheticalBoard;
                        } */
                    } // end for-loop
                    return null;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return hypotheticalBoard;
            }
        }

        private Cell ChooseEmptyCellWithLeastNumberOfCandidates(ref Board board, List<Cell> listOfCells)
        {
            Cell idealCell = new Cell(0, 10, 10);
            int leastNumberOfCandidates = 10; // could be any large number greater than 9
            foreach (Cell cell in listOfCells)
            {
                int numberOfCandidates = GetListOfCandidatesAtCell(ref board, cell.Row, cell.Column).Count;
                if (numberOfCandidates < leastNumberOfCandidates)
                {
                    leastNumberOfCandidates = numberOfCandidates;
                    idealCell = new Cell(cell.Content, cell.Row, cell.Column);
                }
            }
            return idealCell;
        }

        private List<int> GetListOfCandidatesAtCell(ref Board board, int rowIndex, int columnIndex)
        {
            List<int> listOfCandidates = new List<int>();
            for (int testDigit = 1; testDigit < 10; testDigit++)
            {
                if (board.IsPotentialCandidate(testDigit, rowIndex, columnIndex))
                {
                    listOfCandidates.Add(testDigit);
                }
            }
            return listOfCandidates;
        }

    }
}
