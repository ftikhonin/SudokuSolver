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
        private RowCollection _rowCollection = new RowCollection();
        private RowCollection _rowCollectionBckp = new RowCollection();
        public PuzzleGrid _grid;
        private PuzzleGrid _gridBckp;
        
        private int _difficult { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DifficultList.ItemsSource = new List<DifficultBox> { new DifficultBox() };
            PuzzleGrid grid = new PuzzleGrid();
            grid.CreateGrid();
            PuzzleGrid gridBckp = (PuzzleGrid)grid.Clone();
            var cells = grid.Cells;
            int size = PuzzleGrid.Size;

            DataContext = _rowCollection;

            for (int i = 0; i < 9; i++)
            {
                int[] row = new int[size];

                for (int j = 0; j < size; j++)
                {
                    row[j] = cells[i, j];

                }
                _rowCollection.AddRow(new Row(row));
            }

            dataGrid1.ItemsSource = _rowCollection.Rows;
            _rowCollectionBckp = _rowCollection;
            _grid = grid;
            _gridBckp = gridBckp;

        }

        private void Solve_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _difficult = 0;

        }

        private void DifficultList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _grid.SelectedDifficult = Array.FindIndex(PuzzleGrid.DifficultType, x => x == e.AddedItems[0].ToString());
            _grid.Cells = (int[,])_grid.CellsBckp.Clone();            
            _grid.RemoveCells();
            
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

