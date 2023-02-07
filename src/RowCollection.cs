using System.Collections.ObjectModel;
namespace WpfApp1
{
    internal class RowCollection
    {
        public ObservableCollection<Row> Rows { get; private set; } = new ObservableCollection<Row>();

        public void AddRow(Row newRow)
        {
            Rows.Add(newRow);
        }

        public void RemoveCode(int index)
        {
            Rows.RemoveAt(index);
        }
    }
}