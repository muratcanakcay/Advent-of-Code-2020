using System;
using System.Collections.Generic;
using System.Linq;

namespace Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\CodeBase\VS19\AdventOfCode2020\Day06\data.txt"); // ***** change this to the location of data.txt *****

            // get data from file

            List<List<List<char>>> data = new List<List<List<char>>>(); 
            List<List<char>> groupdata = new List<List<char>>();

            foreach (var line in lines)
            {
                if (line == string.Empty)
                {
                    data.Add(groupdata);
                    groupdata = new List<List<char>>();
                    continue;
                }

                List<char> persondata = new List<char>();

                for (int i = 0; i < line.Length; i++)
                    persondata.Add(line[i]);
                
                groupdata.Add(persondata);
            }

            data.Add(groupdata); // add last group;
                        
            // solve the task
            
            int sumanyone = 0;
            int sumeveryone = 0;
            int subtotal = 0;

            foreach (var group in data)
            {
                subtotal = 0;
                List<char> grouplist = new List<char>();

                // part 1 - prepare group list using yes answers from anyone

                foreach (var person in group)
                    foreach (var ch in person)
                        if (!grouplist.Contains(ch)) grouplist.Add(ch);

                sumanyone += (subtotal = grouplist.Count()); // yes answers by anyone                

                // part 2 - remove answer if someone has not answered yes                

                foreach (var ch in grouplist)
                    foreach (var person in group)
                        if (!person.Contains(ch))
                        {
                            subtotal--; 
                            break; // move on to check if next answer was yes for everyone
                        }
                
                sumeveryone += subtotal; // yes answers by everyone
            }

            Console.WriteLine($"Total answers anyone answered yes to: {sumanyone}");
            Console.WriteLine($"Total answers everyone answered yes to: {sumeveryone}");
        }
    }
}
