using System;
using System.IO;

class Program
{
    static void Main()
    {
        int[,] matrix = GenerateRandomMatrix(10, 10, 1, 50);
        //int[,] matrix = ReadMatrixFromFile("Input.txt");
        Console.WriteLine("Original Matrix:");
        PrintMatrix(matrix);

        if (IsSquareMatrix(matrix))
        {
            SaveMatrixToFile(matrix, "Input.txt");

            int[,] matrixCopy = CopyMatrix(matrix); // Criando uma cópia da matriz
            InvertDiagonals(matrixCopy);

            Console.WriteLine("\n\nMatrix with Inverted Diagonals:");
            PrintMatrix(matrixCopy);
            SaveMatrixToFile(matrixCopy, "OutPut.txt");
        }
        else
        {
            Console.WriteLine("Error: The matrix is not square.");
        }
    }

    static bool IsSquareMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        return rows == cols;
    }

    static int[,] CopyMatrix(int[,] original)
    {
        int rows = original.GetLength(0);
        int cols = original.GetLength(1);

        int[,] copy = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                copy[i, j] = original[i, j];
            }
        }

        return copy;
    }

    static int[,] GenerateRandomMatrix(int rows, int cols, int minValue, int maxValue)
    {
        int[,] matrix = new int[rows, cols];
        Random random = new Random();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = random.Next(minValue, maxValue + 1);
            }
        }

        return matrix;
    }

    static int[,] ReadMatrixFromFile(string fileName)
    {
        string[] lines = File.ReadAllLines(fileName);
        int rows = lines.Length;
        int cols = lines[0].Split(' ').Length;

        int[,] matrix = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            string[] values = lines[i].Split(' ');
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = int.Parse(values[j]);
            }
        }

        return matrix;
    }

    static void InvertDiagonals(int[,] matrix)
    {
        int size = matrix.GetLength(0);
        for (int i = 0; i < size / 2; i++)
        {
            // Invert main diagonal
            int temp = matrix[i, i];
            matrix[i, i] = matrix[size - i - 1, size - i - 1];
            matrix[size - i - 1, size - i - 1] = temp;

            // Invert secondary diagonal
            temp = matrix[i, size - i - 1];
            matrix[i, size - i - 1] = matrix[size - i - 1, i];
            matrix[size - i - 1, i] = temp;
        }
    }

    static void PrintMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j].ToString().PadLeft(2, '0'));
                if (j < cols - 1)
                {
                    Console.Write(" ");
                }
            }
            if (i < rows - 1)
            {
                Console.WriteLine();
            }
        }
    }

    static void SaveMatrixToFile(int[,] matrix, string fileName)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        using (StreamWriter writer = new StreamWriter(fileName))
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    writer.Write(matrix[i, j].ToString().PadLeft(2, '0'));
                    if (j < cols - 1)
                    {
                        writer.Write(" ");
                    }
                }
                if (i < rows - 1)
                {
                    writer.WriteLine();
                }
            }
        }
    }
}
