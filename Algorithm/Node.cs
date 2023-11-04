using System;
using System.Linq;

namespace AStarPuzzle.Algorithm
{
    public class Node2
    {
        public int[,] CurrentState { get; set; }
        public Node2 Parent { get; set; }

        public void GetCurrentEmptyIndex(int emptyValue, out int iIndex, out int jIndex)
        {
            for (int i = 0; i < CurrentState.GetLength(0); i++)
            {
                for (int j = 0; j < CurrentState.GetLength(1); j++)
                {
                    if (CurrentState[i, j] == emptyValue)
                    {
                        iIndex = i;
                        jIndex = j;
                        return;
                    }
                }
            }
            iIndex = jIndex = -1;
        }
        public bool CanMoveTop()
        {
            // Kiểm tra xem có thể di chuyển lên trên không.
            // Thường, bạn cần kiểm tra giới hạn của ma trận và điều kiện khác.
            // Ví dụ:
            if (CurrentState == null || CurrentState.GetLength(0) == 0)
            {
                return false; // Ma trận không hợp lệ hoặc rỗng.
            }
            int row = CurrentState.GetLength(0);
            int col = CurrentState.GetLength(1);
            // Kiểm tra xem đã ở hàng đầu cùng chưa.
            return row > 0;
        }

        public bool CanMoveBottom()
        {
            // Kiểm tra xem có thể di chuyển xuống dưới không.
            // Tương tự, kiểm tra giới hạn của ma trận và điều kiện khác.
            // Ví dụ:
            if (CurrentState == null || CurrentState.GetLength(0) == 0)
            {
                return false; // Ma trận không hợp lệ hoặc rỗng.
            }
            int row = CurrentState.GetLength(0);
            int col = CurrentState.GetLength(1);
            // Kiểm tra xem đã ở hàng cuối cùng chưa.
            return row < row - 1;
        }

        public bool CanMoveLeft()
        {
            // Kiểm tra xem có thể di chuyển sang trái không.
            // Tương tự, kiểm tra giới hạn của ma trận và điều kiện khác.
            // Ví dụ:
            if (CurrentState == null || CurrentState.GetLength(0) == 0)
            {
                return false; // Ma trận không hợp lệ hoặc rỗng.
            }
            int row = CurrentState.GetLength(0);
            int col = CurrentState.GetLength(1);
            // Kiểm tra xem đã ở cột đầu tiên chưa.
            return col > 0;
        }

        public bool CanMoveRight()
        {
            // Kiểm tra xem có thể di chuyển sang phải không.
            // Tương tự, kiểm tra giới hạn của ma trận và điều kiện khác.
            // Ví dụ:
            if (CurrentState == null || CurrentState.GetLength(0) == 0)
            {
                return false; // Ma trận không hợp lệ hoặc rỗng.
            }
            int row = CurrentState.GetLength(0);
            int col = CurrentState.GetLength(1);
            // Kiểm tra xem đã ở cột cuối cùng chưa.
            return col < col - 1;
        }

    }
}
