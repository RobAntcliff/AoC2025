using System.IO;
using System;

namespace AoC2025.day3;

public class Day3Part1
{
    public static void Run()
    {
        string inputPath = Path.Combine("day3", "input");
        long result = 0;

        foreach (string line in File.ReadLines(inputPath))
        {
            Console.WriteLine(line);
            //var number = getLargestJoltage(line);
            var number = getLargestJoltagePart2(line);
            Console.WriteLine($"Highest Number is {number}");
            result += number;
        }

        Console.WriteLine(result);
    }

    public static long getLargestJoltage(string line){
        var largestNumber = -1;
        var nextLargestNumber = -1;
        var prevLargestNumber = -1;

        foreach(char c in line){
            var digit = (int) Char.GetNumericValue(c);

            if(digit > largestNumber){
                prevLargestNumber = largestNumber;
                largestNumber = digit;
                nextLargestNumber = -1;
            } else if (digit > nextLargestNumber){
                nextLargestNumber = digit;
            }
        }

        if(nextLargestNumber == -1){
            return (prevLargestNumber * 10) + largestNumber;
        } else {
            return (largestNumber * 10) + nextLargestNumber;
        }
    }

    public static long getLargestJoltagePart2(string line){
        int[] joltageArray = {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};

        for(int i = 0; i < line.Length; i++){
            var index = i + 12 >= line.Length ? 12 - (line.Length - i) : 0;
            Console.WriteLine($"The Index is {index}");
            var digit = (int) Char.GetNumericValue(line[i]);
            bool largerDigitFound = false;
            for(int j = index; j < 12; j++){
                if(largerDigitFound){
                    joltageArray[j] = -1;
                }else if(digit > joltageArray[j]){
                    joltageArray[j] = digit;
                    largerDigitFound = true;
                }
            }

            Console.WriteLine($"[{string.Join(", ", joltageArray)}]");
        }

        long result = 0;
        for(int k = 0; k < joltageArray.Length; k++){
            result += joltageArray[k] * (long) Math.Pow(10, joltageArray.Length - k);
        }

        return result / 10;
    }
}