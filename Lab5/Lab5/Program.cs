using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Вхідні дані: масив рядків
        Console.WriteLine("Enter the number of strings in the array:");
        int n = int.Parse(Console.ReadLine());

        string[] strings = new string[n];

        Console.WriteLine("Enter the strings:");
        for (int i = 0; i < n; i++)
        {
            strings[i] = Console.ReadLine();
        }

        // а) Знайти кількість однакових рядків
        Dictionary<string, int> stringCounts = new Dictionary<string, int>();

        foreach (var str in strings)
        {
            if (stringCounts.ContainsKey(str))
            {
                stringCounts[str]++;
            }
            else
            {
                stringCounts[str] = 1;
            }
        }

        Console.WriteLine("\nCount of identical strings:");
        foreach (var entry in stringCounts)
        {
            Console.WriteLine($"'{entry.Key}' appears {entry.Value} times.");
        }

        // б) Знайти кількість рядків, що починаються з n однакових символів
        Console.WriteLine("\nEnter the number of identical starting characters (n):");
        int identicalStartChars = int.Parse(Console.ReadLine());

        int countStartWithNSameChars = 0;

        foreach (var str in strings)
        {
            if (HasNIdenticalStartingChars(str, identicalStartChars))
            {
                countStartWithNSameChars++;
            }
        }

        Console.WriteLine($"\nNumber of strings starting with {identicalStartChars} identical characters: {countStartWithNSameChars}");
    }

    // Метод для перевірки, чи рядок починається з n однакових символів
    static bool HasNIdenticalStartingChars(string str, int n)
    {
        if (str.Length < n)
        {
            return false;
        }

        for (int i = 1; i < n; i++)
        {
            if (str[i] != str[0])
            {
                return false;
            }
        }

        return true;
    }
}
