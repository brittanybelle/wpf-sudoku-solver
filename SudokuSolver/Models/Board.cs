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

        public Board()
        {
            cells = new List<List<Cell>>();

            for (int i = 0; i < 9; i++)
            {
                // add new row
                cells.Add(new List<Cell>());

                for (int j = 0; j < 9; j++)
                {
                    // add cols within this row
                    cells[i].Add(new Cell());
                }
            }
        }

        public Board(int[] validInputArray)
        {
            /*
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j].Content = validInputArray[i * 9 + j];
                }
            } */
        }

        public List<List<Cell>> Cells
        {
            get { return cells; }
        }

        public bool IsValid()
        {
            return true;
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

        /*
        public List<Cell> GetSquare(int squareIndex)
        {
            // return ...;
        }
        */

    }
}
