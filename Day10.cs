using System;
using System.Collections.Generic;
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

        public static void Part2()
        {
            var numbers = File.ReadAllLines("day10-input.txt")
                .Select(input => long.Parse(input))
                .Prepend(0)
                .OrderByDescending(x => x);

            var startIndex = 0;
            var cache = new Dictionary<long, long>();
            var paths = GetPaths(numbers, startIndex, cache);
            Console.WriteLine(paths);

            static long GetPaths(IOrderedEnumerable<long> numbers, int index, Dictionary<long, long> cache)
            {
                if (cache.ContainsKey(index))
                {
                    return cache[index];
                }

                if (index == numbers.Count() - 1)
                {
                    return 1;
                }

                var pathsAtPlus1 = 0L;
                var pathsAtPlus2 = 0L;
                var pathsAtPlus3 = 0L;
                var number = numbers.ElementAt(index);

                if (index + 1 < numbers.Count()
                    && number - numbers.ElementAt(index + 1) >= 1
                    && number - numbers.ElementAt(index + 1) <= 3)
                {
                    pathsAtPlus1 = GetPaths(numbers, index + 1, cache);
                }

                if (index + 2 < numbers.Count()
                    && number - numbers.ElementAt(index + 2) >= 1
                    && number - numbers.ElementAt(index + 2) <= 3)
                {
                    pathsAtPlus2 = GetPaths(numbers, index + 2, cache);
                }

                if (index + 3 < numbers.Count()
                    && number - numbers.ElementAt(index + 3) >= 1
                    && number - numbers.ElementAt(index + 3) <= 3)
                {
                    pathsAtPlus3 = GetPaths(numbers, index + 3, cache);
                }

                var paths = pathsAtPlus1 + pathsAtPlus2 + pathsAtPlus3;
                cache[index] = paths;
                return paths;
            }
        }
    }
}
