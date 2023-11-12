using System;
using System.Collections.Generic;

namespace AStarPuzzle.Algorithm
{
    public class HeuristicCaculator
    {
        private Dictionary<HeuristicOption, Func<int[,], int[,], int>> _heuristicCalculators;
        public int EmptyValue { get; set; }

        public HeuristicCaculator(int emptyValue)
        {
            InitializeHeuristicCalculators();
            EmptyValue = emptyValue;
        }

        private void InitializeHeuristicCalculators()
        {
            _heuristicCalculators = new Dictionary<HeuristicOption, Func<int[,], int[,], int>>
            {
                { HeuristicOption.MisplacedTiles, CaculateHeuristicMisplacedTiles },
                { HeuristicOption.ManhattanDistance, CaculateHeuristicManhattan },
                { HeuristicOption.EuclideanDistance, CaculateHeuristicEuclidean },
                { HeuristicOption.ChebyshevDistance, CaculateHeuristicChebyshev }
            };
        }

        public int CaculateHeuristic(int[,] matrix, int[,] goal, HeuristicOption heuristicOption)
        {
            if (matrix.GetLength(0) != goal.GetLength(0) || matrix.GetLength(1) != goal.GetLength(1))
            {
                throw new NotSupportedException("Length not same!!!");
            }
            if (_heuristicCalculators.TryGetValue(heuristicOption, out var calculator))
            {
                return calculator(matrix, goal);
            }
            return 0;
        }

        private int CaculateHeuristicEuclidean(int[,] matrix, int[,] goal)
        {
            int size = matrix.GetLength(0);

            double heuristicValue = 0.0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int value = matrix[i, j];

                    if (value != 0)
                    {
                        FindPositionInMatrix(goal, value, out var targetI, out var targetJ);

                        double deltaX = targetI - i; double deltaY = targetJ - j;

                        double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
                        heuristicValue += distance;
                    }
                }
            }
            return (int)heuristicValue;
        }


        private int CaculateHeuristicManhattan(int[,] matrix, int[,] goal)
        {
            int size = matrix.GetLength(0);
            int heuristicValue = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int value = matrix[i, j];

                    if (value != EmptyValue)
                    {
                        FindPositionInMatrix(goal, value, out var targetI, out var targetJ);

                        int distance = Math.Abs(targetI - i) + Math.Abs(targetJ - j);
                        heuristicValue += distance;
                    }
                }
            }
            return heuristicValue;
        }


        private int CaculateHeuristicMisplacedTiles(int[,] matrix, int[,] goal)
        {
            int size = matrix.GetLength(0);
            int heuristicValue = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (matrix[i, j] != EmptyValue)
                    {
                        heuristicValue += matrix[i, j] != goal[i, j] ? 1 : 0;
                    }
                }
            }
            return heuristicValue;
        }

        private int CaculateHeuristicChebyshev(int[,] matrix, int[,] goal)
        {
            int heuristicValue = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    int value = matrix[i, j];
                    FindPositionInMatrix(goal, value, out var targetI, out var targetJ);
                    int distance = Math.Max(Math.Abs(i - targetI), Math.Abs(j - targetJ));
                    heuristicValue += distance;
                }
            }
            return heuristicValue;
        }

        public void FindPositionInMatrix(int[,] matrix, int value, out int i, out int j)
        {
            int size = matrix.GetLength(0);

            for (i = 0; i < size; i++)
            {
                for (j = 0; j < size; j++)
                {
                    if (matrix[i, j] == value)
                        return;
                }
            }

            throw new ArgumentException("Value not found in the matrix.");
        }
    }


}
