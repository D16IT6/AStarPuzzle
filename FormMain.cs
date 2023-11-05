using AStarPuzzle.Algorithm;
using AStarPuzzle.Helpers;
using AStarPuzzle.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AStarPuzzle
{
    public partial class FormMain : Form
    {
        private List<PictureBox> _pictureBoxes = new List<PictureBox>();
        private Image _originalImage;
        private int _size = 3;
        private bool _canSolve = false, _isSolved = false;
        private readonly Color _emptyColor = Color.BlueViolet;
        public CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
        private List<HeuristicType> _heuristicTypes;

        public FormMain()
        {
            InitializeComponent();
            InitHeuristic();
            InjectHeuristicTypeToControl();
        }
        private void InitHeuristic()
        {
            _heuristicTypes = new List<HeuristicType>
            {
                new HeuristicType()
                {
                    Id = 1,
                    Name = "Misplaced Tiles",
                    Description = "Đây là heuristic đơn giản và dễ cài đặt. Nó đếm số lượng ô không ở đúng vị trí trong trạng thái hiện tại so với trạng thái mục tiêu.",
                    Heuristic = HeuristicOption.MisplacedTiles
                },
                new HeuristicType()
                {
                    Id = 2,
                    Name = "Manhattan Distance",
                    Description = "Heuristic này tính tổng khoảng cách Manhattan của các ô từ vị trí hiện tại đến vị trí mục tiêu. Khoảng cách Manhattan giữa hai ô là tổng khoảng cách theo chiều ngang và dọc giữa chúng.",
                    Heuristic = HeuristicOption.ManhattanDistance

                },
                new HeuristicType()
                {
                    Id = 3,
                    Name = "Euclidean Distance",
                    Description = "Heuristic này tính khoảng cách Euclidean từ vị trí hiện tại đến vị trí mục tiêu. Khoảng cách Euclidean giữa hai điểm là căn bậc hai của tổng bình phương khoảng cách theo chiều ngang và dọc giữa chúng.",
                    Heuristic = HeuristicOption.EuclideanDistance
                }
            };
        }
        private void InjectHeuristicTypeToControl()
        {
            cmbHeuristic.DataSource = _heuristicTypes;

            cmbHeuristic.DisplayMember = "Name";
            cmbHeuristic.ValueMember = "Id";

            cmbHeuristic.SelectedIndex = 0;
        }
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

                    _originalImage.LoadImage(ref _pictureBoxes, ref tblSplitImages, _size, btnRandom);

                    picSelectedImage.Image = _originalImage.ResizeImage(picSelectedImage.Width, picSelectedImage.Height);

                }
            }
        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSize.SelectedIndex == -1) return;
            int size = int.Parse((string)cmbSize.SelectedItem);
            tblSplitImages.RowCount = tblSplitImages.ColumnCount = size;
            _size = size;

            tblSplitImages.Controls.Clear();

            if (_originalImage != null) _originalImage.LoadImage(ref _pictureBoxes, ref tblSplitImages, _size, btnRandom);

            tblSplitImages.ResizeTable(_size);

        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            cmbSize.SelectedItem = _size + string.Empty;
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            if (!HandleEmptyImage(_originalImage)) return;

            tblSplitImages.RandomLayout(_pictureBoxes, _size);
            _canSolve = false;
            _isSolved = false;

        }

        private void BeforeStart(ref CancellationTokenSource cancellationTokenSource)
        {
            cancellationTokenSource = new CancellationTokenSource();
        }

        private void ShowSuccessMessage(CancellationTokenSource cancellationTokenSource)
        {
            if (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                MessageBox.Show(@"Hoàn thành", @"Trạng thái công việc", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void btnCanSolve_Click(object sender, EventArgs e)
        {
            if (!HandleEmptyImage(_originalImage)) return;
            var pictureBoxes = tblSplitImages.Controls.Cast<PictureBox>().ToList();

            var inputSolve = MatrixHelper.GetMatrix(_pictureBoxes, _size, Color.BlueViolet);
            var inputSolveFlatten = MatrixHelper.FlattenMatrix(inputSolve);

            var solveResult = GameHelper.CanSolve(pictureBoxes, inputSolveFlatten, _size);

            string temp = $"n = {solveResult.n}\n" +
                          $"N = {solveResult.N}\n" +
                          $"Hàng ô trống(từ 1) ={solveResult.EmptyRowIndex}";

            string outputSolveMessage = solveResult.CanSolve ? "Có thể giải được" : "Không thể giải được" + "\nBạn có muốn sinh ngẫu nhiên lại không";
            var outputSolveIcon = solveResult.CanSolve ? MessageBoxIcon.Question : MessageBoxIcon.Warning;
            var outputSolveButtuon = solveResult.CanSolve ? MessageBoxButtons.OK : MessageBoxButtons.YesNo;

            var result = MessageBox.Show(temp + "\n" + outputSolveMessage, @"Kết quả ước lượng!", outputSolveButtuon, outputSolveIcon);
            if (result.Equals(DialogResult.Yes))
            {
                btnRandom.PerformClick();
            }
            _canSolve = solveResult.CanSolve;

        }


        private async void btnRunSolver_Click(object sender, EventArgs e)
        {
            if (!HandleEmptyImage(_originalImage)) return;
            if (!HandleSize(_size)) return;
            if (!HandleNotSolve(_canSolve)) return;
            if (!HandleSolved(_isSolved)) return;

            var currentHeuristic = GetCurrentHeuristic();

            var currentMatrix = MatrixHelper.GetMatrix(_pictureBoxes, _size, _emptyColor);

            var elapsed = GetTimeSolve(currentMatrix, currentHeuristic.Heuristic, out var stackResult);

            


            int resultCount = stackResult.Count;

            if (resultCount == 0)
            {
                var message = "Trạng thái đích rồi, không cần giải!\nHãy chọn ảnh mới hoặc tạo ngẫu nhiên lại!";
                MessageBox.Show(message, @"Thông báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            var builder = new StringBuilder();
            builder.AppendLine($"Giải theo heuristic \"Misplaced Tiles\": {stackResult.Count - 1} bước, hết {elapsed.Milliseconds} ms");
            builder.AppendLine("Bạn có muốn chạy thuật toán không?");

            var resultMessage = builder.ToString();
            var dialogResult = MessageBox.Show(resultMessage, @"Giải thành công",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult.Equals(DialogResult.Yes))
            {
                pbSolveStep.Minimum = 0;
                pbSolveStep.Maximum = resultCount - 1;
                btnStop.Enabled = btnRandom.Enabled = btnCanSolve.Enabled =
                    btnChooseImage.Enabled = btnRunSolver.Enabled = btnSolveAllAlgorithm.Enabled
                        = btnReset.Enabled = cmbHeuristic.Enabled = false;
                await tblSplitImages.RunSolve(
                    result: stackResult,
                    cancellationTokenSource: CancellationTokenSource,
                    tbSpeed: tbSpeed,
                    beforeStart: BeforeStart,
                    afterStart: ShowSuccessMessage,
                    updateProgressbar: UpdateProgressbarInForm,
                    btnStop: btnStop,
                    btnStopAction: null
                    );

                var dialog = MessageBox.Show(@"Thuật toán đã chạy xong, bạn muốn xóa màn hình và chạy mới không?",
                    @"Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                _isSolved = true;


                if (dialog.Equals(DialogResult.Yes))
                {
                    ResetInput();
                }

                btnStop.Enabled = btnRandom.Enabled = btnCanSolve.Enabled =
                    btnChooseImage.Enabled = btnRunSolver.Enabled = btnSolveAllAlgorithm.Enabled
                        = btnReset.Enabled = cmbHeuristic.Enabled = true;
            }

        }

        private bool HandleSolved(bool solved)
        {
            if (solved)
            {
                string message =
                    "Đã được giải";
                MessageBox.Show(message, @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }


        private bool HandleSize(int size)
        {
            if (size != 3)
            {
                MessageBox.Show(@"Hiện tại chỉ hỗ trợ giải 3x3!
Mức cao hơn máy không chạy nổi vì không gian mẫu quá lớn", @"Chức năng", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
        }

        private bool HandleNotSolve(bool canSolve)
        {
            if (!canSolve)
            {
                string message =
                    "Trạng thái này chưa tìm lời giải!\nHãy bấm \"Ngẫu nhiên\" hoặc nhấn \"Ước lượng\" trước!";
                MessageBox.Show(message, @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        void ResetInput()
        {
            _originalImage = null;
            _pictureBoxes.Clear();
            tblSplitImages.Controls.Clear();
            _canSolve = false;
            picSelectedImage.Image = null;
            pbSolveStep.Value = 0;
            pbSolveStep.Minimum = 0;
            lblSolveStep.Text = @"0/0";
            _isSolved = _canSolve = false;
        }
        private void UpdateProgressbarInForm(int curentValue, int maxValue)
        {
            pbSolveStep.Invoke(new Action(() =>
            {
                pbSolveStep.Value = curentValue;
            }));
            lblSolveStep.Invoke(new Action(() =>
            {
                lblSolveStep.Text = $@"{curentValue}/{maxValue}";

            }));
        }

        private bool HandleEmptyImage(Image image)
        {
            if (image == null)
            {
                MessageBox.Show(@"Chọn ảnh trước đã", @"Chưa chọn ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        HeuristicType GetCurrentHeuristic()
        {
            var heuristic = cmbHeuristic.SelectedItem as HeuristicType;
            return heuristic;
        }

        private void btnSolveAllAlgorithm_Click(object sender, EventArgs e)
        {
            if (!HandleEmptyImage(_originalImage)) return;
            if (!HandleSize(_size)) return;
            if (!HandleNotSolve(_canSolve)) return;
            if (!HandleSolved(_isSolved)) return;

            var currentMatrix = MatrixHelper.GetMatrix(_pictureBoxes, _size, _emptyColor);
            var builder = new StringBuilder();
            builder.AppendLine("Kết quả giải theo các heuristic");

            var elapsed = GetTimeSolve(currentMatrix, HeuristicOption.MisplacedTiles, out var stackResult);
            builder.AppendLine($"Misplaced Tiles: {stackResult.Count - 1} bước, hết {elapsed.Milliseconds} ms");

            stackResult.Clear();
            elapsed = GetTimeSolve(currentMatrix, HeuristicOption.ManhattanDistance, out stackResult);
            builder.AppendLine($"Manhattan Distance: {stackResult.Count - 1} bước, hết {elapsed.Milliseconds} ms");

            stackResult.Clear();
            elapsed = GetTimeSolve(currentMatrix, HeuristicOption.EuclideanDistance, out stackResult);
            builder.AppendLine($"Euclidean Distance: {stackResult.Count - 1} bước, hết {elapsed.Milliseconds} ms");


            MessageBox.Show(builder.ToString(), @"Kết quả giải", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        TimeSpan GetTimeSolve(int[,] matrix, HeuristicOption heuristicOption, out Stack<int[,]> stackResult)
        {
            Stopwatch stopwatch = new Stopwatch();
            var aStar = new AStarAlgorithm();

            stopwatch.Start();
            stackResult = aStar.Solve(matrix, heuristicOption);
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetInput();
        }
    }
}