﻿using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
            int rowIndex = ((DataGrid)sender).ItemContainerGenerator.IndexFromContainer(row);
            int colIndex = col.DisplayIndex;

            if (!ReadOnlyCellsIndex.Any(x => x.X == rowIndex && x.Y == colIndex))
            {
                dataGrid1.CancelEdit();
            }
        }

        private void DataGrid1_OnCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

            int rowIndex = ((DataGrid)sender).ItemContainerGenerator.IndexFromContainer(e.Row);
            int colIndex = e.Column.DisplayIndex;

            //get edited cell
            DataGridRow editedRow = dataGrid1.ItemContainerGenerator.ContainerFromItem(dataGrid1.Items[rowIndex]) as DataGridRow;
            DataGridCell editedCell = dataGrid1.Columns[colIndex].GetCellContent(editedRow).Parent as DataGridCell;

            //set background
            var valTextBox = (TextBox)editedCell.Content;
            var val = valTextBox.Text;

            //if the value does not possible, paint it red
            if (!string.IsNullOrWhiteSpace(val) && ReadOnlyCellsIndex.Any(x => x.X == rowIndex && x.Y == colIndex))
            {
                if (!Possible(rowIndex, colIndex, Convert.ToInt32(val)))
                {
                    editedCell.Background = Brushes.Red;
                }
                else
                {
                    editedCell.Background = Brushes.Transparent;
                }
            }
        }

        public bool Possible(int y, int x, int n)
        {
            for (int i = 0; i < 9; i++)
            {
                if (_grid.Cells[y, i] == n)
                {
                    return false;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                if (_grid.Cells[i, x] == n)
                {
                    return false;
                }
            }
            int x0 = (x / 3) * 3;
            int y0 = (y / 3) * 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var q = _grid.Cells[y0 + i, x0 + j];
                    if (q == n)
                        return false;
                }
            }
            return true;
        }
    }


}

