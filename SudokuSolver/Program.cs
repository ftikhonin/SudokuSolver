using System;

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


            bool q = Solver.possible(4, 4, 5);
            Console.WriteLine(q ? "true" : "false");

        }



    }
}
