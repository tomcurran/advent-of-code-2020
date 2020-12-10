using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    static class Day9
    {
        public static void Part1()
        {
            var preamble = 25;
            var numbers = File.ReadAllLines("day9-input.txt")
                .Select(input => long.Parse(input));
            for (var i = preamble; i < numbers.Count(); i++)
            {
                var checkNumber = numbers.ElementAt(i);
                var numberRange = numbers
                    .Skip(i - preamble)
                    .Take(preamble)
                    .Where(x => x != checkNumber)
                    .ToHashSet();
                if (!numberRange.Any(number => numberRange.Contains(checkNumber - number)))
                {
                    Console.WriteLine(checkNumber);
                }
            }
        }
    }
}
