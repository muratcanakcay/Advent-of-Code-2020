using System;
using System.Collections.Generic;
using System.Linq;

namespace Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            int row = 0;
            int seat = 0;
            List<int> ids = new List<int>();

            string[] lines = System.IO.File.ReadAllLines(@"C:\CodeBase\VS19\AdventOfCode2020\Day05\data.txt"); // ***** change this to the location of data.txt *****

            // Part 1
            
            foreach (var line in lines)
            {
                for (int i = 0; i < 7; i++)
                {
                    ///Console.Write($"{line[i]}");
                    if (line[i] == 'B') row += (int)Math.Pow(2, 6-i);
                }

                for (int i = 7; i < 10; i++)
                {
                    //Console.Write($"{line[i]}"); 
                    if (line[i] == 'R') seat += (int)Math.Pow(2, 9-i);
                }

                //Console.Write($" Row: {row}, Seat: {seat} ID: {row * 8 + seat}");
                //Console.WriteLine();

                ids.Add(row * 8 + seat);
                row = 0;
                seat = 0;
            }            

            Console.WriteLine($"Max ID: {ids.Max()}");

            // Part 2
            
            ids.Sort();

            for (int i = 0; i < ids.Count() - 1; i++) // go through all ids, look for the id where the next one is 2 bigger.
            {
                if (ids[i + 1] == ids[i] + 2) Console.WriteLine($"My ID: {ids[i] + 1}"); 
            }            
        }
    }
}
