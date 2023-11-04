using System;
using System.Linq;

namespace AStarPuzzle.Models
{
    public class SolveResult
    {
        public int n { get; set; }
        public int N { get; set; }
        public int EmptyRowIndex { get; set; }
        public bool CanSolve { get; set; }
    }
}
