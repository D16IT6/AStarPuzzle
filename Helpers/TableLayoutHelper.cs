using AStarPuzzle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AStarPuzzle.Helpers
{
    public static class TableLayoutHelper
    {
        public static void ResizeTable(this TableLayoutPanel tblSplitImages, int size)
        {
            var rowHeight = (float)100.0 / size;
            var rowWidth = (float)100.0 / size;
            tblSplitImages.RowStyles.Clear();
            tblSplitImages.ColumnStyles.Clear();

            for (int i = 0; i < size; ++i)
            {
                tblSplitImages.RowStyles.Add(new RowStyle(SizeType.Percent, rowHeight));
                tblSplitImages.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, rowWidth));
            }
        }

        public static void RandomLayout(this TableLayoutPanel tblSplitImages, List<PictureBox> randomPictureBoxes, int size)
        {
            tblSplitImages.Controls.Clear();

            var random = new Random();
            int temp = 0;
            randomPictureBoxes
                .OrderBy(x => random.Next())
                .ToList()
                .ForEach(x =>
            {
                var tag = x.Tag as PictureTag;
                if (tag == null) return;
                tag.CurrentRowIndex = temp / (size);
                tag.CurrentColumnIndex = temp % (size);
                temp++;
                tblSplitImages.Controls.Add(x);
            });
        }
        public static void SwapCells(this TableLayoutPanel tblSplitImages, CellInfo c1, CellInfo c2)
        {
            // Lấy các điều khiển tại các vị trí được chỉ định
            Control control1 = tblSplitImages.GetControlFromPosition(c1.Column, c1.Row);
            Control control2 = tblSplitImages.GetControlFromPosition(c2.Column, c2.Row);

            // Kiểm tra nếu các điều khiển tồn tại
            if (control1 != null && control2 != null)
            {
                // Lưu trữ vị trí của control1
                int tempIndex = tblSplitImages.Controls.IndexOf(control1);

                // Đổi chỗ vị trí của control1 và control2
                tblSplitImages.Invoke(new Action(() =>
                {
                    tblSplitImages.Controls.SetChildIndex(control1, tblSplitImages.Controls.IndexOf(control2));
                    tblSplitImages.Controls.SetChildIndex(control2, tempIndex);
                }));
            }
        }

        public static void LoadControls(this TableLayoutPanel tblSplitImages, List<PictureBox> pictureBoxes)
        {
            tblSplitImages.Controls.Clear();
            foreach (var pictureBox in pictureBoxes)
            {
                tblSplitImages.Controls.Add(pictureBox);
            }
        }
    }
}
