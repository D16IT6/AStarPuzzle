using System;
using System.Drawing;
namespace AStarPuzzle.Models
{
    [Serializable]
    public class PictureTag
    {
        public int TrueRowIndex { get; set; }
        public int TrueColumnIndex { get; set; }
        public int CurrentRowIndex { get; set; }
        public int CurrentColumnIndex { get; set; }
        public Image Image { get; set; }
    }
}
