using AStarPuzzle.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AStarPuzzle.Helpers
{
    public static class MatrixHelper
    {
        public static int[,] GetMatrix(List<PictureBox> pictureBoxes, int n, Color emtyColor = default)
        {
            int[,] result = new int[n, n];

            if (emtyColor == default) emtyColor = Color.BlueViolet;
            pictureBoxes.ForEach(x =>
            {

                var tag = x.Tag as PictureTag;
                if (tag == null) return;

                var i = tag.TrueRowIndex;
                var j = tag.TrueColumnIndex;
                var iIndex = tag.CurrentRowIndex;
                var jIndex = tag.CurrentColumnIndex;

                var value = i * n + j + 1;
                if (x.BackColor == emtyColor)//empty Value
                {
                    result[iIndex, jIndex] = 0;
                }
                else
                {
                    result[iIndex, jIndex] = value;
                }

            });
            return result;
        }
        public static int[] FlattenMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int[] flattenedArray = new int[rows * cols];
            int index = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    flattenedArray[index] = matrix[i, j];
                    index++;
                }
            }

            return flattenedArray;
        }
        public static int[,] GetGoalMatrix(int n)
        {
            int[,] result = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = i * n + j + 1;
                }
            }
            result[n - 1, n - 1] = 0;
            return result;

        }

    }
}
