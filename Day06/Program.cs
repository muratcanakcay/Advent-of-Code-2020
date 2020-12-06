using System;
using System.Collections.Generic;
using System.Linq;

namespace Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            List < List < List<char> >> data = new List<List<List<char>>>();

            string[] lines = System.IO.File.ReadAllLines(@"C:\CodeBase\VS19\AdventOfCode2020\Day06\data.txt"); // ***** change this to the location of data.txt *****

            // Part 1

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
                {
                    persondata.Add(line[i]);
                }

                groupdata.Add(persondata);
            }

            data.Add(groupdata); // add last group;

            // part 1
            
            int sumanyone = 0;

            foreach(var group in data)
            {
                List<char> grouplist = new List<char>();

                // prepare group list using yes answers from everyone

                foreach (var person in group)
                {
                    foreach (var ch in person)
                        if (!grouplist.Contains(ch)) grouplist.Add(ch);
                }

                sumanyone += grouplist.Count(); // add number of yes answers to total
            }

            Console.WriteLine($"Total answers anyone answered yes to: {sumanyone}");

            // part 2

            int sumeveryone = 0;

            foreach (var group in data)
            {
                // prepare group list using yes answers from everyone
                
                List<char> grouplist = new List<char>();

                foreach (var person in group)
                {
                    foreach (var ch in person)
                        if (!grouplist.Contains(ch)) grouplist.Add(ch);
                }

                // remove answer from group list if someone has not answered yes
                
                List<char> copylist = new List<char>(grouplist);

                foreach (var ch in grouplist)
                {
                    foreach (var person in group)
                    {
                        if (!person.Contains(ch))
                        {
                            copylist.Remove(ch);
                        }
                    }
                }

                sumeveryone += copylist.Count(); // add number of yes answers to total
            }

            Console.WriteLine($"Total answers everyone answered yes to: {sumeveryone}");
        }
    }
}
