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
    }
}
