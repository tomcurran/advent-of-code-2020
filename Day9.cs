using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    static class Day9
    {
        public static void Part1()
        {
            var preamble = 25;
            var numbers = File.ReadAllLines("day9-input.txt").Select(input => long.Parse(input));
            var invalidNumber = GetInvalidNumber(preamble, numbers);
            Console.WriteLine(invalidNumber);
        }

        public static void Part2()
        {
            var preamble = 25;
            var numbers = File.ReadAllLines("day9-input.txt").Select(input => long.Parse(input));
            var invalidNumber = GetInvalidNumber(preamble, numbers);
            for (var rangeSize = 2; rangeSize < numbers.Count(); rangeSize++)
            {
                for (var i = 0; i < numbers.Count(); i++)
                {
                    var numberRange = numbers.Skip(i).Take(rangeSize);
                    if (numberRange.Sum() == invalidNumber)
                    {
                        Console.WriteLine(numberRange.Min() + numberRange.Max());
                        return;
                    }
                }
            }
        }

        private static long GetInvalidNumber(int preamble, IEnumerable<long> numbers)
        {
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
                    return checkNumber;
                }
            }

            throw new Exception();
        }
    }
}
