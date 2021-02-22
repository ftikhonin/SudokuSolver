using System;
using System.Linq;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {


            Solver.Print();
            Console.WriteLine();
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
