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

        private static int[] allowedValues = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public Cell()
        {
            // New cells initialize as empty
            content = 0;
        }

        public Cell(int initialValue)
        {
            content = initialValue;
        }

        public int Content
        {
            get { return content; }
            set
            {
                content = value;
                //Console.WriteLine("Cell updated! New value = " + content);
                NotifyPropertyChanged("Content");
            }
        }

        /*
        public void SetValueLoudly(int newValue)
        {
            content = newValue;
            NotifyPropertyChanged("Content");
        }

        public void SetValueQuietly(int newValue)
        {
            content = newValue;
        } */

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
