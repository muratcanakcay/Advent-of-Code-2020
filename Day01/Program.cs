using System;
using System.Collections.Generic;

namespace Task001
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\CodeBase\VS19\AdventOfCode2020\Day01\data.txt");    // ***********  change this to the location of data.txt
            List<int> numbers = new List<int>();

            foreach (var line in lines)
            {
                numbers.Add(int.Parse(line));
            }

            // foreach (int number in numbers) Console.WriteLine($"{number}");

            for (int i = 0; i < numbers.Count - 1; i++)
            {
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    if (numbers[i] + numbers[j] == 2020)
                    {
                        int p = numbers[i] * numbers[j];
                        Console.WriteLine($"Part 1: {numbers[i]} + {numbers[j]} = 2020 and their product is: {p}");
                    }
                }
            }

            for (int i = 0; i < numbers.Count - 2; i++)
            {
                for (int j = i + 1; j < numbers.Count - 1; j++)
                {
                    for (int k = j + 1; k < numbers.Count; k++)


                        if (numbers[i] + numbers[j] + numbers[k] == 2020)
                        {
                            int p = numbers[i] * numbers[j] * numbers[k];
                            Console.WriteLine($"Part 2: {numbers[i]} + {numbers[j]} + {numbers[k]} = 2020 and their product is: {p}");
                        }
                }
            }

        }
    }
}
