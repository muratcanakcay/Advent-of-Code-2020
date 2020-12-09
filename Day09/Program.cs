using System;
using System.Collections.Generic;
using System.Linq;

namespace Day09
{
    class Program
    {
        static void Main()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\CodeBase\VS19\AdventOfCode2020\Day09\data.txt");      // ***** change this to the location of data.txt *****

            // get data from file

            long[] numbers = new long[lines.Length];

            for (int i = 0; i < lines.Length; i++)
                numbers[i] = long.Parse(lines[i]);

            #region Part 1
            // part1

            bool found = false;
            long number = 0;

            while (!found)
                for (int i = 25; i < numbers.Length; i++)
                {
                    (found, number) = isValid(numbers[i], numbers[(i - 25)..(i)]);
                    if (found) break;                       
                }

            Console.WriteLine($"Part 1 answer: {number} is NOT valid");

            #endregion

            #region Part 2
            //part2

            Console.WriteLine($"Part 2 answer: {findRange(number, numbers)} is min + max of the range summing up to {number}");

            #endregion
        }

        public static (bool, long) isValid(long number, long[] numbers)
        {
            for (int i = 0; i < 24; i++)
            {
                for (int j = i + 1; j < 25; j++)
                {
                    if (number == numbers[i] + numbers[j])
                    {
                        //Console.WriteLine($"{number}  is valid");
                        return (false, number);
                    }
                }
            }
            
            return (true, number);
        }


        public static long findRange(long number, long[] data)
        {
            for (int i = 0; i < data.Length - 1; i++)
            {
                int j = i;
                long sum = data[i];
                
                while (++j < data.Length - 1 && sum <= number)
                {
                    sum += data[j];                    
                    if (sum == number) return ((data[i..(j-1)]).Max() + (data[i..(j - 1)]).Min());
                }
            }
            
            return -1;
        }
    }
}