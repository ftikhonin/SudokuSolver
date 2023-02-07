using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace SudokuSolver
{
    public class PuzzleGrid :ICloneable
    {
        public const int PuzzleSize = 9;
        public int[,] Cells = new int[PuzzleSize, PuzzleSize];
        public int[,] CellsBckp = new int[PuzzleSize, PuzzleSize];
        private readonly int _defaultDifficult = 0;
        public List<int> Candidates = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public int SelectedDifficult
        {
            get => _defaultDifficult;
            set => SetDifficult(value);
        }

        public object Clone()
        {
            PuzzleGrid other = (PuzzleGrid) this.MemberwiseClone();
            other.Cells = this.Cells;
            return other;
        }

        public static string[] DifficultType = new string[5] { "Beginner",   //0 - 32
                                                               "Easy",       //1 - 30
                                                               "Medium",     //2 - 28
                                                               "Tricky",     //3 - 26
                                                               "Fiendish" }; //4 - 24

        /// <summary> The number of filled cells, depending on the complexity (by default 32)</summary>
        public int FilledCells { get; set; } = 32;


        public bool CheckExistsInRow(int val, int row)
        {
            bool result = false;
            for (int i = 0; i < 9; i++)
            {
                if (Cells[row, i] == val)
                    result = true;
            }
            return result;
        }

        public bool CheckExistsInColumn(int val, int col)
        {
            bool result = false;
            for (int i = 0; i < 9; i++)
            {
                if (Cells[i, col] == val)
                    result = true;
            }
            return result;
        }

        public bool CheckExistsInSquere(int val, int row, int col)
        {
            bool result = false;
            if (row < 3)
            {
                if (col < 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (Cells[i, j] == val)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
                else if (col < 6)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 3; j < 6; j++)
                        {
                            if (Cells[i, j] == val)
                            {
                                result = true;
                                break;
                            }

                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 6; j < 9; j++)
                        {
                            if (Cells[i, j] == val)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
            }
            else if (row < 6)
            {
                if (col < 3)
                {
                    for (int i = 3; i < 6; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (Cells[i, j] == val)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
                else if (col < 6)
                {
                    for (int i = 3; i < 6; i++)
                    {
                        for (int j = 3; j < 6; j++)
                        {
                            if (Cells[i, j] == val)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 3; i < 6; i++)
                    {
                        for (int j = 6; j < 9; j++)
                        {
                            if (Cells[i, j] == val)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                if (col < 3)
                {
                    for (int i = 6; i < 9; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (Cells[i, j] == val)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
                else if (col < 6)
                {
                    for (int i = 6; i < 9; i++)
                    {
                        for (int j = 3; j < 6; j++)
                        {
                            if (Cells[i, j] == val)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 6; i < 9; i++)
                    {
                        for (int j = 6; j < 9; j++)
                        {
                            if (Cells[i, j] == val)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public void CreateGrid()
        {
            const int nc = 0;
            for (int x = 0; x < PuzzleSize; x++)
            {
                for (int y = 0; y < PuzzleSize; y++)
                {
                    Cells[x, y] = nc;
                }
            }
            SolveGrid();
            CellsBckp = (int[,])Cells.Clone();
        }


        public void SolveGrid()
        {
            if (!ExistsEmptyCells())
            {
                return;
            }

            for (int i = 0; i < 81; i++)
            {
                int row = i / 9;
                int col = i % 9;
                if (Cells[row, col] == 0)
                {
                    Candidates = Candidates.OrderBy(x => Guid.NewGuid()).ToList(); //shuffle numbers
                    foreach (int value in Candidates)
                    {
                        var er = CheckExistsInRow(value, row);
                        var ec = CheckExistsInColumn(value, col);
                        var es = CheckExistsInSquere(value, row, col);
                        if (!er && !ec && !es)
                        {
                            int cell = value;
                            Cells[row, col] = cell;
                            break;
                        }
                    }
                }
            }

            //check for empty cells and restart if any
            if (ExistsEmptyCells())
            {
                int nc = 0;
                for (int x = 0; x < PuzzleSize; x++)
                {
                    for (int y = 0; y < PuzzleSize; y++)
                    {
                        Cells[x, y] = nc;

                    }
                }
                SolveGrid();
            }
        }

        public void RemoveCells()
        {

            Random a = new Random(); // replace from new Random(DateTime.Now.Ticks.GetHashCode());
                                     // Since similar code is done in default constructor internally
            List<int> randomList = new List<int>();
            int newNumber;
            

            while (randomList.Count < FilledCells)
            {
                newNumber = a.Next(0, 80);
                if (!randomList.Contains(newNumber))
                {
                    randomList.Add(newNumber);
                    int row = newNumber / 9;
                    int col = newNumber % 9;
                    Cells[row, col] = 0;

                    Cell cell = new Cell
                    {
                        X = row,
                        Y = col
                    };
                    //leave cell indexes in the list for readonly
                    ReadOnlyCellsIndex.Add(cell);
                }
            }
        }

        public static List<Cell> ReadOnlyCellsIndex = new List<Cell>();
        public bool ExistsEmptyCells()
        {
            bool result = false;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Cells[i, j] == 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public void  SetDifficult(int value)
        {
            switch (value)
            {
                case 0:
                    FilledCells = 32;
                    break;
                case 1:
                    FilledCells = 30;
                    break;
                case 2:
                    FilledCells = 28;
                    break;
                case 3:
                    FilledCells = 26;
                    break;
                case 4:
                    FilledCells = 24;
                    break;
                default:
                    FilledCells = 32;
                    break;
            }

        }
    }
}
