using AStarPuzzle.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AStarPuzzle.Algorithm
{
    public class Node
    {
        public int[,] Matrix;// ma trận 8 số
        public int Heuristic;//số mảnh sai vị trí của ma trận
        public int Index;// chỉ số của node
        public int Parent;// cha của node, để truy vét kết quả
        public int G;// chi phí đi đến node đó
    }
    class AStarAlgorithm
    {
        private int _index;//chỉ số của Node sẽ tăng sau mỗi lần sinh ra 1 node
        private int _g;// Sau mỗi lần sinh ra các node thì chi phí các node tăng 1 đơn vị, tức node con sẽ có chi phí lớn hơn node cha 1 đơn vị

        private readonly int[,] _goalMatrix;
        private readonly HeuristicCaculator _heuristicCaculator;
        private HeuristicOption _heuristicOption;

        public AStarAlgorithm(int size = 3, int emptyValue = 0)
        {
            _goalMatrix = MatrixHelper.GetGoalMatrix(size);
            _heuristicCaculator = new HeuristicCaculator(emptyValue);

        }

        public async Task<ResultWrapper> SolveAsync(int[,] matrix, HeuristicOption heuristicOption = default)
        {
            var stopWatch = new Stopwatch();
            _heuristicOption = heuristicOption != default ? heuristicOption : HeuristicOption.ManhattanDistance;
            List<Node> close = new List<Node>();
            List<Node> open = new List<Node>();

            //khai báo và khởi tạo cho node đầu tiên
            Node node = new Node
            {
                Matrix = matrix,
                Heuristic = _heuristicCaculator.CaculateHeuristic(matrix, _goalMatrix, _heuristicOption),
                Index = 0,
                Parent = -1,
                G = 0
            };
            //cho trạng thái đầu tiên vào Open;
            open.Add(node);

            int u = 0;
            stopWatch.Start();
            while (open.Count != 0)
            {
                #region chọn node tốt nhất trong tập Open và chuyển nó sang Close

                node = open[u];
                open.Remove(node);
                close.Add(node);
                #endregion

                //nếu node có số mảnh sai là 0, tức là đích thì thoát
                if (node.Heuristic == 0) break;
                //sinh hướng đi của node hiện tại
                var listDistances = await GenerateStepAsync(node);

                foreach (var t in listDistances)
                {
                    //hướng đi không thuộc Open và Close
                    if (!EqualNode(t, open) && !EqualNode(t, close))
                    {
                        open.Add(t);
                    }
                    else
                    {   //nếu hướng đi thuộc Open
                        if (EqualNode(t, open))
                        {
                            /*nếu hướng đi đó tốt hơn thì sẽ được cập nhật lại, 
                                ngược lại thì sẽ không cập nhật*/
                            CompareBetter(t, open);
                        }
                        else
                        {
                            //nếu hướng đi thuộc Close
                            if (EqualNode(t, close))
                            {
                                /*nếu hướng đi đó tốt hơn thì sẽ được cập nhật lại, 
                                    ngược lại thì sẽ không cập nhật và chuyển từ Close sang Open*/
                                if (CompareBetter(t, close))
                                {
                                    var temp = GetDupdicateNodeInClose(t, close);
                                    close.Remove(temp);
                                    open.Add(temp);
                                }
                            }
                        }
                    }
                }
                //chọn vị trí có phí tốt nhất trong Open
                u = await BestOpenIndexAsync(open);

            }
            stopWatch.Stop();

            //truy vét kết quả trong tập Close
            var stackResult = BacktrackingResult(close);

            return new ResultWrapper()
            {
                Result = stackResult,
                Timing = stopWatch.Elapsed
            };
        }
        public Stack<int[,]> Solve(int[,] matrix, HeuristicOption heuristicOption = default)
        {
            _heuristicOption = heuristicOption != default ? heuristicOption : HeuristicOption.ManhattanDistance;

            List<Node> close = new List<Node>();
            List<Node> open = new List<Node>();

            //khai báo và khởi tạo cho node đầu tiên
            Node node = new Node
            {
                Matrix = matrix,
                Heuristic = _heuristicCaculator.CaculateHeuristic(matrix, _goalMatrix, _heuristicOption),
                Index = 0,
                Parent = -1,
                G = 0
            };
            //cho trạng thái đầu tiên vào Open;
            open.Add(node);

            int u = 0;
            while (open.Count != 0)
            {
                #region chọn node tốt nhất trong tập Open và chuyển nó sang Close

                node = open[u];
                open.Remove(node);
                close.Add(node);
                #endregion

                //nếu node có số mảnh sai là 0, tức là đích thì thoát
                if (node.Heuristic == 0) break;
                //sinh hướng đi của node hiện tại
                var listDistances = GenerateStep(node);

                foreach (var t in listDistances)
                {
                    //hướng đi không thuộc Open và Close
                    if (!EqualNode(t, open) && !EqualNode(t, close))
                    {
                        open.Add(t);
                    }
                    else
                    {   //nếu hướng đi thuộc Open
                        if (EqualNode(t, open))
                        {
                            /*nếu hướng đi đó tốt hơn thì sẽ được cập nhật lại, 
                                ngược lại thì sẽ không cập nhật*/
                            CompareBetter(t, open);
                        }
                        else
                        {
                            //nếu hướng đi thuộc Close
                            if (EqualNode(t, close))
                            {
                                /*nếu hướng đi đó tốt hơn thì sẽ được cập nhật lại, 
                                    ngược lại thì sẽ không cập nhật và chuyển từ Close sang Open*/
                                if (CompareBetter(t, close))
                                {
                                    var temp = GetDupdicateNodeInClose(t, close);
                                    close.Remove(temp);
                                    open.Add(temp);
                                }
                            }
                        }
                    }
                }

                //chọn vị trí có phí tốt nhất trong Open
                u = BestOpenIndex(open);

            }
            //truy vét kết quả trong tập Close
            var stackResult = BacktrackingResult(close);

            return stackResult;
        }

        //truy vét kết quả đường đi trong tập Close
        static Stack<int[,]> BacktrackingResult(List<Node> close)
        {
            Stack<int[,]> resultStack = new Stack<int[,]>();

            int t = close[close.Count - 1].Parent;
            Node temp = new Node();
            resultStack.Push(close[close.Count - 1].Matrix);

            while (t != -1)
            {
                foreach (var t1 in close)
                {
                    if (t == t1.Index)
                    {
                        temp = t1;
                        break;
                    }
                }

                resultStack.Push(temp.Matrix);
                t = temp.Parent;
            }

            return resultStack;
        }

        private async Task<List<Node>> GenerateStepAsync(Node node)
        {
            return await Task.Run(() => GenerateStep(node));
        }

        private List<Node> GenerateStep(Node node)
        {
            int n = node.Matrix.GetLength(0);//lấy số hàng của ma trận

            List<Node> listDistances = new List<Node>();

            #region  Xác định vị trí mảnh chống, có giá trị là 0
            int h;
            int c = 0;
            bool ok = false;
            for (h = 0; h < n; h++)
            {
                for (c = 0; c < n; c++)
                    if (node.Matrix[h, c] == 0)
                    {
                        ok = true;
                        break;
                    }

                if (ok) break;
            }
            #endregion

            Node temp = new Node
            {
                Matrix = new int[n, n]
            };
            //Copy mảng Ma trận sang mảng ma trận tạm
            Array.Copy(node.Matrix, temp.Matrix, node.Matrix.Length);

            _g++;// tăng chi phí của node con lên 1 đơn vị

            #region Xét các hướng đi theo 4 hướng: trên, dưới, phải, trái 
            //xét hàng ngang bắt đầu từ hàng thứ 2 trở đi
            if (h > 0 && h <= n - 1)
            {
                // thay đổi hướng đi của ma trận
                temp.Matrix[h, c] = temp.Matrix[h - 1, c];
                temp.Matrix[h - 1, c] = 0;

                //cập nhật lại thông số của node
                temp.Heuristic = _heuristicCaculator.CaculateHeuristic(temp.Matrix, _goalMatrix, _heuristicOption);
                _index++;
                temp.Index = _index;
                temp.Parent = node.Index;
                temp.G = _g + temp.Heuristic;
                listDistances.Add(temp);

                //sau khi thay đổi ma trận thì copy lại ma trận cha cho Matrix để xét trường hợp tiếp theo
                temp = new Node
                {
                    Matrix = new int[n, n]
                };
                Array.Copy(node.Matrix, temp.Matrix, node.Matrix.Length);
            }
            //xét hàng ngang bắt đầu từ hàng thứ cuối cùng - 1 trở xuống
            if (h < n - 1 && h >= 0)
            {
                // thay đổi hướng đi của ma trận
                temp.Matrix[h, c] = temp.Matrix[h + 1, c];
                temp.Matrix[h + 1, c] = 0;

                //cập nhật lại thông số của node
                temp.Heuristic = _heuristicCaculator.CaculateHeuristic(temp.Matrix, _goalMatrix, _heuristicOption);
                _index++;
                temp.Index = _index;
                temp.Parent = node.Index;
                temp.G = _g + temp.Heuristic;
                listDistances.Add(temp);

                //sau khi thay đổi ma trận thì copy lại ma trận cha cho Matrix để xét trường hợp tiếp theo
                temp = new Node
                {
                    Matrix = new int[n, n]
                };
                Array.Copy(node.Matrix, temp.Matrix, node.Matrix.Length);
            }
            //Xét cột dọc bắt đầu từ cột thứ 2 trở đi
            if (c > 0 && c <= n - 1)
            {
                // thay đổi hướng đi của ma trận
                temp.Matrix[h, c] = temp.Matrix[h, c - 1];
                temp.Matrix[h, c - 1] = 0;

                //cập nhật lại thông số của node
                temp.Heuristic = _heuristicCaculator.CaculateHeuristic(temp.Matrix, _goalMatrix, _heuristicOption);
                _index++;
                temp.Index = _index;
                temp.Parent = node.Index;
                temp.G = _g + temp.Heuristic;
                listDistances.Add(temp);

                //sau khi thay đổi ma trận thì copy lại ma trận cha cho Matrix để xét trường hợp tiếp theo
                temp = new Node
                {
                    Matrix = new int[n, n]
                };
                Array.Copy(node.Matrix, temp.Matrix, node.Matrix.Length);
            }
            //Xét cột dọc bắt đầu từ cột cuối cùng -1 trở xuống
            if (c < n - 1 && c >= 0)
            {
                // thay đổi hướng đi của ma trận
                temp.Matrix[h, c] = temp.Matrix[h, c + 1];
                temp.Matrix[h, c + 1] = 0;

                //cập nhật lại thông số của node
                temp.Heuristic = _heuristicCaculator.CaculateHeuristic(temp.Matrix, _goalMatrix, _heuristicOption);
                _index++;
                temp.Index = _index;
                temp.Parent = node.Index;
                temp.G = _g + temp.Heuristic;
                listDistances.Add(temp);

                //đến đây đã xết hết hướng đi nên không cần copy lại ma trận
            }
            #endregion

            return listDistances;
        }

        async Task<int> BestOpenIndexAsync(List<Node> open)
        {
            return await Task.Run(() => BestOpenIndex(open));
        }

        int BestOpenIndex(List<Node> open)
        {
            if (open.Count != 0)
            {
                var min = open[0];
                int vt = 0;

                for (int i = 1; i < open.Count; i++)
                    if (min.Heuristic > open[i].Heuristic)
                    {
                        min = open[i];
                        vt = i;
                    }
                    else
                    {
                        if (min.Heuristic == open[i].Heuristic)
                        {
                            if (min.G > open[i].G)
                            {
                                min = open[i];
                                vt = i;
                            }
                        }
                    }
                return vt;
            }

            return 0;
        }

        bool CompareBetter(Node node, List<Node> nodeList)
        {
            foreach (var tempNode in nodeList)
                if (EqualMatrix(node.Matrix, tempNode.Matrix))
                {
                    if (node.G < tempNode.G)
                    {
                        //vì 2 ma trận bằng nhau lên số mảnh sai vi trị là như nhau lên ta không cần cập nhật
                        tempNode.Parent = node.Parent;// cập nhật lại cha của hướng đi
                        tempNode.G = node.G;// cập nhật lại chi phí đường đi

                        return true;
                    }
                    return false;
                }

            return false;
        }

        Node GetDupdicateNodeInClose(Node node, List<Node> closeList)
        {
            Node dupdicate = new Node();
            foreach (var temp in closeList)
                if (EqualMatrix(node.Matrix, temp.Matrix))
                {
                    dupdicate = temp;
                    break;
                }

            return dupdicate;
        }

        bool EqualNode(Node node, List<Node> nodeList)
        {
            foreach (var tempNode in nodeList)
                if (EqualMatrix(tempNode.Matrix, node.Matrix))
                    return true;

            return false;
        }


        static bool EqualMatrix(int[,] a, int[,] b)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(0); j++)
                    if (a[i, j] != b[i, j])
                        return false;
            }

            return true;
        }

    }

}