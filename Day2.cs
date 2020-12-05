using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    static class Day2
    {
        public static void Part1()
        {
            var regex = new Regex(@"(?<min>\d+)-(?<max>\d+) (?<letter>[a-z]): (?<password>.+)");
            var validPasswords = File.ReadAllLines("day2-input.txt")
                .Count(x =>
                {
                    var match = regex.Match(x);
                    var letter = char.Parse(match.Groups["letter"].Value);
                    var password = match.Groups["password"].Value;
                    var min = int.Parse(match.Groups["min"].Value);
                    var max = int.Parse(match.Groups["max"].Value);
                    var letterCount = password.Count(x => x == letter);
                    return letterCount >= min && letterCount <= max;
                });
            Console.WriteLine(validPasswords);
        }
    }
}
