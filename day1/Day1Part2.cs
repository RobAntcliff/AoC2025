using System.IO;

namespace AoC2025.day1;

public class Day1Part2
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

            if (numberString.Length > 0)
            {
                var number = Int32.Parse(numberString);
                result += number / 100;
                number %= 100;

                if (!isRight)
                {
                    number = Day1Part1.leftAsRight(number);
                    var newStartingNumber = ((startingNumber + number) % 100);
                    if (newStartingNumber == 0 || (newStartingNumber > startingNumber && startingNumber != 0)) result++;
                    startingNumber = newStartingNumber;
                }
                else
                {
                    result += (startingNumber + number) / 100;
                    startingNumber = (startingNumber + number) % 100;
                }
            }
        }

        Console.WriteLine(result);
    }
}