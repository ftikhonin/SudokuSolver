using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class Cell
    {
        public int Value { get; set; } = 0;
        public HashSet<int> Candidates { get; set; }
    }
}
