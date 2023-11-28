using AStarPuzzle.Algorithm;

namespace AStarPuzzle.Models
{
    public class HeuristicType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public HeuristicOption Heuristic { get; set; }
    }
}
