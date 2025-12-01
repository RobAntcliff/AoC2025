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
            var isRight = true;
            var numberString = "";
            foreach (char c in line)
            {
                if (c == 'L')
                {
                    isRight = false;
                }
                else if (c == 'R')
                {
                    //Do nothing
                }
                else
                {
                    //We have a number
                    numberString += c;
                }
            }

            if (numberString.Length > 0)
            {
                var number = Int32.Parse(numberString);
                result += number / 100;
                number %= 100;

                if (!isRight)
                {
                    number = Day1Part1.leftAsRight(number);
                    if (((startingNumber + number) % 100) > startingNumber && startingNumber != 0) result++;
                    startingNumber = (startingNumber + number) % 100;
                    if (startingNumber == 0) result++;
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