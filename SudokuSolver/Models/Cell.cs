using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Models
{

    public class Cell : INotifyPropertyChanged
    {
        private int content;
        private int rowIndex;
        private int columnIndex;

        private static int[] allowedValues = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public Cell(int initialValue, int rowIndex, int columnIndex)
        {
            content = initialValue;
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
        }

        public int Content // There is a GUI Binding to this property
        {
            get { return content; }
            set
            {
                content = value;
                NotifyPropertyChanged("Content");
            }
        }

        public void SetValueQuietly(int newValue)
        {
            content = newValue;
        }

        public int Row
        {
            get { return rowIndex; }
        }

        public int Column
        {
            get { return columnIndex; }
        }

        public static int[] AllowedValues
        {
            get { return allowedValues; }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
