using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    static class Day10
    {
        public static void Part1()
        {
            var numbers = File.ReadAllLines("day10-input.txt")
                .Select(input => long.Parse(input))
                .Prepend(0)
                .OrderBy(x => x);

            var oneCount = 0;
            var threeCount = 1;

            for (var i = 0; i < numbers.Count() - 1; i++)
            {
                var diff = numbers.ElementAt(i + 1) - numbers.ElementAt(i);
                if (diff == 1)
                {
                    oneCount++;
                }
                else if (diff == 3)
                {
                    threeCount++;
                }
            }

            Console.WriteLine(oneCount * threeCount);
        }
    }
}
