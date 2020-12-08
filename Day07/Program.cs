using System;
using System.Collections.Generic;
using System.Linq;

namespace Day07
{
    class Program
    {
        static void Main()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\CodeBase\VS19\AdventOfCode2020\Day07\data.txt");      // ***** change this to the location of data.txt *****

            Dictionary<string, List<string>> data1 = new Dictionary<string, List<string>>();                        // data structure for part 1 ( {innerbag : {list of bags that can carry innerbag}) 
            Dictionary<string, List<(int, string)>> data2 = new Dictionary<string, List<(int, string)>>();          // data structure for part 2 ( {outerbag : {list of (number, name) of bags outerbag must carry})
            
            string mybag = "shiny gold";
            
            #region Part 1
            // part1
            // get data from file
            
            foreach (var line in lines)
            {
                string[] components = line.Split(' ');
                if (components[4] == "no") continue;

                string outerbag = components[0] + ' ' + components[1];

                for (int i = 5; i < components.Length; i += 4)
                {
                    string innerbag = components[i] + ' ' + components[i + 1];
                    
                    if (data1.ContainsKey(innerbag))
                    {
                        if (!data1[innerbag].Contains(outerbag)) data1[innerbag].Add(outerbag);
                    }
                    else
                    {
                        List<string> newbaglist = new List<string>();
                        newbaglist.Add(outerbag);
                        data1.Add(innerbag, newbaglist);
                        //Console.WriteLine($"{outerbag} added to outerbag list for {innerbag}.");
                    }
                }           
            }

            //solve the task            
            
            List<string> possiblebags = new List<string>();
            findbags1(mybag, ref possiblebags, in data1);
            #endregion
            
            #region Part 2
            //part2
            // get data from file

            foreach (var line in lines)
            {
                string[] components = line.Split(' ');
                if (components[4] == "no") continue;

                string outerbag = components[0] + ' ' + components[1];

                for (int i = 4; i < components.Length; i += 4)
                {
                    int count = int.Parse(components[i]);
                    string innerbag = components[i+1] + ' ' + components[i + 2];

                    if (data2.ContainsKey(outerbag))
                    {
                        if (!data2[outerbag].Contains((count, innerbag))) data2[outerbag].Add((count, innerbag));
                    }
                    else
                    {
                        List<(int,string)> newbaglist = new List<(int, string)>();
                        newbaglist.Add((count, innerbag));
                        data2.Add(outerbag, newbaglist);
                        //Console.WriteLine($"({count} {innerbag}) added to innerbag list for {outerbag}.");
                    }
                }
            }

            // solve the task
            int requiredbags = findbags2(mybag, in data2) - 1;
#endregion

            Console.WriteLine($"Part 1: Total possible bags: {possiblebags.Count()}");
            Console.WriteLine($"Part 2: Total required bags: {requiredbags}");
        }

        public static void findbags1(string bag, ref List<string> bagslist, in Dictionary<string, List<string>> data) // recursively populates the bagslist with all the bags that mybag can be carried in
        {
            if(data.ContainsKey(bag))
            {
                foreach(var outerbag in data[bag])
                {
                    if(!bagslist.Contains(outerbag)) bagslist.Add(outerbag); // add outerbag to bagslist if it's not in the list
                    findbags1(outerbag, ref bagslist, in data); // find the bags that outerbag can be carried in
                }
            }
        }

        public static int findbags2(string bag, in Dictionary<string, List<(int, string)>> data) // recursively counts all bags that mybag must contain 
        {
            int requiredbags = 1;
            
            if (data.ContainsKey(bag))
            {
                foreach (var innerbag in data[bag])
                {
                    requiredbags += innerbag.Item1 * findbags2(innerbag.Item2, in data); // find the bags that innerbag must contain
                }
            }
            return requiredbags;
        }
    }
}
