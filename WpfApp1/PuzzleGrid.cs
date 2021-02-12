using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using SudokuSolver
namespace SudokuSolver
{
    class PuzzleGrid
    {
        public const int size = 9;
        public Cell[,] cells = new Cell[size, size];
        public Cell[,] cellsSnapshot;
        private int _defaultDifficult = 0;
        public static int SelectedDifficult { get; set; }
        /// <summary> Сложность  </summary>
        public int Difficult
        {
            get { return _defaultDifficult; }
            set
            {
                SetDifficult(value);
            }
        }
        public static string[] DifficultType = new string[5] { "Beginner",  //0 - 32
                                                               "Easy",      //1 - 30
                                                               "Medium",    //2 - 28
                                                               "Tricky",    //3 - 26
                                                               "Fiendish" };//4 - 24
        /// <summary> Количество заполненных клеток, в зависимости от уровня сложности (по умолчанию 32)</summary>
        private int _filledCells { get; set; } = 32;



        public bool CheckExistsInRow(int val, int row)
        {
            bool result = false;
            for (int i = 0; i < 9; i++)
            {
                if (cells[row, i].Value == val)
                    result = true;
            }
            return result;
        }

        public bool CheckExistsInColumn(int val, int col)
        {
            bool result = false;
            for (int i = 0; i < 9; i++)
            {
                if (cells[i, col].Value == val)
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
                            if (cells[i, j].Value == val)
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
                            if (cells[i, j].Value == val)
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
                            if (cells[i, j].Value == val)
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
                            if (cells[i, j].Value == val)
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
                            if (cells[i, j].Value == val)
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
                            if (cells[i, j].Value == val)
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
                            if (cells[i, j].Value == val)
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
                            if (cells[i, j].Value == val)
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
                            if (cells[i, j].Value == val)
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
            Cell nc = new Cell();
            nc.Value = 0;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    cells[x, y] = nc;

                }
            }

            SolveGrid();
        }
        public List<int> Candidates = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public List<int> CandidatesBckp = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public void SolveGrid()
        {
            if (!ExistsEmptyCells())
            {
                return;
            }

            for (int i = 0; i < 81; i++)
            {
                bool isFilled = false;
                if (i == 47)
                { var q = 1; }
                int row = i / 9;
                int col = i % 9;
                if (cells[row, col].Value == 0)
                {
                    Candidates = Candidates.OrderBy(x => Guid.NewGuid()).ToList(); //shuffle numbers
                    foreach (int value in Candidates)
                    {
                        var er = CheckExistsInRow(value, row);
                        var ec = CheckExistsInColumn(value, col);
                        var es = CheckExistsInSquere(value, row, col);
                        if (!er && !ec && !es)
                        {
                            Cell cell = new Cell();
                            cell.Value = value;
                            cells[row, col] = cell;
                            isFilled = true;
                            break;
                        }
                    }
                }

            }

            //check for empty cells and restart if any
            if (ExistsEmptyCells())
            {
                Cell nc = new Cell();
                nc.Value = 0;
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        cells[x, y] = nc;

                    }
                }
                SolveGrid();
            }
        }




        public bool ExistsEmptyCells()
        {
            bool result = false;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (cells[i, j].Value == 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public void SetDifficult(int value)
        {
            switch (value)
            {
                case 0:
                    _filledCells = 32;
                    break;
                case 1:
                    _filledCells = 30;
                    break;
                case 2:
                    _filledCells = 28;
                    break;
                case 3:
                    _filledCells = 26;
                    break;
                case 4:
                    _filledCells = 24;
                    break;
                default:
                    _filledCells = 32;
                    break;
            }
        }


    }
}
