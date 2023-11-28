using AStarPuzzle.Algorithm;
using AStarPuzzle.Helpers;
using AStarPuzzle.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AStarPuzzle
{
    public partial class FormMain : Form
    {
        private List<PictureBox> _pictureBoxes = new List<PictureBox>();
        private Image _originalImage;
        private int _size = 3;
        private bool _canSolve, _isSolved, _hasSolve;
        private readonly Color _emptyColor = Color.BlueViolet;
        public CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
        private List<HeuristicType> _heuristicTypes;
        private ResultWrapper _misPlacedResult;
        private ResultWrapper _manhattanResult;
        private ResultWrapper _euclideanResult;
        private ResultWrapper _chebyshevResult;
        private readonly int _warningSize = 5;

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
                    Description =
                        "Đây là heuristic đơn giản và dễ cài đặt. Nó đếm số lượng ô không ở đúng vị trí trong trạng thái hiện tại so với trạng thái mục tiêu.",
                    Heuristic = HeuristicOption.MisplacedTiles
                },
                new HeuristicType()
                {
                    Id = 2,
                    Name = "Manhattan Distance",
                    Description =
                        "Heuristic này tính tổng khoảng cách Manhattan của các ô từ vị trí hiện tại đến vị trí mục tiêu. Khoảng cách Manhattan giữa hai ô là tổng khoảng cách theo chiều ngang và dọc giữa chúng.",
                    Heuristic = HeuristicOption.ManhattanDistance

                },
                new HeuristicType()
                {
                    Id = 3,
                    Name = "Euclidean Distance",
                    Description =
                        "Heuristic này tính khoảng cách Euclidean từ vị trí hiện tại đến vị trí mục tiêu. Khoảng cách Euclidean giữa hai điểm là căn bậc hai của tổng bình phương khoảng cách theo chiều ngang và dọc giữa chúng.",
                    Heuristic = HeuristicOption.EuclideanDistance
                },
                new HeuristicType()
                {
                    Id = 4,
                    Name = "Chebyshev Distance",
                    Description =
                        "Heuristic này tính khoảng cách Chebyshev từ vị trí hiện tại đến vị trí mục tiêu. Khoảng cách Chebyshev giữa hai điểm là giá trị lớn nhất giữa sự chênh lệch theo chiều ngang và chiều dọc giữa chúng.",
                    Heuristic = HeuristicOption.ChebyshevDistance
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

                    picSelectedImage.Image =
                        _originalImage.ResizeImage(picSelectedImage.Width, picSelectedImage.Height);

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

            if (_originalImage != null)
                _originalImage.LoadImage(ref _pictureBoxes, ref tblSplitImages, _size, btnRandom);

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

        // ReSharper disable once RedundantAssignment
        private void BeforeStart(ref CancellationTokenSource cancellationTokenSource)
        {
            cancellationTokenSource = new CancellationTokenSource();
        }

        private void ShowSuccessMessage(CancellationTokenSource cancellationTokenSource)
        {
            if (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                MessageBox.Show(@"Hoàn thành", @"Trạng thái công việc", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            }
        }

        private void btnCanSolve_Click(object sender, EventArgs e)
        {
            if (!HandleEmptyImage(_originalImage)) return;
            var pictureBoxes = tblSplitImages.Controls.Cast<PictureBox>().ToList();

            var inputSolve = MatrixHelper.GetMatrix(_pictureBoxes, _size, Color.BlueViolet);
            var inputSolveFlatten = MatrixHelper.FlattenMatrix(inputSolve);

            var solveResult = GameHelper.CanSolve(pictureBoxes, inputSolveFlatten, _size, Color.BlueViolet);

            string temp = $"n = {solveResult.n}\n" +
                          $"N = {solveResult.N}\n" +
                          $"Hàng ô trống(từ 1) ={solveResult.EmptyRowIndex}";


            string outputSolveMessage = solveResult.CanSolve
                ? "Có thể giải được"
                : "Không thể giải được" + "\nBạn có muốn sinh ngẫu nhiên lại không";
            var outputSolveIcon = solveResult.CanSolve ? MessageBoxIcon.Question : MessageBoxIcon.Warning;
            var outputSolveButtuon = solveResult.CanSolve ? MessageBoxButtons.OK : MessageBoxButtons.YesNo;

            var result = MessageBox.Show(temp + @"
" + outputSolveMessage, @"Kết quả ước lượng!", outputSolveButtuon,
                outputSolveIcon);
            if (result.Equals(DialogResult.Yes))
            {
                btnRandom.PerformClick();
            }

            _canSolve = solveResult.CanSolve;

            WriteLog(temp + "\n");
        }


        private async void btnRunSolver_Click(object sender, EventArgs e)
        {
            if (!HandleEmptyImage(_originalImage)) return;
            if (!HandleSize(_size)) return;
            if (!HandleNotSolve(_canSolve)) return;
            if (!HandleSolved(_isSolved)) return;

            if (_size >= _warningSize && !_hasSolve)
            {
                if (!PromptLargeSizeWarning()) return;
            }

            var currentHeuristic = GetCurrentHeuristic();
            if (currentHeuristic == null) return;

            var currentMatrix = MatrixHelper.GetMatrix(_pictureBoxes, _size, _emptyColor);

            ToggleAll(false);

            var resultWrapper = GetResultCache(currentHeuristic.Heuristic);

            bool hasCache = resultWrapper != null;

            if (!hasCache)
            {
                resultWrapper = await GetTimeSolveAsync(currentMatrix, currentHeuristic.Heuristic);
            }

            var stackResult = resultWrapper.Result;
            int resultCount = stackResult.Count;

            if (resultCount == 0)
            {
                var message = "Trạng thái đích rồi, không cần giải!\nHãy chọn ảnh mới hoặc tạo ngẫu nhiên lại!";
                MessageBox.Show(message, @"Thông báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var builder = new StringBuilder();

            if (!hasCache)
            {
                builder.AppendLine($"Giải theo heuristic \"{currentHeuristic.Name}\" {stackResult.Count - 1} bước");
                builder.AppendLine($"Thời gian: {resultWrapper.TimingMessage()}");
                WriteLog(builder.ToString());
            }

            builder.AppendLine("Bạn có muốn chạy thuật toán không?");

            var resultMessage = builder.ToString();
            var dialogResult = MessageBox.Show(resultMessage, @"Giải thành công",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult.Equals(DialogResult.Yes))
            {
                WriteBeginSolve();

                pbSolveStep.Minimum = 0;
                pbSolveStep.Maximum = resultCount - 1;
                ToggleAll(false);

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

                ClearResultCache();

            }
            ToggleAll(true);

        }

        private ResultWrapper GetResultCache(HeuristicOption heuristicOption)
        {
            switch (heuristicOption)
            {
                case HeuristicOption.ManhattanDistance:
                    return _manhattanResult;
                case HeuristicOption.MisplacedTiles:
                    return _misPlacedResult;

                case HeuristicOption.EuclideanDistance:
                    return _euclideanResult;

                case HeuristicOption.ChebyshevDistance:
                    return _chebyshevResult;
                default:
                    return null;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetInput();
            ClearResultCache();
        }

        private void ClearResultCache()
        {
            _manhattanResult = _chebyshevResult = _euclideanResult = _misPlacedResult = null;
        }

        private async void btnSolveAllAlgorithm_Click(object sender, EventArgs e)
        {
            if (!HandleEmptyImage(_originalImage)) return;
            if (!HandleSize(_size)) return;
            if (!HandleNotSolve(_canSolve)) return;
            if (!HandleSolved(_isSolved)) return;

            if (_size >= _warningSize && !PromptLargeSizeWarning())
            {
                return;
            }

            var currentMatrix = MatrixHelper.GetMatrix(_pictureBoxes, _size, _emptyColor);

            var builder = new StringBuilder();
            builder.AppendLine("Kết quả giải theo các heuristic");

            WriteBeginSolve();
            ToggleAll(false);

            List<Task<ResultWrapper>> tasks = new List<Task<ResultWrapper>>();

            foreach (var heuristicType in _heuristicTypes)
            {
                HeuristicOption heuristicOption = (HeuristicOption)heuristicType.Id;
                //if (heuristicOption == HeuristicOption.MisplacedTiles) continue;//Bỏ qua Misplaced để test
                await ProcessHeuristicOptionAsync(currentMatrix, heuristicOption, builder, tasks);
            }

            bool cancelAllTask = false;
            while (tasks.Count > 0)
            {
                var completedTask = await Task.WhenAny(tasks);
                tasks.Remove(completedTask);

                var resultWrapper = await completedTask;
                var stackResult = resultWrapper.Result;

                switch (resultWrapper.Heuristic)
                {
                    case HeuristicOption.ChebyshevDistance:
                        _chebyshevResult = resultWrapper;
                        break;
                    case HeuristicOption.EuclideanDistance:
                        _euclideanResult = resultWrapper;
                        break;
                    case HeuristicOption.ManhattanDistance:
                        _manhattanResult = resultWrapper;
                        break;
                    case HeuristicOption.MisplacedTiles:
                    default:
                        _misPlacedResult = resultWrapper;
                        break;
                }

                var currentHeuristic = _heuristicTypes.Find(x => x.Heuristic == resultWrapper.Heuristic);
                var innerBuilder = new StringBuilder();
                innerBuilder.Append($"{currentHeuristic.Name ?? ""}: {stackResult.Count - 1} bước");
                innerBuilder.AppendLine(" -->  " + resultWrapper.TimingMessage());
                WriteLog(innerBuilder.ToString());

                if (_size < _warningSize) continue;

                StringBuilder stopBuilder = new StringBuilder();
                stopBuilder.AppendLine(
                    $"Đã tìm thấy lời giải bằng {currentHeuristic.Name ?? ""}");
                stopBuilder.AppendLine("Bạn có muốn dừng chạy và giải luôn không?");
                stopBuilder.AppendLine("Giải tiếp sẽ tốn nhiều thời gian.");

                var dialogResult = MessageBox.Show(
                    stopBuilder.ToString(), @"Đã tìm được lời giải",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (!dialogResult.Equals(DialogResult.OK)) continue;

                tasks.Clear();
                cmbHeuristic.SelectedIndex = currentHeuristic.Id - 1;
                cancelAllTask = true;
                _hasSolve = true;

            }

            ToggleAll(true);

            //MessageBox.Show(builder.ToString(), @"Kết quả giải", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (!cancelAllTask)
            {
                MessageBox.Show(builder.ToString(), @"Kết quả giải", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                btnRunSolver.PerformClick();
            }
        }

        private bool PromptLargeSizeWarning()
        {
            var builderMessage = new StringBuilder();
            builderMessage.AppendLine($"Kích thước trò chơi quá lớn ({_size}x{_size}), chỉ nên chọn giải theo từng Heurisitic");
            builderMessage.AppendLine("Bạn vẫn muốn tiếp tục chứ, giao diện sẽ bị đứng trong thời gian dài!");
            builderMessage.AppendLine("Nếu giải kích thước lớn, nên chọn Euclidean Distance!");
            return MessageBox.Show(builderMessage.ToString(), @"Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }


        private Task ProcessHeuristicOptionAsync(int[,] currentMatrix, HeuristicOption heuristicOption, StringBuilder builder, List<Task<ResultWrapper>> tasks)
        {
            var task = Task.Run(async () =>
            {
                var resultWrapper = await GetTimeSolveAsync(currentMatrix, heuristicOption);
                var stackResult = resultWrapper.Result;
                builder.Append($"{_heuristicTypes.Find(x => x.Heuristic == heuristicOption).Name ?? ""}: {stackResult.Count - 1} bước");
                builder.AppendLine(" --> " + resultWrapper.TimingMessage());
                resultWrapper.Heuristic = heuristicOption;
                return resultWrapper;
            });

            tasks.Add(task);
            return Task.CompletedTask;
        }

        async Task<ResultWrapper> GetTimeSolveAsync(int[,] matrix, HeuristicOption heuristicOption)
        {
            var aStar = new AStarAlgorithm(size: _size, emptyValue: 0);

            var resultWrapper = await aStar.SolveAsync(matrix, heuristicOption);

            return resultWrapper;
        }


        void ToggleAll(bool status)
        {
            btnStop.Enabled = btnRandom.Enabled = btnCanSolve.Enabled =
                btnChooseImage.Enabled = btnRunSolver.Enabled = btnSolveAllAlgorithm.Enabled
                    = btnReset.Enabled = cmbHeuristic.Enabled = cmbHeuristic.Enabled = cmbSize.Enabled = status;
        }

        void WriteBeginSolve()
        {
            char endL = '\n';
            rtLog.Text += @"Bắt đầu giải, vui lòng đợi 
";
            rtLog.Text += "".PadLeft(30, '*');
            if (!rtLog.Text.EndsWith(endL.ToString())) rtLog.Text += endL;

            rtLog.SelectionStart = rtLog.Text.Length;
            rtLog.ScrollToCaret();
        }

        private void WriteLog(string logMessage)
        {
            char endL = '\n';

            rtLog.Text += logMessage;
            rtLog.Text += "".PadLeft(30, '*');
            if (!rtLog.Text.EndsWith(endL.ToString())) rtLog.Text += endL;

            rtLog.SelectionStart = rtLog.Text.Length;
            rtLog.ScrollToCaret();
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
            if (size <= 10) return true;
            MessageBox.Show($@"Hiện tại chỉ hỗ trợ giải {size}x{size}!
Mức cao hơn máy không chạy nổi vì không gian mẫu quá lớn", @"Chức năng", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            return false;

        }

        private bool HandleNotSolve(bool canSolve)
        {
            if (canSolve) return true;
            string message =
                "Trạng thái này chưa tìm lời giải!\nHãy bấm \"Ngẫu nhiên\" hoặc nhấn \"Ước lượng\" trước!";
            MessageBox.Show(message, @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;

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
            rtLog.Text = "";
            _hasSolve = false;

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
            if (image != null) return true;
            MessageBox.Show(@"Chọn ảnh trước đã", @"Chưa chọn ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }


        HeuristicType GetCurrentHeuristic()
        {
            var heuristic = cmbHeuristic.SelectedItem as HeuristicType;
            return heuristic;
        }

    }
}