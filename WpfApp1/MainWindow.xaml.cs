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
//using SudokuSolver.Solver
namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RowCollection rowCollection = new RowCollection();
        public static List<Row> Rows;
        private int _difficult { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DifficultList.ItemsSource = new List<DifficultBox> { new DifficultBox() };
            var newGrid = new PuzzleGrid();
            newGrid.CreateGrid();
            var grid = newGrid.cells;
            int size = PuzzleGrid.size;

            DataContext = rowCollection;

            for (int i = 0; i < 9; i++)
            {
                int[] row = new int[size];

                for (int j = 0; j < size; j++)
                {
                    row[j] = grid[i, j].Value;

                }
                rowCollection.AddRow(new Row(row));
            }
            dataGrid1.ItemsSource = rowCollection.Rows;


        }

        private void Solve_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _difficult = 0;

        }

        private void DifficultList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PuzzleGrid.SelectedDifficult = Array.FindIndex(PuzzleGrid.DifficultType, x => x == e.AddedItems[0].ToString());
        }
    }
    public class DifficultBox
    {
        public static List<string> _difficultTypes = new List<string>();

        static DifficultBox()
        {
            for (int i = 0; i < PuzzleGrid.DifficultType.Length; i++)
            {
                var q = PuzzleGrid.DifficultType[i];
                _difficultTypes.Add(q);
            }
        }

        public IEnumerable<string> DifficultSource => _difficultTypes;
        public string DifficultSourceSelectedCurrently = "Easy";
    }
}

