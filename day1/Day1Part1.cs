using System.IO;

namespace AoC2025.day1;

public class Day1Part1
{
    public static void Run()
    {
        string inputPath = Path.Combine("day1", "Day1Part1Input.txt");
        var result = 0;
        var startingNumber = 50;

        foreach (string line in File.ReadLines(inputPath))
        {
            var isRight = line[0] == 'R';
            var numberString = line.Substring(1);
            var number = Int32.Parse(numberString);
            if (!isRight) number = leftAsRight(number);
            startingNumber = (startingNumber + number) % 100;
            if (startingNumber == 0) result++;
        }

        Console.WriteLine(result);
    }

    public static int leftAsRight(int leftNumber)
    {
        var moddedNumber = leftNumber % 100;
        var numberAsIfRight = 100 - moddedNumber;
        return numberAsIfRight;
    }
}