using System;
using System.Linq;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {


            //Сделать заполнение массива рандомными числами и зависимости от выбора уровня сложности - заполнять определенное кол-во клеток
            //for (int i = 0; i < 9; i++)
            //{
            //    for (int j = 0; j < 9; j++)
            //    {
            //        arr[i,j] = j;
            //    }
            //}


            //bool q = Solver.possible(0, 2, 1);
            //Console.WriteLine(q ? "true" : "false");
            //Solver.solve();
            Solver.Print();
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if(Solver.arr[x,y] == 0)
                    {
                        Solver.solve();
                    }
                }
            }
            Solver.Print();

                }



    }
}
