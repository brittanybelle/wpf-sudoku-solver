using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SudokuSolver.Models
{
    public class Board
    {
        private List<List<Cell>> cells;

        public Board()
        {
            cells = new List<List<Cell>>();

            // Add new row, of type List<Cell>
            for (int i = 0; i < 9; i++)
            {
                cells.Add(new List<Cell>());

                // Add new Cells within this row/List<Cell>
                for (int j = 0; j < 9; j++)
                {
                    cells[i].Add(new Cell(0, i, j));
                    cells[i][j].PropertyChanged += CheckCellValidityOnUpdate;
                }
            }
        }

        public void UpdateBoard(List<int> inputArray)
        {
            ClearBoard();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i][j].Content = inputArray[i*9 + j];
                }
            }
        }

        public void UpdateBoardQuietly(List<int> inputArray)
        {
            ClearBoard();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i][j].SetValueQuietly(inputArray[i * 9 + j]);
                }
            }
        }

        public List<int> GetBoardStateAsIntArray()
        {
            List<int> newList = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    newList.Add(cells[i][j].Content);
                }
            }
            return newList;
        }

        public void ClearBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i][j].Content = 0;
                }
            }
        }

        public List<List<Cell>> Cells
        {
            get { return cells; }
        }

        public bool IsValid()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (! CanAddCellAt(i, j))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void CheckCellValidityOnUpdate(object sender, PropertyChangedEventArgs args)
        {
            int rowIndex = (sender as Cell).Row;
            int columnIndex = (sender as Cell).Column;

            if (! CanAddCellAt(rowIndex, columnIndex))
            {
                cells[rowIndex][columnIndex].Content = 0;
                MessageBox.Show("You can't change the cell to this - illegal input!");
            }
        }

        public bool IsPotentialCandidate(int testNumber, int rowIndex, int columnIndex)
        {
            cells[rowIndex][columnIndex].SetValueQuietly(testNumber);

            if(CanAddCellAt(rowIndex, columnIndex))
            {
                cells[rowIndex][columnIndex].SetValueQuietly(0);
                return true;
            }

            else
            {
                cells[rowIndex][columnIndex].SetValueQuietly(0);
                return false;
            }
        }

        public bool CanAddCellAt(int rowIndex, int columnIndex)
        {
            // Check row
            if (ThereAreRepeatsInList(GetRow(rowIndex)))
            {
                return false;
            }

            // Check column

            if (ThereAreRepeatsInList(GetColumn(columnIndex)))
            {
                return false;
            }

            // Check i-th 3x3 square
            int squareIndex = GetSquareIndex(rowIndex, columnIndex);
            if (ThereAreRepeatsInList(GetSquare(squareIndex)))
            {
                return false;
            }

            return true;
        }

        private bool ThereAreRepeatsInList(List<Cell> cellsToCheck)
        {
            // Iterate over all cells in list of 9
            for (int i = 0; i < 8; i++)
            {
                if (cellsToCheck[i].Content != 0)
                {
                    // Given the current (nonzero) cell, iterate over all remaining cells
                    for (int j = i + 1; j < 9; j++)
                    {
                        if (cellsToCheck[i].Content == cellsToCheck[j].Content)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public Cell GetCell(int rowIndex, int columnIndex)
        {
            return cells[rowIndex][columnIndex];
        }

        public int GetCellValue(int rowIndex, int columnIndex)
        {
            return GetCell(rowIndex, columnIndex).Content;
        }

        public List<Cell> GetRow(int rowIndex)
        {
            return cells[rowIndex];
        }

        public List<Cell> GetColumn(int columnIndex)
        {
            List<Cell> column = new List<Cell>();
            for (int i = 0; i < 9; i++)
            {
                column.Add(cells[i][columnIndex]);
            }
            return column;
        }
        
        public List<Cell> GetSquare(int squareIndex)
        {
            List<Cell> square = new List<Cell>();

            int rowStartIndex = 0;
            if (squareIndex == 3 || squareIndex == 4 || squareIndex == 5)
            {
                rowStartIndex = 3;
            }
            else if (squareIndex == 6 || squareIndex == 7 || squareIndex == 8)
            {
                rowStartIndex = 6;
            }

            int modIndex = squareIndex % 3; // Returns 0 for left column,
                                            // 1 for middle, 2 for right

            int colStartIndex = modIndex * 3;

            for (int i = rowStartIndex; i < rowStartIndex + 3; i++)
            {
                for (int j = colStartIndex; j < colStartIndex + 3; j++)
                {
                    square.Add(cells[i][j]);
                }
            }

            return square;
        }

        private int GetSquareIndex(int rowIndex, int columnIndex)
        {
            if (rowIndex < 3)
            {
                if (columnIndex < 3) { return 0; }
                else if (columnIndex < 6) { return 1; }
                else { return 2; }
            }

            else if (rowIndex < 6)
            {
                if (columnIndex < 3) { return 3; }
                else if (columnIndex < 6) { return 4; }
                else { return 5; }
            }

            else
            {
                if (columnIndex < 3) { return 6; }
                else if (columnIndex < 6) { return 7; }
                else { return 8; }
            }
        }

    }
}
