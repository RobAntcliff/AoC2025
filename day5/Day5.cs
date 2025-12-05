using System.IO;
using System;

namespace AoC2025.day5;

public class Day5
{
    public static void Run()
    {
        string inputPath = Path.Combine("day5", "input");

        List<(long, long)> ranges = getRanges(inputPath);
        List<long> ingredients = getIngredients(inputPath);

        part1(ranges, ingredients);
        part2(ranges);
    }

    public static void part1(List<(long, long)> ranges, List<long> ingredients){
        int result = 0;

        foreach(long num in ingredients){
            foreach((long, long) range in ranges){
                if(num >= range.Item1 && num <= range.Item2){
                    result++;
                    break;
                }
            }
        }

        Console.WriteLine("Part1 result is " + result);
    }

    public static void part2(List<(long, long)> ranges){
        long result = 0;

        List<(long, long)> mergedRanges = getMergedRanges(ranges);

        foreach((long, long) range in mergedRanges){
            Console.WriteLine($"{range.Item1}-{range.Item2}");
            result += range.Item2 - range.Item1 + 1;
        }

        Console.WriteLine("Part 2 result is " + result);
    }

    public static List<(long, long)> getMergedRanges(List<(long, long)> ranges){
        var mergedRanges = new List<(long, long)>();

        foreach((long, long) range in ranges){
            bool foundMerge = false;
            for(int i = 0; i < mergedRanges.Count; i++){
                (long, long) mergedRange = mergedRanges[i];
                if(range.Item1 >= mergedRange.Item1 && range.Item1 <= mergedRange.Item2 
                && range.Item2 >= mergedRange.Item1 && range.Item2 <= mergedRange.Item2){
                    // Don't need to do anything as the range is already within the list
                    foundMerge = true;
                    break;
                }
                else if(range.Item1 <= mergedRange.Item1 && range.Item1 <= mergedRange.Item2 
                && range.Item2 >= mergedRange.Item1 && range.Item2 >= mergedRange.Item2){
                    mergedRanges[i] = (range.Item1, range.Item2);
                    foundMerge = true;
                    break;
                }
                else if(range.Item1 <= mergedRange.Item1 && range.Item1 <= mergedRange.Item2 
                && range.Item2 >= mergedRange.Item1 && range.Item2 <= mergedRange.Item2){
                    mergedRanges[i] = (range.Item1, mergedRange.Item2);
                    foundMerge = true;
                    break;
                }
                else if(range.Item1 >= mergedRange.Item1 && range.Item1 <= mergedRange.Item2 
                && range.Item2 >= mergedRange.Item1 && range.Item2 >= mergedRange.Item2){
                    mergedRanges[i] = (mergedRange.Item1, range.Item2);
                    foundMerge = true;
                    break;
                }
            }

            if(!foundMerge){mergedRanges.Add(range);}
            else{mergedRanges = getMergedRanges(mergedRanges);}
        }

        return mergedRanges;
    }

    public static List<(long, long)> getRanges(string inputPath){
        List<(long, long)> ranges = new List<(long, long)>();
        foreach (string line in File.ReadLines(inputPath))
        {
            if (string.IsNullOrWhiteSpace(line)){break;}
            string[] parts = line.Split('-');
            if (parts.Length == 2 && long.TryParse(parts[0], out long first) && long.TryParse(parts[1], out long second))
            {
                ranges.Add((first, second));
            }
        }
        return ranges;
    }

    public static List<long> getIngredients(string inputPath){
        var numbers = new List<long>();
        bool foundEmptyLine = false;
        foreach (string line in File.ReadLines(inputPath))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                foundEmptyLine = true;
                continue;
            }

            if(foundEmptyLine && long.TryParse(line, out long num)){
                numbers.Add(num);
            }
        }

        return numbers;
    }
}