using SudokuSolver;
using System.Collections.Generic;
namespace WpfApp1
{
    public class DifficultBox
    {
        public static List<string> DifficultTypes = new List<string>();

        static DifficultBox()
        {
            for (int i = 0; i < PuzzleGrid.DifficultType.Length; i++)
            {
                var difficultType = PuzzleGrid.DifficultType[i];
                DifficultTypes.Add(difficultType);
            }
        }

        public IEnumerable<string> DifficultSource => DifficultTypes;
        public string DifficultSourceSelectedCurrently = "Easy";
    }
}

