using System.IO;
using System;

namespace AoC2025.day3;

public class Day3Part1
{
    public static void Run()
    {
        string inputPath = Path.Combine("day3", "input");
        long part1Result = 0;
        long part2Result = 0;

        foreach (string line in File.ReadLines(inputPath))
        {
            Console.WriteLine(line);
            var part1number = getLargestJoltage(line, 2);
            var part2number = getLargestJoltage(line, 12);
            part1Result += part1number;
            part2Result += part2number;
        }

        Console.WriteLine($"Part 1 result is {part1Result}");
        Console.WriteLine($"Part 2 result is {part2Result}");
    }

    public static long getLargestJoltage(string line, int numberOfDigits)
    {
        int[] joltageArray = new int[numberOfDigits];
        Array.Fill(joltageArray, -1);

        for (int i = 0; i < line.Length; i++)
        {
            var index = i + numberOfDigits >= line.Length ? numberOfDigits - (line.Length - i) : 0;
            //Console.WriteLine($"The Index is {index}");
            var digit = (int)Char.GetNumericValue(line[i]);
            bool largerDigitFound = false;
            for (int j = index; j < numberOfDigits; j++)
            {
                if (largerDigitFound)
                {
                    joltageArray[j] = -1;
                }
                else if (digit > joltageArray[j])
                {
                    joltageArray[j] = digit;
                    largerDigitFound = true;
                }
            }

            //Console.WriteLine($"[{string.Join(", ", joltageArray)}]");
        }

        long result = 0;
        for (int k = 0; k < joltageArray.Length; k++)
        {
            result += joltageArray[k] * (long)Math.Pow(10, joltageArray.Length - k);
        }

        return result / 10;
    }
}