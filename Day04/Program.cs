using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#nullable enable

namespace Task002
{
    class Program
    {
        static void Main(string[] args)
        {
            int passNo = 1;
            List<Passport> passports = new List<Passport>();
            string[] lines = System.IO.File.ReadAllLines(@"C:\CodeBase\VS19\AdventOfCode2020\Day04\data.txt"); // ***** change this to the location of data.txt *****

            Passport p = new Passport(); // template passport to fill
            p.passNo = passNo;

            foreach (var line in lines)
            {                
                if (line == string.Empty) // empty line means store old passport and create new
                {
                    passports.Add(new Passport(p));
                    
                    p.clear();                    
                    p.passNo = ++passNo;
                    
                    continue;
                }

                string[] components = line.Split(' '); // split line into components
                
                foreach (var component in components) 
                {
                    string[] fields = component.Split(':'); // split component into key:value pair

                    switch (fields[0])
                    {
                        case "byr":
                            p.byr = fields[1];
                            break;
                        case "iyr":
                            p.iyr = fields[1];
                            break;
                        case "eyr":
                            p.eyr = fields[1];
                            break;
                        case "hgt":
                            p.hgt = fields[1];
                            break;
                        case "hcl":
                            p.hcl = fields[1];
                            break;
                        case "ecl":
                            p.ecl = fields[1];
                            break;
                        case "pid":
                            p.pid = fields[1];
                            break;
                        case "cid":
                            p.cid = fields[1];
                            break;
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
                if (passport.isValid() == "yes") validpassportspart1++;
                if (passport.isValid2() == "yes") validpassportspart2++;
            }

            Console.WriteLine();
            Console.WriteLine($"Part1 : Valid passports: {validpassportspart1}");
            Console.WriteLine($"Part2 : Valid passports: {validpassportspart2}");
        }
        
        public class Passport  // Passport class
        {
            public int passNo;
            public string? byr = null; 
            public string? iyr = null;
            public string? eyr = null;
            public string? hgt = null;
            public string? hcl = null;
            public string? ecl = null;
            public string? pid = null;
            public string? cid = null;

            public Passport() {}

            public Passport(Passport p) // copy constructor
            {
                this.passNo = p.passNo;
                this.byr = p.byr;
                this.iyr = p.iyr;
                this.eyr = p.eyr;
                this.hgt = p.hgt;
                this.hcl = p.hcl;
                this.ecl = p.ecl;
                this.pid = p.pid;
                this.cid = p.cid;
            }

            public string isValid() // check validity for part 1
            {
                if (byr == null || iyr == null || eyr == null || hgt == null || hcl == null || ecl == null || pid == null) return "no";

                return "yes";
            }

            public string isValid2() // check validity for part 2
            {
                if (byr == null || iyr == null || eyr == null || hgt == null || hcl == null || ecl == null || pid == null) return "no";

                if (byr.Length != 4 || int.Parse(byr!) < 1920 || int.Parse(byr!) > 2002 ||
                    iyr.Length != 4 || int.Parse(iyr!) < 2010 || int.Parse(iyr!) > 2020 ||
                    eyr.Length != 4 || int.Parse(eyr!) < 2020 || int.Parse(eyr!) > 2030)
                {
                    Console.WriteLine($"byr: {byr}, iyr: {iyr} or eyr: {eyr} failed.");
                    return "no";
                }

                if (hgt.EndsWith("cm"))
                {
                    string[] height = hgt.Split('c');
                    if (!height[0].All(char.IsDigit) || int.Parse(height[0]) < 150 || int.Parse(height[0]) > 193)
                    {
                        Console.WriteLine($"hgt: {hgt} failed.");
                        return "no";
                    }
                }
                else if (hgt.EndsWith("in"))
                {
                    string[] height = hgt.Split('i');
                    if (!height[0].All(char.IsDigit) || int.Parse(height[0]) < 59 || int.Parse(height[0]) > 76)
                    {
                        Console.WriteLine($"hgt: {hgt} failed.");
                        return "no";
                    }
                }
                else
                {
                    Console.WriteLine($"hgt: {hgt} failed.");
                    return "no";
                }

                if (!hcl.StartsWith('#')) return "no";
                else
                {
                    string[] color = hcl.Split('#');
                    if (color[1].Length != 6 || !Regex.Match(color[1], "^[a-f0-9]*$").Success)
                    {
                        Console.WriteLine($"Regex {color[1]} failed.");
                        return "no";
                    }
                }

                if (ecl != "amb" && ecl != "blu" && ecl != "brn" && ecl != "gry" && ecl != "grn" && ecl != "hzl" && ecl != "oth")
                {
                    Console.WriteLine($"ecl: {ecl} failed."); 
                    return "no";
                }

                if (pid.Length != 9 || !pid.All(char.IsDigit))
                {
                    Console.WriteLine($"pid: {pid} failed.");
                    return "no";
                }

                return "yes";
            }

            public void clear()
            {
                this.passNo = 0;
                this.byr = null;
                this.iyr = null;
                this.eyr = null;
                this.hgt = null;
                this.hcl = null;
                this.ecl = null;
                this.pid = null;
                this.cid = null;
            }

            public override string ToString()
            {
                return $"PassNo({passNo}) byr:({byr} iyr:({iyr} eyr:({eyr}) hgt:({hgt}) hcl:({hcl}) ecl:({ecl}) pid:({pid}) cid:({cid})";
            }
        }
    }
}
