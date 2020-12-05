using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    static class Day1
    {
        public static void Part1()
        {
            var inputs = File.ReadAllLines("day1-input.txt")
                .Select(int.Parse)
                .ToHashSet();
            foreach (var input in inputs)
            {
                var find = 2020 - input;
                if (inputs.Contains(find))
                {
                    Console.WriteLine(input * find);
                    return;
                }
            }
        }

        public static void Part2()
        {
            var inputs = File.ReadAllLines("day1-input.txt")
                .Select(int.Parse)
                .ToHashSet();
            foreach (var input in inputs)
            {
                foreach (var input2 in inputs)
                {
                    var find = 2020 - input - input2;
                    if (inputs.Contains(find))
                    {
                        Console.WriteLine(input * input2 * find);
                        return;
                    }
                }
            }
        }
    }
}
