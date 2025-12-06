using System;
using System.IO;
using System.Linq;

namespace AoC2025.day2;

public class Day2Part1
{
    public static void Run()
    {
        string input = File.ReadAllText("day2/Day2Part1Input");
        //string input = File.ReadAllText("day2/TestInput.txt");

        // Split by comma
        string[] parts = input.Split(',');
        var tupleNumberList = new List<(long, long)>();

        // For each part, split by hyphen to get the two numbers
        foreach (string part in parts)
        {
            string[] numbers = part.Split('-');
            if (numbers.Length == 2)
            {
                long num1 = long.Parse(numbers[0]);
                long num2 = long.Parse(numbers[1]);

                tupleNumberList.Add((num1, num2));
            }
        }

        //var maxMagnitudeDifference = 0;

        long result = 0;
        foreach (var (num1, num2) in tupleNumberList)
        {
            result += sumOfInvalidIds(num1, num2);
        }

        Console.WriteLine(result);
    }

    //Step 1: See if either of the numbers are even. If neither are, then don't need to calculate
    //Step 2: If one of the numbers is even, and the other is odd, we have to get range of the number that is even
    //Step 3: Iterate through the numbers comparing the digits of the 2 numbers at the same points. If one is larger than the other, it means all of the rest can be any range of numbers
    //Step 4: Create a new list of sets containing all of the matching numbers between the pairs
    //Step 5: Permute through all of the possible duplicate combinations and add them up (Print them for the sample input)

    public static long sumOfInvalidIds(long num1, long num2)
    {
        bool num1IsEven = num1.ToString().Length % 2 == 0;
        bool num2IsEven = num2.ToString().Length % 2 == 0;

        if (!num1IsEven && !num2IsEven) return 0;
        if (!num1IsEven) num1 = (long)Math.Pow(10, num1.ToString().Length); //Becuase if it were 999 for example, it would start at 1000
        if (!num2IsEven) num2 = (long)Math.Pow(10, num1.ToString().Length) - 1;

        Console.WriteLine($"Formatted Range: {num1} to {num2}");

        int maxNumberOfSets = num1.ToString().Length;
        var setOfNumbers = new List<HashSet<int>>();
        for (int j = 0; j < maxNumberOfSets; j++)
        {
            setOfNumbers.Add(new HashSet<int>());
        }

        string num1String = num1.ToString();
        string num2String = num2.ToString();

        bool addAllDigits = false;
        for (int i = 0; i < num1String.Length; i++)
        {
            if (addAllDigits)
            {
                setOfNumbers[i] = new HashSet<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            }
            if (num1String[i] == num2String[i])
            {
                setOfNumbers[i].Add(num1String[i] - '0');
            }
            else
            {
                addAllDigits = true;
                var number1 = num1String[i] - '0';
                var number2 = num2String[i] - '0';
                var difference = number2 - number1;
                while (difference >= 0)
                {
                    setOfNumbers[i].Add(number1 + difference);
                    difference--;
                }
            }
        }

        var resultHashSet = new List<HashSet<int>>();
        for (int j = 0; j <= maxNumberOfSets / 2 - 1; j++)
        {
            resultHashSet.Add(new HashSet<int>());
        }
        for (int k = 0; k < num1.ToString().Length / 2; k++)
        {
            var index1 = k;
            var index2 = k + num1.ToString().Length / 2;

            resultHashSet[index1] = new HashSet<int>(setOfNumbers[index1]);
            resultHashSet[index1].IntersectWith(setOfNumbers[index2]);
        }

        string toPrint = string.Join(" ", resultHashSet.Select(set =>
            "{" + string.Join(", ", set.OrderBy(x => x)) + "}"));
        Console.WriteLine(toPrint);

        var numbers = GeneratePermutations(resultHashSet);

        toPrint = string.Join(", ", numbers);
        Console.WriteLine(toPrint);

        long result = 0;
        foreach (var number in numbers)
        {
            if (number >= num1 && number <= num2)
            {
                result += number;
            }
        }

        return result;
    }

    public static List<long> GeneratePermutations(List<HashSet<int>> sets)
    {
        var results = new List<long>();

        if (sets.Count == 0) return results;

        var combinations = new List<string> { "" };

        foreach (var set in sets)
        {
            var newCombinations = new List<string>();
            foreach (var combo in combinations)
            {
                foreach (int digit in set)
                {
                    newCombinations.Add(combo + digit);
                }
            }
            combinations = newCombinations;
        }

        foreach (var combo in combinations)
        {
            long number = long.Parse(combo);
            int digits = combo.Length;
            long duplicated = number * (long)Math.Pow(10, digits) + number;
            results.Add(duplicated);
        }

        return results;
    }

    public static List<long> GeneratePermutationsPart2(List<HashSet<int>> sets)
    {
        var results = new List<long>();

        if (sets.Count == 0) return results;

        var combinations = new List<string> { "" };

        foreach (var set in sets)
        {
            var newCombinations = new List<string>();
            foreach (var combo in combinations)
            {
                foreach (int digit in set)
                {
                    newCombinations.Add(combo + digit);
                }
            }
            combinations = newCombinations;
        }

        // Convert strings to longs and duplicate
        foreach (var combo in combinations)
        {
            long number = long.Parse(combo);
            // Duplicate: 135 -> 135135
            int digits = combo.Length;
            long duplicated = number * (long)Math.Pow(10, digits) + number;
            results.Add(duplicated);
        }

        return results;
    }
}