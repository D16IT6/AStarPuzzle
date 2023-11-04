using System;
using System.Linq;

namespace AStarPuzzle.Algorithm
{
    public enum HeuristicOption
    {
        MisplacedTiles = 1,
        ManhattanDistance = 2,
        EuclideanDistance = 3
    }
}
