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
        /// <summary> Сложность  </summary>
        public int Difficult
        {
            get { return _defaultDifficult; }
            set
            {
                SetDifficult(value);
            }
        }
        /// <summary> Количество заполненных клеток, в зависимости от уровня сложности (по умолчанию 32)</summary>
        private int _filledCells { get; set; } = 32;

        //0 - 32
        //1 - 30
        //2 - 28
        //3 - 26
        //4 - 24

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
            if (val == 0)
                return result;
            if (row < 3)
            {
                if (col < 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (cells[i, j].Value == val)
                                result = true;
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
                                result = true;
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
                                result = true;
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
                                result = true;
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
                                result = true;
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
                                result = true;
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
                                result = true;
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
                                result = true;
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
                                result = true;
                        }
                    }
                }
            }
            return result;
        }

        public void CreateGrid()
        {
            List<int> candidates = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var candidatesBckp = candidates;
            Cell nc = new Cell();
            nc.Value = 0;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    cells[x, y] = nc;

                }
            }

            PutValue();

            Candidates = candidatesBckp;


            Difficult = 3;
        }
        public List<int> Candidates = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public List<int> CandidatesBckp = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Random Rnd = new Random();
        public void PutValue()
        {
            if (!ExistsEmptyCells())
            {
                return;
            }

            for (int i = 0; i < 81; i++)
            {
                bool isFilled = false;
                if( i == 47)
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
                            //if (ExistsEmptyCells())
                        }                        
                    }
                }

                //break;
                //cells[row, col].Value = 0;
            }
            bool ex = ExistsEmptyCells();
            if (ex)
            {
                //PutValue();
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
        /// <summary> Метод проверяет есть ли заданное значение n в столбце выше </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <param name="n"></param>
        public bool ExistsInColumn(int y, int x, int n)
        {
            bool chk = false;
            for (int i = 0; i < y; i++)
            {
                if (cells[i, x].Value == n)
                {
                    chk = true;
                    return chk;
                }

            }
            return chk;
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
