using System;
using System.Collections.Generic;
using System.Linq;

namespace Day08
{
    class Program
    {
        static void Main()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\CodeBase\VS19\AdventOfCode2020\Day08\data.txt");      // ***** change this to the location of data.txt *****

            (bool visited, string com, string arg)[] data = new (bool visited, string com, string arg)[lines.Length];
            int n = 0;

            #region Part 1
            // part1
            // get data from file

            foreach (var line in lines)
            {
                string[] components = line.Split(' ');
                data[n++] = (false, components[0], components[1]);
            }

            //solve the task

            int acc = 0;
            n = 0; 

            while (true)
            {
                if (data[n].visited == true) break;
                data[n].visited = true;

                switch (data[n].com)
                {
                    case "nop":
                        n++;
                        break;
                    case "acc":
                        acc += int.Parse(data[n].arg);
                        //Console.WriteLine($"Acc arg: {data[n].arg}");
                        n++;                        
                        break;
                    case "jmp":
                        //Console.WriteLine($"Jmp arg: {data[n].arg}");
                        n += int.Parse(data[n].arg);
                        break;
                }
            }

            #endregion

            #region Part 2
            //part2
            // solve the task

            bool success = false;
            int lastidx = 0;                                                    // the last nop/jmp that was modified
            int acc2 = 0;

            while (!success)                                                    // we will modify a nop/jmp until it works, changing the next one at each loop
            {
                bool newtry = true;                                             // get ready for new try 
                int currentidx = 0;                                             // keeping track of the jmp/nop commands 
                for(int i = 0; i < data.Length; i++) data[i].visited = false;   // reset visited array
                acc2 = 0;                                                       // reset acc value
                n = 0;                                                          // reset array position

                // Console.WriteLine($"********************************* NEW TRY! lastidx : {lastidx}");
                // Console.WriteLine();

                while (true)
                {
                    
                    if (n == lines.Length)
                    {
                        Console.WriteLine("Exited succesfully!");
                        success = true;
                        break;
                    }
                    
                    if (data[n].visited == true) break;
                    
                    data[n].visited = true;

                    switch (data[n].com)
                    {
                        case "nop":
                            {
                                currentidx++;

                                if ((lastidx < currentidx) && newtry)   // change nop to jmp
                                {
                                    //Console.WriteLine($"Switching to Jmp. at idx: {currentidx} arg: {data[n].arg}");
                                    lastidx = currentidx;
                                    newtry = false;
                                    n += int.Parse(data[n].arg);
                                    break;
                                }

                                n++;
                                break;
                            }
                        
                        case "acc":
                            {
                                acc2 += int.Parse(data[n].arg);

                                //Console.WriteLine($"Acc arg: {data[n].arg}");
                                n++;
                                break;
                            }
                        
                        case "jmp":
                            {
                                currentidx++;

                                if ((lastidx < currentidx) && newtry) // change jmp to nop
                                {
                                    //Console.WriteLine($"Switching to Nop. at idx: {currentidx} ");
                                    lastidx = currentidx;
                                    newtry = false;
                                    n++;
                                    break;
                                }

                                //Console.WriteLine($"Jmp arg: {data[n].arg}");
                                n += int.Parse(data[n].arg);
                                break;
                            }
                    }
                }
            }

            #endregion

            Console.WriteLine($"Part 1: acc: {acc}");
            Console.WriteLine($"Part 1: acc: {acc2}");
        }
    }
}
