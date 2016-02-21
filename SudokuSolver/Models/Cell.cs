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
        private int[] allowedValues;
        private int content;

        public Cell()
        {
            // New cells initialize as empty
            content = 0;

            // Set allowed values (digits 0-9)
            allowedValues = new int[10];
            for (int i = 0; i < 10; i++)
            {
                allowedValues[i] = i;
            }
        }

        public int Content
        {
            get { return content; }
            set
            {
                content = value;
                Console.WriteLine("Cell updated! New value = " + content);
                NotifyPropertyChanged("Content");
            }
        }

        public int[] AllowedValues
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
