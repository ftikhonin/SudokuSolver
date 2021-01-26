using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    static class Solver
    {
        const int size = 9;
        public static int[,] arr = new int[size, size] { { 5, 3, 0, 0, 7, 0, 0, 0, 0 },
                                               { 6, 0, 0, 1, 9, 5, 0, 0, 0 },
                                               { 0, 9, 8, 0, 0, 0, 0, 6, 0 },
                                               { 8, 0, 0, 0, 6, 0, 0, 0, 3 },
                                               { 4, 0, 0, 8, 0, 3, 0, 0, 1 },
                                               { 7, 0, 0, 0, 2, 0, 0, 0, 6 },
                                               { 0, 6, 0, 0, 0, 0, 2, 8, 0 },
                                               { 0, 0, 0, 4, 1, 9, 0, 0, 5 },
                                               { 0, 0, 0, 0, 8, 0, 0, 7, 9 }, };
        public static bool possible(int y, int x, int n)
        {
            for (int i = 0; i < 9; i++)
            {
                if (arr[y, n] == n)
                {
                    return false;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                if (arr[i, x] == n)
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
                    var q = arr[y0 + i, x0 + j];
                    //var qq = arr[0, 0];
                    if (q == n)
                        return false;
                }
            }
            return true;
        }
        public static void solve()
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (arr[y, x] == 0)
                    {
                        for (int n = 1; n < 9; n++)
                        {
                            if (possible(y, x, n))
                            {
                                arr[y, x] = n;
                                solve();
                                arr[y, x] = 0;
                            }
                        }
                    }
                }
            }

        }

        public static void Print()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(string.Concat(arr[i, j] == 0 ? "  " : arr[i, j].ToString() + " "));
                    if ((j + 1) % 3 == 0)
                        Console.Write("|");

                }
                Console.WriteLine();
                if ((i + 1) % 3 == 0)
                    Console.WriteLine("--------------------|");
            }
        }
    }
}
