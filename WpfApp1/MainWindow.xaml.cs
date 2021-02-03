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
        const int size = 9;
        //public static int[,] arr = new int[size, size] { { 5, 3, 0, 0, 7, 0, 0, 0, 0 },
        //                                                 { 6, 0, 0, 1, 9, 5, 0, 0, 0 },
        //                                                 { 0, 9, 8, 0, 0, 0, 0, 6, 0 },
        //                                                 { 8, 0, 0, 0, 6, 0, 0, 0, 3 },
        //                                                 { 4, 0, 0, 8, 0, 3, 0, 0, 1 },
        //                                                 { 7, 0, 0, 0, 2, 0, 0, 0, 6 },
        //                                                 { 0, 6, 0, 0, 0, 0, 2, 8, 0 },
        //                                                 { 0, 0, 0, 4, 1, 9, 0, 0, 5 },
        //                                                 { 0, 0, 0, 0, 8, 0, 0, 7, 9 }, };
        public static int[,] arr = new int[size, size] { { 0, 0, 0, 0, 0, 9, 4, 7, 0 },
                                                         { 0, 0, 2, 0, 3, 0, 0, 9, 8 },
                                                         { 0, 6, 0, 0, 0, 2, 0, 0, 1 },
                                                         { 0, 0, 0, 0, 0, 0, 5, 0, 7 },
                                                         { 0, 7, 0, 0, 0, 0, 0, 6, 0 },
                                                         { 8, 0, 3, 0, 0, 0, 0, 0, 0 },
                                                         { 6, 0, 0, 1, 0, 0, 0, 2, 0 },
                                                         { 7, 4, 0, 0, 6, 0, 9, 0, 0 },
                                                         { 0, 1, 9, 4, 0, 0, 0, 0, 0 }, };
        public int[,] res;

        public static List<Row> Rows;
        public MainWindow()
        {
            InitializeComponent();
            res = arr;

            List<Row> rows = new List<Row>();
            for (int i = 0; i < 9; i++)
            {
                var q = new Row();
                q = new Row() { val1 = arr[i, 0], val2 = arr[i, 1], val3 = arr[i, 2], val4 = arr[i, 3], val5 = arr[i, 4], val6 = arr[i, 5], val7 = arr[i, 6], val8 = arr[i, 7], val9 = arr[i, 8] };
                rows.Add(q);                
            }

            dataGrid1.ItemsSource = rows;

        }
        private void FitToContent()
        {
            // where dg is my data grid's name...
            foreach (DataGridColumn column in dataGrid1.Columns)
            {
                //if you want to size your column as per the cell content
                column.Width = new DataGridLength(1.0, DataGridLengthUnitType.SizeToCells);
                //if you want to size your column as per the column header
                column.Width = new DataGridLength(1.0, DataGridLengthUnitType.SizeToHeader);
                //if you want to size your column as per both header and cell content
                column.Width = new DataGridLength(1.0, DataGridLengthUnitType.Auto);
            }
        }

        private void Solve_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            GetResult();
            List<Row> _rows = new List<Row>();
            for (int i = 0; i < 9; i++)
            {
                var rw = new Row();
                rw = new Row() { val1 = res[i, 0], 
                    val2 = res[i, 1], 
                    val3 = res[i, 2], 
                    val4 = res[i, 3], 
                    val5 = res[i, 4], 
                    val6 = res[i, 5], 
                    val7 = res[i, 6], 
                    val8 = res[i, 7], 
                    val9 = res[i, 8] };
                _rows.Add(rw);
            }
            Rows = _rows;
            dataGrid1.ItemsSource = Rows;
            dataGrid1.Items.Refresh();
            //dataGrid1.UpdateLayout();
            
        }
        public bool possible(int y, int x, int n)
        {

            for (int i = 0; i < 9; i++)
            {
                if (res[y, i] == n)
                {
                    return false;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                if (res[i, x] == n)
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
                    var q = res[y0 + i, x0 + j];
                    if (q == n)
                        return false;
                }
            }
            return true;
        }
        public void solve()
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (res[y, x] == 0)
                    {
                        for (int n = 1; n < 10; n++)
                        {
                            if (possible(y, x, n))
                            {
                                res[y, x] = n;
                                solve();
                                res[y, x] = 0;
                            }
                        }
                        return;
                    }

                }
            }
        }
        public void GetResult()
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (res[x, y] == 0)
                    {
                        solve();
                    }
                }
            }
        }


    }

    public class Row
    {
        public int? val1 { get; set; }
        public int? val2 { get; set; }
        public int? val3 { get; set; }
        public int? val4 { get; set; }
        public int? val5 { get; set; }
        public int? val6 { get; set; }
        public int? val7 { get; set; }
        public int? val8 { get; set; }
        public int? val9 { get; set; }
        //public List <int?> values { get; set; }

    }
}

