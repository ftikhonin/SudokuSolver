using SudokuSolver;
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
namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly RowCollection _rowCollection = new RowCollection();
        public PuzzleGrid _grid;

        private int Difficult { get; set; }
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
            Difficult = 0;
            _grid.Cells = (int[,])_grid.CellsBckp.Clone();
            UpdateDataGridCells();
        }

        private void DifficultList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _grid.SelectedDifficult = Array.FindIndex(PuzzleGrid.DifficultType, x => x == e.AddedItems[0].ToString());
            _grid.Cells = (int[,])_grid.CellsBckp.Clone();            
            _grid.RemoveCells();
            UpdateDataGridCells();
        }

        private void UpdateDataGridCells()
        {
            var grid = _grid.Cells;
            int size = PuzzleGrid.Size;

            _rowCollection.Rows.Clear();
            DataContext = _rowCollection;

            for (int i = 0; i < 9; i++)
            {
                int[] row = new int[size];

                for (int j = 0; j < size; j++)
                {
                    row[j] = grid[i, j];

                }
                _rowCollection.AddRow(new Row(row));
            }

            dataGrid1.ItemsSource = _rowCollection.Rows;
            dataGrid1.UpdateLayout();
        }
    }
}

