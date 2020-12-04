using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#nullable enable

namespace Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            
            List<Passport> passports = new List<Passport>();
            string[] lines = System.IO.File.ReadAllLines(@"C:\CodeBase\VS19\AdventOfCode2020\Day04\data.txt"); // ***** change this to the location of data.txt *****

            Passport p = new Passport(); // empty passport to fill
            
            foreach (var line in lines)
            {                
                if (line == string.Empty) // store filled passport and create new empty passport
                {
                    passports.Add(new Passport(p));
                    p = new Passport();
            
                    continue;
                }

                string[] components = line.Split(' '); // split line into components
                
                foreach (var component in components) 
                {
                    string[] fields = component.Split(':'); // split component into key:value pair

                    switch (fields[0])
                    {
                        case "byr":
                            p.data.byr = fields[1]; break;
                        case "iyr":
                            p.data.iyr = fields[1]; break;
                        case "eyr":
                            p.data.eyr = fields[1]; break;
                        case "hgt":
                            p.data.hgt = fields[1]; break;
                        case "hcl":
                            p.data.hcl = fields[1]; break;
                        case "ecl":
                            p.data.ecl = fields[1]; break;
                        case "pid":
                            p.data.pid = fields[1]; break;
                        case "cid":
                            p.data.cid = fields[1]; break;
                    }
                }
            }
                        
            passports.Add(new Passport(p)); // add last passport             

            Console.WriteLine($"Total passports: {passports.Count()}");

            int validpassportspart1 = 0;
            int validpassportspart2 = 0;

            foreach (Passport passport in passports)
            {
                //Console.WriteLine($"{passport}");
                if (passport.isValid()) validpassportspart1++;
                if (passport.isValid2()) validpassportspart2++;
            }

            Console.WriteLine();
            Console.WriteLine($"Part1 : Valid passports: {validpassportspart1}");
            Console.WriteLine($"Part2 : Valid passports: {validpassportspart2}");
        }

        public class Passport  // Passport class
        {
            public (string? byr, string? iyr, string? eyr, string? hgt, string? hcl, string? ecl, string? pid, string? cid) data = (null, null, null, null, null, null, null, null);
                
            public Passport() {} // default constructor 

            public Passport(Passport p) // copy constructor
            {                
                this.data = p.data;                
            }

            public bool isValid() // check validity for part 1
            {
                if (data.byr == null || 
                    data.iyr == null || 
                    data.eyr == null || 
                    data.hgt == null || 
                    data.hcl == null || 
                    data.ecl == null || 
                    data.pid == null) return false;

                return true;
            }

            public bool isValid2() // check validity for part 2
            {
                //check nulls
                if (!this.isValid()) return false;

                //check byr, iyr, eyr
                if (data.byr.Length != 4 || int.Parse(data.byr) < 1920 || int.Parse(data.byr) > 2002 ||
                    data.iyr.Length != 4 || int.Parse(data.iyr) < 2010 || int.Parse(data.iyr) > 2020 ||
                    data.eyr.Length != 4 || int.Parse(data.eyr) < 2020 || int.Parse(data.eyr) > 2030) return false;
                
                //check hgt
                if (data.hgt.EndsWith("cm"))
                {
                    string[] height = data.hgt.Split('c');
                    if (!height[0].All(char.IsDigit) || int.Parse(height[0]) < 150 || int.Parse(height[0]) > 193) return false;

                }
                else if (data.hgt.EndsWith("in"))
                {
                    string[] height = data.hgt.Split('i');
                    if (!height[0].All(char.IsDigit) || int.Parse(height[0]) < 59 || int.Parse(height[0]) > 76) return false;
                }
                else return false;

                //check hcl
                if (!data.hcl.StartsWith('#') || 
                    data.hcl[1..].Length != 6 || 
                    !Regex.Match(data.hcl[1..], "^[a-f0-9]*$").Success) return false;

                //check ecl
                if (data.ecl != "amb" && 
                    data.ecl != "blu" && 
                    data.ecl != "brn" && 
                    data.ecl != "gry" && 
                    data.ecl != "grn" && 
                    data.ecl != "hzl" && 
                    data.ecl != "oth") return false;
                
                //check pid
                if (data.pid.Length != 9 || 
                    !data.pid.All(char.IsDigit)) return false;

                return true;
            }

            public override string ToString()
            {
                return $"byr:({data.byr} iyr:({data.iyr} eyr:({data.eyr}) hgt:({data.hgt}) hcl:({data.hcl}) ecl:({data.ecl}) pid:({data.pid}) cid:({data.cid})";
            }
        }
    }
}
