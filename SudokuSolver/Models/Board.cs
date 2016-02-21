using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Models
{
    public class Board
    {
        private List<List<Cell>> cells;

        public Board(List<int> inputArray)
        {
            cells = new List<List<Cell>>();

            Console.WriteLine("you got in the board() constructor!!");
            for (int i = 0; i < 9; i++)
            {
                // add new row
                cells.Add(new List<Cell>());

                for (int j = 0; j < 9; j++)
                {
                    // add cols within this row
                    int cellValue = inputArray.ElementAt(i * 9 + j);
                    cells[i].Add(new Cell(cellValue));
                }
            }
        }

        public Board()
        {
            cells = new List<List<Cell>>();

            for (int i = 0; i < 9; i++)
            {
                cells.Add(new List<Cell>());
                for (int j = 0; j < 9; j++)
                {
                    cells[i].Add(new Cell(0));
                }
            }
        }

        public void UpdateBoard(List<int> inputArray)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells.ElementAt(i).ElementAt(j).Content = inputArray.ElementAt(i * 9+ j);
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
                // Check i-th row
                if (ThereAreRepeatsInList(GetRow(i)))
                {
                    return false;
                }

                // Check i-th column
                
                if (ThereAreRepeatsInList(GetColumn(i)))
                {
                    return false;
                }

                // Check i-th 3x3 square
                if (ThereAreRepeatsInList(GetSquare(i)))
                {
                    return false;
                }
            }

            return true;
        }

        private bool ThereAreRepeatsInList(List<Cell> cellsToCheck)
        {
            // Iterate over all cells
            for (int i = 0; i < 8; i++)
            {
                if (cellsToCheck.ElementAt(i).Content != 0)
                {
                    // Given the current (nonzero) cell, iterate over all remaining cells
                    for (int j = i + 1; j < 9; j++)
                    {
                        if (cellsToCheck.ElementAt(i).Content == cellsToCheck.ElementAt(j).Content)
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
            return cells.ElementAt(rowIndex).ElementAt(columnIndex);
        }

        public int GetCellValue(int rowIndex, int columnIndex)
        {
            return GetCell(rowIndex, columnIndex).Content;
        }

        public List<Cell> GetRow(int rowIndex)
        {
            return cells.ElementAt(rowIndex);
        }

        public List<Cell> GetColumn(int columnIndex)
        {
            List<Cell> column = new List<Cell>();
            for (int i = 0; i < 9; i++)
            {
                column.Add(cells.ElementAt(i).ElementAt(columnIndex));
            }
            return column;
        }
        
        public List<Cell> GetSquare(int squareIndex)
        {
            List<Cell> square = new List<Cell>();

            int iStartIndex = 0;
            if (squareIndex == 3 || squareIndex == 4 || squareIndex == 5)
            {
                iStartIndex = 3;
            }
            else if (squareIndex == 6 || squareIndex == 7 || squareIndex == 8)
            {
                iStartIndex = 6;
            }

            int modIndex = squareIndex % 3; // Returns 0 for left column,
                                            // 1 for middle, 2 for right
            int jStartIndex = modIndex * 3;

            for (int i = iStartIndex; i < iStartIndex + 3; i++)
            {
                for (int j = jStartIndex; j < jStartIndex + 3; j++)
                {
                    square.Add(cells.ElementAt(i).ElementAt(j));
                }
            }

            return square;
        }
        
    }
}
