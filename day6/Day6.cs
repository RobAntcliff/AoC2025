using System.IO;
using System;

namespace AoC2025.day6;

public class Day6
{
    public static void Run()
    {
        part1();
        part2();
    }

    public static void part1()
    {
        string inputPath = Path.Combine("day6", "testInput");

        var numbersAndOperatorsList = new List<string[]>();
        foreach (string line in File.ReadLines(inputPath))
        {
            var newLine = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            numbersAndOperatorsList.Add(newLine);
        }

        long result = 0;
        for (int i = 0; i < numbersAndOperatorsList[0].Length; i++)
        {
            long toAdd = 0;
            var op = numbersAndOperatorsList[numbersAndOperatorsList.Count - 1][i];
            if (op.Equals("*"))
            {
                toAdd = 1;
                for (int j = 0; j < numbersAndOperatorsList.Count - 1; j++)
                {
                    toAdd *= Int64.Parse(numbersAndOperatorsList[j][i]);
                }
            }
            else if (op.Equals("+"))
            {
                for (int j = 0; j < numbersAndOperatorsList.Count - 1; j++)
                {
                    toAdd += Int64.Parse(numbersAndOperatorsList[j][i]);
                }
            }
            else
            {
                Console.WriteLine("Operator is borked");
            }
            result += toAdd;
        }

        Console.WriteLine("Part 1 result is " + result);
    }

    public static void part2()
    {
        string inputPath = Path.Combine("day6", "input");
        long result = 0;
        var numbersAndOperatorsList = new List<string>();
        foreach (string line in File.ReadLines(inputPath))
        {
            numbersAndOperatorsList.Add(line);
        }

        char op = ' ';
        List<long> values = new List<long>();
        for (int i = 0; i < numbersAndOperatorsList[0].Length; i++)
        {
            List<char> myLovelyChars = new List<char>();
            for (int j = 0; j < numbersAndOperatorsList.Count; j++)
            {
                myLovelyChars.Add(numbersAndOperatorsList[j][i]);
            }
            if (myLovelyChars.Last() != ' ')
            {
                op = numbersAndOperatorsList[numbersAndOperatorsList.Count - 1][i];
            }

            long num = 0;
            bool nonEmptyCharFound = false;
            for (int k = 0; k < myLovelyChars.Count - 1; k++)
            {
                if (myLovelyChars[k] != ' ')
                {
                    nonEmptyCharFound = true;
                    num = (num * 10) + (long)Char.GetNumericValue(myLovelyChars[k]);
                }
            }

            if (nonEmptyCharFound)
            {
                values.Add(num);
            }
            else
            {
                result += operateOnTheNumbers(op, values);
                op = ' ';
                values = new List<long>();
            }
        }

        result += operateOnTheNumbers(op, values);

        Console.WriteLine("Part 2 result is " + result);
    }

    public static long operateOnTheNumbers(char op, List<long> numbersToOperateOn)
    {
        long result = 0;
        if (op == '*')
        {
            result = 1;
            foreach (long number in numbersToOperateOn)
            {
                Console.Write(number + " * ");
                result *= number;
            }
        }
        else if (op == '+')
        {
            foreach (long number in numbersToOperateOn)
            {
                Console.Write(number + " + ");
                result += number;
            }
        }
        else
        {
            Console.WriteLine("Operator is borked");
        }
        return result;
    }
}