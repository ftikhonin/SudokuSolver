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
        public MainWindow()
        {
            InitializeComponent();

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
            // var rw = new Row();
            //    rw = new Row() { val1 = res[i, 0], 
            ////GetResult();
            //List<Row> _rows = new List<Row>();
            //for (int i = 0; i < 9; i++)
            //{
            //    var rw = new Row();
            //    rw = new Row() { val1 = res[i, 0], 
            //        val2 = res[i, 1], 
            //        val3 = res[i, 2], 
            //        val4 = res[i, 3], 
            //        val5 = res[i, 4], 
            //        val6 = res[i, 5], 
            //        val7 = res[i, 6], 
            //        val8 = res[i, 7], 
            //        val9 = res[i, 8] };
            //    _rows.Add(rw);
            //}
            //Rows = _rows;
            //dataGrid1.ItemsSource = Rows;
            //dataGrid1.Items.Refresh();
            //dataGrid1.UpdateLayout();

        }
    }
}

