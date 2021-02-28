using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static SudokuSolver.PuzzleGrid;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly RowCollection _rowCollection = new RowCollection();
        public PuzzleGrid _grid;

        public MainWindow()
        {
            InitializeComponent();
            DifficultList.ItemsSource = new List<DifficultBox> { new DifficultBox() };
            PuzzleGrid grid = new PuzzleGrid();
            grid.CreateGrid();
            _grid = grid;
            UpdateDataGridCells();            
        }

        private void Solve_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _grid.Cells = (int[,])_grid.CellsBckp.Clone();
            UpdateDataGridCells();
        }

        private void DifficultList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _grid.SelectedDifficult = Array.FindIndex(DifficultType, x => x == e.AddedItems[0].ToString());
            PuzzleGrid grid = new PuzzleGrid();
            grid.CreateGrid();
            _grid = grid;
            _grid.Cells = (int[,])_grid.CellsBckp.Clone();            
            _grid.RemoveCells();
            UpdateDataGridCells();
        }

        private void UpdateDataGridCells()
        {
            var grid = _grid.Cells;

            _rowCollection.Rows.Clear();
            DataContext = _rowCollection;

            for (int i = 0; i < 9; i++)
            {
                int[] row = new int[PuzzleSize];

                for (int j = 0; j < PuzzleSize; j++)
                {
                    row[j] = grid[i, j];
                }
                _rowCollection.AddRow(new Row(row));
            }

            dataGrid1.ItemsSource = _rowCollection.Rows;
            dataGrid1.UpdateLayout();
        }
        /// <summary>Numeric input only</summary>
        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^1-9]+").IsMatch(e.Text);
        }

        /// <summary>Makes filled cells read-only</summary>
        private void DataGrid1_OnPreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            var col = e.Column;
            var row = e.Row;
            int row_index = ((DataGrid)sender).ItemContainerGenerator.IndexFromContainer(row);
            int col_index = col.DisplayIndex;


            if (!ReadOnlyCellsIndex.Any(x => x.X == row_index && x.Y == col_index))
            {
                //TODO: make filled cells read-only and remove this message box
                MessageBox.Show("Gotcha!");
            }
            var q = ReadOnlyCellsIndex;
        }
    }
}

