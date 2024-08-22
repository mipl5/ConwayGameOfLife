using System;
using System.Threading;

namespace ConwayGameOfLife
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] board = GetMat(10);
            ConwayGameOfLife(board);
        }

        private static int[,] GetMat(int size)
        {
            int[,] mat = new int[size, size];
            Random rnd = new Random();
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    mat[i, j] = 1;//rnd.Next(0, 2);
                }
            }
            return mat;
        }

        private static void ConwayGameOfLife(int[,] board)
        {
            Print(board);
            Thread.Sleep(2000);
            Console.Clear();
            while (true)
            {
                DoRound(board);
                Print(board);
                Thread.Sleep(2000);
                Console.Clear();
            }
        }
        private static void Print(int[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(board[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        private static void DoRound(int[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (i != 0 && j != 0 && i != board.GetLength(0) - 1 && j != board.GetLength(1) - 1)
                    {
                        if (CenterBacteriaCount(board, i, j) != -1)
                            board[i, j] = CenterBacteriaCount(board, i, j);
                    }
                    else if ((i == 0 && j != 0 && j != board.GetLength(1) - 1) ||
                        (i != 0 && i != board.GetLength(0) - 1 && j == board.GetLength(1) - 1) ||
                        (i == board.GetLength(0) - 1 && j != 0 && j != board.GetLength(1) - 1) ||
                        (i != 0 && i != board.GetLength(0) - 1 && j == 0))
                    {
                        if (PeriemeterBacteriaCount(board, i, j) != -1)
                            board[i, j] = PeriemeterBacteriaCount(board, i, j);
                    }
                    else
                    {
                        if (CornerBacteriaCount(board, i, j) != -1)
                            board[i, j] = CornerBacteriaCount(board, i, j);
                    }
                }
            }
        }
        private static int CornerBacteriaCount(int[,] board, int row, int col)
        {
            int indexRow = row;
            int indexCol = col;
            if (indexRow > 0)
                indexRow--;
            if (indexCol > 0)
                indexCol--;
            int counter = CountZeroNeighbors(board, row, indexRow, col, indexCol);

            if (board[row, col] == 0 && counter == 3)
                return 1;
            else if (counter == 0 || counter == 1 || counter > 3)
                return 0;
            return -1;
        }
        private static int CountZeroNeighbors(int[,] board, int row, int indexRow, int col, int indexCol)
        {
            int counter = 0;
            for (int i = indexRow; i <= indexRow + 1; i++)
            {
                for (int j = indexCol; j <= indexCol + 1; j++)
                {
                    if (!(row == i && col == j))
                    {
                        if (board[i, j] != 0)
                        {
                            counter++;
                        }
                    }
                }
            }
            return counter;
        }
        private static int CenterBacteriaCount(int[,] board, int row, int col)
        {
            int counter = 0;
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (!(row == i && col == j))
                    {
                        if (board[i, j] != 0)
                        {
                            counter++;
                        }
                    }
                }
            }

            if (board[row, col] == 0 && counter == 3)
                return 1;
            else if (counter == 0 || counter == 1 || counter > 3)
                return 0;
            return -1;
        }
        private static int PeriemeterBacteriaCount(int[,] board, int row, int col)
        {
            int indexRow = row;
            int indexCol = col;
            if (indexRow > 0)
                indexRow--;
            if (indexCol > 0)
                indexCol--;
            int counter = PeriemeterBacteriaCountZeroNeighbors(board, row, indexRow, col, indexCol);

            if (board[row, col] == 0 && counter == 3)
                return 1;
            else if (counter == 0 || counter == 1 || counter > 3)
                return 0;
            return -1;
        }
        private static int PeriemeterBacteriaCountZeroNeighbors(int[,] board, int row, int indexRow, int col, int indexCol)
        {
            int counter = -1;
            if (row == 0 || row == board.GetLength(0) - 1)
                counter = CountUpDown(board, row, indexRow, col, indexCol);
            else
                counter = CountRightLeft(board, row, indexRow, col, indexCol);

            if (board[row, col] == 0 && counter == 3)
                return 1;
            else if (counter == 0 || counter == 1 || counter > 3)
                return 0;
            return -1;
        }

        private static int CountRightLeft(int[,] board, int row, int indexRow, int col, int indexCol)
        {
            int counter = 0; 
            for (int i = indexRow; i <= indexRow + 2; i++)
            {
                for (int j = indexCol; j <= indexCol + 1; j++)
                {
                    if (!(row == i && col == j))
                    {
                        if (board[i, j] != 0)
                        {
                            counter++;
                        }
                    }
                }
            }
            return counter;
        }

        private static int CountUpDown(int[,] board, int row, int indexRow, int col, int indexCol)
        {
            int counter = 0;
            for (int i = indexRow; i <= indexRow + 1; i++)
            {
                for (int j = indexCol; j <= indexCol + 2; j++)
                {
                    if (!(row == i && col == j))
                    {
                        if (board[i, j] != 0)
                        {
                            counter++;
                        }
                    }
                }
            }
            return counter;
        }
    }
}
