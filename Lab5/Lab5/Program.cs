using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string[] strings = { "abc", "aaa", "abc", "bbb", "aaa", "ccc", "aaab", "aaaa" };
        Console.WriteLine("Початковий масив рядків: " + string.Join(", ", strings));

        // а) Знайти кількість однакових рядків
        var grouped = strings.GroupBy(s => s).ToDictionary(g => g.Key, g => g.Count());
        Console.WriteLine("Кількість однакових рядків:");
        foreach (var kvp in grouped)
        {
            Console.WriteLine($"Рядок '{kvp.Key}' зустрічається {kvp.Value} раз(и)");
        }

        // б) Кількість рядків, що починаються з n однакових символів
        int n = 2; // Задане значення n
        int count = strings.Count(s => s.Length >= n && s.Take(n).All(c => c == s[0]));
        Console.WriteLine($"Кількість рядків, що починаються з {n} однакових символів: {count}");
    }
}
