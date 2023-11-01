using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace AStarPuzzle
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private Panel[,] _panelBoxs;
        private PictureBox[,] _pictureBoxes;
        private Image _originalImage;
        private int _size = 3;
        private string folderPath = Path.Combine(Application.StartupPath, "Images");

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = @"Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";
                openFileDialog.Title = @"Chọn ảnh để chạy thuật toán A*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    _originalImage = Image.FromFile(selectedFilePath);

                    _originalImage = LoadImage(_originalImage);
                }
            }
        }
        public void SaveImage(Image image, string filePath)
        {
            Bitmap temp = new Bitmap(image);
            temp.Save(filePath, ImageFormat.Jpeg);
        }

        private Image ResizeImage(Image originalImage, int containerWidth, int containerHeight)
        {
            if (originalImage.Width < containerWidth || originalImage.Height < containerHeight)
            {
                float widthScale = (float)containerWidth / originalImage.Width;
                float heightScale = (float)containerHeight / originalImage.Height;

                float scale = Math.Max(widthScale, heightScale);

                int newWidth = (int)(originalImage.Width * scale);
                int newHeight = (int)(originalImage.Height * scale);

                Bitmap resizedImage = new Bitmap(originalImage, newWidth, newHeight);
                SaveImage(resizedImage,folderPath + "\\CutCenter.jpg");

                //return CutCenterImage(resizedImage, containerWidth, containerHeight);
                return resizedImage;
            }

            return originalImage;
        }

        Image CutCenterImage(Image originalImage, int containerWidth, int containerHeight)
        {
            if (containerWidth > containerHeight)
            {
                var cutSize = containerHeight;
                int temp = Math.Abs(containerHeight - containerWidth) / 2;
                int fromX = temp;
                var fromY = 0;

                Bitmap croppedImage = new Bitmap(cutSize, cutSize);
                using (Graphics g = Graphics.FromImage(originalImage))
                {
                    // Vẽ phần cắt từ ảnh gốc lên hình ảnh mới
                    g.DrawImage(
                        croppedImage,
                        new Rectangle(0, 0, cutSize, cutSize),
                        new Rectangle(fromX, fromY, cutSize, cutSize), GraphicsUnit.Pixel
                        );
                }
                SaveImage(croppedImage, folderPath + "\\Crop.jpg");

                return croppedImage;
            }

            if (containerWidth < containerHeight)
            {
                var cutSize = containerWidth;
                int temp = Math.Abs(containerHeight - containerWidth) / 2;

                int fromX = 0;
                var fromY = temp;


                Bitmap croppedImage = new Bitmap(cutSize, cutSize);
                using (Graphics g = Graphics.FromImage(croppedImage))
                {
                    // Vẽ phần cắt từ ảnh gốc lên hình ảnh mới
                    g.DrawImage(
                        originalImage,
                        new Rectangle(0, 0, cutSize, cutSize),
                        new Rectangle(fromX, fromY, cutSize, cutSize), GraphicsUnit.Pixel
                    );
                }
                SaveImage(croppedImage, folderPath + "\\Crop.jpg");

                return croppedImage;
            }
            return originalImage;
        }

        private Image LoadImage(Image image)
        {
            image = ResizeImage(image, tblSplitImages.Width, tblSplitImages.Height);

            tblSplitImages.Controls.Clear();
            _panelBoxs = new Panel[_size, _size];
            _pictureBoxes = new PictureBox[_size, _size];


            var cellSize = image.Width / _size;
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    _panelBoxs[i, j] = new Panel()
                    {
                        Dock = DockStyle.Fill
                    };
                    _pictureBoxes[i, j] = new PictureBox()
                    {
                        Dock = DockStyle.Fill,
                        Width = cellSize,
                        Height = cellSize,
                    };

                    Image cellImage = new Bitmap(cellSize, cellSize);
                    using (Graphics g = Graphics.FromImage(cellImage))
                    {
                        g.DrawImage(image,
                            new Rectangle(0, 0, cellSize, cellSize),
                            new Rectangle(j * cellSize, i * cellSize, cellSize, cellSize), GraphicsUnit.Pixel);
                    }

                    _pictureBoxes[i, j].Image = cellImage;
                    SaveImage(cellImage, folderPath +$"\\cellImages{i}-{j}.jpg");


                    _panelBoxs[i, j].Controls.Add(_pictureBoxes[i, j]);

                    tblSplitImages.Controls.Add(_pictureBoxes[i, j], j, i);
                }
            }

            return image;
        }


        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSize.SelectedIndex == -1) return;
            int size = int.Parse((string)cmbSize.SelectedItem);
            tblSplitImages.RowCount = tblSplitImages.ColumnCount = size;
            _size = size;


            tblSplitImages.Controls.Clear();

            if (_originalImage != null) LoadImage(_originalImage);
            ResizeTable(size);

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            cmbSize.SelectedItem = "3";
        }

        void ResizeTable(int size)
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
    }
}