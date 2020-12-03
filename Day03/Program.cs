using System;
using System.Collections.Generic;
using System.Linq;

namespace Task003
{
    class Program
    {
        static void Main(string[] args)
        {
            long product = 1;
            string[] lines = System.IO.File.ReadAllLines(@"C:\CodeBase\VS19\AdventOfCode2020\Day03\data.txt"); // ***** change this to the location of data.txt *****

            //foreach (var line in lines) Console.WriteLine(line);            

            (int, int)[] slopes = { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };

            foreach (var slope in slopes)
                product *= trees(lines, slope);

            Console.WriteLine($"Product of the slopes is: {product}");
        }

        public static int trees(string[] lines, (int, int) slope)
        {
            int i = 0;
            int pos = 0;
            int trees = 0;
            int length = lines[0].Length; // line length

            while (i < lines.Length)
            {
                if (lines[i][pos] == '#')
                    trees++;

                pos += slope.Item1;
                i += slope.Item2;

                if (pos >= length) pos %= length;
            }

            Console.WriteLine($"Trees for slope ({slope.Item1}, {slope.Item2}): {trees}");

            return trees;
        }
    }
}
