using System;
using System.Linq;

class Program
{
    static void Main()
    {
        // Одновимірний масив
        double[] array = { -3.2, 2.5, -1.1, 4.6, -2.8, 0.9 };
        Console.WriteLine("Початковий масив: " + string.Join(", ", array));

        // а) Кількість від’ємних елементів
        int negativeCount = array.Count(x => x < 0);
        Console.WriteLine("Кількість від’ємних елементів: " + negativeCount);

        // б) Сума модулів після мінімального за модулем елемента
        int minIndex = Array.IndexOf(array, array.OrderBy(Math.Abs).First());
        double sumAfterMin = array.Skip(minIndex + 1).Sum(Math.Abs);
        Console.WriteLine("Сума модулів після мінімального за модулем елемента: " + sumAfterMin);

        // Замінити від’ємні елементи їх квадратами
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] < 0) array[i] *= array[i];
        }

        // Упорядкувати масив за зростанням
        Array.Sort(array);
        Console.WriteLine("Перетворений масив: " + string.Join(", ", array));

        // Двовимірний масив
        int[,] matrix = {
            {1, 2, 3},
            {4, 5, 6},
            {7, 8, 9}
        };
        Console.WriteLine("Початкова матриця:");
        PrintMatrix(matrix);

        // а) Обмін верхнього правого і нижнього лівого
        Swap(ref matrix[0, 2], ref matrix[2, 0]);

        // б) Обмін верхнього лівого і нижнього правого
        Swap(ref matrix[0, 0], ref matrix[2, 2]);

        Console.WriteLine("Модифікована матриця:");
        PrintMatrix(matrix);
    }

    static void Swap(ref int a, ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }

    static void PrintMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}