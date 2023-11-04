using AStarPuzzle.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;


namespace AStarPuzzle.Helpers
{
    public static class ImageHelper
    {
        public static void SaveImage(this Image originalImage, string filePath)
        {
            Bitmap temp = new Bitmap(originalImage);
            temp.Save(filePath, ImageFormat.Jpeg);
        }

        public static Image ResizeImage(this Image originalImage, int containerWidth, int containerHeight)
        {
            Bitmap resizedImage = new Bitmap(originalImage, containerWidth, containerHeight);

            return resizedImage;

        }
        public static Image LoadImage(this Image originalImage, ref List<PictureBox> randomPictureBoxes, ref TableLayoutPanel tblSplitImages, int size, Button btnRandom = null)
        {
            randomPictureBoxes.Clear();

            originalImage = originalImage.ResizeImage(tblSplitImages.Width, tblSplitImages.Height);

            tblSplitImages.Controls.Clear();

            var cellWidth = tblSplitImages.Width / size;
            var cellHeight = tblSplitImages.Height / size;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var pictureBox = new PictureBox()
                    {
                        Dock = DockStyle.Fill,
                        Padding = new Padding(0),
                        Margin = new Padding(0),
                        BorderStyle = BorderStyle.Fixed3D
                    };

                    Image cellImage = new Bitmap(cellWidth, cellHeight);
                    using (Graphics g = Graphics.FromImage(cellImage))
                    {
                        g.DrawImage(originalImage,
                            new Rectangle(0, 0, cellWidth, cellHeight),
                            new Rectangle(j * cellHeight, i * cellWidth, cellWidth, cellHeight), GraphicsUnit.Pixel);
                    }

                    if ((i + 1) * (j + 1) < size * size)
                        pictureBox.Image = cellImage;
                    else
                        pictureBox.BackColor = Color.BlueViolet;
                    pictureBox.Tag = new PictureTag()
                    {
                        TrueColumnIndex = j,
                        TrueRowIndex = i,
                        Image = cellImage
                    };

                    pictureBox.Click += delegate (object sender, EventArgs args)
                    {
                        var item = (PictureBox)sender;
                        var tag = item.Tag as PictureTag;

                        if (tag == null) return;
                        StringBuilder builder = new StringBuilder();

                        builder.AppendLine($"vị trí hiện tại: [{tag.CurrentRowIndex}][{tag.CurrentColumnIndex}]");
                        builder.AppendLine($"vị trí đúng: [{tag.TrueRowIndex}][{tag.TrueColumnIndex}]");

                        MessageBox.Show(builder.ToString(), @"Chi tiết ô", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    };
                    randomPictureBoxes.Add(pictureBox);

                }
            }
            if (btnRandom != null) btnRandom.PerformClick();
            return originalImage;
        }
    }
}
