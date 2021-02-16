//using SudokuSolver.Solver
namespace WpfApp1
{
    public class Row
    {
        public Row(int[] input)
        {
            SetValues(input);
        }

        #region Properties
        public string Val1
        {
            get => _val1;
            set => _val1 = CheckZero(value);
        }
        public string Val2
        {
            get => _val2;
            set => _val2 = CheckZero(value);
        }
        public string Val3
        {
            get => _val3;
            set => _val3 = CheckZero(value);
        }
        public string Val4
        {
            get => _val4;
            set => _val4 = CheckZero(value);
        }
        public string Val5
        {
            get => _val5;
            set => _val5 = CheckZero(value);
        }
        public string Val6
        {
            get => _val6;
            set => _val6 = CheckZero(value);
        }
        public string Val7
        {
            get => _val7;
            set => _val7 = CheckZero(value);
        }
        public string Val8
        {
            get => _val8;
            set => _val8 = CheckZero(value);
        }
        public string Val9
        {
            get => _val9;
            set => _val9 = CheckZero(value);
        }
        #endregion
        private string _val1;
        private string _val2;
        private string _val3;
        private string _val4;
        private string _val5;
        private string _val6;
        private string _val7;
        private string _val8;
        private string _val9;
        
        private void SetValues(int[] input)
        {
            Val1 = input[0].ToString();
            Val2 = input[1].ToString();
            Val3 = input[2].ToString();
            Val4 = input[3].ToString();
            Val5 = input[4].ToString();
            Val6 = input[5].ToString();
            Val7 = input[6].ToString();
            Val8 = input[7].ToString();
            Val9 = input[8].ToString();
        }

        private string CheckZero(string val)
        {
            return val == "0" ? "" : val;
        }
    }
}

