using System.IO;
using System;

namespace AoC2025.day4;

public class Day4Part1
{
    public static void Run()
    {
        string inputPath = Path.Combine("day4", "input");

        List<string> map = new List<string>();

        foreach (string line in File.ReadLines(inputPath))
        {
            map.Add(line);
        }

        var part1Result = part1(map);
        var part2Result = part2(map);

        Console.WriteLine("Part 1 Result: " + part1Result);
        Console.WriteLine("Part 2 Result: " + part2Result);
    }

    public static int part1(List<string> map, bool removeRolls = false){
        int result = 0;
        for(int y = 0; y < map.Count; y++){
            for(int x = 0; x < map.Count; x++){
                char currentChar = map[y][x];
                if(currentChar == '@'){
                    if(isValidRollOfPaper(x, y, map)){
                        result++;
                        if(removeRolls) {
                            char[] row = map[y].ToCharArray();
                            row[x] = '.';
                            map[y] = new string(row);
                        }
                    }
                }
            }
        }
        return result;
    }

    public static int part2(List<string> map){
        int result = 0;
        var foundRolls = 1;
        while(foundRolls > 0){
            foundRolls = part1(map, true);
            result += foundRolls;
        }
        return result;
    }

    public static (int, int) North = (0, -1);
    public static (int, int) South = (0, 1);
    public static (int, int) East = (1, 0);
    public static (int, int) West = (-1, 0);
    public static List<(int, int)> Directions = new List<(int, int)>
    {
        North,
        South,
        East,
        West,
        (North.Item1 + East.Item1, North.Item2 + East.Item2),  // Northeast
        (South.Item1 + East.Item1, South.Item2 + East.Item2),  // Southeast
        (South.Item1 + West.Item1, South.Item2 + West.Item2),  // Southwest
        (North.Item1 + West.Item1, North.Item2 + West.Item2)   // Northwest
    };

    public static bool isValidRollOfPaper(int x, int y, List<string> map){
        int numberOfRolls = 0;
        foreach((int, int) direction in Directions){
            try { if(map[y + direction.Item2][x + direction.Item1] == '@') numberOfRolls++; }
            catch (Exception) { } // This catches out of boubds errors instead of having to actually deal with them :) 
        }
        return numberOfRolls < 4;
    }
}