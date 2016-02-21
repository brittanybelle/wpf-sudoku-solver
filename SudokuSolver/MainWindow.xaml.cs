﻿using System;
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

namespace SudokuSolver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Board InputBoard = new Board();
        Board OutputBoard = new Board();

        public MainWindow()
        {
            InitializeComponent();
            
            InputGrid.ItemsSource = InputBoard.Cells;
            OutputGrid.ItemsSource = InputBoard.Cells;
        }

        private void LoadFileButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Load file button was clicked.");
        }

        private void SolveButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Solve button was clicked.");
        }
        
    }
}
