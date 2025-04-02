using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Choose the type of array to process:");
        Console.WriteLine("1 - One-dimensional array");
        Console.WriteLine("2 - Two-dimensional array");
        int choice = int.Parse(Console.ReadLine());

        if (choice == 1)
        {
            // 1. One-dimensional array
            Console.WriteLine("Enter the number of elements in the array:");
            int n = int.Parse(Console.ReadLine());
            double[] array = new double[n];

            Console.WriteLine("Enter the elements of the array:");
            for (int i = 0; i < n; i++)
            {
                array[i] = double.Parse(Console.ReadLine());
            }

            // a) Count of negative elements
            int negativeCount = array.Count(x => x < 0);
            Console.WriteLine("Number of negative elements: " + negativeCount);

            // b) Sum of the absolute values of elements after the minimum by absolute value
            int minIndex = Array.IndexOf(array, array.MinBy(Math.Abs));
            double sumOfModules = array.Skip(minIndex + 1).Sum(x => Math.Abs(x));
            Console.WriteLine("Sum of absolute values of elements after the minimum by absolute value: " + sumOfModules);

            // Replace negative elements with their squares
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < 0)
                {
                    array[i] = array[i] * array[i];
                }
            }

            // Sort the array in ascending order
            Array.Sort(array);

            Console.WriteLine("Array after replacing negative elements with their squares and sorting:");
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }
        }
        else if (choice == 2)
        {
            // 2. Two-dimensional array
            Console.WriteLine("Enter the number of rows and columns for the two-dimensional array (separated by space):");
            string[] dimensions = Console.ReadLine().Split();
            int rows = int.Parse(dimensions[0]);
            int cols = int.Parse(dimensions[1]);

            int[,] matrix = new int[rows, cols];

            Console.WriteLine("Enter the elements of the array:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = int.Parse(Console.ReadLine());
                }
            }

            // a) Swap the upper-right and lower-left corner elements
            int temp = matrix[0, cols - 1];
            matrix[0, cols - 1] = matrix[rows - 1, 0];
            matrix[rows - 1, 0] = temp;

            Console.WriteLine("Array after swapping the upper-right and lower-left corner elements:");
            PrintMatrix(matrix);

            // b) Swap the lower-right and upper-left corner elements
            temp = matrix[rows - 1, cols - 1];
            matrix[rows - 1, cols - 1] = matrix[0, 0];
            matrix[0, 0] = temp;

            Console.WriteLine("Array after swapping the lower-right and upper-left corner elements:");
            PrintMatrix(matrix);
        }
        else
        {
            Console.WriteLine("Invalid choice. Please try again.");
        }
    }

    // Helper method to print the matrix to the console
    static void PrintMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
