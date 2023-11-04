namespace AStarPuzzle.Models
{
    public class CellInfo
    {
        public CellInfo(int row, int column)
        {
            Column = column;
            Row = row;
        }
        public int Column { get; set; }
        public int Row { get; set; }
    }
}
