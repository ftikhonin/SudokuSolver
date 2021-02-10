//using SudokuSolver.Solver
namespace WpfApp1
{
    public class Row
    {
        public Row(int[] input)
        {
            SetValues(input);
        }

        private void SetValues(int[] input)
        {
            val1 = input[0];
            val2 = input[1];
            val3 = input[2];
            val4 = input[3];
            val5 = input[4];
            val6 = input[5];
            val7 = input[6];
            val8 = input[7];
            val9 = input[8];
        }

        public int val1 { get; set; }
        public int val2 { get; set; }
        public int val3 { get; set; }
        public int val4 { get; set; }
        public int val5 { get; set; }
        public int val6 { get; set; }
        public int val7 { get; set; }
        public int val8 { get; set; }
        public int val9 { get; set; }
    }
}

