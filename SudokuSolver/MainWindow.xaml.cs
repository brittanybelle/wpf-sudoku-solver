using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using SudokuSolver.Models;
using System.Data;
using Microsoft.Win32;
using System.IO;

namespace SudokuSolver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Board InputBoard = new Board();
        Solver solver = new Solver();

        public MainWindow()
        {
            InitializeComponent();
            
            InputGrid.ItemsSource = InputBoard.Cells;
            OutputGrid.ItemsSource = solver.ActiveBoard.Cells;
        }

        private void LoadFileButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Load file button was clicked.");
            // note to self - remember to check for file IO exceptions here
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|Comma separated values (*.csv)|*.csv";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true) // True if user selects file
            {
                String inputString = File.ReadAllText(openFileDialog.FileName);
                List<int> newBoardData = InputParser.GetBoard(inputString);
                if (newBoardData != null)
                {
                    InputBoard.UpdateBoard(newBoardData);
                }
            }            
        }

        private void SolveButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Solve button was clicked.");
            solver.Solve(InputBoard.GetBoardStateAsIntArray());
        }
        
    }
}
