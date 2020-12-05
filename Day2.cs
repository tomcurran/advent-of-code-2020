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

        public static void Part2()
        {
            var regex = new Regex(@"(?<position1>\d+)-(?<position2>\d+) (?<letter>[a-z]): (?<password>.+)");
            var validPasswords = File.ReadAllLines("day2-input.txt")
                .Count(x =>
                {
                    var match = regex.Match(x);
                    var letter = char.Parse(match.Groups["letter"].Value);
                    var password = match.Groups["password"].Value;
                    var position1 = int.Parse(match.Groups["position1"].Value);
                    var position2 = int.Parse(match.Groups["position2"].Value);
                    return password.ElementAt(position1 - 1) == letter
                        ^ password.ElementAt(position2 - 1) == letter;
                });
            Console.WriteLine(validPasswords);
        }
    }
}
