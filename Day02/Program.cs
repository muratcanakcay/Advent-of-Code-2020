using System;
using System.Collections.Generic;
using System.Linq;

namespace Task002
{
    class Program
    {
        static void Main(string[] args)
        {
            int valid_counter = 0;
            int valid_counter2 = 0;

            string[] lines = System.IO.File.ReadAllLines(@"C:\CodeBase\VS19\AdventOfCode2020\Day02\data.txt"); // ***** change this to the location of data.txt *****
            List<(int, int, char, string)> data = new List<(int, int, char, string)>();

            foreach (var line in lines)
            {
                string[] components = line.Split(' ');
                string[] minmax = components[0].Split('-');
                //Console.WriteLine($"Min: {minmax[0]}, Max: {minmax[1]}, Char: {components[1][0]}, Word: {components[2]}");
                data.Add((int.Parse(minmax[0]), int.Parse(minmax[1]), components[1][0], components[2]));
            }

            foreach (var line in data)
            {
                int occurrence = line.Item4.Count(c => (c == (char)line.Item3));

                if (occurrence >= line.Item1 && occurrence <= line.Item2)
                {
                    //Console.WriteLine($"Min: {line.Item1}, Max: {line.Item2}, Char: {line.Item3}, Word: {line.Item4}, Occurrence: {occurrence}"); ;
                    valid_counter++;
                }
            }

            Console.WriteLine($"Valid passwords for part 1: {valid_counter}");

            foreach (var line in data)
            {
                if (line.Item4[line.Item1 - 1] == line.Item3 ^ line.Item4[line.Item2 - 1] == line.Item3)
                {
                    //Console.WriteLine($"Min: {line.Item1}, Max: {line.Item2}, Char: {line.Item3}, Word: {line.Item4}"); ;
                    valid_counter2++;
                }
            }

            Console.WriteLine($"Valid passwords for part 2: {valid_counter2}");
        }
    }
}
