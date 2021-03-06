﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Day06
{
    class Program
    {
        static void Main()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\CodeBase\VS19\AdventOfCode2020\Day06\data.txt"); // ***** change this to the location of data.txt *****

            // get data from file

            List<List<string>> data = new List<List<string>>(); 
            List<string> groupdata = new List<string>();

            foreach (var line in lines)
            {
                if (line == string.Empty)
                {
                    data.Add(groupdata);
                    groupdata = new List<string>();
                    continue;
                }

                string persondata = "";

                for (int i = 0; i < line.Length; i++)
                    persondata += line[i];
                
                groupdata.Add(persondata);
            }

            data.Add(groupdata); // add last group;
                        
            // solve the task
            
            int sumanyone = 0;
            int sumeveryone = 0;
            
            foreach (var group in data)
            {
                int subtotal = 0;
                List<char> groupanswers = new List<char>();

                // part 1 - prepare group list using yes answers from anyone

                foreach (var person in group)
                    foreach (var ch in person)
                        if (!groupanswers.Contains(ch)) groupanswers.Add(ch);

                sumanyone += (subtotal = groupanswers.Count()); // yes answers by anyone                

                // part 2 - remove answer if someone has not answered yes                

                foreach (var ch in groupanswers)
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
