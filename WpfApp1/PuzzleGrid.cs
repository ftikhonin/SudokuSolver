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
        public void CreateGrid()
        {
            List<int> candidates = new List<int>{ 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var candidatesBckp = candidates;
 
            var rnd = new Random();
            for (int x = 0; x < size; x++)                
            {
                for (int y = 0; y < size; y++)
                {

                    Cell cell = new Cell();
                    var indx = rnd.Next(candidates.Count);
                    var selVal = candidates[indx];
                    cell.Value = selVal;

                    candidates = candidates.Where(n => n != selVal).ToList();
                    cells[x,y] = cell;
                }
                candidates = candidatesBckp;
            }
                    
            Difficult = 3;
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
