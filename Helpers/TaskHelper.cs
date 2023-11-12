using AStarPuzzle.Algorithm;
using AStarPuzzle.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AStarPuzzle.Helpers
{
    public delegate void ActionRef<T>(ref T item);
    public static class TaskHelper
    {
        public static void StopTask(CancellationTokenSource cancellationTokenSource)
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }
        }

        public static int GetSleepTime(TrackBar tbSpeed)
        {
            var speed = 0;
            tbSpeed.Invoke(new Action(() => { speed = tbSpeed.Value; }));
            return 1000 / speed;
        }

        public static async Task RunSolve
        (
            this TableLayoutPanel tblSplitImages,
            Stack<int[,]> result,
            CancellationTokenSource cancellationTokenSource,
            TrackBar tbSpeed,
            ActionRef<CancellationTokenSource> beforeStart = null,
            Action<CancellationTokenSource> afterStart = null,
            Action<int, int> updateProgressbar = null,
            Button btnStop = null,
            Action btnStopAction = null
        )
        {
            beforeStart?.Invoke(ref cancellationTokenSource);
            if (btnStop != null)
            {
                btnStop.Click += ((sender, args) =>
                {

                    StopTask(cancellationTokenSource);
                    btnStopAction?.Invoke();
                });
            }

            if (result.Count == 0) return;
            int n = result.Count;
            var task = new Task(
                () =>
                {
                    while (result.Count > 1)
                    {

                        Thread.Sleep(GetSleepTime(tbSpeed));

                        if (cancellationTokenSource.Token.IsCancellationRequested)
                        {
                            MessageBox.Show(@"Đã dừng", @"Trạng thái công việc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        var currentState = result.Pop();
                        var nextState = result.Peek();

                        var heuristicHelper = new HeuristicCaculator(0);

                        heuristicHelper.FindPositionInMatrix(currentState, 0, out var currentI, out var currentJ);

                        heuristicHelper.FindPositionInMatrix(nextState, 0, out var nextI, out var nextJ);

                        var currentEmptyIndex = new CellInfo(currentI, currentJ);
                        var nextEmptyIndex = new CellInfo(nextI, nextJ);

                        tblSplitImages.SwapCells(currentEmptyIndex, nextEmptyIndex);

                        updateProgressbar?.Invoke(n - result.Count, n - 1);

                    }
                }, cancellationTokenSource.Token);
            task.Start();

            await task;

            afterStart?.Invoke(cancellationTokenSource);
        }
    }
}
