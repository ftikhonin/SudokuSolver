using SudokuSolver;
using System.Collections.Generic;
namespace WpfApp1
{
    public class DifficultBox
    {
        public static List<string> _difficultTypes = new List<string>();

        static DifficultBox()
        {
            for (int i = 0; i < PuzzleGrid.DifficultType.Length; i++)
            {
                var q = PuzzleGrid.DifficultType[i];
                _difficultTypes.Add(q);
            }
        }

        public IEnumerable<string> DifficultSource => _difficultTypes;
        public string DifficultSourceSelectedCurrently = "Easy";
    }
}

