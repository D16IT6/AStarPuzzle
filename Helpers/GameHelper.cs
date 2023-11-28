using AStarPuzzle.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AStarPuzzle.Helpers
{
    public static class GameHelper
    {
        public static int FindEmptyRowIndexFrom1(List<PictureBox> pictureBoxes, Color color = default)
        {
            int emptyPictureRowIndex = -1;
            if (pictureBoxes.Where(x =>
                {
                    PictureBox pictureBox = x;
                    if (pictureBox == null) return false;
                    return pictureBox.BackColor == color;
                }).FirstOrDefault() is PictureBox emptyPictureBox)
            {
                var pictureTag = emptyPictureBox.Tag as PictureTag;
                if (pictureTag == null) return -1;
                emptyPictureRowIndex = pictureTag.CurrentRowIndex;
            }

            return emptyPictureRowIndex + 1;//vị trí tính từ 0, phải tăng thêm 1
        }
        public static SolveResult CanSolve(List<PictureBox> pictureBoxes, int[] listValueFrom1ToN, int n, Color color = default)
        {
            //n: kích thước của ma trận n * n
            //N: Inversion count - ước lượng đảo ngược, dùng để đánh giá tính khả dịch của trò chơi

            //Với n lẻ:
            //Chỉ cần N mod 2 = 0

            //Với n chẵn:
            //tính vị trí ô từ 1:
            //N mod 2 = 0 và ô trống nằm trên dòng chẵn tính từ trên xuống.
            //N mod 2 = 1 và ô trống nằm trên dòng lẻ tính từ trên xuống.

            int misplacedTiles = 0;
            int length = listValueFrom1ToN.Length;
            for (int i = 0; i < length - 1; ++i)
            {
                if (listValueFrom1ToN[i] == 0) continue;
                for (int j = i + 1; j < length; ++j)
                {
                    if (listValueFrom1ToN[j] == 0) continue;

                    if (listValueFrom1ToN[i] > listValueFrom1ToN[j]) misplacedTiles++;

                }
            }
            var emptyRowIndex = FindEmptyRowIndexFrom1(pictureBoxes, color);
            if (n % 2 != 0)
                return new SolveResult()
                {
                    n = n,
                    N = misplacedTiles,
                    CanSolve = misplacedTiles % 2 == 0,
                    EmptyRowIndex = emptyRowIndex
                };
            bool canSolve;
            if (misplacedTiles % 2 == 0) canSolve = emptyRowIndex % 2 == 0;
            else canSolve = emptyRowIndex % 2 == 1;

            return new SolveResult()
            {
                n = n,
                N = misplacedTiles,
                CanSolve = canSolve,
                EmptyRowIndex = emptyRowIndex
            };
        }
    }
}
