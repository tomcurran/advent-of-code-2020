using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    static class Day4
    {
        public static void Part1()
        {
            var fields = new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            var input = File.ReadAllText("day4-input.txt");
            var passports = Regex.Split(input, @"\n\n");
            var keysRegex = new Regex(@"(?<key>\S+):");
            var validPassports = passports.Count(passport =>
            {
                var keyMatches = keysRegex.Matches(passport);
                var keys = keyMatches.Select(x => x.Groups["key"].Value);
                return fields.All(field => keys.Contains(field));
            });
            Console.WriteLine(validPassports);
        }

        public static void Part2()
        {
            var fields = new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            var yearRegex = new Regex(@"^[0-9]{4}$");
            var hgtRegex = new Regex(@"^(?<number>[0-9]+)(?<unit>in|cm)$");
            var hclRegex = new Regex(@"^#[0-9a-f]{6}$");
            var ecls = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            var pidRegex = new Regex(@"^[0-9]{9}$");

            var input = File.ReadAllText("day4-input.txt");
            var passports = Regex.Split(input, @"\n\n");
            var fieldsRegex = new Regex(@"(?<key>\S+):(?<value>\S+)");
            var validPassports = passports.Count(passport =>
            {
                var fieldsMatches = fieldsRegex.Matches(passport);

                var keys = fieldsMatches.Select(x => x.Groups["key"].Value);
                if (!fields.All(field => keys.Contains(field)))
                {
                    return false;
                }

                var byr = fieldsMatches.First(x => x.Groups["key"].Value == "byr").Groups["value"].Value;
                if (!yearRegex.Match(byr).Success)
                {
                    return false;
                }
                var byrInt = int.Parse(byr);
                if (byrInt < 1920 || byrInt > 2002)
                {
                    return false;
                }

                var iyr = fieldsMatches.First(x => x.Groups["key"].Value == "iyr").Groups["value"].Value;
                if (!yearRegex.Match(iyr).Success)
                {
                    return false;
                }
                var iyrInt = int.Parse(iyr);
                if (iyrInt < 2010 || iyrInt > 2020)
                {
                    return false;
                }

                var eyr = fieldsMatches.First(x => x.Groups["key"].Value == "eyr").Groups["value"].Value;
                if (!yearRegex.Match(eyr).Success)
                {
                    return false;
                }
                var eyrInt = int.Parse(eyr);
                if (eyrInt < 2020 || eyrInt > 2030)
                {
                    return false;
                }

                var hgt = fieldsMatches.First(x => x.Groups["key"].Value == "hgt").Groups["value"].Value;
                var hgtMatch = hgtRegex.Match(hgt);
                if (!hgtMatch.Success)
                {
                    return false;
                }
                var hgtNumber = int.Parse(hgtMatch.Groups["number"].Value);
                var hgtUnit = hgtMatch.Groups["unit"].Value;
                if ((hgtUnit == "cm" && (hgtNumber < 150 || hgtNumber > 193))
                    || hgtUnit == "in" && (hgtNumber < 59 || hgtNumber > 76))
                {
                    return false;
                }

                var hcl = fieldsMatches.First(x => x.Groups["key"].Value == "hcl").Groups["value"].Value;
                if (!hclRegex.Match(hcl).Success)
                {
                    return false;
                }

                var ecl = fieldsMatches.First(x => x.Groups["key"].Value == "ecl").Groups["value"].Value;
                if (!ecls.Contains(ecl))
                {
                    return false;
                }

                var pid = fieldsMatches.First(x => x.Groups["key"].Value == "pid").Groups["value"].Value;
                if (!pidRegex.Match(pid).Success)
                {
                    return false;
                }

                return true;
            });

            Console.WriteLine(validPassports);
        }
    }
}
