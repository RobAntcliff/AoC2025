using System.IO;
using System;

namespace AoC2025.day7;

public class Day7
{
    public static void Run()
    {
        part1();
        part2();
    }

    public static void part1()
    {
        string inputPath = Path.Combine("day7", "testInput");

        List<string> lines = new List<string>();
        foreach (string line in File.ReadLines(inputPath))
        {
            lines.Add(line);
        }

        int count = 0;
        HashSet<int> tachyonIndexes = new HashSet<int>();
        foreach (string line in lines)
        {
            for (int i = 0; i < line.Length; i++)
            {
                char currentChar = line[i];
                if (currentChar == 'S')
                {
                    tachyonIndexes.Add(i);
                }

                if (currentChar == '^')
                {
                    if (tachyonIndexes.Contains(i))
                    {
                        count++;
                        tachyonIndexes.Remove(i);
                        tachyonIndexes.Add(i + 1);
                        tachyonIndexes.Add(i - 1);
                    }
                }
            }
        }

        Console.WriteLine("Part 1 result is " + count);
    }

    public static void part2()
    {
        string inputPath = Path.Combine("day7", "input");

        List<string> lines = new List<string>();
        foreach (string line in File.ReadLines(inputPath))
        {
            lines.Add(line);
        }

        var alreadyFoundValues = new Dictionary<(int, int), long>();

        long paths = 0;
        paths = quantumTraverse(lines, lines[0].IndexOf('S'), 0, alreadyFoundValues);

        Console.WriteLine("Part 2 result is " + paths);
    }

    public static long quantumTraverse(List<string> lines, int x, int y, Dictionary<(int, int), long> alreadyFoundValues)
    {
        long paths = 0;
        string line = lines[y];
        char currentChar = line[x];

        if (currentChar == '^')
        {
            long pathVal1;
            if (!alreadyFoundValues.TryGetValue((x + 1, y + 1), out pathVal1))
            {
                var value = quantumTraverse(lines, x + 1, y + 1, alreadyFoundValues);
                alreadyFoundValues.Add((x + 1, y + 1), value);
                pathVal1 = value;
            }
            long pathVal2;
            if (!alreadyFoundValues.TryGetValue((x - 1, y + 1), out pathVal2))
            {
                var value = quantumTraverse(lines, x - 1, y + 1, alreadyFoundValues);
                alreadyFoundValues.Add((x - 1, y + 1), value);
                pathVal2 = value;
            }
            return paths += pathVal1 + pathVal2;
        }

        if (y + 1 < lines.Count)
        {
            return quantumTraverse(lines, x, y + 1, alreadyFoundValues);
        }

        return 1;
    }
}